using MateralPublish.Extensions;
using MateralPublish.Helper;
using System.Diagnostics;
using System.Text;

namespace MateralPublish.Models
{
    public abstract class BaseProjectModel
    {
        /// <summary>
        /// 项目文件夹信息
        /// </summary>
        public DirectoryInfo ProjectDirectoryInfo { get; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name => ProjectDirectoryInfo.Name;
        protected BaseProjectModel(string directoryPath)
        {
            ProjectDirectoryInfo = new DirectoryInfo(directoryPath);
            if (!ProjectDirectoryInfo.Exists) throw new Exception("项目不存在");
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="publishDirectoryInfo"></param>
        /// <param name="version"></param>
        public virtual async Task PublishAsync(DirectoryInfo publishDirectoryInfo, string version)
        {
            await UpdateVersionAsync(version, ProjectDirectoryInfo);
            await PublishAsync(publishDirectoryInfo, ProjectDirectoryInfo);
            await UploadNugetPackageHelper.UploadNugetPackagesAsync();
        }
        /// <summary>
        /// 更新版本号
        /// </summary>
        /// <param name="version"></param>
        /// <param name="csprojFileInfo"></param>
        /// <returns></returns>
        protected virtual async Task UpdateVersionAsync(string version, DirectoryInfo rootDirectoryInfo)
        {
            FileInfo? csprojFileInfo = rootDirectoryInfo.GetFiles().FirstOrDefault(m => m.Extension == ".csproj");
            if (csprojFileInfo != null)
            {
                await UpdateVersionAsync(version, csprojFileInfo);
            }
            else
            {
                IEnumerable<DirectoryInfo> subDirectoryInfos = rootDirectoryInfo.GetDirectories();
                foreach (DirectoryInfo directoryInfo in subDirectoryInfos)
                {
                    await UpdateVersionAsync(version, directoryInfo);
                }
            }
        }
        /// <summary>
        /// 更新版本号
        /// </summary>
        /// <param name="version"></param>
        /// <param name="csprojFileInfo"></param>
        /// <returns></returns>
        protected virtual async Task UpdateVersionAsync(string version, FileInfo csprojFileInfo)
        {
            const string versionStartCode = "<Version>";
            const string materalPackageStartCode = "<PackageReference Include=\"Materal.";
            const string materalPackageVersionStartCode = "\" Version=\"";
            string projectName = Path.GetFileNameWithoutExtension(csprojFileInfo.Name);
            ConsoleHelper.WriteLine($"正在更新{projectName}版本号...");
            string[] csprojFileContents = await File.ReadAllLinesAsync(csprojFileInfo.FullName);
            for (int i = 0; i < csprojFileContents.Length; i++)
            {
                string tempCode = csprojFileContents[i].Trim();
                if (tempCode.StartsWith(versionStartCode))
                {
                    csprojFileContents[i] = $"\t\t<Version>{version}</Version>";
                }
                else if (tempCode.StartsWith(materalPackageStartCode))
                {
                    int versionLength = tempCode.IndexOf(materalPackageVersionStartCode);
                    string packageName = tempCode[materalPackageStartCode.Length..versionLength];
                    csprojFileContents[i] = $"\t\t<PackageReference Include=\"Materal.{packageName}\" Version=\"{version}\" />";
                }
            }
            await File.WriteAllLinesAsync(csprojFileInfo.FullName, csprojFileContents, Encoding.UTF8);
            ConsoleHelper.WriteLine($"{projectName}版本号已更新到{version}");
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="publishDirectoryInfo"></param>
        /// <param name="rootDirectoryInfo"></param>
        protected virtual async Task PublishAsync(DirectoryInfo publishDirectoryInfo, DirectoryInfo rootDirectoryInfo)
        {
            FileInfo? csprojFileInfo = rootDirectoryInfo.GetFiles().FirstOrDefault(m => m.Extension == ".csproj");
            if (csprojFileInfo != null)
            {
                if (IsPublishProject(Path.GetFileNameWithoutExtension(csprojFileInfo.Name)))
                {
                    await PublishAsync(publishDirectoryInfo, csprojFileInfo);
                }
            }
            else
            {
                IEnumerable<DirectoryInfo> subDirectoryInfos = rootDirectoryInfo.GetDirectories();
                foreach (DirectoryInfo directoryInfo in subDirectoryInfos)
                {
                    await PublishAsync(publishDirectoryInfo, directoryInfo);
                }
            }
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="publishDirectoryInfo"></param>
        /// <param name="csprojFileInfo"></param>
        protected virtual async Task<DirectoryInfo?> PublishAsync(DirectoryInfo publishDirectoryInfo, FileInfo csprojFileInfo)
        {
            string projectName = Path.GetFileNameWithoutExtension(csprojFileInfo.Name);
            CmdHelper cmdHelper = new();
            DirectoryInfo truePublishDirectoryInfo = Path.Combine(publishDirectoryInfo.FullName, projectName).GetNewDirectoryInfo();
            string cmd = $"dotnet publish {csprojFileInfo.FullName} -o {truePublishDirectoryInfo.FullName} -c Release";
            ConsoleHelper.WriteLine($"正在发布{projectName}代码...");
            cmdHelper.OutputDataReceived += CmdHelper_OutputDataReceived;
            cmdHelper.ErrorDataReceived += CmdHelper_ErrorDataReceived;
            await cmdHelper.RunCmdCommandsAsync(cmd);
            ConsoleHelper.WriteLine($"{projectName}代码发布完毕");
            return truePublishDirectoryInfo;
        }
        private void CmdHelper_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            ConsoleHelper.WriteLine(e.Data, ConsoleColor.DarkRed);
        }
        private void CmdHelper_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            ConsoleHelper.WriteLine(e.Data, ConsoleColor.DarkGreen);
        }
        /// <summary>
        /// 是要发布的项目
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected virtual bool IsPublishProject(string name)
        {
            return name.EndsWith(".Test");
        }
    }
}
