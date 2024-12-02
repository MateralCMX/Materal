#nullable enable
using EnvDTE;
using EnvDTE80;
using Materal.Tools.Core.ProjectClear;
using System.IO;

namespace MateralToolsVSIX.Commands
{
    [Command(PackageIds.ProjectClearCommand)]
    public class ProjectClearCommand : BaseCommand<ProjectClearCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            DTE2? dte = await VS.GetServiceAsync<DTE, DTE2>();
            if (dte is null || dte.SelectedItems is null || dte.SelectedItems.Count == 0) return;
            ProjectClearConfig config = new();
            config.DictionaryWhiteList.Remove(".vs");
            IProjectClearService projectClearService = new ProjectClearService(config);
            IProgress<ClearProgress> progress = new Progress<ClearProgress>(m =>
            {
                _ = VS.StatusBar.ShowMessageAsync($"正在处理: {m.CurrentPath},已处理文件:{m.ProcessedFiles},已处理文件夹:{m.ProcessedDirectories}");
            });
            foreach (SelectedItem selectedItem in dte.SelectedItems)
            {
                if (selectedItem.Project is null || !selectedItem.Project.FullName.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase)) continue;
                string projectPath = Path.GetDirectoryName(selectedItem.Project.FullName);
                await projectClearService.ClearProjectAsync(projectPath, progress);
            }
            await VS.StatusBar.ShowMessageAsync("项目清理完成");
        }
    }
}
