﻿namespace Materal.Tools.Core.MateralPublish
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
        string GetNowVersion(string projectPath);
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="projectPath"></param>
        /// <param name="version"></param>
        /// <param name="projects"></param>
        /// <returns></returns>
        Task PublishAsync(string projectPath, string version, ICollection<IMateralProject> projects);
        /// <summary>
        /// 是Materal项目路径
        /// </summary>
        /// <param name="projectPath"></param>
        /// <returns></returns>
        bool IsMateralProjectPath(string projectPath);
        /// <summary>
        /// 获得Materal项目根路径
        /// </summary>
        /// <param name="projectPath"></param>
        /// <returns></returns>
        DirectoryInfo GetMateralProjectRootPath(string projectPath);
    }
}
