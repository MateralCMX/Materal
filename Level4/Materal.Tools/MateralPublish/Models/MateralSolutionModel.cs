using Materal.Tools.Helper;
using MateralPublish.Extensions;

namespace MateralPublish.Models
{
    public class MateralSolutionModel
    {
        /// <summary>
        /// 项目文件夹信息
        /// </summary>
        public DirectoryInfo ProjectDirectoryInfo { get; }
        /// <summary>
        /// 发布文件夹信息
        /// </summary>
        public DirectoryInfo PublishDirectoryInfo { get; }
        /// <summary>
        /// 主项目
        /// </summary>
        public MainProjectModel MainProject { get; }
        /// <summary>
        /// 日志项目
        /// </summary>
        public LoggerProjectModel LoggerProject { get; }
        /// <summary>
        /// 业务流项目
        /// </summary>
        public BusinessFlowProjectModel BusinessFlowProject { get; }
        /// <summary>
        /// TFMS项目
        /// </summary>
        public TFMSProjectModel TFMSProject { get; }
        /// <summary>
        /// TTA项目
        /// </summary>
        public TTAProjectModel TTAProject { get; }
        /// <summary>
        /// 工作流项目
        /// </summary>
        public WorkflowProjectModel WorkflowProject { get; }
        /// <summary>
        /// 基础核心项目
        /// </summary>
        public BaseCoreProjectModel BaseCoreProject { get; }
        /// <summary>
        /// 调度器项目
        /// </summary>
        public OscillatorProjectModel OscillatorProject { get; }
        public MateralSolutionModel(string path)
        {
            ProjectDirectoryInfo = GetProjectDirectoryInfo(path);
            NugetServerHelper.NugetDirectoryInfo = Path.Combine(ProjectDirectoryInfo.FullName, "nupkgs").GetNewDirectoryInfo();
            PublishDirectoryInfo = Path.Combine(ProjectDirectoryInfo.FullName, "publish").GetNewDirectoryInfo();
            MainProject = new MainProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level0", "Materal"));
            LoggerProject = new LoggerProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level1", "Materal.Logger"));
            TFMSProject = new TFMSProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level2", "Materal.TFMS"));
            TTAProject = new TTAProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level2", "Materal.ThreeTierArchitecture"));
            WorkflowProject = new WorkflowProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level2", "Materal.Workflow"));
            BusinessFlowProject = new BusinessFlowProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level3", "Materal.BusinessFlow"));
            OscillatorProject = new OscillatorProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level3", "Materal.Oscillator"));
            BaseCoreProject = new BaseCoreProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level3", "Materal.BaseCore"));
        }
        /// <summary>
        /// 获得下一个版本号
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetNextVersionAsync()
        {
            string nowVersion = await MainProject.GetNowVersionAsync();
            string[] versions = nowVersion.Split('.');
            int lastVersionNumber = Convert.ToInt32(versions.Last());
            versions[^1] = (lastVersionNumber + 1).ToString();
            string nextVersion = string.Join('.', versions);
            return nextVersion;
        }
        /// <summary>
        /// 获得版本号
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetVersionAsync()
        {
            string nowVersion = await MainProject.GetNowVersionAsync();
            return nowVersion;
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="version"></param>
        /// <param name="uploadNuget"></param>
        public async Task PublishAsync(string version, bool uploadNuget)
        {
            await MainProject.PublishAsync(PublishDirectoryInfo, version);
            await LoggerProject.PublishAsync(PublishDirectoryInfo, version);
            await TFMSProject.PublishAsync(PublishDirectoryInfo, version);
            await TTAProject.PublishAsync(PublishDirectoryInfo, version);
            await WorkflowProject.PublishAsync(PublishDirectoryInfo, version);
            await BusinessFlowProject.PublishAsync(PublishDirectoryInfo, version);
            await OscillatorProject.PublishAsync(PublishDirectoryInfo, version);
            await BaseCoreProject.PublishAsync(PublishDirectoryInfo, version);
            if (uploadNuget)
            {
                await NugetServerHelper.UploadNugetPackagesAsync();
            }
            ConsoleHelper.WriteLine("正在清理临时文件...");
            ConsoleHelper.WriteLine($"删除文件夹{PublishDirectoryInfo.FullName}");
            PublishDirectoryInfo.Delete(true);
        }
        /// <summary>
        /// 获得项目文件夹信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private DirectoryInfo GetProjectDirectoryInfo(string path)
        {
            path ??= Environment.CurrentDirectory;
            DirectoryInfo result = new(path);
            if (result.Name != "Materal" || !result.GetFiles().Any(m => m.Name == "LICENSE"))
            {
                if (result.Parent == null) throw new Exception("未找到项目根路径");
                result = GetProjectDirectoryInfo(result.Parent.FullName);
            }
            return result;
        }
    }
}
