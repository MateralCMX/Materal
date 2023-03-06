using Materal.Abstractions;
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
        private static readonly IHubContext<ConsoleMessageHub, IConsoleMessageHub> _hubContext;
        /// <summary>
        /// 构造方法
        /// </summary>
        static ApplicationRuntimeModel()
        {
            _taskQueue = new(RunTask);
            _hubContext = MateralServices.GetService<IHubContext<ConsoleMessageHub, IConsoleMessageHub>>();
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
        private readonly List<string> _consoleMessages = new();
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
        private readonly IApplicationHandler _applicationHandler;
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
        public async void AddConsoleMessage(string message)
        {
            const int consoleCount = 500;
            if (string.IsNullOrWhiteSpace(message)) return;
            _consoleMessages.Add(message);
            if (_consoleMessages.Count > consoleCount)
            {
                _consoleMessages.RemoveRange(0, _consoleMessages.Count - consoleCount);
            }
            await _hubContext.Clients.All.NewConsoleMessageEvent(ApplicationInfo.ID, message);
        }
        /// <summary>
        /// 清空控制台消息
        /// </summary>
        public async void ClearConsoleMessage()
        {
            _consoleMessages.Clear();
            await _hubContext.Clients.All.ClearConsoleMessageEvent(ApplicationInfo.ID);
        }
        /// <summary>
        /// 获得控制台消息
        /// </summary>
        /// <returns></returns>
        public List<string> GetConsoleMessages() => _consoleMessages;
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public async Task ShutDownAsync()
        {
            ExecuteStopTask();
            _taskQueue.Complete();
            await _taskQueue.Completion;
        }
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
                    rarFileInfo = rarFileInfos.OrderByDescending(m => m.LastWriteTime).FirstOrDefault();
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
            AddConsoleMessage($"解压文件{Path.GetFileName(rarFilePath)}...");
            string unRarPath = Path.Combine(RarFilesDirectoryPath, "Temp");
            DirectoryInfo unRarDirectoryInfo = Directory.CreateDirectory(unRarPath);
            string winRarPath = Path.Combine(ApplicationConfig.WinRarPath, "UnRaR.exe");
            if (!File.Exists(winRarPath)) throw new RCException("UnRar.exe文件丢失");
            string cmdArgs = $"x -o+ -y \"{rarFilePath}\" \"{unRarPath}\"";
            ProcessHelper processHelper = new();
            void DataHandler(object sender, DataReceivedEventArgs e)
            {
                if (e.Data == null || string.IsNullOrWhiteSpace(e.Data)) return;
                AddConsoleMessage(e.Data);
            }
            processHelper.ErrorDataReceived += DataHandler;
            processHelper.OutputDataReceived += DataHandler;
            processHelper.ProcessStart(winRarPath, cmdArgs);
            return unRarPath;
        }
        /// <summary>
        /// 拷贝文件夹
        /// </summary>
        /// <param name="sourceDirectoryInfo"></param>
        /// <param name="targetDirectoryInfo"></param>
        private void MoveToDirectory(DirectoryInfo sourceDirectoryInfo, DirectoryInfo targetDirectoryInfo)
        {
            if (!sourceDirectoryInfo.Exists) return;
            try
            {
                if (!targetDirectoryInfo.Exists)
                {

                    AddConsoleMessage($"创建文件夹:{targetDirectoryInfo.FullName}");
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
                        AddConsoleMessage($"移动文件夹:{subDirectoryInfo.FullName}到{targetDirectoryInfo.FullName}");
                        subDirectoryInfo.MoveTo(targetSubDirectoryInfo.FullName);
                    }
                }
                foreach (FileInfo subFileInfo in sourceDirectoryInfo.GetFiles())
                {
                    FileInfo targetSubFileInfo = new(Path.Combine(targetDirectoryInfo.FullName, subFileInfo.Name));
                    if (targetSubFileInfo.Exists)
                    {
                        AddConsoleMessage($"删除文件:{targetSubFileInfo.FullName}");
                        targetSubFileInfo.Delete();
                    }
                    AddConsoleMessage($"移动文件:{subFileInfo.FullName}到{targetSubFileInfo.FullName}");
                    subFileInfo.MoveTo(targetSubFileInfo.FullName);
                }
            }
            catch (Exception ex)
            {
                AddConsoleMessage(ex.GetErrorMessage());
                AddConsoleMessage($"删除文件夹:{sourceDirectoryInfo.FullName}");
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
