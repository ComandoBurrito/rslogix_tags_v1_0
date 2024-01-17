# RSLogix Tags sender and receiver

A demo program that comunicates a Micrologix 1400 (1766-L32BXB) PLC directly with the Windonws Console via Ethernet using the "libplctag" library (C# wrapper).


![Screenshot 2024-01-16 173501](https://github.com/ComandoBurrito/rslogix_tags_v1_0/assets/26191102/9eea9f62-420c-4350-90b1-7f8c3a6e8777)




Get Libplctag at https://github.com/kyle-github/libplctag

## Packages
This repository contains two .NET packages that are published to Nuget.org:

| Package | Downloads | Stable | Preview |
|-|-|-|-|
| [libplctag](https://www.nuget.org/packages/libplctag/) | ![Nuget](https://img.shields.io/nuget/dt/libplctag) | ![Nuget version](https://img.shields.io/nuget/v/libplctag) | ![Nuget version](https://img.shields.io/nuget/vpre/libplctag) |
| [libplctag.NativeImport](https://www.nuget.org/packages/libplctag.NativeImport/) | ![Nuget](https://img.shields.io/nuget/dt/libplctag.NativeImport) | ![Nuget version](https://img.shields.io/nuget/v/libplctag.NativeImport) | ![Nuget version](https://img.shields.io/nuget/vpre/libplctag.NativeImport) |

### License:
This code is released under MIT license. The Libplctag dll is released under LGPL license.

### Installing on new pc

Get and update the libplctag class to make it work:
https://www.nuget.org/packages/libplctag.NativeImport/

Get and install .NET Framework 4.6.1 (this is the compatible version that will allow you to make the connection between the PLC and the console):
https://dotnet.microsoft.com/en-us/download/dotnet-framework/net461
