using System;
//using System.CommandLine;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        var format = "Zip";
        //var pkgName = "";

        //ArgumentSyntax.Parse(args, syntax =>
        //{
            //syntax.DefineOption("f|format", ref format, "The format that you want to pack in. Currently valid: zip");
            //syntax.DefineOption("o|output", ref pkgName, "The resulting package name");
        //});

        var shellOutCommand = "dotnet";
        //var arguments = $"msbuild /t:Packer /p:PackageName={pkgName} /p:Format={format} /v:Quiet";
        var arguments = $"msbuild /t:Packer /p:Format={format}";

        var psi = new ProcessStartInfo
        {
            FileName = shellOutCommand,
            Arguments = arguments
        };

        var process = new Process
        {
            StartInfo = psi,

        };

        var rcode = process.Start();
        

    }
}
