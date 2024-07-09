namespace Materal.Tools.Core.MateralPublish
{
    /// <summary>
    /// Materal发布服务
    /// </summary>
    public interface IMateralPublishService
    {
        /// <summary>
        /// 获取所有项目
        /// </summary>
        /// <returns></returns>
        ICollection<IMateralProject> GetAllProjects();
        /// <summary>
        /// 获得当前版本号
        /// </summary>
        /// <param name="projectPath"></param>
        /// <returns></returns>
        Task<string> GetNowVersionAsync(string projectPath);
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="projectPath"></param>
        /// <param name="version"></param>
        /// <param name="projects"></param>
        /// <param name="OnMessage"></param>
        /// <returns></returns>
        Task PublishAsync(string projectPath, string version, ICollection<IMateralProject> projects, Action<MessageLevel, string?>? OnMessage);
    }
}
