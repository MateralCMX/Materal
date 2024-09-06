using RC.Demo.Repository;
using Materal.MergeBlock.Consul.Abstractions;

namespace RC.Demo.Application
{
    /// <summary>
    /// Demo模块
    /// </summary>
    [DependsOn(typeof(DemoRepositoryModule))]
    public class DemoModule() : RCModule("RCDemo模块")
    {
        /// <inheritdoc/>
        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            base.OnConfigureServices(context);
            context.Services.AddConsulConfig("RCDemo", ["RC.Demo"]);
        }
        /// <inheritdoc/>
        public override async Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
        {
            using IServiceScope scope = context.ServiceProvider.CreateScope();
            IUserService userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            await userService.AddDefaultUserAsync();
        }
    }
}