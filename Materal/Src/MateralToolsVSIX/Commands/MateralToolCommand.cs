#nullable enable
using MateralToolsVSIX.ToolWindows;

namespace MateralToolsVSIX
{
    [Command(PackageIds.MateralToolCommand)]
    internal sealed class MateralToolCommand : BaseCommand<MateralToolCommand>
    {
        protected override Task ExecuteAsync(OleMenuCmdEventArgs e) => MateralToolWindow.ShowAsync();
    }
}
