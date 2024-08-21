namespace Materal.Extensions.Models
{
    /// <summary>
    /// 整数转换模型
    /// </summary>
    public class IntConvertModel
    {
        /// <summary>
        /// 数字
        /// </summary>
        public Dictionary<int, string> Numbers { get; set; } = [];
        /// <summary>
        /// 单位
        /// </summary>
        public List<string> Units { get; set; } = [];
        /// <summary>
        /// 扩展
        /// </summary>
        public Dictionary<int, string> Extend { get; set; } = [];
    }
}
