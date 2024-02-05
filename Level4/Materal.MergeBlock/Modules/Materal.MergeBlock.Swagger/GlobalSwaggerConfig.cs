namespace Materal.MergeBlock.Swagger
{
    /// <summary>
    /// 全局Swagger配置模型
    /// </summary>
    public class GlobalSwaggerConfig
    {
        /// <summary>
        /// 配置键
        /// </summary>
        public static string ConfigKey { get; } = "Swagger";
        /// <summary>
        /// 启用标识
        /// </summary>
        public bool Enable { get; set; } = true;
    }
}