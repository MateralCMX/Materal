using System.Text;
using System.Xml;

namespace Materal.Tools.Core.MateralPublish.MateralProjects
{
    /// <summary>
    /// 基础Materal项目
    /// </summary>
    public abstract class BaseMateralProject : IMateralProject
    {
        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; }
        /// <summary>
        /// 位序
        /// </summary>
        public int Index { get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 根目录信息
        /// </summary>
        public DirectoryInfo RootDirectoryInfo { get; }
        /// <summary>
        /// Nuget目录信息
        /// </summary>
        public DirectoryInfo NugetDirectoryInfo { get; }
        /// <summary>
        /// 发布目录信息
        /// </summary>
        public DirectoryInfo PublishDirectoryInfo { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public BaseMateralProject(DirectoryInfo projectDirectoryInfo, int level, int index, string name)
        {
            Level = level;
            Index = index;
            Name = name;
            string rootPath = Path.Combine(projectDirectoryInfo.FullName, $"Level{level}", name);
            RootDirectoryInfo = new(rootPath);
            NugetDirectoryInfo = Path.Combine(projectDirectoryInfo.FullName, "Nupkgs").GetNewDirectoryInfo();
            PublishDirectoryInfo = Path.Combine(projectDirectoryInfo.FullName, "Publish").GetNewDirectoryInfo();
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="version"></param>
        /// <param name="onMessage"></param>
        /// <returns></returns>
        public virtual async Task PublishAsync(string version, Action<MessageLevel, string>? onMessage = null)
        {
            await UpdateVersionAsync(version, RootDirectoryInfo, onMessage);
            await PackageAsync(RootDirectoryInfo, onMessage);
        }
        #region 版本更新
        /// <summary>
        /// 更新版本
        /// </summary>
        /// <param name="version"></param>
        /// <param name="directoryInfo"></param>
        /// <param name="onMessage"></param>
        /// <returns></returns>
        protected virtual async Task UpdateVersionAsync(string version, DirectoryInfo directoryInfo, Action<MessageLevel, string>? onMessage = null)
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
        protected virtual async Task UpdateVsixVersionAsync(string version, FileInfo fileInfo, Action<MessageLevel, string>? onMessage = null)
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
        protected virtual async Task UpdateCsprojVersionAsync(string version, FileInfo fileInfo, Action<MessageLevel, string>? onMessage = null)
        {
            string projectName = Path.GetFileNameWithoutExtension(fileInfo.Name);
            XmlDocument xmlDocument = new();
            xmlDocument.Load(fileInfo.FullName);
            if (xmlDocument.FirstChild is null || xmlDocument.FirstChild.Name != "Project") throw new ToolsException($"{fileInfo.FullName}文件格式错误");
            foreach (XmlNode node in xmlDocument.FirstChild.ChildNodes)
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
        protected virtual void UpdateCsprojPropertyGroupVersion(string projectName, string version, XmlNode node, Action<MessageLevel, string>? onMessage = null)
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
        protected virtual void UpdateCsprojItemGroupVersion(string projectName, string version, XmlNode node, Action<MessageLevel, string>? onMessage = null)
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
        /// <param name="onMessage"></param>
        /// <returns></returns>
        protected virtual async Task PackageAsync(DirectoryInfo directoryInfo, Action<MessageLevel, string>? onMessage = null)
        {
            if (!directoryInfo.Exists) throw new ToolsException($"{directoryInfo.FullName}不存在");
            FileInfo? csprojFileInfo = directoryInfo.GetFiles().FirstOrDefault(m => m.Extension == ".csproj");
            if (csprojFileInfo != null)
            {
                if (!CanPackage(csprojFileInfo)) return;
                await PackageAsync(csprojFileInfo, onMessage);
            }
            else
            {
                IEnumerable<DirectoryInfo> subDirectoryInfos = directoryInfo.GetDirectories();
                foreach (DirectoryInfo subDirectoryInfo in subDirectoryInfos)
                {
                    await PackageAsync(subDirectoryInfo, onMessage);
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
            XmlNode? identityNode = xmlDocument.SelectSingleNode("//Project//PropertyGroup//IsPackable");
            if (identityNode is null) return false;
            if (!string.IsNullOrWhiteSpace(identityNode.Value) && identityNode.Value.Equals("true", StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }
        /// <summary>
        /// 打包
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="onMessage"></param>
        /// <returns></returns>
        protected virtual async Task PackageAsync(FileInfo fileInfo, Action<MessageLevel, string>? onMessage = null)
        {
        }
        #endregion
        #region 发布

        #endregion
    }
}
