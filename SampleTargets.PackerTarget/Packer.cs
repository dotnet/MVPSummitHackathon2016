using Microsoft.Build.Framework;
using System;
using CliCommands.Packer.Task.Models;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CliCommands.Packer.Task
{
    public class Packer : Microsoft.Build.Utilities.Task
    {

        [Required]
        public string Format {get; set; }
        
        [Required]
        public string PublishedOutput {get; set; }
        
        public string PackageName {get; set; } 

        public string ApplicationName {get; set; }
        public string ApplicationVersion {get; set; }
        public string Rid { get; set; }
        public string MetadataFile {get; set;}
        public ITaskItem AdditionalMetadata { get; set; }
        public bool IsPortable => String.IsNullOrEmpty(Rid);

        public override bool Execute()
        {
            var packer = GetPackerFromFormat();
            var metadata = GetPackagingMetadata();
            if (!packer.Validate())
            {
                Log.LogError($"The chosen packer is not valid for this operating system.");
                return false;
            }
            Log.LogMessage(MessageImportance.Normal, $"Chosen packer is: {Format}; published output is {PublishedOutput}");
            Log.LogMessage(MessageImportance.Normal, $"The package will be published as {metadata.PackageFileName}");
            try {
                packer.Pack(metadata);
                Log.LogMessage(MessageImportance.High, "Packaging complete!");
                return true;
            } catch (Exception ex)
            {
                Log.LogError($"An error happened: {ex.Message}");
                return false;
            }
        }

        private PackagingMetadata GetPackagingMetadata()
        {
            var result = new PackagingMetadata();
            result.DeploymentType = IsPortable ? DeploymentType.FrameworkDependent : DeploymentType.SelfContained;
            result.ApplicationName = ApplicationName;
            result.ApplicationVersion = ApplicationVersion;
            if (String.IsNullOrEmpty(PackageName))
            {
                result.PackageFileName = Path.Combine(PublishedOutput, $"{result.ApplicationName}-{result.ApplicationVersion}");
            } else
            {
                result.PackageFileName = PackageName;
            }
            result.PathToFiles = PublishedOutput;
            result.CustomMetadata = (Dictionary<string, string>)AdditionalMetadata.CloneCustomMetadata();
            // We inverse here and now go through the file. If the key exists, we simply skip since the properties 
            // win by order. 
            if (!String.IsNullOrEmpty(MetadataFile) && File.Exists(MetadataFile))
            {
                var fileMetadata = JObject.Parse(File.ReadAllText(MetadataFile));
                foreach (var line in fileMetadata)
                {
                    var key = line.Key.Substring(0,1).ToUpper() + line.Key.Substring(1, line.Key.Length);
                    if (!result.CustomMetadata.ContainsKey(key))
                        result.CustomMetadata.Add(key, line.Value.ToString());
                }

            }
            return result;
        }

        private IPacker GetPackerFromFormat()
        {
            var type = this.GetType().
                GetTypeInfo().Assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IPacker)) && t.Name != "BasePacker")
                .Single(t => t.Name.ToLowerInvariant().Contains(Format.ToLowerInvariant()));
            return (IPacker)type.GetConstructor(Type.EmptyTypes).Invoke(Type.EmptyTypes);
        }
    }
}
