using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace MateralPublish.Helper
{
    public static class NugetServerHelper
    {
        private const string _ftpUrl = "ftp://82.156.11.176:21/NugetPackages/";
        private const string _ftpUser = "GDB_FTP";
        private const string _ftpPassword = "GDB2022";
        private const string _nugetUrl = "https://nuget.gudianbustu.com/nuget/";
        public static DirectoryInfo? NugetDirectoryInfo { get; set; }
        private static readonly HttpClient _httpClient = new();
        private const string _materalID = "Materal.Abstractions";
        /// <summary>
        /// 检查是否上传成功
        /// </summary>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static async Task<string> GetLastMateralVersionAsync()
        {
            string url = $"{_nugetUrl}Packages";
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
            XmlNodeList? allNodes = (xmlDocument.LastChild?.ChildNodes) ?? throw new Exception($"未在Nuget服务器上找到任何包");
            string? reslut = null;
            foreach (XmlNode item in allNodes)
            {
                if (item.Name != "entry") continue;
                string? value = item.FirstChild?.FirstChild?.Value;
                if (string.IsNullOrWhiteSpace(value)) continue;
                value = value[$"{_nugetUrl}Packages(Id='".Length..];
                string id = value[.._materalID.Length];
                if (id != _materalID) continue;
                reslut = GetMaxVersion(value[(id.Length + 11)..^2], reslut);
            }
            if (string.IsNullOrWhiteSpace(reslut)) throw new Exception($"未在Nuget服务器上找到包{_materalID}");
            return reslut;
        }
        /// <summary>
        /// 获得最大版本号
        /// </summary>
        /// <param name="version1"></param>
        /// <param name="version2"></param>
        /// <returns></returns>
        public static string GetMaxVersion(string version1, string? version2)
        {
            if (string.IsNullOrEmpty(version2)) return version1;
            int[] version1s = version1.Split(".").Select(m => Convert.ToInt32(m)).ToArray();
            int[] version2s = version2.Split(".").Select(m => Convert.ToInt32(m)).ToArray();
            int length = version1s.Length > version2s.Length ? version2s.Length : version1s.Length;
            for (int i = 0; i < length; i++)
            {
                if (version1s[i] > version2s[i]) return version1;
                if (version2s[i] > version1s[i]) return version2;
            }
            return version1s.Length >= version2s.Length ? version1 : version2;
        }
        /// <summary>
        /// 上传Nuget包
        /// </summary>
        /// <returns></returns>
        public static async Task UploadNugetPackagesAsync()
        {
            if (NugetDirectoryInfo == null) return;
            FileInfo[] fileInfos = NugetDirectoryInfo.GetFiles();
            foreach (FileInfo nugetFileInfo in fileInfos)
            {
                await UploadNugetPackageAsync(nugetFileInfo);
            }
            foreach (FileInfo nugetFileInfo in fileInfos)
            {
                while (!await CheckUploadSuccessAsync(nugetFileInfo.Name))
                {
                    ConsoleHelper.WriteLine("等待Nuget服务器清理缓存...");
                    await Task.Delay(1000);
                }
                ConsoleHelper.WriteLine($"Nuget服务器已检测到包{nugetFileInfo.Name}");
            }
        }
        /// <summary>
        /// 上传Nuget包
        /// </summary>
        /// <returns></returns>
        private static async Task UploadNugetPackageAsync(FileInfo nugetFileInfo)
        {
            ConsoleHelper.WriteLine($"开始上传{nugetFileInfo.Name}到服务器...");
            string url = _ftpUrl + nugetFileInfo.Name;
#pragma warning disable SYSLIB0014 // 类型或成员已过时
            FtpWebRequest reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(url));
#pragma warning restore SYSLIB0014 // 类型或成员已过时
            reqFTP.UseBinary = true;
            reqFTP.Credentials = new NetworkCredential(_ftpUser, _ftpPassword);
            reqFTP.KeepAlive = false;
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.ContentLength = nugetFileInfo.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            using FileStream fs = nugetFileInfo.OpenRead();
            using Stream strm = await reqFTP.GetRequestStreamAsync();
            contentLen = fs.Read(buff, 0, buffLength);
            while (contentLen != 0)
            {
                strm.Write(buff, 0, contentLen);
                contentLen = fs.Read(buff, 0, buffLength);
            }
            strm.Close();
            fs.Close();
            ConsoleHelper.WriteLine($"{nugetFileInfo.Name}上传成功...");
        }
        /// <summary>
        /// 检查是否上传成功
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        private static async Task<bool> CheckUploadSuccessAsync(string fileName)
        {
            string[] fileNames = fileName.Split('.');
            List<string> ids = new();
            List<string> versions = new();
            Regex regex = new("\\d+");
            foreach (string item in fileNames)
            {
                if (item == "nupkg") break;
                if (regex.IsMatch(item))
                {
                    versions.Add(item);
                }
                else
                {
                    ids.Add(item);
                }
            }
            string id = string.Join('.', ids);
            string version = string.Join('.', versions);
            return await CheckUploadSuccessAsync(id, version);
        }
        /// <summary>
        /// 检查是否上传成功
        /// </summary>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        private static async Task<bool> CheckUploadSuccessAsync(string id, string version)
        {
            string url = $"{_nugetUrl}Packages(Id='{id}',Version='{version}')";
            HttpRequestMessage httpRequestMessage = new()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };
            HttpResponseMessage httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
            bool result = httpResponseMessage.StatusCode == HttpStatusCode.OK;
            return result;
        }
    }
}
