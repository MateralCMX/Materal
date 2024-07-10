namespace Materal.Tools.Core.MateralPublish
{
    /// <summary>
    /// Materal项目
    /// </summary>
    public interface IMateralProject
    {
        /// <summary>
        /// 等级
        /// </summary>
        int Level { get; }
        /// <summary>
        /// 位序
        /// </summary>
        int Index { get; }
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="projectDirectoryInfo"></param>
        /// <param name="nugetDirectoryInfo"></param>
        /// <param name="publishDirectoryInfo"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        Task PublishAsync(DirectoryInfo projectDirectoryInfo, DirectoryInfo nugetDirectoryInfo, DirectoryInfo publishDirectoryInfo, string version);
    }
}
