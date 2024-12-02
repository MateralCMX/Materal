namespace Materal.Tools.Core.ProjectClear
{
    /// <summary>
    /// 项目清理配置
    /// </summary>
    public class ProjectClearConfig
    {
        /// <summary>
        /// 文件夹白名单(要删除的)
        /// </summary>
        public List<string> DictionaryWhiteList { get; set; } = [".vs", "bin", "obj", "node_modules"];
        /// <summary>
        /// 文件夹黑名单(不删除的)
        /// </summary>
        public List<string> DictionaryBlackList { get; set; } = [".git"];
        /// <summary>
        /// 是否可以删除空文件夹
        /// </summary>
        public bool CanDeleteEmptyDirectory { get; set; } = true;
        /// <summary>
        /// 文件过滤器
        /// </summary>
        public Func<FileInfo, bool>? FileFilter { get; set; }
    }
}
