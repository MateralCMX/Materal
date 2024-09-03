namespace Materal.MergeBlock.Consul.Abstractions
{
    /// <summary>
    /// 模块Consul配置
    /// </summary>
    public class ModuleConsulConfig
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; } = Guid.NewGuid();
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; } = string.Empty;
        /// <summary>
        /// 标签
        /// </summary>
        public string[] Tags { get; set; } = [];
        /// <summary>
        /// 健康检查路径
        /// </summary>
        public string? HealthPath { get; set; }
    }
}
