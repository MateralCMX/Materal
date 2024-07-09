namespace Materal.Tools.Core.MateralVersion
{
    /// <summary>
    /// Materal版本服务
    /// </summary>
    public interface IMateralVersionService
    {
        /// <summary>
        /// 更新版本
        /// </summary>
        /// <param name="projectPath"></param>
        /// <param name="nugetPaths"></param>
        /// <param name="onMessage"></param>
        /// <returns></returns>
        Task UpdateVersionAsync(string projectPath, string[] nugetPaths, Action<MessageLevel, string?>? onMessage = null);
        /// <summary>
        /// 更新版本
        /// </summary>
        /// <param name="projectPath"></param>
        /// <param name="version"></param>
        /// <param name="onMessage"></param>
        /// <returns></returns>
        Task UpdateVersionAsync(string projectPath, string version, Action<MessageLevel, string?>? onMessage = null);
        /// <summary>
        /// 获得当前版本
        /// </summary>
        /// <param name="nugetPaths"></param>
        /// <returns></returns>
        Task<string> GetNowVersionAsync(string[] nugetPaths);
    }
}
