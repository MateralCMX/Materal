using MateralPublish.Extensions;
using MateralPublish.Helper;
using System.Net;
using System.Text;

namespace MateralPublish.Models
{
    public class ProjectModel
    {
        private static readonly HttpClient _httpClient = new();
        /// <summary>
        /// 项目文件夹信息
        /// </summary>
        public DirectoryInfo ProjectDirectoryInfo { get; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name => ProjectDirectoryInfo.Name;
        public ProjectModel(string directoryPath)
        {
            ProjectDirectoryInfo = new DirectoryInfo(directoryPath);
            if (!ProjectDirectoryInfo.Exists) throw new Exception("项目不存在");
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="publishDirectoryInfo"></param>
        /// <param name="version"></param>
        public virtual async Task PublishAsync(DirectoryInfo publishDirectoryInfo, DirectoryInfo nugetDirectoryInfo, string version)
        {
            await UpdateVersionAsync(version, ProjectDirectoryInfo);
            await PublishAsync(publishDirectoryInfo, nugetDirectoryInfo, version, ProjectDirectoryInfo);
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
                IEnumerable<DirectoryInfo> subDirectoryInfos = rootDirectoryInfo.GetDirectories().Where(m => IsPublishProject(m.Name));
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
        /// <param name="version"></param>
        /// <param name="rootDirectoryInfo"></param>
        protected virtual async Task PublishAsync(DirectoryInfo publishDirectoryInfo, DirectoryInfo nugetDirectoryInfo, string version, DirectoryInfo rootDirectoryInfo)
        {
            FileInfo? csprojFileInfo = rootDirectoryInfo.GetFiles().FirstOrDefault(m => m.Extension == ".csproj");
            if (csprojFileInfo != null)
            {
                await PublishAsync(publishDirectoryInfo, nugetDirectoryInfo, version, csprojFileInfo);
            }
            else
            {
                IEnumerable<DirectoryInfo> subDirectoryInfos = rootDirectoryInfo.GetDirectories().Where(m => IsPublishProject(m.Name));
                foreach (DirectoryInfo directoryInfo in subDirectoryInfos)
                {
                    await PublishAsync(publishDirectoryInfo, nugetDirectoryInfo, version, directoryInfo);
                }
            }
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="publishDirectoryInfo"></param>
        /// <param name="version"></param>
        /// <param name="csprojFileInfo"></param>
        protected virtual async Task<DirectoryInfo?> PublishAsync(DirectoryInfo publishDirectoryInfo, DirectoryInfo nugetDirectoryInfo, string version, FileInfo csprojFileInfo)
        {
            string projectName = Path.GetFileNameWithoutExtension(csprojFileInfo.Name);
            CmdHelper cmdHelper = new();
            DirectoryInfo truePublishDirectoryInfo = Path.Combine(publishDirectoryInfo.FullName, projectName).GetNewDirectoryInfo();
            string cmd = $"dotnet publish {csprojFileInfo.FullName} -o {truePublishDirectoryInfo.FullName} -c Release";
            ConsoleHelper.WriteLine($"正在发布{projectName}代码...");
            await cmdHelper.RunCmdCommandsAsync(cmd);
            ConsoleHelper.WriteLine($"{projectName}代码发布完毕");
            FileInfo? nugetFileInfo = nugetDirectoryInfo.GetFiles().FirstOrDefault(m => m.Name == $"{projectName}.{version}.nupkg");
            if (nugetFileInfo == null) return truePublishDirectoryInfo;
            await UploadNugetPackages(nugetFileInfo);
            while (!await CheckUploadSuccess(projectName, version))
            {
                ConsoleHelper.WriteLine("等待Nuget服务器清理缓存...");
                await Task.Delay(1000);
            }
            ConsoleHelper.WriteLine($"Nuget服务器已检测到包{nugetFileInfo.Name}");
            return truePublishDirectoryInfo;
        }
        /// <summary>
        /// 是要发布的项目
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected virtual bool IsPublishProject(string name)
        {
            return name != ".vs" && name != "bin" && name != "obj" && !name.EndsWith("Test") && !name.EndsWith("Demo");
        }
        /// <summary>
        /// 上传Nuget包
        /// </summary>
        /// <returns></returns>
        protected virtual async Task UploadNugetPackages(FileInfo nugetFileInfo)
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
        protected virtual async Task<bool> CheckUploadSuccess(string fileName, string version)
        {
            string url = $"https://nuget.gudianbustu.com/nuget/Packages(Id='{fileName}',Version='{version}')";
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
