using Materal.Tools.Core.ProjectClear;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;
using SubCommand = System.CommandLine.Command;

namespace Materal.Tools.Command
{
    public partial class Program
    {
        [AddSubCommand]
        public void AddProjectClearCommand(RootCommand rootCommand)
        {
            SubCommand command = new("ProjectClear", "清理项目文件夹[.vs、bin、obj、node_modules、空文件夹]");
            Option<string?> pathOption = new("--Path", "指定路径");
            pathOption.AddAlias("-p");
            pathOption.IsRequired = false;
            pathOption.SetDefaultValue(null);
            command.AddOption(pathOption);
            command.SetHandler(ProjectClear, pathOption);
            rootCommand.AddCommand(command);
        }
        private void ProjectClear(string? path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                path = Environment.CurrentDirectory;
            }
            IProjectClearService service = _serviceProvider.GetRequiredService<IProjectClearService>();
            service.ClearProject(path);
        }
    }
}
