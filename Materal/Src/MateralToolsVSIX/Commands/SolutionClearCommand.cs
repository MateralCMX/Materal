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
            new ProjectClearService().ClearProject(projectPath);
            await VS.StatusBar.ShowMessageAsync("项目清理完成");
        }
    }
}
