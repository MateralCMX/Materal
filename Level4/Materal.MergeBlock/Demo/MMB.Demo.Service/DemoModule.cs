using Microsoft.Extensions.DependencyInjection;

namespace MMB.Demo.Service
{
    public class DemoModule : MergeBlockModule, IMergeBlockModule
    {
        public override async Task OnConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            //if (context.Configuration is not IConfigurationBuilder configuration) return;
            //string? url = context.Configuration.GetValue("ConfigUrl");
            //if (url is null || !url.IsUrl()) throw new MergeBlockException("配置中心地址错误");
            //configuration.AddDefaultNameSpace(url, "MMBProject")
            //    .AddNameSpace("Demo");
            await base.OnConfigServiceBeforeAsync(context);
        }
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.MvcBuilder?.AddApplicationPart(GetType().Assembly);
            await base.OnConfigServiceAsync(context);
        }
    }
}
