namespace Materal.Gateway.Models
{
    /// <summary>
    /// 全局配置模型
    /// </summary>
    public class GlobalConfigModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string ConsulHost { get; set; } = "127.0.0.1";
        /// <summary>
        /// 
        /// </summary>
        public int ConsulPort { get; set; } = 8500;
    }
}
