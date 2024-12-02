namespace Materal.Tools.Core.ProjectClear
{
    /// <summary>
    /// 项目清理服务
    /// </summary>
    public interface IProjectClearService
    {
        /// <summary>
        /// 清理项目
        /// </summary>
        /// <param name="projectPath"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        void ClearProject(string projectPath, IProgress<ClearProgress>? progress = null);
        /// <summary>
        /// 异步清理项目
        /// </summary>
        /// <param name="projectPath"></param>
        /// <param name="progress"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ClearProjectAsync(string projectPath, IProgress<ClearProgress>? progress = null, CancellationToken cancellationToken = default);
    }
}
