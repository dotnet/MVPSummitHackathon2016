# Extensibility of the CLI toolset hackathon sample

This repository contains the basic sample of an extension to the MSBuild edition of the [CLI toolset for .NET Core](https://github.com/dotnet/cli). The idea of the repo is to provide an easy way to get started with developing extensions to the CLI and the SDK in terms of both MSBuild targets as well as "classic" tools that can be invoked using the `dotnet-<command>` invocation pattern.

The application in question is a proof-of-concept tool that allows the user to package a published application as a zip file. It is implemented as:

1. A target that depends on the publish target from the SDK
2. A tool that allows the user to invoke the target as `dotnet packer`

The `sample` directory contains a sample csproj file with added artifacts that can be restored using `dotnet restore` and that will allow this tool to run. 


