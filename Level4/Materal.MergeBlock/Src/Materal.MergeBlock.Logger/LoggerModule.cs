using Materal.Logger;
using Microsoft.Extensions.Configuration;

namespace Materal.MergeBlock.Logger
{
    public class LoggerModule : MergeBlockModule, IMergeBlockModule
    {
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.AddMateralLogger(options =>
            {
                string applicationName = context.Configuration.GetValue(nameof(ApplicationConfigModel.ApplicationName)) ?? "MergeBlockApplication";
                options.AddCustomConfig("ApplicationName", applicationName);
            });
            await base.OnConfigServiceAsync(context);
        }
    }
}
