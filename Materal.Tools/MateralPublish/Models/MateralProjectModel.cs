using MateralPublish.Extensions;

namespace MateralPublish.Models
{
    public class MateralProjectModel
    {
        /// <summary>
        /// 项目文件夹信息
        /// </summary>
        public DirectoryInfo ProjectDirectoryInfo { get; }
        /// <summary>
        /// Nuget包文件夹信息
        /// </summary>
        public DirectoryInfo NugetDirectoryInfo { get; }
        /// <summary>
        /// 发布文件夹信息
        /// </summary>
        public DirectoryInfo PublishDirectoryInfo { get; }
        /// <summary>
        /// 主项目
        /// </summary>
        public MainProjectModel Project { get; }
        /// <summary>
        /// 日志项目
        /// </summary>
        public ProjectModel LoggerProject { get; }
        /// <summary>
        /// 业务流项目
        /// </summary>
        public ProjectModel BusinessFlowProject { get; }
        /// <summary>
        /// 网关项目
        /// </summary>
        public ProjectModel GatewayProject { get; }
        /// <summary>
        /// TFMS项目
        /// </summary>
        public ProjectModel TFMSProject { get; }
        /// <summary>
        /// TTA项目
        /// </summary>
        public ProjectModel ThreeTierArchitectureProject { get; }
        /// <summary>
        /// 工作流项目
        /// </summary>
        public ProjectModel WorkflowProject { get; }
        /// <summary>
        /// 基础核心项目
        /// </summary>
        public BaseCoreProjectModel BaseCoreProject { get; }
        /// <summary>
        /// 调度器项目
        /// </summary>
        public ProjectModel OscillatorProject { get; }
        /// <summary>
        /// 发布中心项目
        /// </summary>
        public ProjectModel ReleaseCenterProject { get; }
        public MateralProjectModel(string path)
        {
            ProjectDirectoryInfo = GetProjectDirectoryInfo(path);
            NugetDirectoryInfo = Path.Combine(ProjectDirectoryInfo.FullName, "nupkgs").GetNewDirectoryInfo();
            PublishDirectoryInfo = Path.Combine(ProjectDirectoryInfo.FullName, "publish").GetNewDirectoryInfo();
            Project = new MainProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level0", "Materal"));
            LoggerProject = new ProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level1", "Materal.Logger"));
            BusinessFlowProject = new ProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level2", "Materal.BusinessFlow"));
            GatewayProject = new ProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level2", "Materal.Gateway"));
            TFMSProject = new ProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level2", "Materal.TFMS"));
            ThreeTierArchitectureProject = new ProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level2", "Materal.ThreeTierArchitecture"));
            WorkflowProject = new ProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level2", "Materal.Workflow"));
            BaseCoreProject = new BaseCoreProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level3", "Materal.BaseCore"));
            OscillatorProject = new ProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level3", "Materal.Oscillator"));
            ReleaseCenterProject = new ProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level4", "ReleaseCenter"));
        }
        public async Task<string> GetNextVersionAsync()
        {
            string nowVersion = await Project.GetNowVersionAsync();
            string[] versions = nowVersion.Split('.');
            int lastVersionNumber = Convert.ToInt32(versions.Last());
            versions[versions.Length - 1] = (lastVersionNumber + 1).ToString();
            string nextVersion = string.Join('.', versions);
            return nextVersion;
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="options"></param>
        public async Task PublishAsync(string version)
        {
            await Project.PublishAsync(PublishDirectoryInfo, NugetDirectoryInfo, version);
            await LoggerProject.PublishAsync(PublishDirectoryInfo, NugetDirectoryInfo, version);
            await BusinessFlowProject.PublishAsync(PublishDirectoryInfo, NugetDirectoryInfo, version);
            await GatewayProject.PublishAsync(PublishDirectoryInfo, NugetDirectoryInfo, version);
            await TFMSProject.PublishAsync(PublishDirectoryInfo, NugetDirectoryInfo, version);
            await ThreeTierArchitectureProject.PublishAsync(PublishDirectoryInfo, NugetDirectoryInfo, version);
            await WorkflowProject.PublishAsync(PublishDirectoryInfo, NugetDirectoryInfo, version);
            await OscillatorProject.PublishAsync(PublishDirectoryInfo, NugetDirectoryInfo, version);
            await BaseCoreProject.PublishAsync(PublishDirectoryInfo, NugetDirectoryInfo, version);
            //await ReleaseCenterProject.PublishAsync(PublishDirectoryInfo, NugetDirectoryInfo, version);
        }
        /// <summary>
        /// 获得项目文件夹信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private DirectoryInfo GetProjectDirectoryInfo(string path)
        {
            path ??= AppDomain.CurrentDomain.BaseDirectory;
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
