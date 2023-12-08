using Microsoft.Extensions.DependencyInjection;

namespace Materal.MergeBlock.Authorization
{
    public class DemoModule : MergeBlockModule, IMergeBlockModule
    {
        public override Task OnConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            return base.OnConfigServiceBeforeAsync(context);
        }
        public override Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.MvcBuilder?.AddApplicationPart(GetType().Assembly);
            return base.OnConfigServiceAsync(context);
        }
        public override Task OnConfigServiceAfterAsync(IConfigServiceContext context)
        {
            return base.OnConfigServiceAfterAsync(context);
        }
        public override Task OnApplicationInitBeforeAsync(IApplicationContext context)
        {
            return base.OnApplicationInitBeforeAsync(context);
        }
        public override Task OnApplicationInitAsync(IApplicationContext context)
        {
            return base.OnApplicationInitAsync(context);
        }
        public override Task OnApplicationInitAfterAsync(IApplicationContext context)
        {
            return base.OnApplicationInitAfterAsync(context);
        }
    }
}
