using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Jupiter.Wrappers
{
    internal class MethodWrapper
    {
        private readonly SafeHandle _processHandle;
        
        internal MethodWrapper(string processName)
        {
            // Ensure the argument passed in is valid

            if (string.IsNullOrWhiteSpace(processName))
            {
                throw new ArgumentException("One or more of the arguments provided was invalid");
            }
            
            // Get an instance of the process
            
            Process process;

            try
            {
                process = Process.GetProcessesByName(processName)[0];
            }

            catch (IndexOutOfRangeException)
            {
                // The process isn't currently running

                throw new Exception($"No process with name {processName} is currently running");
            }
            
            // Get a handle to the process

            _processHandle = process.SafeHandle;
        }
        
        internal MethodWrapper(int processId)
        {
            // Ensure the argument passed in is valid

            if (processId == 0)
            {
                throw new ArgumentException("One or more of the arguments provided was invalid");
            }
            
            // Get an instance of the process
            
            Process process;

            try
            {
                process = Process.GetProcessById(processId);
            }

            catch (IndexOutOfRangeException)
            {
                // The process isn't currently running

                throw new Exception($"No process with id {processId} is currently running");
            }
            
            // Get a handle to the process

            _processHandle = process.SafeHandle;
        }
        
        internal MethodWrapper(SafeHandle processHandle)
        {
            // Ensure the argument passed in is valid

            _processHandle = processHandle ?? throw new ArgumentException("One or more of the arguments provided was invalid");
        }
        
        internal IntPtr AllocateMemory(int size)
        {
            // Ensure the argument passed in is valid

            if (size == 0)
            {
                throw new ArgumentException("One or more of the arguments provided was invalid");
            }
            
            return Methods.AllocateMemory.Allocate(_processHandle, size);
        }

        internal bool FreeMemory(IntPtr baseAddress, int size)
        {
            // Ensure the arguments passed in are valid

            if (baseAddress == IntPtr.Zero || size == 0)
            {
                throw new ArgumentException("One or more of the arguments provided was invalid");
            }
            
            return Methods.FreeMemory.Free(_processHandle, baseAddress, size);
        }
        
        internal byte[] ReadMemory(IntPtr baseAddress, int size)
        {
            // Ensure the arguments passed in are valid

            if (baseAddress == IntPtr.Zero || size == 0)
            {
                throw new ArgumentException("One or more of the arguments provided was invalid");
            }
            
            return Methods.ReadMemory.Read(_processHandle, baseAddress, size);
        }

        internal TStructure ReadMemory<TStructure>(IntPtr baseAddress)
        {
            // Ensure the argument passed in is valid

            if (baseAddress == IntPtr.Zero)
            {
                throw new ArgumentException("One or more of the arguments provided was invalid");
            }
            
            return Methods.ReadMemory.Read<TStructure>(_processHandle, baseAddress);
        }
        
        internal bool WriteMemory(IntPtr baseAddress, byte[] buffer)
        {
            // Ensure the arguments passed in are valid

            if (baseAddress == IntPtr.Zero || buffer == null)
            {
                throw new ArgumentException("One or more of the arguments provided was invalid");
            }
            
            return Methods.WriteMemory.Write(_processHandle, baseAddress, buffer);
        }

        internal bool WriteMemory<TStructure>(IntPtr baseAddress, TStructure structure)
        {
            // Ensure the argument passed in is valid

            if (baseAddress == IntPtr.Zero)
            {
                throw new ArgumentException("One or more of the arguments provided was invalid");
            }
            
            return Methods.WriteMemory.Write(_processHandle, baseAddress, structure);
        }
    }
}