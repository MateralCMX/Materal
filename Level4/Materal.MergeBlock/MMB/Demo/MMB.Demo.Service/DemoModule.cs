using Microsoft.Extensions.DependencyInjection;
using MMB.Core.Abstractions;
using MMB.Demo.Abstractions;

namespace MMB.Demo.Service
{
    public class DemoModule : MMBModule, IMergeBlockModule
    {
        public DemoModule() : base("Demo") { }
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.Configure<ApplicationConfig>(context.Configuration);
            await base.OnConfigServiceAsync(context);
        }
    }
}
