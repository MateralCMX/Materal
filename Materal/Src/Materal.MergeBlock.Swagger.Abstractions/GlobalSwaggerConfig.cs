using Materal.MergeBlock.Abstractions;

namespace Materal.MergeBlock.Swagger.Abstractions
{
    /// <summary>
    /// 全局Swagger配置模型
    /// </summary>
    [Options(ConfigKey)]
    public class GlobalSwaggerOptions : IOptions
    {
        /// <summary>
        /// 配置键
        /// </summary>
        public const string ConfigKey = "Swagger";
        /// <summary>
        /// 启用标识
        /// </summary>
        public bool Enable { get; set; } = true;
    }
}