﻿using Materal.Common;
using Materal.WindowsHelper;
using Microsoft.AspNetCore.SignalR;
using RC.Core.Common;
using RC.Deploy.Common;
using RC.Deploy.Domain;
using RC.Deploy.Enums;
using RC.Deploy.Hubs;
using RC.Deploy.ServiceImpl.ApplicationHandlers;
using System.Diagnostics;
using System.Threading.Tasks.Dataflow;

namespace RC.Deploy.ServiceImpl.Models
{
    /// <summary>
    /// 应用程序模型
    /// </summary>
    public class ApplicationRuntimeModel
    {
        /// <summary>
        /// 任务队列
        /// </summary>
        private static readonly ActionBlock<ApplicationTask> _taskQueue;
        private static readonly IHubContext<ConsoleMessageHub> _hubContext;
        /// <summary>
        /// 构造方法
        /// </summary>
        static ApplicationRuntimeModel()
        {
            _taskQueue = new(RunTask);
            _hubContext = MateralServices.GetService<IHubContext<ConsoleMessageHub>>();
        }
        /// <summary>
        /// 应用程序信息
        /// </summary>
        public ApplicationInfo ApplicationInfo { get; set; }
        /// <summary>
        /// 应用程序状态
        /// </summary>
        public ApplicationStatusEnum ApplicationStatus { get; set; } = ApplicationStatusEnum.Stop;
        /// <summary>
        /// 控制台信息
        /// </summary>
        private List<string> _consoleMessages = new();
        /// <summary>
        /// 压缩包文件组文件夹路径
        /// </summary>
        public string RarFilesDirectoryPath => Path.Combine(ApplicationConfig.UploadFilePath, ApplicationInfo.Name);
        /// <summary>
        /// 发布文件夹路径
        /// </summary>
        public string PublishDirectoryPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Application", ApplicationInfo.RootPath);
        /// <summary>
        /// 应用程序处理器
        /// </summary>
        private IApplicationHandler _applicationHandler { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="applicationInfo"></param>
        public ApplicationRuntimeModel(ApplicationInfo applicationInfo)
        {
            ApplicationInfo = applicationInfo;
            _applicationHandler = ApplicationInfo.ApplicationType.GetApplicationHandler();
        }
        /// <summary>
        /// 执行启动任务
        /// </summary>
        public void ExecuteStartTask() => _taskQueue.Post(new ApplicationTask
        {
            Application = this,
            TargetAction = Start
        });
        /// <summary>
        /// 执行关闭任务
        /// </summary>
        public void ExecuteStopTask() => _taskQueue.Post(new ApplicationTask
        {
            Application = this,
            TargetAction = Stop
        });
        /// <summary>
        /// 执行应用最后一个文件任务
        /// </summary>
        public void ExecuteApplyLatestFileTask() => _taskQueue.Post(new ApplicationTask
        {
            Application = this,
            TargetAction = () => { ApplyFile(); }
        });
        /// <summary>
        /// 执行应用文件任务
        /// </summary>
        public void ExecuteApplyFileTask(string fileName) => _taskQueue.Post(new ApplicationTask
        {
            Application = this,
            TargetAction = () => { ApplyFile(fileName); }
        });
        /// <summary>
        /// 杀死程序
        /// </summary>
        public void Kill() => _applicationHandler.KillApplication(this);
        /// <summary>
        /// 添加控制台消息
        /// </summary>
        /// <param name="message"></param>
        public void AddConsoleMessage(string message)
        {
            const int consoleCount = 500;
            if (string.IsNullOrWhiteSpace(message)) return;
            _consoleMessages.Add(message);
            if (_consoleMessages.Count > consoleCount)
            {
                _consoleMessages.RemoveRange(0, _consoleMessages.Count - consoleCount);
            }
            _hubContext.Clients.All.SendAsync("NewConsoleMessage", ApplicationInfo.ID, message);
        }
        /// <summary>
        /// 清空控制台消息
        /// </summary>
        public void ClearConsoleMessage()
        {
            _consoleMessages.Clear();
            _hubContext.Clients.All.SendAsync("ClearConsoleMessage", ApplicationInfo.ID);
        }
        /// <summary>
        /// 获得控制台消息
        /// </summary>
        /// <returns></returns>
        public List<string> GetConsoleMessages() => _consoleMessages;
        #region 私有方法
        /// <summary>
        /// 启动
        /// </summary>
        /// <exception cref="RCException"></exception>
        private void Start() => _applicationHandler.StartApplication(this);
        /// <summary>
        /// 停止
        /// </summary>
        /// <exception cref="RCException"></exception>
        private void Stop() => _applicationHandler.StopApplication(this);
        /// <summary>
        /// 更新最新的文件
        /// </summary>
        /// <returns></returns>
        private void ApplyFile(string? fileName = null)
        {
            if (ApplicationStatus != ApplicationStatusEnum.Stop) throw new RCException("应用程序尚未停止。");
            ApplicationStatus = ApplicationStatusEnum.Update;
            try
            {
                AddConsoleMessage($"{ApplicationInfo.Name}开始更新...");
                DirectoryInfo rarFilesDirectoryInfo = new(RarFilesDirectoryPath);
                if (!rarFilesDirectoryInfo.Exists) return;
                FileInfo[] rarFileInfos = rarFilesDirectoryInfo.GetFiles();
                if (rarFileInfos.Length <= 0) return;
                FileInfo? rarFileInfo;
                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    rarFileInfo = rarFileInfos.FirstOrDefault(m => m.Name == fileName);
                }
                else
                {
                    rarFileInfo = rarFileInfos.OrderByDescending(m => m.CreationTime).FirstOrDefault();
                }
                if (rarFileInfo == null) return;
                string unRarDirectoryPath = UnRarFile(rarFileInfo.FullName);
                DirectoryInfo unRarDirectoryInfo = new(unRarDirectoryPath);
                DirectoryInfo publishDirectoryInfo = new(PublishDirectoryPath);
                if (publishDirectoryInfo.Exists && !ApplicationInfo.IsIncrementalUpdating)
                {
                    publishDirectoryInfo.Delete(true);
                }
                DirectoryInfo? targetDirectoryInf = unRarDirectoryInfo.GetDirectories().FirstOrDefault(m => m.Name == ApplicationInfo.RootPath);
                if (targetDirectoryInf != null)
                {
                    MoveToDirectory(targetDirectoryInf, publishDirectoryInfo);
                }
                unRarDirectoryInfo.Delete(true);
            }
            finally
            {
                ApplicationStatus = ApplicationStatusEnum.Stop;
            }
        }
        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="rarFilePath"></param>
        /// <returns></returns>
        /// <exception cref="RCException"></exception>
        private string UnRarFile(string rarFilePath)
        {
            AddConsoleMessage($"{ApplicationInfo.Name}解压文件{Path.GetFileName(rarFilePath)}...");
            string unRarPath = Path.Combine(RarFilesDirectoryPath, "Temp");
            DirectoryInfo unRarDirectoryInfo = Directory.CreateDirectory(unRarPath);
            string winRarPath = Path.Combine(ApplicationConfig.WinRarPath, "UnRaR.exe");
            if (!File.Exists(winRarPath)) throw new RCException("UnRar.exe文件丢失");
            string cmdArgs = $"x -o+ -y \"{rarFilePath}\" \"{unRarPath}\"";
            ProcessManager processManager = new();
            void DataHandler(object sender, DataReceivedEventArgs e)
            {
                if (e.Data == null || string.IsNullOrWhiteSpace(e.Data)) return;
                AddConsoleMessage(e.Data);
            }
            processManager.ErrorDataReceived += DataHandler;
            processManager.OutputDataReceived += DataHandler;
            processManager.ProcessStart(winRarPath, cmdArgs);
            return unRarPath;
        }
        /// <summary>
        /// 拷贝文件夹
        /// </summary>
        /// <param name="sourceDirectoryInfo"></param>
        /// <param name="targetDirectoryInfo"></param>
        private static void MoveToDirectory(DirectoryInfo sourceDirectoryInfo, DirectoryInfo targetDirectoryInfo)
        {
            if (!sourceDirectoryInfo.Exists) return;
            try
            {
                if (!targetDirectoryInfo.Exists)
                {
                    targetDirectoryInfo.Create();
                }
                foreach (DirectoryInfo subDirectoryInfo in sourceDirectoryInfo.GetDirectories())
                {
                    DirectoryInfo targetSubDirectoryInfo = new(Path.Combine(targetDirectoryInfo.FullName, subDirectoryInfo.Name));
                    if (targetSubDirectoryInfo.Exists)
                    {
                        MoveToDirectory(subDirectoryInfo, targetSubDirectoryInfo);
                    }
                    else
                    {
                        subDirectoryInfo.MoveTo(targetSubDirectoryInfo.FullName);
                    }
                }
                foreach (FileInfo subFileInfo in sourceDirectoryInfo.GetFiles())
                {
                    FileInfo targetSubFileInfo = new(Path.Combine(targetDirectoryInfo.FullName, subFileInfo.Name));
                    if (targetSubFileInfo.Exists)
                    {
                        targetSubFileInfo.Delete();
                    }
                    subFileInfo.MoveTo(targetSubFileInfo.FullName);
                }
            }
            catch (Exception)
            {
                sourceDirectoryInfo.Delete(true);
                throw;
            }
        }
        /// <summary>
        /// 运行任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        private static async Task RunTask(ApplicationTask task)
        {
            try
            {
                if (task.TargetTask != null)
                {
                    await task.TargetTask.Invoke();
                }
                task.TargetAction?.Invoke();
            }
            catch (Exception ex)
            {
                task.Application?.AddConsoleMessage(ex.GetErrorMessage());
            }
        }
        #endregion
    }
}