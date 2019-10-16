using Materal.ConDep.Manager.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Materal.ConvertHelper;

namespace Materal.ConDep.Manager
{
    public class AppManagerImpl: IAppManager
    {
        private readonly AppCollection _appCollection;
        public AppManagerImpl()
        {
            string jsonFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}ApplicationData.json";
            _appCollection = new AppCollection(jsonFilePath);
        }
        public void StartAllApp()
        {
            _appCollection.StartAll();
        }
        public void RestartAllApp()
        {
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
            AppModel appModel = _appCollection[id];
            if (appModel == null) throw new InvalidOperationException("应用程序不存在");
            appModel.Start();
        }
        public void RestartApp(Guid id)
        {
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
    }
}
