using Materal.MergeBlock.Consul.Abstractions;
using RC.Authority.Repository;

namespace RC.Authority.Application
{
    /// <summary>
    /// Authority模块
    /// </summary>
    [DependsOn(typeof(AuthorityRepositoryModule))]
    public class AuthorityModule() : RCModule("RCAuthority模块")
    {
        /// <inheritdoc/>
        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            base.OnConfigureServices(context);
            context.Services.AddConsulConfig("RCAuthority", ["RC.Authority"]);
        }
    }
}