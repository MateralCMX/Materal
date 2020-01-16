using Materal.ConDep.Services;
using Materal.ConDep.Services.Models;
using Materal.ConvertHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Materal.ConDep.ServiceImpl
{
    public class AppServiceImpl : IAppService
    {
        private readonly AppCollection _appCollection;
        public AppServiceImpl()
        {
            string jsonFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}ApplicationData.json";
            _appCollection = new AppCollection(jsonFilePath);
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
        public List<string> GetConsoleList(Guid id)
        {
            return _appCollection.GetAppConsoleList(_appCollection[id]);
        }
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
    }
}
