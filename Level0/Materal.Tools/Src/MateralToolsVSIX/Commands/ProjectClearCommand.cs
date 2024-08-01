#nullable enable
using EnvDTE;
using EnvDTE80;
using System.Collections.Generic;

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
            List<string> messages = [];
            foreach (SelectedItem selectedItem in dte.SelectedItems)
            {
                if (selectedItem.Project is null || !selectedItem.Project.FullName.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase)) continue;
                List<string> clearMessage = ClearBinObjHelper.ClearBinObj(selectedItem.Project.FullName);
                messages.AddRange(clearMessage);
            }
            if (messages.Count <= 0)
            {
                await VS.StatusBar.ShowMessageAsync("清理bin、obj完成");
                return;
            }
            await VS.MessageBox.ShowErrorAsync(string.Join("\n", messages));
        }
    }
}
