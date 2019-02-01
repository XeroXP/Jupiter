## Jupiter

[![Build status](https://ci.appveyor.com/api/projects/status/jp6fnwbq34w012gj?svg=true)](https://ci.appveyor.com/project/Akaion/jupiter)

A Windows external memory editing library written in C# that supports several memory editing methods.

----

### Supported Methods

* Allocate Memory
* Free Memory
* Protect Memory
* Read Memory
* Write Memory

### Extensions

* Pattern Scanning with support for wildcard bytes

----

### Installation

* Download and install Jupiter using [NuGet](https://www.nuget.org/packages/Jupiter)

----

### Useage

Any method can be overloaded with a process id instead of a process name

#### Allocate Memory

```csharp
using Jupiter;

var memoryModule = new MemoryModule();

// Allocate memory in a remote process

var allocatedMemoryAddress = memoryModule.AllocateMemory("processName", size);
```

#### Free Memory

```csharp
using Jupiter;

var memoryModule = new MemoryModule();

// Free memory in a remote process at an address

memoryModule.FreeMemory("processName", address);
```

#### Protect Memory

```csharp
using Jupiter;

var memoryModule = new MemoryModule();

// Protect memory in a remote process at an address

memoryModule.ProtectMemory("processName", address, size, protectionConstant);
```

#### Read Memory

```csharp
using Jupiter;

var memoryModule = new MemoryModule();

// Read a byte array from a remote process at an address

var memoryBytes = memoryModule.ReadMemory("processName", address, size);

// Read a structure from a remote process at an address

var memoryBoolean = memoryModule.ReadMemory<bool>("processName", address);

// Read a string from a remote process at an address

var memoryStringBytes = memoryModule.ReadMemory("processName", address, sizeOfString);

var memoryString = Encoding.Default.GetString(memoryStringBytes);
```

#### Write Memory

```csharp
using Jupiter;

var memoryModule = new MemoryModule();

// Write a byte array, structure or string into a remote process at an address

memoryModule.WriteMemory("processName", address, object);
```

#### Pattern Scan

```csharp
using Jupiter;

var memoryModule = new MemoryModule();

// Find the addresses where a pattern appears in a remote process

var patternAddresses = memoryModule.PatternScan("processName", IntPtr.Zero, "45 FF ?? 01 ?? ?? 2A");
```

You also have the option to overload the pattern with a byte array representing the pattern if wildcard bytes are not needed

```csharp
using Jupiter;

var memoryModule = new MemoryModule();

// Find the addresses where a pattern appears in a remote process

var pattern = new byte[] {0x45, 0xFF, 0x00, 0x01, 0x24, 0xAA, 0x2A};

var patternAddresses = memoryModule.PatternScan("processName", IntPtr.Zero, pattern);
```
----

### Contributing

Pull requests are welcome. 

For large changes, please open an issue first to discuss what you would like to add.
