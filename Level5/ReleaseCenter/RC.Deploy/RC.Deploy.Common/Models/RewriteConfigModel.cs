namespace RC.Deploy.Common.Models
{
    /// <summary>
    /// 重写配置
    /// </summary>
    public class RewriteConfigModel
    {
        /// <summary>
        /// 启用标识
        /// </summary>
        public bool Enable { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; } = string.Empty;
    }
}
