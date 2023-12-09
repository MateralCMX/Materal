using Microsoft.Extensions.DependencyInjection;

namespace MMB.Demo.Abstractions
{
    public class DemoModule : MergeBlockModule, IMergeBlockModule
    {
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.Configure<ApplicationConfig>(context.Configuration);
            await base.OnConfigServiceAsync(context);
        }
    }
}
