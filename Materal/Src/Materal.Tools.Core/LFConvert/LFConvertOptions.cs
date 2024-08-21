namespace Materal.Tools.Core.LFConvert
{
    /// <summary>
    /// LF转换器选项
    /// </summary>
    public class LFConvertOptions
    {
        /// <summary>
        /// 过滤器
        /// </summary>
        public Func<FileInfo, bool> Filter { get; set; } = m => true;
        /// <summary>
        /// 递归
        /// </summary>
        public bool Recursive { get; set; } = false;
    }
}
