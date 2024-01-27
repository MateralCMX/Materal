using Materal.MergeBlock.Abstractions.NormalModule;

namespace Materal.MergeBlock.NormalModule
{
    /// <summary>
    /// 配置服务上下文
    /// </summary>
    public class NormalConfigServiceContext(IServiceCollection services, ConfigurationManager configuration) : ConfigServiceContext(services, configuration), INormalConfigServiceContext
    {
    }
}
