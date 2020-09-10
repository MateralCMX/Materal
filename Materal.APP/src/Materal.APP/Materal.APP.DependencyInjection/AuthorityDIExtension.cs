using Materal.APP.ServiceImpl;
using Materal.APP.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.APP.DependencyInjection
{
    public static class AuthorityDIExtension
    {
        public static void AddMateralAPPServices(this IServiceCollection services)
        {
            services.AddSingleton<IServerService, ServerServiceImpl>();
        }
    }
}
