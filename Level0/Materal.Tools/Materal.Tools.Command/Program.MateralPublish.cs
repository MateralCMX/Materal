using Materal.Tools.Core.MateralPublish;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;
using SubCommand = System.CommandLine.Command;

namespace Materal.Tools.Command
{
    public partial class Program
    {
        [AddSubCommand]
        public void AddMateralPublishCommand(RootCommand rootCommand)
        {
            SubCommand command = new("MateralPublish", "发布Materal项目");

            Option<string?> pathOption = new("--Path", "指定路径");
            pathOption.AddAlias("-p");
            pathOption.IsRequired = false;
            pathOption.SetDefaultValue(null);
            command.AddOption(pathOption);

            Option<string?> versionOption = new("--Version", "指定路径");
            versionOption.AddAlias("-v");
            versionOption.IsRequired = false;
            versionOption.SetDefaultValue(null);
            command.AddOption(versionOption);

            command.SetHandler(MateralPublishAsync, pathOption, versionOption);
            rootCommand.AddCommand(command);
        }
        private async Task MateralPublishAsync(string? path, string? version)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                path = Environment.CurrentDirectory;
            }
            IMateralPublishService service = _serviceProvider.GetRequiredService<IMateralPublishService>();
            DirectoryInfo directoryInfo = new(path);
            DirectoryInfo rootDirectoryInfo = service.GetMateralProjectRootPath(directoryInfo.FullName);
            version ??= service.GetNowVersion(rootDirectoryInfo.FullName);
            ICollection<IMateralProject> allProjects = service.GetAllProjects();
            if (directoryInfo.FullName == rootDirectoryInfo.FullName)//发布所有项目
            {
                await service.PublishAsync(directoryInfo.FullName, version, allProjects);
            }
            else if (directoryInfo.Parent?.FullName == rootDirectoryInfo.FullName && directoryInfo.Name.StartsWith("Level"))//发布一个等级的项目
            {
                int level = Convert.ToInt32(directoryInfo.Name[5..]);
                ICollection<IMateralProject> levelProjects = allProjects.Where(m => m.Level == level).ToList();
                await service.PublishAsync(rootDirectoryInfo.FullName, version, levelProjects);
            }
            else if (directoryInfo.Parent?.Parent?.FullName == rootDirectoryInfo.FullName)//发布一个独立的项目
            {
                string name = directoryInfo.Name;
                int level = Convert.ToInt32(directoryInfo.Parent.Name[5..]);
                IMateralProject? project = allProjects.FirstOrDefault(m => m.Level == level && m.Name == name);
                if (project is null) return;
                await service.PublishAsync(rootDirectoryInfo.FullName, version, [project]);
            }
        }
    }
}
