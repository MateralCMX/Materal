﻿using Materal.Extensions.DependencyInjection;

namespace Materal.MergeBlock.DependencyInjection
{
    public class DependencyInjectionModule : MergeBlockModule, IMergeBlockModule
    {
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.HostBuilder.UseServiceProviderFactory(new MateralServiceProviderFactory());
            context.Services.AddInterceptor<DataValidationInterceptorAttribute>((im, m) =>
            {
                string methodName = m.Name;
                if (methodName.StartsWith("get_") || methodName.StartsWith("set_")) return false;
                if (m.DeclaringType is null || !m.DeclaringType.IsAssignableTo<IBaseService>()) return false;
                if (m.GetParameters().Length <= 0) return false;
                return true;
            });
            await base.OnConfigServiceAsync(context);
        }
    }
}
