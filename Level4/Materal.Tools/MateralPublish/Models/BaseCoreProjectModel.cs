﻿using Materal.Tools.Helper;
using System.Text;

namespace MateralPublish.Models
{
    public class BaseCoreProjectModel : BaseProjectModel
    {
        private readonly DirectoryInfo vsixDirectoryInfo;
        public BaseCoreProjectModel(string directoryPath) : base(directoryPath)
        {
            vsixDirectoryInfo = new(Path.Combine(ProjectDirectoryInfo.FullName, "Src", "MateralBaseCoreVSIX"));
        }
        protected override bool IsPublishProject(string name)
        {
            string[] whiteList = new[]
            {
                "Materal.BaseCore.Test",
                "MateralBasePlugBuild"
            };
            return whiteList.Contains(name);
        }
        protected override async Task UpdateVersionAsync(string version, FileInfo csprojFileInfo)
        {
            if(csprojFileInfo.Name != "MateralBaseCoreVSIX.csproj")
            {
                await base.UpdateVersionAsync(version, csprojFileInfo);
            }
            else
            {
                FileInfo? sourceFile = vsixDirectoryInfo.GetFiles().FirstOrDefault(m => m.Name == "source.extension.vsixmanifest");
                if(sourceFile == null) return;
                string projectName = Path.GetFileNameWithoutExtension(csprojFileInfo.Name);
                ConsoleHelper.WriteLine($"正在更新{projectName}版本号...");
                string[] codeContent = await File.ReadAllLinesAsync(sourceFile.FullName);
                const string versionStartCode = "<Identity Id=\"MateralBaseCoreVSIX.42d6481d-f3e4-4d15-afe3-d0c3d6f5bcba\" Version=\"";
                const string versionEndCode = "\" Language=\"zh-Hans\" Publisher=\"Materal\" />";
                string vsixVersion = $"{version}.1";
                for (int i = 0; i < codeContent.Length; i++)
                {
                    string code = codeContent[i].Trim();
                    if (code.StartsWith(versionStartCode))
                    {
                        bool isNewVersion = !code.Substring(versionStartCode.Length).StartsWith(version);
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
        protected override async Task<DirectoryInfo?> PublishAsync(DirectoryInfo publishDirectoryInfo, FileInfo csprojFileInfo)
        {
            if(csprojFileInfo.Name == "MateralBasePlugBuild.csproj")
            {
                DirectoryInfo? directoryInfo = await base.PublishAsync(publishDirectoryInfo, csprojFileInfo);
                if(directoryInfo == null) return null;
                CopyToolsFile(directoryInfo);
                return directoryInfo;
            }
            else
            {
                return await base.PublishAsync(publishDirectoryInfo, csprojFileInfo);
            }
        }
        protected override string[] GetPublishCommand(DirectoryInfo publishDirectoryInfo, FileInfo csprojFileInfo)
        {
            if (csprojFileInfo.Name == "MateralBasePlugBuild.csproj")
            {
                string cmd = $"dotnet publish {csprojFileInfo.FullName} -o {publishDirectoryInfo.FullName} -c Release";
                return new[] { cmd };
            }
            else
            {
                return base.GetPublishCommand(publishDirectoryInfo, csprojFileInfo);
            }
        }
        private void CopyToolsFile(DirectoryInfo plugBuildDirectoryInfo, string prefix = "")
        {
            string[] blackList = new[]
            {
                "ModelData.json",
                "Materal.BaseCore.CodeGenerator.pdb",
                "MateralBasePlugBuild.pdb",
                "MateralBasePlugBuild.exe"
            };
            DirectoryInfo toolsDirectoryInfo = new(Path.Combine(vsixDirectoryInfo.FullName, "Tools", prefix));
            foreach (FileInfo fileInfo in plugBuildDirectoryInfo.GetFiles())
            {
                if (blackList.Contains(fileInfo.Name)) continue;
                string newFilePath = Path.Combine(toolsDirectoryInfo.FullName, fileInfo.Name);
                fileInfo.CopyTo(newFilePath, true);
            }
            foreach (DirectoryInfo directoryInfo in plugBuildDirectoryInfo.GetDirectories())
            {
                string nextPrefix = Path.Combine(prefix, directoryInfo.Name);
                CopyToolsFile(directoryInfo, nextPrefix);
            }
        }
    }
}
