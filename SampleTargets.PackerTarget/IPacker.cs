using CliCommands.Packer.Task.Models;

namespace CliCommands.Packer.Task
{
    public interface IPacker
    {
        void Pack(PackagingMetadata metadata);

        bool Validate();
    }
}