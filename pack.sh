#!/bin/bash

dotnet restore
dotnet build
cd dotnet-packer
dotnet pack -o ../nupkgs
cd ../SampleTargets.PackerTarget
dotnet pack -o ../nupkgs
cd ..
