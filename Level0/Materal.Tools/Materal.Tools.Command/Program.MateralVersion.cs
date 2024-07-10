using Materal.Tools.Core.MateralVersion;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;
using SubCommand = System.CommandLine.Command;

namespace Materal.Tools.Command
{
    public partial class Program
    {
        [AddSubCommand]
        public void AddMateralVersionCommand(RootCommand rootCommand)
        {
            SubCommand command = new("MateralVersion", "更改Materal的版本");

            Option<string?> pathOption = new("--Path", "指定路径");
            pathOption.AddAlias("-p");
            pathOption.IsRequired = false;
            pathOption.SetDefaultValue(null);
            command.AddOption(pathOption);

            Option<string?> versionOption = new("--Version", "指定版本");
            versionOption.AddAlias("-v");
            versionOption.IsRequired = false;
            versionOption.SetDefaultValue(null);
            command.AddOption(versionOption);

            command.SetHandler(MateralVersion, pathOption, versionOption);
            rootCommand.AddCommand(command);
        }
        private void MateralVersion(string? path, string? version)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                path = Environment.CurrentDirectory;
            }
            IMateralVersionService service = _serviceProvider.GetRequiredService<IMateralVersionService>();
            if (string.IsNullOrWhiteSpace(version))
            {
                service.UpdateVersionAsync(path);
            }
            else
            {
                service.UpdateVersionAsync(path, version);
            }
        }
    }
}
