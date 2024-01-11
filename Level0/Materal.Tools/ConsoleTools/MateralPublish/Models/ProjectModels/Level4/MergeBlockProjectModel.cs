using Materal.Tools.Helper;
using MateralPublish.Extensions;
using System.Text;

namespace MateralPublish.Models.ProjectModels.Level4
{
    public class MergeBlockProjectModel : BaseProjectModel
    {
        private readonly DirectoryInfo _vsixDirectoryInfo;
        private const string MateralMergeBlockVSIXName = "MateralMergeBlockVSIX";
        public MergeBlockProjectModel(string solutionPath) : base(solutionPath, 4, "Materal.MergeBlock")
        {
            _vsixDirectoryInfo = new(Path.Combine(ProjectDirectoryInfo.FullName, "Src", MateralMergeBlockVSIXName));
        }
        /// <summary>
        /// 更新版本号
        /// </summary>
        /// <param name="version"></param>
        /// <param name="csprojFileInfo"></param>
        /// <returns></returns>
        protected override async Task UpdateVersionAsync(string version, FileInfo csprojFileInfo)
        {
            if (csprojFileInfo.Name != "MateralMergeBlockVSIX.csproj")
            {
                await base.UpdateVersionAsync(version, csprojFileInfo);
            }
            else
            {
                FileInfo? sourceFile = _vsixDirectoryInfo.GetFiles().FirstOrDefault(m => m.Name == "source.extension.vsixmanifest");
                if (sourceFile == null) return;
                string projectName = Path.GetFileNameWithoutExtension(csprojFileInfo.Name);
                ConsoleHelper.WriteLine($"正在更新{projectName}版本号...");
                string[] codeContent = await File.ReadAllLinesAsync(sourceFile.FullName);
                const string versionStartCode = "<Identity Id=\"MateralMergeBlockVSIX.b1552ba8-4727-4fcf-8258-ef4040604f52\" Version=\"";
                const string versionEndCode = "\" Language=\"zh-Hans\" Publisher=\"Materal\" />";
                string vsixVersion = $"{version}.1";
                for (int i = 0; i < codeContent.Length; i++)
                {
                    string code = codeContent[i].Trim();
                    if (code.StartsWith(versionStartCode))
                    {
                        bool isNewVersion = !code[versionStartCode.Length..].StartsWith(version);
                        if (isNewVersion)
                        {
                            codeContent[i] = $"{versionStartCode}{vsixVersion}{versionEndCode}";
                        }
                    }
                }
                await File.WriteAllLinesAsync(sourceFile.FullName, codeContent, Encoding.UTF8);
                ConsoleHelper.WriteLine($"{projectName}版本号已更新到{vsixVersion}");
            }
        }
        public override async Task PublishAsync(string version)
        {
            await base.PublishAsync(version);
            await PublishVSIXAsync();
        }
        ///// <summary>
        ///// 发布
        ///// </summary>
        ///// <param name="csprojFileInfo"></param>
        ///// <returns></returns>
        //protected override async Task PublishAsync(FileInfo csprojFileInfo)
        //{
            //await base.PublishAsync(csprojFileInfo);
            //if (csprojFileInfo.Name != "MateralBasePlugBuild.csproj") return;
            //await CopyToolsFileAsync(csprojFileInfo);
            //DeletePlugDirectory();
            //await PublishVSIXAsync();
        //}
        ///// <summary>
        ///// 复制工具文件
        ///// </summary>
        ///// <param name="csprojFileInfo"></param>
        ///// <returns></returns>
        //private async Task CopyToolsFileAsync(FileInfo csprojFileInfo)
        //{
        //    if (csprojFileInfo.Name != "MateralBasePlugBuild.csproj" || PublishHelper.PublishDirectoryInfo is null) return;
        //    string[] csprojFileContent = await File.ReadAllLinesAsync(csprojFileInfo.FullName);
        //    string[] targetFrameworks = [];
        //    foreach (string fileLine in csprojFileContent)
        //    {
        //        string content = fileLine.Trim();
        //        if (content.StartsWith("<TargetFramework>"))
        //        {
        //            targetFrameworks = [content[17..^18]];
        //            break;
        //        }
        //        else if (content.StartsWith("<TargetFrameworks>"))
        //        {
        //            targetFrameworks = content[18..^19].Split(";");
        //            break;
        //        }
        //    }
        //    if (targetFrameworks.Length == 0) return;
        //    string targetFramework = targetFrameworks.OrderByDescending(m => m).First();
        //    DirectoryInfo plugBuildDirectoryInfo = new(Path.Combine(PublishHelper.PublishDirectoryInfo.FullName, "MateralBasePlugBuild", targetFramework));
        //    ConsoleHelper.WriteLine("正在复制MateralBasePlugBuild工具...");
        //    CopyToolsFile(plugBuildDirectoryInfo);
        //    ConsoleHelper.WriteLine("MateralBasePlugBuild工具复制成功");
        //}
        ///// <summary>
        ///// 删除插件目录
        ///// </summary>
        //private static void DeletePlugDirectory()
        //{
        //    if (PublishHelper.PublishDirectoryInfo is null) return;
        //    DirectoryInfo plugBuildDirectoryInfo = new(Path.Combine(PublishHelper.PublishDirectoryInfo.FullName, "MateralBasePlugBuild"));
        //    if (plugBuildDirectoryInfo.Exists)
        //    {
        //        ConsoleHelper.WriteLine("正在删除MateralBasePlugBuild目录...");
        //        plugBuildDirectoryInfo.Delete(true);
        //        ConsoleHelper.WriteLine("MateralBasePlugBuild目录已删除");
        //    }
        //}
        ///// <summary>
        ///// 复制工具文件
        ///// </summary>
        ///// <param name="plugBuildDirectoryInfo"></param>
        ///// <param name="prefix"></param>
        //private void CopyToolsFile(DirectoryInfo plugBuildDirectoryInfo, string prefix = "")
        //{
        //    string[] blackList =
        //    [
        //        "ModelData.json",
        //        "Materal.MergeBlock.CodeGenerator.pdb",
        //        "MateralBasePlugBuild.pdb",
        //        "MateralBasePlugBuild.exe"
        //    ];
        //    DirectoryInfo toolsDirectoryInfo = new(Path.Combine(_vsixDirectoryInfo.FullName, "Tools", prefix));
        //    if (!toolsDirectoryInfo.Exists)
        //    {
        //        toolsDirectoryInfo.Create();
        //        toolsDirectoryInfo.Refresh();
        //    }
        //    foreach (FileInfo fileInfo in plugBuildDirectoryInfo.GetFiles())
        //    {
        //        if (blackList.Contains(fileInfo.Name)) continue;
        //        string newFilePath = Path.Combine(toolsDirectoryInfo.FullName, fileInfo.Name);
        //        fileInfo.CopyTo(newFilePath, true);
        //    }
        //    foreach (DirectoryInfo directoryInfo in plugBuildDirectoryInfo.GetDirectories())
        //    {
        //        string nextPrefix = Path.Combine(prefix, directoryInfo.Name);
        //        CopyToolsFile(directoryInfo, nextPrefix);
        //    }
        //}
        /// <summary>
        /// 发布VSIX
        /// </summary>
        private async Task PublishVSIXAsync()
        {
            if (PublishHelper.PublishDirectoryInfo is null) return;
            FileInfo vsixCsprojFileInfo = new(Path.Combine(_vsixDirectoryInfo.FullName, $"{MateralMergeBlockVSIXName}.csproj"));
            string projectName = Path.GetFileNameWithoutExtension(vsixCsprojFileInfo.Name);
            CmdHelper cmdHelper = new();
            DirectoryInfo publishDirectoryInfo = Path.Combine(PublishHelper.PublishDirectoryInfo.FullName, projectName).GetNewDirectoryInfo();
            string[] cmds = [$"msbuild {vsixCsprojFileInfo.FullName} /p:Configuration=Release /p:OutputPath={publishDirectoryInfo.FullName}"];
            ConsoleHelper.WriteLine($"正在发布{projectName}插件...");
            cmdHelper.OutputDataReceived += CmdHelper_OutputDataReceived;
            cmdHelper.ErrorDataReceived += CmdHelper_ErrorDataReceived;
            await cmdHelper.RunCmdCommandsAsync(cmds);
            FileInfo vsixFileInfo = new(Path.Combine(publishDirectoryInfo.FullName, $"{MateralMergeBlockVSIXName}.vsix"));
            if (vsixFileInfo.Exists)
            {
                vsixFileInfo.MoveTo(Path.Combine(PublishHelper.PublishDirectoryInfo.FullName, vsixFileInfo.Name));
            }
            publishDirectoryInfo.Delete(true);
            ConsoleHelper.WriteLine($"{projectName}插件发布完毕");
        }
    }
}
