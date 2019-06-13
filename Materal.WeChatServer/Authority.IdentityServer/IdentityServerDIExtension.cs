using DependencyInjection;
using IdentityServer4.Validation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Authority.IdentityServer
{
    public static class IdentityServerDIExtension
    {
        /// <summary>
        /// 添加认证服务器服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddIdentityServerServices(this IServiceCollection services)
        {
            services.AddBaseServices();
            services.AddAuthorityServices();
            services.AddAutoMapperService(Assembly.Load("Authority.ServiceImpl"));
            IIdentityServerBuilder builder = services.AddIdentityServer()
                .AddInMemoryApiResources(IdentityConfig.GetAPIs())
                .AddInMemoryClients(IdentityConfig.GetClients())
                .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources());
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            builder.AddDeveloperSigningCredential();
        }
    }
}
