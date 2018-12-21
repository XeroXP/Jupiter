using System;
using System.Runtime.InteropServices;
using Jupiter.Wrapper;

namespace Jupiter
{
    public class MemoryModule
    {
        public IntPtr AllocateMemory(string processName, int size)
        {
            using (var memoryWrapper = new MemoryWrapper(processName))
            {
                return memoryWrapper.AllocateMemory(size);
            }
        }
        
        public IntPtr AllocateMemory(int processId, int size)
        {
            using (var memoryWrapper = new MemoryWrapper(processId))
            {
                return memoryWrapper.AllocateMemory(size);
            }    
        }
        
        public IntPtr AllocateMemory(SafeHandle processHandle, int size)
        {
            using (var memoryWrapper = new MemoryWrapper(processHandle))
            {
                return memoryWrapper.AllocateMemory(size);
            }    
        }

        public bool FreeMemory(string processName, IntPtr baseAddress, int size)
        {
            using (var memoryWrapper = new MemoryWrapper(processName))
            {
                return memoryWrapper.FreeMemory(baseAddress, size);
            }
        }
        
        public bool FreeMemory(int processId, IntPtr baseAddress, int size)
        {
            using (var memoryWrapper = new MemoryWrapper(processId))
            {
                return memoryWrapper.FreeMemory(baseAddress, size);
            }
        }
        
        public bool FreeMemory(SafeHandle processHandle, IntPtr baseAddress, int size)
        {
            using (var memoryWrapper = new MemoryWrapper(processHandle))
            {
                return memoryWrapper.FreeMemory(baseAddress, size);
            }
        }
        
        public IntPtr[] PatternScan(string processName, IntPtr baseAddress, string pattern)
        {
            using (var memoryWrapper = new MemoryWrapper(processName))
            {
                return memoryWrapper.PatternScan(baseAddress, pattern);
            }
        }
        
        public IntPtr[] PatternScan(int processId, IntPtr baseAddress, string pattern)
        {
            using (var memoryWrapper = new MemoryWrapper(processId))
            {
                return memoryWrapper.PatternScan(baseAddress, pattern);
            }
        }
        
        public IntPtr[] PatternScan(SafeHandle processHandle, IntPtr baseAddress, string pattern)
        {
            using (var memoryWrapper = new MemoryWrapper(processHandle))
            {
                return memoryWrapper.PatternScan(baseAddress, pattern);
            }
        }

        public byte[] ReadMemory(string processName, IntPtr baseAddress, int size)
        {
            using (var memoryWrapper = new MemoryWrapper(processName))
            {
                return memoryWrapper.ReadMemory(baseAddress, size);
            }
        }
        
        public byte[] ReadMemory(int processId, IntPtr baseAddress, int size)
        {
            using (var memoryWrapper = new MemoryWrapper(processId))
            {
                return memoryWrapper.ReadMemory(baseAddress, size);
            }
        }
        
        public byte[] ReadMemory(SafeHandle processHandle, IntPtr baseAddress, int size)
        {
            using (var memoryWrapper = new MemoryWrapper(processHandle))
            {
                return memoryWrapper.ReadMemory(baseAddress, size);
            }
        }

        public TStructure ReadMemory<TStructure>(string processName, IntPtr baseAddress)
        {
            using (var memoryWrapper = new MemoryWrapper(processName))
            {
                return memoryWrapper.ReadMemory<TStructure>(baseAddress);  
            }   
        }
        
        public TStructure ReadMemory<TStructure>(int processId, IntPtr baseAddress)
        {     
            using (var memoryWrapper = new MemoryWrapper(processId))
            {
                return memoryWrapper.ReadMemory<TStructure>(baseAddress);  
            }   
        }
        
        public TStructure ReadMemory<TStructure>(SafeHandle processHandle, IntPtr baseAddress)
        {     
            using (var memoryWrapper = new MemoryWrapper(processHandle))
            {
                return memoryWrapper.ReadMemory<TStructure>(baseAddress);  
            }   
        }

        public bool WriteMemory(string processName, IntPtr baseAddress, byte[] buffer)
        {
            using (var memoryWrapper = new MemoryWrapper(processName))
            {
                return memoryWrapper.WriteMemory(baseAddress, buffer);
            }
        }
        
        public bool WriteMemory(int processId, IntPtr baseAddress, byte[] buffer)
        {
            using (var memoryWrapper = new MemoryWrapper(processId))
            {
                return memoryWrapper.WriteMemory(baseAddress, buffer);
            }
        }
        
        public bool WriteMemory(SafeHandle processHandle, IntPtr baseAddress, byte[] buffer)
        {
            using (var memoryWrapper = new MemoryWrapper(processHandle))
            {
                return memoryWrapper.WriteMemory(baseAddress, buffer);
            }
        }

        public bool WriteMemory<TStructure>(string processName, IntPtr baseAddress, TStructure structure)
        {
            using (var memoryWrapper = new MemoryWrapper(processName))
            {
                return memoryWrapper.WriteMemory(baseAddress, structure);
            }
        }
        
        public bool WriteMemory<TStructure>(int processId, IntPtr baseAddress, TStructure structure)
        {
            using (var memoryWrapper = new MemoryWrapper(processId))
            {
                return memoryWrapper.WriteMemory(baseAddress, structure);
            }
        }
        
        public bool WriteMemory<TStructure>(SafeHandle processHandle, IntPtr baseAddress, TStructure structure)
        {
            using (var memoryWrapper = new MemoryWrapper(processHandle))
            {
                return memoryWrapper.WriteMemory(baseAddress, structure);
            }
        }
    }
}