using Materal.MergeBlock.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Materal.MergeBlock.Swagger.Abstractions.Extensions
{
    /// <summary>
    /// 服务集合扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Swagger分组
        /// </summary>
        /// <param name="services"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerGroup(this IServiceCollection services, string name, string? description = null)
        {
            services.AddSwaggerGen(m =>
            {
                OpenApiInfo openApiInfo = new()
                {
                    Title = description ?? name,
                    Version = name,
                    Description = "提供WebAPI接口"
                };
                m.SwaggerDoc(openApiInfo.Version, openApiInfo);
            });
            services.Add(new ServiceDescriptor(typeof(ISwaggerConfigService), new MergeBlockSwaggerConfigService(name)));
            return services;
        }
    }
}
