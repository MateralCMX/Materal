using Materal.ConDep.Common;
using Materal.ConDep.ServiceImpl.Models;
using Materal.ConDep.Services;
using Materal.ConDep.Services.Models;
using Materal.ConvertHelper;
using Materal.DotNetty.Server.Core.Models;
using Materal.StringHelper;
using Materal.WindowsHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Materal.ConDep.ServiceImpl
{
    public class AppServiceImpl : IAppService
    {
        private readonly AppCollection _appCollection;
        private readonly WebAppCollection _webAppCollection;
        public AppServiceImpl()
        {
            string appJsonFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}ApplicationData.json";
            _appCollection = new AppCollection(appJsonFilePath);
            string webAppJsonFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}WebApplicationData.json";
            _webAppCollection = new WebAppCollection(webAppJsonFilePath);
        }

        public void StartAllApp()
        {
            if (!CanRun()) throw new InvalidOperationException("正在发布中,请稍后");
            _appCollection.StartAll();
        }
        public void RestartAllApp()
        {
            if (!CanRun()) throw new InvalidOperationException("正在发布中,请稍后");
            _appCollection.StopAll();
            _appCollection.StartAll();
        }
        public void StopAllApp()
        {
            _appCollection.StopAll();
        }
        public void StopAppByPaths(params string[] paths)
        {
            _appCollection.StopByPath(paths);
        }
        public async Task AddAppAsync(AppModel appModel)
        {
            appModel.ID = Guid.NewGuid();
            _appCollection.Add(appModel);
            await _appCollection.SaveDataAsync();
        }
        public async Task EditAppAsync(AppModel appModel)
        {
            AppModel appModelFromDB = _appCollection[appModel.ID];
            if (appModelFromDB == null) throw new InvalidOperationException("应用程序不存在");
            appModelFromDB.Stop();
            appModel.CopyProperties(appModelFromDB);
            await _appCollection.SaveDataAsync();
        }
        public async Task DeleteAppAsync(Guid id)
        {
            AppModel appModelFromDB = _appCollection[id];
            if (appModelFromDB == null) throw new InvalidOperationException("应用程序不存在");
            appModelFromDB.Stop();
            _appCollection.Remove(appModelFromDB);
            await _appCollection.SaveDataAsync();
        }
        public AppModel GetAppInfo(Guid id)
        {
            AppModel appModel = _appCollection[id];
            if (appModel == null) throw new InvalidOperationException("应用程序不存在");
            return appModel;
        }

        public async Task AddWebAppAsync(WebAppModel appModel)
        {
            appModel.ID = Guid.NewGuid();
            _webAppCollection.Add(appModel);
            await _webAppCollection.SaveDataAsync();
        }

        public async Task EditWebAppAsync(WebAppModel appModel)
        {
            WebAppModel appModelFromDB = _webAppCollection[appModel.ID];
            if (appModelFromDB == null) throw new InvalidOperationException("应用程序不存在");
            appModel.CopyProperties(appModelFromDB);
            await _webAppCollection.SaveDataAsync();
        }

        public async Task DeleteWebAppAsync(Guid id)
        {
            WebAppModel appModelFromDB = _webAppCollection[id];
            if (appModelFromDB == null) throw new InvalidOperationException("应用程序不存在");
            _webAppCollection.Remove(appModelFromDB);
            await _webAppCollection.SaveDataAsync();
        }

        public WebAppModel GetWebAppInfo(Guid id)
        {
            WebAppModel appModel = _webAppCollection[id];
            if (appModel == null) throw new InvalidOperationException("应用程序不存在");
            return appModel;
        }

        public void StartApp(Guid id)
        {
            if (!CanRun()) throw new InvalidOperationException("正在发布中,请稍后");
            AppModel appModel = _appCollection[id];
            if (appModel == null) throw new InvalidOperationException("应用程序不存在");
            appModel.Start();
        }
        public void RestartApp(Guid id)
        {
            if (!CanRun()) throw new InvalidOperationException("正在发布中,请稍后");
            AppModel appModel = _appCollection[id];
            if (appModel == null) throw new InvalidOperationException("应用程序不存在");
            appModel.Restart();
        }
        public void StopApp(Guid id)
        {
            AppModel appModel = _appCollection[id];
            if (appModel == null) throw new InvalidOperationException("应用程序不存在");
            appModel.Stop();
            _appCollection.ClearConsole(appModel);
        }
        public List<AppListModel> GetAppList()
        {
            return _appCollection.GetAppLists();
        }

        public List<WebAppModel> GetWebAppList()
        {
            return _webAppCollection.GetAppLists();
        }

        public List<string> GetConsoleList(Guid id)
        {
            return _appCollection.GetAppConsoleList(_appCollection[id]);
        }

        public async Task UpdateAppAsync(IUploadFileModel file)
        {
            string workingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory!, "Application");
            string saveDirectory = Path.Combine(workingDirectory, "Backup");
            if (!Directory.Exists(saveDirectory)) Directory.CreateDirectory(saveDirectory);
            var model = new UpdateAppFileModel
            {
                WorkingDirectory = workingDirectory,
                FilePath = Path.Combine(saveDirectory, file.Name)
            };
            file.SaveAs(model.FilePath);
            await UpdateAppFileAsync(model);
        }

        #region 私有方法
        /// <summary>
        /// 是否可以运行
        /// </summary>
        /// <returns></returns>
        private bool CanRun()
        {
            string path = $"{AppDomain.CurrentDomain.BaseDirectory}Application//Temp";
            if (!Directory.Exists(path)) return true;
            var directoryInfo = new DirectoryInfo(path);
            return directoryInfo.GetDirectories().Length == 0;
        }
        /// <summary>
        /// 更新APP文件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task UpdateAppFileAsync(UpdateAppFileModel model)
        {
            var appManager = ApplicationService.GetService<IAppService>();
            string tempPath = Path.Combine(model.WorkingDirectory, $"Temp/{StringManager.GetRandomStrByGuid()}");
            DirectoryInfo tempDirectoryInfo = null;
            if (!Directory.Exists(tempPath)) tempDirectoryInfo = Directory.CreateDirectory(tempPath);
            tempDirectoryInfo ??= new DirectoryInfo(tempPath);
            var cmdManager = new CmdManager();
            string winRarCmd = Path.Combine(ApplicationConfig.WinRarPath, "UnRAR.exe");
            await cmdManager.RunCmdCommandsAsync($"\"{winRarCmd}\" x -o+ -y {model.FilePath} {tempPath}");
            DirectoryInfo[] directoryInfos = tempDirectoryInfo.GetDirectories();
            string[] paths = directoryInfos.Select(m => m.Name).ToArray();
            appManager.StopAppByPaths(paths);
            foreach (DirectoryInfo directoryInfo in directoryInfos)
            {
                try
                {
                    string dirPath = Path.Combine(model.WorkingDirectory, directoryInfo.Name);
                    CopyDirectory(directoryInfo, dirPath);
                }
                finally
                {
                    directoryInfo.Delete(true);
                }
            }
            tempDirectoryInfo.Delete(true);
        }
        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="targetPath"></param>
        private void CopyDirectory(DirectoryInfo directoryInfo, string targetPath)
        {
            if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
            #region CopyFile
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            foreach (FileInfo fileInfo in fileInfos)
            {
                string filePath = Path.Combine(targetPath, fileInfo.Name);
                if (File.Exists(filePath)) File.Delete(filePath);
                File.Move(fileInfo.FullName, filePath);
            }
            #endregion
            #region CopyChildDirectory
            DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
            foreach (DirectoryInfo info in directoryInfos)
            {
                string dirPath = Path.Combine(targetPath, info.Name);
                if (Directory.Exists(dirPath)) Directory.Delete(dirPath, true);
                Directory.Move(info.FullName, dirPath);
            }
            #endregion
        }
        #endregion
    }
}
