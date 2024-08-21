#nullable enable
using System.Collections.Generic;

namespace MateralToolsVSIX.Commands
{
    [Command(PackageIds.SolutionClearCommand)]
    public class SolutionClearCommand : BaseCommand<SolutionClearCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            Solution? solution = await VS.Solutions.GetCurrentSolutionAsync();
            if (solution is null) return;
            List<string> messages = ClearBinObj(solution);
            if (messages.Count <= 0)
            {
                await VS.StatusBar.ShowMessageAsync("清理bin、obj完成");
                return;
            }
            await VS.MessageBox.ShowErrorAsync(string.Join("\n", messages));
        }
        private static List<string> ClearBinObj(SolutionItem solutionItem)
        {
            List<string> messages = [];
            if (solutionItem.FullPath is null) return messages;
            if (solutionItem.Type == SolutionItemType.Project)
            {
                List<string> clearMessage = ClearBinObjHelper.ClearBinObj(solutionItem.FullPath);
                messages.AddRange(clearMessage);
            }
            else
            {
                foreach (SolutionItem? item in solutionItem.Children)
                {
                    if (item is null) continue;
                    messages.AddRange(ClearBinObj(item));
                }
            }
            return messages;
        }
    }
}
