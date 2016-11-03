using System;
using CliCommands.Packer.Task.Models;

namespace CliCommands.Packer.Task.Packers
{
    public abstract class BasePacker : IPacker
    {
        public abstract void Pack(PackagingMetadata metadata);

        public virtual bool Validate()
        {
            // By default, we always assume the packer is valid; 
            // it is the job of any packer to override and implement custom logic
            return true;
        }
    }
}