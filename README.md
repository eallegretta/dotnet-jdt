# dotnet-jdt
dotnet-jdt is a cross-platform .NET Core 2.1 or later global tool for applying [JSON Document Transformation](https://github.com/Microsoft/json-document-transforms)


### <a name="dotnet-jdt-tool"></a> Global tool for .NET Core 2.1 and later  [![NuGet package](https://img.shields.io/nuget/dt/dotnet-jdt.svg)](https://www.nuget.org/packages/dotnet-jdt/) 

.NET Core 2.1 introduces the concept of [global tools](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools),
meaning that you can install `dotnet-jdt` using the .NET CLI and use it everywhere. One advantage of this approach 
is that you can use the same command, for both installation and usage, across all platforms.

> :warning: To use global tools, .Net Core SDK 2.1.300 or later is required. 

Install `dotnet-jdt` as a global tool (only once):

```cmd
dotnet tool install --global dotnet-jdt --version 1.0.0
```

And then you can apply XDT transforms, from the command-line, anywhere on your PC, e.g.:

```shell
dotnet jdt --source original.json --transform delta.json --output final.json
```

