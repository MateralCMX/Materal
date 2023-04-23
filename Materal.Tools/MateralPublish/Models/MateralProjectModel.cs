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
        public ProjectModel Project { get; }
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
        public ProjectModel BaseCoreProject { get; }
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
            NugetDirectoryInfo = GetNewDirectoryInfo(Path.Combine(ProjectDirectoryInfo.FullName, "nupkgs"));
            PublishDirectoryInfo = GetNewDirectoryInfo(Path.Combine(ProjectDirectoryInfo.FullName, "publish"));
            Project = new ProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level0", "Materal"));
            LoggerProject = new ProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level1", "Materal.Logger"));
            BusinessFlowProject = new ProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level2", "Materal.BusinessFlow"));
            GatewayProject = new ProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level2", "Materal.Gateway"));
            TFMSProject = new ProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level2", "Materal.TFMS"));
            ThreeTierArchitectureProject = new ProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level2", "Materal.ThreeTierArchitecture"));
            WorkflowProject = new ProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level2", "Materal.Workflow"));
            BaseCoreProject = new ProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level3", "Materal.BaseCore"));
            OscillatorProject = new ProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level3", "Materal.Oscillator"));
            ReleaseCenterProject = new ProjectModel(Path.Combine(ProjectDirectoryInfo.FullName, "Level4", "ReleaseCenter"));
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="newVersion"></param>
        public void Publish(string? newVersion)
        {
            string version;
            if (string.IsNullOrWhiteSpace(newVersion))
            {
                version = "3.0.0";
            }
            else
            {
                version = newVersion;
            }
            Project.Publish(PublishDirectoryInfo, version);
        }
        /// <summary>
        /// 获得新文件夹信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static DirectoryInfo GetNewDirectoryInfo(string path)
        {
            DirectoryInfo result = new(path);
            if (result.Exists)
            {
                result.Delete(true);
            }
            result.Create();
            result.Refresh();
            return result;
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
