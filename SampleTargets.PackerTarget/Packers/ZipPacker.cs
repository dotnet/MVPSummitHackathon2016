using CliCommands.Packer.Task.Models;
using System.IO.Compression;
using System.IO;
using System;

namespace CliCommands.Packer.Task.Packers
{
    public class ZipPacker : BasePacker
    {
        public override void Pack(PackagingMetadata metadata)
        {
            var finalName = metadata.PackageFileName + (metadata.PackageFileName.Contains(".zip") ? String.Empty : ".zip");
            if (File.Exists(finalName))
                File.Delete(finalName);
            var tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            try {
                ZipFile.CreateFromDirectory(metadata.PathToFiles, tempFile);
                File.Copy(tempFile, finalName);
            } catch
            {
                throw;
            } finally 
            {
                File.Delete(tempFile);
            }

        }
    }
}