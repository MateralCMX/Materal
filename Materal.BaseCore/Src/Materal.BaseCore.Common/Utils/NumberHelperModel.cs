namespace Materal.BaseCore.Common.Utils
{
    /// <summary>
    /// 数字帮助模型
    /// </summary>
    public class NumberHelperModel
    {
        /// <summary>
        /// 数字
        /// </summary>
        public Dictionary<uint, string> Numbers { get; set; } = new();
        /// <summary>
        /// 单位
        /// </summary>
        public List<string> Units { get; set; } = new();
        /// <summary>
        /// 扩展
        /// </summary>
        public Dictionary<int, string> Extend { get; set; } = new();
    }
}
