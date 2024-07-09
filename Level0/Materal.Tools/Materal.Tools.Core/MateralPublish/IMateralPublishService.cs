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
        /// <param name="projectPath"></param>
        /// <returns></returns>
        ICollection<IMateralProject> GetAllProjects(string projectPath);
        /// <summary>
        /// 获得当前版本号
        /// </summary>
        /// <param name="projectPath"></param>
        /// <returns></returns>
        Task<string> GetNowVersionAsync(string projectPath);
    }
}
