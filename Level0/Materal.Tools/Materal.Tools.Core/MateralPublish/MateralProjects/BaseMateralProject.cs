using System.Text;
using System.Xml;

namespace Materal.Tools.Core.MateralPublish.MateralProjects
{
    /// <summary>
    /// 基础Materal项目
    /// </summary>
    /// <remarks>
    /// 构造方法
    /// </remarks>
    public abstract class BaseMateralProject(int level, int index, string name) : IMateralProject
    {
        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; } = level;
        /// <summary>
        /// 位序
        /// </summary>
        public int Index { get; } = index;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = name;
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="projectDirectoryInfo"></param>
        /// <param name="nugetDirectoryInfo"></param>
        /// <param name="publishDirectoryInfo"></param>
        /// <param name="version"></param>
        /// <param name="onMessage"></param>
        /// <returns></returns>
        public virtual async Task PublishAsync(DirectoryInfo projectDirectoryInfo, DirectoryInfo nugetDirectoryInfo, DirectoryInfo publishDirectoryInfo, string version, Action<MessageLevel, string?>? onMessage = null)
        {
            string rootPath = Path.Combine(projectDirectoryInfo.FullName, $"Level{Level}", Name);
            DirectoryInfo rootDirectoryInfo = new(rootPath);
            await UpdateVersionAsync(version, rootDirectoryInfo, onMessage);
            await PackageAsync(rootDirectoryInfo, nugetDirectoryInfo, onMessage);
            await PublishAsync(rootDirectoryInfo, publishDirectoryInfo, onMessage);
        }
        #region 版本更新
        /// <summary>
        /// 更新版本
        /// </summary>
        /// <param name="version"></param>
        /// <param name="directoryInfo"></param>
        /// <param name="onMessage"></param>
        /// <returns></returns>
        protected virtual async Task UpdateVersionAsync(string version, DirectoryInfo directoryInfo, Action<MessageLevel, string?>? onMessage = null)
        {
            if (!directoryInfo.Exists) throw new ToolsException($"{directoryInfo.FullName}不存在");
            FileInfo? csprojFileInfo = directoryInfo.GetFiles().FirstOrDefault(m => m.Extension == ".csproj");
            FileInfo? vsixmanifestFileInfo = directoryInfo.GetFiles().FirstOrDefault(m => m.Name == "source.extension.vsixmanifest");
            if (vsixmanifestFileInfo != null)
            {
                await UpdateVsixVersionAsync(version, vsixmanifestFileInfo, onMessage);
            }
            else if (csprojFileInfo != null)
            {
                await UpdateCsprojVersionAsync(version, csprojFileInfo, onMessage);
            }
            else
            {
                IEnumerable<DirectoryInfo> subDirectoryInfos = directoryInfo.GetDirectories();
                foreach (DirectoryInfo subDirectoryInfo in subDirectoryInfos)
                {
                    await UpdateVersionAsync(version, subDirectoryInfo, onMessage);
                }
            }
        }
        /// <summary>
        /// 更新版本
        /// </summary>
        /// <param name="version"></param>
        /// <param name="fileInfo"></param>
        /// <param name="onMessage"></param>
        /// <returns></returns>
        protected virtual async Task UpdateVsixVersionAsync(string version, FileInfo fileInfo, Action<MessageLevel, string?>? onMessage = null)
        {
            string projectName = Path.GetFileNameWithoutExtension(fileInfo.Name);
            XmlDocument xmlDocument = new();
            xmlDocument.Load(fileInfo.FullName);
            XmlNamespaceManager nsmgr = new(xmlDocument.NameTable);
            nsmgr.AddNamespace("vsx", "http://schemas.microsoft.com/developer/vsx-schema/2011");
            XmlNode identityNode = xmlDocument.SelectSingleNode("//vsx:Identity", nsmgr) ?? throw new ToolsException($"{fileInfo.FullName}文件格式错误");
            if (identityNode.Attributes is null || identityNode.Attributes.Count <= 0) return;
            XmlAttribute? versionAttribute = identityNode.Attributes["Version"];
            if (versionAttribute is null) return;
            if (!versionAttribute.Value.StartsWith(version))
            {
                string newVersion = $"{version}.1";
                onMessage?.Invoke(MessageLevel.Information, $"正在更新{projectName}版本到{newVersion}");
                versionAttribute.Value = newVersion;
            }
            string xmlContent = xmlDocument.GetFormatXmlContent();
            xmlContent = xmlContent.Trim();
            await File.WriteAllTextAsync(fileInfo.FullName, xmlContent, Encoding.UTF8);
        }
        /// <summary>
        /// 更新版本
        /// </summary>
        /// <param name="version"></param>
        /// <param name="fileInfo"></param>
        /// <param name="onMessage"></param>
        /// <returns></returns>
        protected virtual async Task UpdateCsprojVersionAsync(string version, FileInfo fileInfo, Action<MessageLevel, string?>? onMessage = null)
        {
            string projectName = Path.GetFileNameWithoutExtension(fileInfo.Name);
            XmlDocument xmlDocument = new();
            xmlDocument.Load(fileInfo.FullName);
            XmlNode? projectNode = xmlDocument.SelectSingleNode("//Project");
            if (projectNode is null) return;
            foreach (XmlNode node in projectNode.ChildNodes)
            {
                switch (node.Name)
                {
                    case "PropertyGroup":
                        UpdateCsprojPropertyGroupVersion(projectName, version, node, onMessage);
                        break;
                    case "ItemGroup":
                        UpdateCsprojItemGroupVersion(projectName, version, node, onMessage);
                        break;
                }
            }
            string xmlContent = xmlDocument.GetFormatXmlContent();
            xmlContent = xmlContent.StartsWith("<?xml") switch
            {
                true => xmlContent[(xmlContent.IndexOf("?>", StringComparison.Ordinal) + 2)..],
                false => xmlContent
            };
            xmlContent = xmlContent.Trim();
            await File.WriteAllTextAsync(fileInfo.FullName, xmlContent, Encoding.UTF8);
        }
        /// <summary>
        /// 更新Csproj PropertyGroup版本
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="version"></param>
        /// <param name="node"></param>
        /// <param name="onMessage"></param>
        protected virtual void UpdateCsprojPropertyGroupVersion(string projectName, string version, XmlNode node, Action<MessageLevel, string?>? onMessage = null)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode.Name != "Version") continue;
                if (childNode.InnerText == version) return;
                onMessage?.Invoke(MessageLevel.Information, $"正在更新{projectName}版本到{version}");
                childNode.InnerText = version;
                return;
            }
        }
        /// <summary>
        /// 更新Csproj PropertyGroup版本
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="version"></param>
        /// <param name="node"></param>
        /// <param name="onMessage"></param>
        protected virtual void UpdateCsprojItemGroupVersion(string projectName, string version, XmlNode node, Action<MessageLevel, string?>? onMessage = null)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode.Name != "PackageReference" || childNode.Attributes is null || childNode.Attributes.Count <= 0) continue;
                XmlAttribute? nameAttribute = childNode.Attributes["Include"];
                if (nameAttribute is null || (!nameAttribute.Value.StartsWith("Materal.") && !nameAttribute.Value.StartsWith("RC."))) continue;
                XmlAttribute? versionAttribute = childNode.Attributes["Version"];
                if (versionAttribute is null) continue;
                if (versionAttribute.Value == version) return;
                onMessage?.Invoke(MessageLevel.Information, $"正在更新{projectName}->{nameAttribute.Value}的版本到{version}");
                versionAttribute.Value = version;
                return;
            }
        }
        #endregion
        #region 打包
        /// <summary>
        /// 打包
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="nugetDirectoryInfo"></param>
        /// <param name="onMessage"></param>
        /// <returns></returns>
        protected virtual async Task PackageAsync(DirectoryInfo directoryInfo, DirectoryInfo nugetDirectoryInfo, Action<MessageLevel, string?>? onMessage = null)
        {
            if (!directoryInfo.Exists) throw new ToolsException($"{directoryInfo.FullName}不存在");
            FileInfo? csprojFileInfo = directoryInfo.GetFiles().FirstOrDefault(m => m.Extension == ".csproj");
            if (csprojFileInfo != null)
            {
                if (!CanPackage(csprojFileInfo)) return;
                await PackageAsync(csprojFileInfo, nugetDirectoryInfo, onMessage);
            }
            else
            {
                IEnumerable<DirectoryInfo> subDirectoryInfos = directoryInfo.GetDirectories();
                foreach (DirectoryInfo subDirectoryInfo in subDirectoryInfos)
                {
                    await PackageAsync(subDirectoryInfo, nugetDirectoryInfo, onMessage);
                }
            }
        }
        /// <summary>
        /// 打包
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        protected virtual bool CanPackage(FileInfo fileInfo)
        {
            if (!fileInfo.Exists) return false;
            XmlDocument xmlDocument = new();
            xmlDocument.Load(fileInfo.FullName);
            XmlNode? isPackableNode = xmlDocument.SelectSingleNode("//Project//PropertyGroup//IsPackable");
            if (isPackableNode is null) return false;
            if (!string.IsNullOrWhiteSpace(isPackableNode.InnerText) && isPackableNode.InnerText.Equals("true", StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }
        /// <summary>
        /// 打包
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="nugetDirectoryInfo"></param>
        /// <param name="onMessage"></param>
        /// <returns></returns>
        protected virtual async Task PackageAsync(FileInfo fileInfo, DirectoryInfo nugetDirectoryInfo, Action<MessageLevel, string?>? onMessage = null)
        {
            string projectName = Path.GetFileNameWithoutExtension(fileInfo.Name);
            CmdHelper cmdHelper = new();
            string[] cmds = GetPackageCommand(fileInfo, nugetDirectoryInfo);
            onMessage?.Invoke(MessageLevel.Information, $"正在打包{projectName}...");
            cmdHelper.OutputDataReceived += (sender, e) => onMessage?.Invoke(MessageLevel.Information, e.Data);
            cmdHelper.ErrorDataReceived += (sender, e) => onMessage?.Invoke(MessageLevel.Error, e.Data);
            await cmdHelper.RunCmdCommandsAsync(cmds);
            onMessage?.Invoke(MessageLevel.Information, $"{projectName}打包完毕");
        }
        /// <summary>
        /// 获得发布命令
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="nugetDirectoryInfo"></param>
        /// <returns></returns>
        protected virtual string[] GetPackageCommand(FileInfo fileInfo, DirectoryInfo nugetDirectoryInfo)
            => [$"msbuild {fileInfo.FullName} /t:restore",
                $"msbuild {fileInfo.FullName} /t:pack /t:Rebuild /p:Configuration=Release /p:PackageOutputPath={nugetDirectoryInfo}"];
        #endregion
        #region 发布
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="publishDirectoryInfo"></param>
        /// <param name="onMessage"></param>
        /// <returns></returns>
        protected virtual async Task PublishAsync(DirectoryInfo directoryInfo, DirectoryInfo publishDirectoryInfo, Action<MessageLevel, string?>? onMessage = null)
        {
            if (!directoryInfo.Exists) throw new ToolsException($"{directoryInfo.FullName}不存在");
            FileInfo? csprojFileInfo = directoryInfo.GetFiles().FirstOrDefault(m => m.Extension == ".csproj");
            if (csprojFileInfo != null)
            {
                if (CanPublish(csprojFileInfo))
                {
                    await PublishAsync(csprojFileInfo, publishDirectoryInfo, onMessage);
                }
                else if (CanPublishVSIX(csprojFileInfo))
                {
                    await PublishVSIXAsync(csprojFileInfo, publishDirectoryInfo, onMessage);
                }
            }
            else
            {
                IEnumerable<DirectoryInfo> subDirectoryInfos = directoryInfo.GetDirectories();
                foreach (DirectoryInfo subDirectoryInfo in subDirectoryInfos)
                {
                    await PublishAsync(subDirectoryInfo, publishDirectoryInfo, onMessage);
                }
            }
        }
        /// <summary>
        /// 能否发布
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        protected virtual bool CanPublish(FileInfo fileInfo)
        {
            if (!fileInfo.Exists) return false;
            XmlDocument xmlDocument = new();
            xmlDocument.Load(fileInfo.FullName);
            XmlNode? isPublishNode = xmlDocument.SelectSingleNode("//Project//PropertyGroup//IsPublish");
            if (isPublishNode is null) return false;
            if (!string.IsNullOrWhiteSpace(isPublishNode.InnerText) && isPublishNode.InnerText.Equals("true", StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="publishDirectoryInfo"></param>
        /// <param name="onMessage"></param>
        /// <returns></returns>
        protected virtual async Task PublishAsync(FileInfo fileInfo, DirectoryInfo publishDirectoryInfo, Action<MessageLevel, string?>? onMessage = null)
        {
            string projectName = Path.GetFileNameWithoutExtension(fileInfo.Name);
            CmdHelper cmdHelper = new();
            string[] cmds = GetPublishCommand(fileInfo, publishDirectoryInfo);
            onMessage?.Invoke(MessageLevel.Information, $"正在发布{projectName}...");
            cmdHelper.OutputDataReceived += (sender, e) => OutputDataReceived(e.Data, onMessage);
            cmdHelper.ErrorDataReceived += (sender, e) => ErrorDataReceived(e.Data, onMessage);
            await cmdHelper.RunCmdCommandsAsync(cmds);
            onMessage?.Invoke(MessageLevel.Information, $"{projectName}发布完毕");
        }
        /// <summary>
        /// 获得发布命令
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="publishDirectoryInfo"></param>
        /// <returns></returns>
        protected virtual string[] GetPublishCommand(FileInfo fileInfo, DirectoryInfo publishDirectoryInfo)
        {
            List<string> cmds = [$"msbuild {fileInfo.FullName} /t:restore",];
            XmlDocument xmlDocument = new();
            xmlDocument.Load(fileInfo.FullName);
            XmlNode? targetFrameworksNode = xmlDocument.SelectSingleNode("//Project//PropertyGroup//TargetFrameworks");
            if (targetFrameworksNode is null)
            {
                cmds.Add(GetPublishCommand(fileInfo, publishDirectoryInfo, null));
            }
            else
            {
                string[] targets = targetFrameworksNode.InnerText.Split(';');
                foreach (string target in targets)
                {
                    cmds.Add(GetPublishCommand(fileInfo, publishDirectoryInfo, target));
                }
            }
            return [.. cmds];
        }
        /// <summary>
        /// 获得发布命令
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="publishDirectoryInfo"></param>
        /// <param name="targetFrameworks"></param>
        /// <returns></returns>
        protected virtual string GetPublishCommand(FileInfo fileInfo, DirectoryInfo publishDirectoryInfo, string? targetFrameworks)
        {
            string projectName = Path.GetFileNameWithoutExtension(fileInfo.Name);
            StringBuilder cmdBuilder = new();
            cmdBuilder.Append($"msbuild {fileInfo.FullName} /t:Rebuild /p:Configuration=Release");
            DirectoryInfo publishProjectDirectoryInfo = Path.Combine(publishDirectoryInfo.FullName, projectName).GetNewDirectoryInfo();
            if (!string.IsNullOrWhiteSpace(targetFrameworks))
            {
                publishProjectDirectoryInfo = Path.Combine(publishProjectDirectoryInfo.FullName, targetFrameworks).GetNewDirectoryInfo();
                cmdBuilder.Append($" /p:TargetFramework={targetFrameworks}");
            }
            string? publishPath = publishProjectDirectoryInfo.FullName;
            cmdBuilder.Append($" /p:OutputPath={publishPath}");
            string cmd = cmdBuilder.ToString();
            return cmd;
        }
        /// <summary>
        /// 能否发布VS插件
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        protected virtual bool CanPublishVSIX(FileInfo fileInfo)
        {
            if (!fileInfo.Exists || fileInfo.Directory is null || !fileInfo.Directory.GetFiles().Any(m => m.Name == "source.extension.vsixmanifest")) return false;
            XmlDocument xmlDocument = new();
            xmlDocument.Load(fileInfo.FullName);
            XmlNamespaceManager nsmgr = new(xmlDocument.NameTable);
            nsmgr.AddNamespace("vsx", "http://schemas.microsoft.com/developer/msbuild/2003");
            XmlNode? isPublishNode = xmlDocument.SelectSingleNode("//vsx:IsPublish", nsmgr);
            if (isPublishNode is null) return false;
            if (!string.IsNullOrWhiteSpace(isPublishNode.InnerText) && isPublishNode.InnerText.Equals("true", StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }
        /// <summary>
        /// 发布VS插件
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="publishDirectoryInfo"></param>
        /// <param name="onMessage"></param>
        /// <returns></returns>
        protected virtual async Task PublishVSIXAsync(FileInfo fileInfo, DirectoryInfo publishDirectoryInfo, Action<MessageLevel, string?>? onMessage = null)
        {
            string projectName = Path.GetFileNameWithoutExtension(fileInfo.Name);
            CmdHelper cmdHelper = new();
            DirectoryInfo publishProjectDirectoryInfo = Path.Combine(publishDirectoryInfo.FullName, projectName).GetNewDirectoryInfo();
            string[] cmds = GetPublishVSIXCommand(fileInfo, publishProjectDirectoryInfo);
            onMessage?.Invoke(MessageLevel.Information, $"正在发布VS插件{projectName}...");
            cmdHelper.OutputDataReceived += (sender, e) => OutputDataReceived(e.Data, onMessage);
            cmdHelper.ErrorDataReceived += (sender, e) => ErrorDataReceived(e.Data, onMessage);
            await cmdHelper.RunCmdCommandsAsync(cmds);
            FileInfo vsixFileInfo = new(Path.Combine(publishProjectDirectoryInfo.FullName, $"{projectName}.vsix"));
            if (!vsixFileInfo.Exists) throw new ToolsException($"{vsixFileInfo.FullName}不存在");
            vsixFileInfo.MoveTo(Path.Combine(publishDirectoryInfo.FullName, vsixFileInfo.Name));
            publishProjectDirectoryInfo.Delete(true);
            onMessage?.Invoke(MessageLevel.Information, $"VS插件{projectName}发布完毕");
        }
        /// <summary>
        /// 获得发布命令
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="publishDirectoryInfo"></param>
        /// <returns></returns>
        protected virtual string[] GetPublishVSIXCommand(FileInfo fileInfo, DirectoryInfo publishDirectoryInfo)
            => [$"msbuild {fileInfo.FullName} /t:restore",
                $"msbuild {fileInfo.FullName} /t:Rebuild /p:Configuration=Release /p:OutputPath={publishDirectoryInfo.FullName}"];
        #endregion
        /// <summary>
        /// 错误数据接收
        /// </summary>
        /// <param name="message"></param>
        /// <param name="onMessage"></param>
        protected virtual void ErrorDataReceived(string? message, Action<MessageLevel, string?>? onMessage = null)
            => onMessage?.Invoke(MessageLevel.Error, message);
        /// <summary>
        /// 输出数据接收
        /// </summary>
        /// <param name="message"></param>
        /// <param name="onMessage"></param>
        protected virtual void OutputDataReceived(string? message, Action<MessageLevel, string?>? onMessage = null)
        {
            MessageLevel messageLevel = MessageLevel.Information;
            if (!string.IsNullOrWhiteSpace(message))
            {
                if (message.Contains(" error ", StringComparison.OrdinalIgnoreCase))
                {
                    messageLevel = MessageLevel.Error;
                }
                else if (message.Contains(" warning ", StringComparison.OrdinalIgnoreCase))
                {
                    messageLevel = MessageLevel.Warning;
                }
            }
            onMessage?.Invoke(messageLevel, message);
        }
    }
}
