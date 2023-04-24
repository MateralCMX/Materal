using System.Net;
using System.Text.RegularExpressions;

namespace MateralPublish.Helper
{
    public static class UploadNugetPackageHelper
    {
        public static DirectoryInfo? NugetDirectoryInfo
        {
            get => _nugetDirectoryInfo; set
            {
                _nugetDirectoryInfo = value;
                if(_nugetDirectoryInfo == null)
                {
                    _successNugetDirectoryInfo = null;
                }
                else
                {
                    _successNugetDirectoryInfo = new(Path.Combine(_nugetDirectoryInfo.FullName, "Success"));
                    if (!_successNugetDirectoryInfo.Exists)
                    {
                        _successNugetDirectoryInfo.Create();
                        _successNugetDirectoryInfo.Refresh();
                    }
                }
            }
        }
        private static DirectoryInfo? _successNugetDirectoryInfo;
        private static DirectoryInfo? _nugetDirectoryInfo;
        private static readonly HttpClient _httpClient = new();
        /// <summary>
        /// 复原
        /// </summary>
        public static void Recover()
        {
            if (NugetDirectoryInfo == null || _successNugetDirectoryInfo == null) return;
            ConsoleHelper.WriteLine("正在复原Nuget文件...");
            if (_successNugetDirectoryInfo.Exists)
            {
                foreach (FileInfo fileInfo in _successNugetDirectoryInfo.GetFiles())
                {
                    ConsoleHelper.WriteLine($"移动{fileInfo.Name}");
                    fileInfo.MoveTo(Path.Combine(NugetDirectoryInfo.FullName, fileInfo.Name));
                }
                _successNugetDirectoryInfo.Delete(true);
            }
        }
        /// <summary>
        /// 上传Nuget包
        /// </summary>
        /// <returns></returns>
        public static async Task UploadNugetPackagesAsync()
        {
            if (NugetDirectoryInfo == null || _successNugetDirectoryInfo == null) return;
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
                string newFilePath = Path.Combine(_successNugetDirectoryInfo.FullName, nugetFileInfo.Name);
                nugetFileInfo.MoveTo(newFilePath, true);
            }
        }
        /// <summary>
        /// 上传Nuget包
        /// </summary>
        /// <returns></returns>
        private static async Task UploadNugetPackageAsync(FileInfo nugetFileInfo)
        {
            ConsoleHelper.WriteLine($"开始上传{nugetFileInfo.Name}到服务器...");
            const string baseUrl = "ftp://82.156.11.176:21/NugetPackages/";
            string url = baseUrl + nugetFileInfo.Name;
#pragma warning disable SYSLIB0014 // 类型或成员已过时
            FtpWebRequest reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(url));
#pragma warning restore SYSLIB0014 // 类型或成员已过时
            reqFTP.UseBinary = true;
            reqFTP.Credentials = new NetworkCredential("GDB_FTP", "GDB2022");
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
            string url = $"https://nuget.gudianbustu.com/nuget/Packages(Id='{id}',Version='{version}')";
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
