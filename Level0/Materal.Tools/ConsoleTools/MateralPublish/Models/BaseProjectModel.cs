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
        public int Level { get; }
        public virtual int Index => 0;
        protected BaseProjectModel(string solutionPath, int level, string projectName)
        {
            Level = level;
            string levelName = $"Level{level}";
            string path = Path.Combine(solutionPath, levelName, projectName);
            ProjectDirectoryInfo = new DirectoryInfo(path);
            if (!ProjectDirectoryInfo.Exists) throw new Exception("项目不存在");
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="version"></param>
        public virtual async Task PublishAsync(string version)
        {
            await UpdateVersionAsync(version, ProjectDirectoryInfo);
            if (PublishHelper.PublishDirectoryInfo is null) return;
            await PackageAsync(ProjectDirectoryInfo);
            await PublishAsync(ProjectDirectoryInfo);
        }
        #region 更新版本号
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
            const string materalPackageStartCode = "<PackageReference Include=\"Materal.";
            const string rcPackageStartCode = "<PackageReference Include=\"RC.";
            await UpdateVersionAsync(version, csprojFileInfo, [materalPackageStartCode, rcPackageStartCode]);
        }
        /// <summary>
        /// 更新版本号
        /// </summary>
        /// <param name="version"></param>
        /// <param name="csprojFileInfo"></param>
        /// <returns></returns>
        protected virtual async Task UpdateVersionAsync(string version, FileInfo csprojFileInfo, string[] packageStartCodes)
        {
            const string versionStartCode = "<Version>";
            const string materalPackageVersionStartCode = "\" Version=\"";
            string projectName = Path.GetFileNameWithoutExtension(csprojFileInfo.Name);
            ConsoleHelper.WriteLine($"正在更新{projectName}版本号...");
            string[] csprojFileContents = await File.ReadAllLinesAsync(csprojFileInfo.FullName);
            for (int i = 0; i < csprojFileContents.Length; i++)
            {
                string tempCode = csprojFileContents[i].Trim();
                foreach (string packageStartCode in packageStartCodes)
                {
                    if (tempCode.StartsWith(packageStartCode))
                    {
                        int versionLength = tempCode.IndexOf(materalPackageVersionStartCode);
                        if (versionLength > 0)
                        {
                            string packageName = tempCode[packageStartCode.Length..versionLength];
                            csprojFileContents[i] = $"\t\t<PackageReference Include=\"Materal.{packageName}\" Version=\"{version}\"";
                            if (tempCode.EndsWith("/>"))
                            {
                                csprojFileContents[i] += $" />";
                            }
                            else
                            {
                                csprojFileContents[i] += $">";
                            }
                        }
                        else
                        {
                            if (i + 1 >= csprojFileContents.Length) continue;
                            string nextTempCode = csprojFileContents[i + 1].Trim();
                            if (nextTempCode.StartsWith(versionStartCode))
                            {
                                csprojFileContents[i + 1] = $"\t\t\t<Version>{version}</Version>";
                                i++;
                            }
                        }
                    }
                    else if (tempCode.StartsWith(versionStartCode))
                    {
                        csprojFileContents[i] = $"\t\t<Version>{version}</Version>";
                    }
                }
            }
            await File.WriteAllLinesAsync(csprojFileInfo.FullName, csprojFileContents, Encoding.UTF8);
            ConsoleHelper.WriteLine($"{projectName}版本号已更新到{version}");
        }
        #endregion
        #region 打包
        /// <summary>
        /// 打包
        /// </summary>
        /// <param name="publishDirectoryInfo"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        protected virtual async Task PackageAsync(DirectoryInfo rootDirectoryInfo)
        {
            FileInfo? csprojFileInfo = rootDirectoryInfo.GetFiles().FirstOrDefault(m => m.Extension == ".csproj");
            if (csprojFileInfo != null)
            {
                if (!await IsPackageProjectAsync(csprojFileInfo)) return;
                await PackageAsync(csprojFileInfo);
            }
            else
            {
                IEnumerable<DirectoryInfo> subDirectoryInfos = rootDirectoryInfo.GetDirectories();
                foreach (DirectoryInfo directoryInfo in subDirectoryInfos)
                {
                    await PackageAsync(directoryInfo);
                }
            }
        }
        /// <summary>
        /// 是要发布的项目
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected virtual async Task<bool> IsPackageProjectAsync(FileInfo csprojFileInfo)
        {
            if (!csprojFileInfo.Exists) return false;
            string fileContent = await File.ReadAllTextAsync(csprojFileInfo.FullName);
            return fileContent.Contains("<IsPackable>true</IsPackable>");
        }
        /// <summary>
        /// 打包
        /// </summary>
        /// <param name="csprojFileInfo"></param>
        protected virtual async Task PackageAsync(FileInfo csprojFileInfo)
        {
            string projectName = Path.GetFileNameWithoutExtension(csprojFileInfo.Name);
            CmdHelper cmdHelper = new();
            string[] cmds = GetPackageCommand(csprojFileInfo);
            ConsoleHelper.WriteLine($"正在打包{projectName}...");
            cmdHelper.OutputDataReceived += CmdHelper_OutputDataReceived;
            cmdHelper.ErrorDataReceived += CmdHelper_ErrorDataReceived;
            await cmdHelper.RunCmdCommandsAsync(cmds);
            ConsoleHelper.WriteLine($"{projectName}打包完毕");
        }
        /// <summary>
        /// 获得发布命令
        /// </summary>
        /// <param name="csprojFileInfo"></param>
        /// <returns></returns>
        protected virtual string[] GetPackageCommand(FileInfo csprojFileInfo)
            => [$"msbuild {csprojFileInfo.FullName} /t:pack /p:Configuration=Release /p:PackageOutputPath={NugetServerHelper.NugetDirectoryInfo}"];
        #endregion
        #region 发布
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="rootDirectoryInfo"></param>
        protected virtual async Task PublishAsync(DirectoryInfo rootDirectoryInfo)
        {
            FileInfo? csprojFileInfo = rootDirectoryInfo.GetFiles().FirstOrDefault(m => m.Extension == ".csproj");
            if (csprojFileInfo != null)
            {
                if (await IsPublishProjectAsync(csprojFileInfo))
                {
                    await PublishAsync(csprojFileInfo);
                }
            }
            else
            {
                IEnumerable<DirectoryInfo> subDirectoryInfos = rootDirectoryInfo.GetDirectories();
                foreach (DirectoryInfo directoryInfo in subDirectoryInfos)
                {
                    await PublishAsync(directoryInfo);
                }
            }
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="csprojFileInfo"></param>
        protected virtual async Task PublishAsync(FileInfo csprojFileInfo)
        {
            string projectName = Path.GetFileNameWithoutExtension(csprojFileInfo.Name);
            CmdHelper cmdHelper = new();
            string[] cmds = GetPublishCommand(csprojFileInfo);
            ConsoleHelper.WriteLine($"正在发布{projectName}代码...");
            cmdHelper.OutputDataReceived += CmdHelper_OutputDataReceived;
            cmdHelper.ErrorDataReceived += CmdHelper_ErrorDataReceived;
            await cmdHelper.RunCmdCommandsAsync(cmds);
            ConsoleHelper.WriteLine($"{projectName}代码发布完毕");
        }
        /// <summary>
        /// 获得发布命令
        /// </summary>
        /// <param name="csprojFileInfo"></param>
        /// <returns></returns>
        protected virtual string[] GetPublishCommand(FileInfo csprojFileInfo)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"msbuild {csprojFileInfo.FullName} /p:Configuration=Release");
            string? publishPath = null;
            if (PublishHelper.PublishDirectoryInfo is not null)
            {
                string projectName = Path.GetFileNameWithoutExtension(csprojFileInfo.Name);
                DirectoryInfo publishDirectoryInfo = Path.Combine(PublishHelper.PublishDirectoryInfo.FullName, projectName).GetNewDirectoryInfo();
                publishPath = publishDirectoryInfo.FullName;
            }
            if (!string.IsNullOrWhiteSpace(publishPath))
            {
                stringBuilder.Append($" /p:OutputPath={publishPath}");
            }
            string cmd = stringBuilder.ToString();
            return [cmd];
            //$"msbuild {csprojFileInfo.FullName} /p:Configuration=Release /p:OutputPath={publishPath}"





            //            StringBuilder stringBuilder = new();
            //            stringBuilder.Append($"dotnet publish {csprojFileInfo.FullName}");
            //#if NET6_0_OR_GREATER
            //            string[] csprojFileContent = await File.ReadAllLinesAsync(csprojFileInfo.FullName);
            //#else
            //            string[] csprojFileContent = File.ReadAllLines(csprojFileInfo.FullName);
            //            await Task.CompletedTask;
            //#endif
            //            string[] targetFrameworks = [];
            //            foreach (string fileLine in csprojFileContent)
            //            {
            //                string content = fileLine.Trim();
            //                if (content.StartsWith("<TargetFramework>"))
            //                {
            //                    targetFrameworks = [content[17..^18]];
            //                    break;
            //                }
            //                else if (content.StartsWith("<TargetFrameworks>"))
            //                {
            //                    targetFrameworks = content[18..^19].Split(';');
            //                    break;
            //                }
            //            }
            //            string? publishPath = null;
            //            if (PublishHelper.PublishDirectoryInfo is not null)
            //            {
            //                string projectName = Path.GetFileNameWithoutExtension(csprojFileInfo.Name);
            //                DirectoryInfo publishDirectoryInfo = Path.Combine(PublishHelper.PublishDirectoryInfo.FullName, projectName).GetNewDirectoryInfo();
            //                publishPath = publishDirectoryInfo.FullName;
            //            }
            //            stringBuilder.Append(" -c Release");
            //            List<string> result = [];
            //            if (targetFrameworks.Length > 1)
            //            {
            //                foreach (string targetFramework in targetFrameworks)
            //                {
            //                    string item = stringBuilder.ToString();
            //                    if (!string.IsNullOrWhiteSpace(publishPath))
            //                    {
            //                        item += $" -o {publishPath}\\{targetFramework}";
            //                    }
            //                    item += $" -f {targetFramework}";
            //                    result.Add(item);
            //                }
            //            }
            //            else
            //            {
            //                if (!string.IsNullOrWhiteSpace(publishPath))
            //                {
            //                    stringBuilder.Append($" -o {publishPath}");
            //                }
            //                result.Add(stringBuilder.ToString());
            //            }
            //            return [.. result];
        }
        /// <summary>
        /// 是要发布的项目
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        protected virtual async Task<bool> IsPublishProjectAsync(FileInfo csprojFileInfo)
        {
            if (!csprojFileInfo.Exists) return false;
            string fileContent = await File.ReadAllTextAsync(csprojFileInfo.FullName);
            return fileContent.Contains("<IsPublish>true</IsPublish>");
        }
        #endregion
        protected void CmdHelper_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Data)) return;
            ConsoleHelper.WriteLine(e.Data, ConsoleColor.DarkRed);
        }

        protected void CmdHelper_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Data)) return;
            ConsoleColor consoleColor = ConsoleColor.DarkGreen;
            if (e.Data.Contains(" error "))
            {
                consoleColor = ConsoleColor.DarkRed;
            }
            else if (e.Data.Contains(" warning "))
            {
                consoleColor = ConsoleColor.DarkYellow;
            }
            ConsoleHelper.WriteLine(e.Data, consoleColor);
        }
    }
}
