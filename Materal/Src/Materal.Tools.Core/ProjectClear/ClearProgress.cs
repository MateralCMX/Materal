namespace Materal.Tools.Core.ProjectClear
{
    /// <summary>
    /// 清理进度信息
    /// </summary>
    public class ClearProgress
    {
        /// <summary>
        /// 当前处理的路径
        /// </summary>
        public string CurrentPath { get; set; } = string.Empty;
        /// <summary>
        /// 已处理的文件数量
        /// </summary>
        public int ProcessedFiles { get; set; }
        /// <summary>
        /// 已处理的文件夹数量
        /// </summary>
        public int ProcessedDirectories { get; set; }
    }
}
