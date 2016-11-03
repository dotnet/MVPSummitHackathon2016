using System.Collections.Generic;

namespace CliCommands.Packer.Task.Models
{
    public class PackagingMetadata
    {
        public string PackageFileName { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationVersion {get; set; }
        public string PathToFiles {get; set; }
        public DeploymentType DeploymentType { get; set; }

        public Dictionary<string, string> CustomMetadata {get; set;} 


    }
}