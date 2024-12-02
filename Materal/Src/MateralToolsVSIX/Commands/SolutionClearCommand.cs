#nullable enable
using Materal.Tools.Core.ProjectClear;
using System.IO;

namespace MateralToolsVSIX.Commands
{
    [Command(PackageIds.SolutionClearCommand)]
    public class SolutionClearCommand : BaseCommand<SolutionClearCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            Solution? solution = await VS.Solutions.GetCurrentSolutionAsync();
            if (solution is null || solution.FullPath is null) return;
            string projectPath = Path.GetDirectoryName(solution.FullPath);
            ProjectClearConfig config = new();
            config.DictionaryWhiteList.Remove(".vs");
            IProjectClearService projectClearService = new ProjectClearService(config);
            IProgress<ClearProgress> progress = new Progress<ClearProgress>(m =>
            {
                _ = VS.StatusBar.ShowMessageAsync($"正在处理: {m.CurrentPath},已处理文件:{m.ProcessedFiles},已处理文件夹:{m.ProcessedDirectories}");
            });
            await projectClearService.ClearProjectAsync(projectPath, progress);
            await VS.StatusBar.ShowMessageAsync("项目清理完成");
        }
    }
}
