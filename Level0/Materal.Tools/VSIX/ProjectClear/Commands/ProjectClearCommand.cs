using EnvDTE;
using System.Collections.Generic;
using System.IO;
using Solution = Community.VisualStudio.Toolkit.Solution;

namespace ProjectClear
{
    [Command(PackageIds.ProjectClearCommand)]
    internal sealed class ProjectClearCommand : BaseCommand<ProjectClearCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            Solution solution = await VS.Solutions.GetCurrentSolutionAsync();
            List<string> messages = ClearSolution(solution);
            if (messages.Count <= 0)
            {
                await VS.StatusBar.ShowMessageAsync("清理bin、obj完成");
                return;
            }
            await VS.MessageBox.ShowErrorAsync(string.Join("\n", messages));
        }
        private List<string> ClearSolution(Solution solution)
        {
            List<string> messages = [];
            foreach (SolutionItem item in solution.Children)
            {
                messages.AddRange(ClearProject(item));
            }
            return messages;
        }
        private List<string> ClearProject(SolutionItem solutionItem)
        {
            List<string> messages = [];
            if (solutionItem.Type == SolutionItemType.Project)
            {
                DirectoryInfo directoryInfo = new(Path.GetDirectoryName(solutionItem.FullPath));
                foreach (DirectoryInfo subDirectoryInfo in directoryInfo.GetDirectories())
                {
                    if (subDirectoryInfo.Name != "bin" && subDirectoryInfo.Name != "obj") continue;
                    try
                    {
                        subDirectoryInfo.Delete(true);
                    }
                    catch (Exception ex)
                    {
                        messages.Add($"删除文件夹失败:{subDirectoryInfo.FullName}");
                        messages.Add(ex.Message);
                    }
                }
            }
            else
            {
                foreach (SolutionItem item in solutionItem.Children)
                {
                    messages.AddRange(ClearProject(item));
                }
            }
            return messages;
        }
    }
}
