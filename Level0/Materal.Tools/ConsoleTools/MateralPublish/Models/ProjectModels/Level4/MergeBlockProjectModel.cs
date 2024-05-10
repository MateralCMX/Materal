using MateralPublish.Extensions;
using MateralPublish.Helper;
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
            if (csprojFileInfo.Name != $"{MateralMergeBlockVSIXName}.csproj")
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
                const string versionStartCode = $"<Identity Id=\"{MateralMergeBlockVSIXName}.b1552ba8-4727-4fcf-8258-ef4040604f52\" Version=\"";
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
            string[] cmds = [$"msbuild {vsixCsprojFileInfo.FullName} /p:Configuration=Release /p:OutputPath={publishDirectoryInfo.FullName} -r"];
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
