using System;
using System.Diagnostics;
using System.Linq;

namespace dotnetpacker
{
    class Program
    {
        static void Main(string[] args)
        {
            var msbArguments = $"msbuild /t:Packer /p:Format=zip /v:m";

            var psi = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = msbArguments
            };

            var process = new Process
            {
                StartInfo = psi,

            };

            var rcode = process.Start();


        }
    }
}