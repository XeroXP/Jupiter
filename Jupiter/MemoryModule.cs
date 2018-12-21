using System;
using System.Runtime.InteropServices;
using Jupiter.Wrappers;

namespace Jupiter
{
    public class MemoryModule
    {
        public IntPtr AllocateMemory(string processName, int size)
        {
            return new MethodWrapper(processName).AllocateMemory(size);
        }
        
        public IntPtr AllocateMemory(int processId, int size)
        {
            return new MethodWrapper(processId).AllocateMemory(size);
        }
        
        public IntPtr AllocateMemory(SafeHandle processHandle, int size)
        {
            return new MethodWrapper(processHandle).AllocateMemory(size);  
        }

        public bool FreeMemory(string processName, IntPtr baseAddress, int size)
        {
            return new MethodWrapper(processName).FreeMemory(baseAddress, size);
        }
        
        public bool FreeMemory(int processId, IntPtr baseAddress, int size)
        {
            return new MethodWrapper(processId).FreeMemory(baseAddress, size);
        }
        
        public bool FreeMemory(SafeHandle processHandle, IntPtr baseAddress, int size)
        {
            return new MethodWrapper(processHandle).FreeMemory(baseAddress, size);
        }
        
        public IntPtr[] PatternScan(string processName, IntPtr baseAddress, string pattern)
        {
            return new ExtensionWrapper(processName).PatternScan(baseAddress, pattern);
        }
        
        public IntPtr[] PatternScan(int processId, IntPtr baseAddress, string pattern)
        {
            return new ExtensionWrapper(processId).PatternScan(baseAddress, pattern);
        }
        
        public IntPtr[] PatternScan(SafeHandle processHandle, IntPtr baseAddress, string pattern)
        {
            return new ExtensionWrapper(processHandle).PatternScan(baseAddress, pattern);
        }

        public byte[] ReadMemory(string processName, IntPtr baseAddress, int size)
        {
            return new MethodWrapper(processName).ReadMemory(baseAddress, size);
        }
        
        public byte[] ReadMemory(int processId, IntPtr baseAddress, int size)
        {
            return new MethodWrapper(processId).ReadMemory(baseAddress, size);
        }
        
        public byte[] ReadMemory(SafeHandle processHandle, IntPtr baseAddress, int size)
        {
            return new MethodWrapper(processHandle).ReadMemory(baseAddress, size);
        }

        public TStructure ReadMemory<TStructure>(string processName, IntPtr baseAddress)
        {
            return new MethodWrapper(processName).ReadMemory<TStructure>(baseAddress);   
        }
        
        public TStructure ReadMemory<TStructure>(int processId, IntPtr baseAddress)
        {     
            return new MethodWrapper(processId).ReadMemory<TStructure>(baseAddress);  
        }
        
        public TStructure ReadMemory<TStructure>(SafeHandle processHandle, IntPtr baseAddress)
        {     
            return new MethodWrapper(processHandle).ReadMemory<TStructure>(baseAddress);    
        }

        public bool WriteMemory(string processName, IntPtr baseAddress, byte[] buffer)
        {
            return new MethodWrapper(processName).WriteMemory(baseAddress, buffer);
        }
        
        public bool WriteMemory(int processId, IntPtr baseAddress, byte[] buffer)
        {
            return new MethodWrapper(processId).WriteMemory(baseAddress, buffer);
        }
        
        public bool WriteMemory(SafeHandle processHandle, IntPtr baseAddress, byte[] buffer)
        {
            return new MethodWrapper(processHandle).WriteMemory(baseAddress, buffer);
        }

        public bool WriteMemory<TStructure>(string processName, IntPtr baseAddress, TStructure structure)
        {
            return new MethodWrapper(processName).WriteMemory(baseAddress, structure);
        }
        
        public bool WriteMemory<TStructure>(int processId, IntPtr baseAddress, TStructure structure)
        {
            return new MethodWrapper(processId).WriteMemory(baseAddress, structure);
        }
        
        public bool WriteMemory<TStructure>(SafeHandle processHandle, IntPtr baseAddress, TStructure structure)
        {
            return new MethodWrapper(processHandle).WriteMemory(baseAddress, structure);
        }
    }
}