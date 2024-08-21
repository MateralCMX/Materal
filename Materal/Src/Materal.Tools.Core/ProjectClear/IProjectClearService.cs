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
        /// <returns></returns>
        void ClearProject(string projectPath);
    }
}
