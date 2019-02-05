using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Jupiter.Etc;
using Jupiter.Methods;
using Microsoft.Win32.SafeHandles;

namespace Jupiter.Extensions
{
    internal static class PatternScanner
    {
        internal static IntPtr[] Scan(SafeProcessHandle processHandle, IntPtr baseAddress, List<string> pattern)
        {
            // Initialize a list to store the memory regions of the process
            
            var memoryRegions = new ConcurrentBag<Native.MemoryBasicInformation>
            {
                // Get the first memory region
                
                QueryMemory(processHandle, baseAddress)      
            };
            
            while (true)
            {
                // Get the address of the next memory region
                
                var nextMemoryRegionAddress = (long) memoryRegions.First().BaseAddress + (long) memoryRegions.First().RegionSize;
                
                // Get the next memory region
                
                var nextMemoryRegion = QueryMemory(processHandle, (IntPtr) nextMemoryRegionAddress);
                
                if (nextMemoryRegion.BaseAddress == IntPtr.Zero)
                {
                    // Last memory region
                    
                    break;
                }
                
                memoryRegions.Add(nextMemoryRegion);
            }
            
            // Filter the memory regions to avoid searching unnecessary regions
            
            var filteredMemoryRegions = memoryRegions.Where(memoryRegion => memoryRegion.State == (int) Native.MemoryAllocation.Commit
                                                                         && memoryRegion.Protect != (int) Native.MemoryProtection.NoAccess
                                                                         && memoryRegion.Protect != (int) Native.MemoryProtection.Guard
                                                                         && memoryRegion.Type != (int) Native.MemoryRegionType.MemoryImage).ToList();
            
            // Search the filtered memory regions for the pattern
            
            var patternAddresses = new ConcurrentBag<List<IntPtr>>();
            
            Parallel.ForEach(filteredMemoryRegions, memoryRegion =>
            {
                var addresses = FindPattern(processHandle, memoryRegion, pattern);
                
                patternAddresses.Add(addresses);
            });
            
            // Return an array of addresses where the pattern was found
            
            return patternAddresses.SelectMany(address => address).Where(address => address != IntPtr.Zero).ToArray();
        }
        
        private static Native.MemoryBasicInformation QueryMemory(SafeProcessHandle processHandle, IntPtr baseAddress)
        {
            var memoryInformationSize = Marshal.SizeOf(typeof(Native.MemoryBasicInformation));
            
            return Native.VirtualQueryEx(processHandle, baseAddress, out var memoryInformation, memoryInformationSize) ? memoryInformation : default;
        }
        
        private static List<IntPtr> FindPattern(SafeProcessHandle processHandle, Native.MemoryBasicInformation memoryRegion, IReadOnlyList<string> pattern)
        {
            var patternAddresses = new List<IntPtr>();
            
            // Ensure the memory region size is valid
            
            if ((long) memoryRegion.RegionSize > int.MaxValue)
            {
                return patternAddresses;
            }
            
            // Get the bytes of the memory region
            
            byte[] memoryRegionBytes;
            
            try
            {
                memoryRegionBytes = ReadMemory.Read(processHandle, memoryRegion.BaseAddress, (int) memoryRegion.RegionSize);
            }
            
            catch (Win32Exception)
            {
                return patternAddresses;
            }
            
            // Calculate the indexes of any wildcard bytes
            
            var wildCardIndexList = pattern.Select((wildcard, index) => wildcard == "??" ? index : -1).Where(index => index != -1).ToList();
            
            // Initialize a lookup directory
            
            var lookUpDirectory = new int[byte.MaxValue + 1];
            
            foreach (var index in Enumerable.Range(0, lookUpDirectory.Length))
            {
                lookUpDirectory[index] = pattern.Count;
            }
            
            // Initialize the pattern in the lookup directory
            
            foreach (var index in Enumerable.Range(0, pattern.Count))
            {
                if (!wildCardIndexList.Contains(index))
                {
                    lookUpDirectory[int.Parse(pattern[index], NumberStyles.HexNumber)] = pattern.Count - index - 1;
                }      
            }
            
            var patternIndex = pattern.Count - 1;
            
            // Get the last byte in the pattern
            
            var lastByte = int.Parse(pattern.Last(), NumberStyles.HexNumber);
            
            // Search for the pattern in the memory region
            
            while (patternIndex < memoryRegionBytes.Length)
            {
                var currentByte = memoryRegionBytes[patternIndex];
                
                if (currentByte == lastByte)
                {
                    // Check if the pattern exists at the current index
                    
                    var patternFound = Enumerable.Range(0, pattern.Count - 2).Reverse().All(index => pattern[index] == "??" || memoryRegionBytes[patternIndex - pattern.Count + index + 1] == int.Parse(pattern[index], NumberStyles.HexNumber));
                    
                    if (patternFound)
                    {
                        patternAddresses.Add(memoryRegion.BaseAddress + (patternIndex - pattern.Count + 1));
                    }
                    
                    patternIndex += 1;
                }
                
                else
                {
                    patternIndex += lookUpDirectory[currentByte];
                }
            }
            
            return patternAddresses;
        }
    }
}