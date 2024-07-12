using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;
using System.Xml;

namespace Materal.Tools.Core.MateralVersion
{
    /// <summary>
    /// Materal版本服务
    /// </summary>
    public class MateralVersionService(ILogger<MateralVersionService>? logger = null) : IMateralVersionService
    {
        private readonly string[] _defaultNugetPaths = {
            "https://nuget.gudianbustu.com/nuget/",
            @"E:\Project\Materal\Materal\Nupkgs"
        };
        private const string _materalID = "Materal.Abstractions";
        private readonly HttpClient _httpClient = new();
        /// <summary>
        /// 更新版本
        /// </summary>
        /// <param name="projectPath"></param>
        /// <param name="nugetPaths"></param>
        /// <returns></returns>
        public async Task UpdateVersionAsync(string projectPath, string[]? nugetPaths = null)
        {
            logger?.LogInformation("正在获取最新的Materal版本...");
            string version = await GetNowVersionAsync(nugetPaths);
            logger?.LogInformation($"最新的Materal版本为{version}");
            await UpdateVersionAsync(projectPath, version);
        }
        /// <summary>
        /// 更新版本
        /// </summary>
        /// <param name="projectPath"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public async Task UpdateVersionAsync(string projectPath, string version)
        {
            logger?.LogInformation($"开始更新Materal版本到{version}...");
            DirectoryInfo directoryInfo = new(projectPath);
            await UpdateVersionAsync(version, directoryInfo);
            logger?.LogInformation("Materal版本更新完毕");
        }
        /// <summary>
        /// 更新版本
        /// </summary>
        /// <param name="version"></param>
        /// <param name="directoryInfo"></param>
        /// <returns></returns>
        protected virtual async Task UpdateVersionAsync(string version, DirectoryInfo directoryInfo)
        {
            if (!directoryInfo.Exists) throw new ToolsException($"{directoryInfo.FullName}不存在");
            FileInfo? csprojFileInfo = directoryInfo.GetFiles().FirstOrDefault(m => m.Extension == ".csproj");
            if (csprojFileInfo != null)
            {
                await UpdateCsprojVersionAsync(version, csprojFileInfo);
            }
            else
            {
                IEnumerable<DirectoryInfo> subDirectoryInfos = directoryInfo.GetDirectories();
                foreach (DirectoryInfo subDirectoryInfo in subDirectoryInfos)
                {
                    await UpdateVersionAsync(version, subDirectoryInfo);
                }
            }
        }
        /// <summary>
        /// 更新版本
        /// </summary>
        /// <param name="version"></param>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        protected virtual async Task UpdateCsprojVersionAsync(string version, FileInfo fileInfo)
        {
            string projectName = Path.GetFileNameWithoutExtension(fileInfo.Name);
            XmlDocument xmlDocument = new();
            xmlDocument.Load(fileInfo.FullName);
            XmlNode? projectNode = xmlDocument.SelectSingleNode("//Project");
            if (projectNode is null) return;
            foreach (XmlNode node in projectNode.ChildNodes)
            {
                if (node.Name != "ItemGroup") continue;
                UpdateCsprojItemGroupVersion(projectName, version, node);
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
        protected virtual void UpdateCsprojItemGroupVersion(string projectName, string version, XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode.Name != "PackageReference" || childNode.Attributes is null || childNode.Attributes.Count <= 0) continue;
                XmlAttribute? nameAttribute = childNode.Attributes["Include"];
                if (nameAttribute is null || (!nameAttribute.Value.StartsWith("Materal.") && !nameAttribute.Value.StartsWith("RC."))) continue;
                XmlAttribute? versionAttribute = childNode.Attributes["Version"];
                if (versionAttribute is null) continue;
                if (versionAttribute.Value == version) continue;
                logger?.LogInformation($"正在更新{projectName}->{nameAttribute.Value}的版本到{version}");
                versionAttribute.Value = version;
            }
        }
        /// <summary>
        /// 获得当前版本
        /// </summary>
        /// <param name="nugetPaths"></param>
        /// <returns></returns>
        /// <exception cref="ToolsException"></exception>
        public async Task<string> GetNowVersionAsync(string[]? nugetPaths = null)
        {
            string? version = null;
            nugetPaths ??= _defaultNugetPaths;
            foreach (string nugetPath in nugetPaths)
            {
                try
                {
                    string tempVersion = await GetNowVersionAsync(nugetPath);
                    if (string.IsNullOrWhiteSpace(version))
                    {
                        version = tempVersion;
                    }
                    else
                    {
                        version = GetMaxVersion(version, tempVersion);
                    }
                }
                catch
                {
                }
            }
            if (string.IsNullOrWhiteSpace(version)) throw new ToolsException("未找到Nuget包");
            return version;
        }
        /// <summary>
        /// 获得当前版本
        /// </summary>
        /// <param name="nugetPath"></param>
        /// <returns></returns>
        /// <exception cref="ToolsException"></exception>
        private async Task<string> GetNowVersionAsync(string nugetPath)
        {
            if (nugetPath.StartsWith("http"))
            {
                return await GetNowVersionByServerAsync(nugetPath);
            }
            else
            {
                return GetNowVersionByLocal(nugetPath);
            }
        }
        /// <summary>
        /// 通过本地获得当前版本
        /// </summary>
        /// <param name="nugegtPath"></param>
        /// <returns></returns>
        private static string GetNowVersionByLocal(string nugegtPath)
        {
            DirectoryInfo directoryInfo = new(nugegtPath);
            if (!directoryInfo.Exists) throw new ToolsException($"{nugegtPath}不存在");
            FileInfo[] files = directoryInfo.GetFiles($"{_materalID}.*.nupkg");
            string? version = null;
            foreach (FileInfo file in files)
            {
                string tempVersion = file.Name[(_materalID.Length + 1)..^6];
                if (version is null)
                {
                    version = tempVersion;
                }
                else
                {
                    version = GetMaxVersion(version, tempVersion);
                }
            }
            if (version is null) throw new ToolsException($"获取版本失败");
            return version;
        }
        /// <summary>
        /// 通过服务器获得当前版本
        /// </summary>
        /// <param name="nugetUrl"></param>
        /// <returns></returns>
        private async Task<string> GetNowVersionByServerAsync(string nugetUrl)
        {
            string url = $"{nugetUrl}Packages";
            HttpRequestMessage httpRequestMessage = new()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };
            HttpResponseMessage httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
            if (httpResponseMessage.StatusCode != HttpStatusCode.OK) throw new Exception($"[{httpResponseMessage.StatusCode}]访问Nuget服务器失败");
            using MemoryStream memoryStream = new();
            await httpResponseMessage.Content.CopyToAsync(memoryStream);
            byte[] buffer = memoryStream.ToArray();
            string httpResponseResult = Encoding.UTF8.GetString(buffer);
            XmlDocument xmlDocument = new();
            xmlDocument.LoadXml(httpResponseResult);
            XmlNamespaceManager nsmgr = new(xmlDocument.NameTable);
            nsmgr.AddNamespace("vsx", "http://www.w3.org/2005/Atom");
            XmlNodeList? entryNodes = xmlDocument.SelectNodes("//vsx:entry", nsmgr);
            if (entryNodes is null) throw new Exception($"未在Nuget服务器上找到包{_materalID}");
            string? version = null;
            foreach (XmlNode item in entryNodes)
            {
                string? value = item.FirstChild?.InnerText;
                if (string.IsNullOrWhiteSpace(value)) continue;
                value = value[$"{nugetUrl}Packages(Id='".Length..];
                string id = value[.._materalID.Length];
                if (id != _materalID) continue;
                version = GetMaxVersion(value[(id.Length + 11)..^2], version);
            }
            if (string.IsNullOrWhiteSpace(version)) throw new Exception($"未在Nuget服务器上找到包{_materalID}");
            return version;
        }
        /// <summary>
        /// 获得最大版本号
        /// </summary>
        /// <param name="version1"></param>
        /// <param name="version2"></param>
        /// <returns></returns>
        public static string GetMaxVersion(string version1, string? version2)
        {
            if (version2 is null || string.IsNullOrEmpty(version2)) return version1;
            int[] version1s = version1.Split('.').Select(m => Convert.ToInt32(m)).ToArray();
            int[] version2s = version2.Split('.').Select(m => Convert.ToInt32(m)).ToArray();
            int length = version1s.Length > version2s.Length ? version2s.Length : version1s.Length;
            for (int i = 0; i < length; i++)
            {
                if (version1s[i] > version2s[i]) return version1;
                if (version2s[i] > version1s[i]) return version2;
            }
            return version1s.Length >= version2s.Length ? version1 : version2;
        }
    }
}
