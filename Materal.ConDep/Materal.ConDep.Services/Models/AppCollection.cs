using Materal.ConvertHelper;
using Materal.FileHelper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Materal.ConDep.Services.Models
{
    /// <summary>
    /// 应用程序集合
    /// </summary>
    public class AppCollection
    {
        private readonly List<AppModel> apps;
        private readonly ConcurrentDictionary<AppModel, List<string>> appConsole = new ConcurrentDictionary<AppModel, List<string>>();
        private readonly string _jsonFilePath;
        private const int consoleMaxCount = 1000;
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="jsonFilePath"></param>
        public AppCollection(string jsonFilePath)
        {
            _jsonFilePath = jsonFilePath;
            if (!File.Exists(jsonFilePath))
            {
                File.WriteAllText(jsonFilePath, "[]");
            }
            string jsonData = File.ReadAllText(_jsonFilePath);
            apps = jsonData.JsonToObject<List<AppModel>>();
            foreach (AppModel app in apps)
            {
                app.ErrorDataReceived += App_DataReceived;
                app.OutputDataReceived += App_DataReceived;
            }
            KillOldProgram();
        }
        /// <summary>
        /// 杀死程序
        /// </summary>
        private void KillOldProgram()
        {
            Process[] processes = Process.GetProcessesByName("dotnet");
            Process currentProcess = Process.GetCurrentProcess();
            foreach (Process process in processes)
            {
                if (currentProcess.Id == process.Id) return;
                var isKill = false;
                foreach (ProcessModule processModule in process.Modules)
                {
                    foreach (AppModel app in apps)
                    {
                        if (processModule.ModuleName != app.MainModuleName) continue;
                        process.Kill();
                        isKill = true;
                        break;
                    }
                    if (isKill)
                    {
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 应用数据返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="app"></param>
        private void App_DataReceived(object sender, DataReceivedEventArgs e, AppModel app)
        {
            if (!appConsole.ContainsKey(app))
            {
                appConsole.TryAdd(app, new List<string>());
            }
            if (appConsole[app].Count > consoleMaxCount) appConsole[app] = appConsole[app].Skip(500).ToList();
            if (string.IsNullOrEmpty(e.Data)) return;
            appConsole[app].Add($"[{DateTime.Now:yyyy/MM/dd HH:mm:ss}]{e.Data}");
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public async Task SaveDataAsync()
        {
            string jsonData = apps.ToJson();
            await TextFileManager.WriteTextAsync(_jsonFilePath, jsonData);
        }
        /// <summary>
        /// 总数
        /// </summary>
        public int Count => apps.Count;
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="app"></param>
        public void Add(AppModel app)
        {
            app.ErrorDataReceived += App_DataReceived;
            app.OutputDataReceived += App_DataReceived;
            apps.Add(app);
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="app"></param>
        public void Remove(AppModel app)
        {
            app.ErrorDataReceived -= App_DataReceived;
            app.OutputDataReceived -= App_DataReceived;
            apps.Remove(app);
            appConsole.TryRemove(app, out List<string> _);
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="id"></param>
        public void RemoveAt(Guid id)
        {
            AppModel app = apps.FirstOrDefault(m => m.ID == id);
            if (app != null)
            {
                Remove(app);
            }
        }
        /// <summary>
        /// 开始所有
        /// </summary>
        public void StartAll()
        {
            foreach (AppModel app in apps)
            {
                app.Start();
            }
        }
        /// <summary>
        /// 停止所有
        /// </summary>
        public void StopAll()
        {
            foreach (AppModel app in apps)
            {
                app.Stop();
                ClearConsole(app);
            }
        }
        /// <summary>
        /// 根据路径停止
        /// </summary>
        /// <param name="paths"></param>
        public void StopByPath(params string[] paths)
        {
            foreach (AppModel app in apps)
            {
                if (!paths.Contains(app.AppPath)) continue;
                app.Stop();
                ClearConsole(app);
            }
        }
        /// <summary>
        /// ID索引
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AppModel this[Guid id] => apps.FirstOrDefault(m => m.ID == id);
        /// <summary>
        /// 获得应用列表
        /// </summary>
        /// <returns></returns>
        public List<AppListModel> GetAppLists()
        {
            var result = new List<AppListModel>();
            foreach (AppModel app in apps)
            {
                result.Add(app.CopyProperties<AppListModel>());
            }
            return result;
        }
        /// <summary>
        /// 获得应用控制台列表
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public List<string> GetAppConsoleList(AppModel app)
        {
            if (!appConsole.ContainsKey(app)) throw new InvalidOperationException("还没有任何信息");
            return appConsole[app];
        }
        /// <summary>
        /// 清空控制台
        /// </summary>
        /// <param name="app"></param>
        public void ClearConsole(AppModel app)
        {
            if (appConsole.ContainsKey(app))
            {
                appConsole.TryRemove(app, out List<string> _);
            }
        }
    }
}
