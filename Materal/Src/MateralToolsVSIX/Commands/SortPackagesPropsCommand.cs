#nullable enable
using EnvDTE;
using EnvDTE80;
using Materal.Tools.Core.NugegtPackages;

namespace MateralToolsVSIX.Commands
{
    [Command(PackageIds.SortPackagesPropsCommand)]
    internal sealed class SortPackagesPropsCommand : BaseCommand<SortPackagesPropsCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            DTE2? dte = await VS.GetServiceAsync<DTE, DTE2>();
            if (dte is null || dte.SelectedItems is null || dte.SelectedItems.Count != 1) return;
            IPackagesPropsService packagesPropsService = new PackagesPropsService();
            foreach (SelectedItem selectedItem in dte.SelectedItems)
            {
                if (string.IsNullOrWhiteSpace(selectedItem.ProjectItem?.FileNames[1]) || selectedItem.ProjectItem is null || !selectedItem.ProjectItem.FileNames[1].EndsWith("Packages.props", StringComparison.OrdinalIgnoreCase)) continue;
                packagesPropsService.SortAndRemoveDuplicates(selectedItem.ProjectItem.FileNames[1]);
            }
            await VS.StatusBar.ShowMessageAsync("Directory.Packages.props文件整理成功");
        }
    }
}
