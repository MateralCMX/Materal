using Materal.Extensions;

namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 服务配置上下文
    /// </summary>
    public class ServiceConfigurationContext(IServiceCollection services, IConfiguration? configuration)
    {
        /// <summary>
        /// 配置对象
        /// </summary>
        public IConfiguration? Configuration { get; } = configuration ?? services.GetSingletonInstance<IConfiguration>();
        /// <summary>
        /// 服务集合
        /// </summary>
        public IServiceCollection Services { get; } = services;
    }
}
