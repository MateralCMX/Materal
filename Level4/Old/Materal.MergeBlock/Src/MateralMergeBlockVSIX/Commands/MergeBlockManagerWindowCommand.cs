namespace MateralMergeBlockVSIX
{
    [Command(PackageIds.ShowManagerWindowCommand)]
    internal sealed class MergeBlockManagerWindowCommand : BaseCommand<MergeBlockManagerWindowCommand>
    {
        protected override Task ExecuteAsync(OleMenuCmdEventArgs e) => MergeBlockManagerWindow.ShowAsync();
    }
}
