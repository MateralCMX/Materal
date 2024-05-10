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
            string solutionPath = Path.GetDirectoryName(solution.FullPath);
            List<string> messages = ClearDirectories(solutionPath);
            if (messages.Count <= 0)
            {
                await VS.StatusBar.ShowMessageAsync("清理bin、obj完成");
                return;
            }
            await VS.MessageBox.ShowErrorAsync(string.Join("\n", messages));
        }
        private List<string> ClearDirectories(string startLocation)
        {
            List<string> messages = [];
            DirectoryInfo directoryInfo = new(startLocation);
            foreach (DirectoryInfo subDirectoryInfo in directoryInfo.GetDirectories())
            {
                if (subDirectoryInfo.Name == "bin" || subDirectoryInfo.Name == "obj")
                {
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
                else
                {
                    List<string> subMessages = ClearDirectories(subDirectoryInfo.FullName);
                    messages.AddRange(subMessages);
                }
            }
            return messages;
        }
    }
}
