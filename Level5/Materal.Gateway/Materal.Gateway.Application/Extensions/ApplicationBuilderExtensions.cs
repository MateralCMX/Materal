using Materal.Gateway.Application.Middleware;
using Microsoft.AspNetCore.Builder;
using MMLib.SwaggerForOcelot.Configuration;
using MMLib.SwaggerForOcelot.Repositories;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Materal.Gateway.Application.Extensions
{
    /// <summary>
    /// 构建器扩展
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 使用Materal网关SwaggerUI
        /// </summary>
        /// <param name="app"></param>
        /// <param name="setupAction"></param>
        /// <param name="setupUiAction"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMateralGatewaySwaggerUI(this IApplicationBuilder app, Action<SwaggerForOcelotUIOptions>? setupAction = null, Action<SwaggerUIOptions>? setupUiAction = null)
        {
            SwaggerForOcelotUIOptions options = app.ApplicationServices.GetRequiredService<IOptions<SwaggerForOcelotUIOptions>>().Value;
            setupAction?.Invoke(options);
            UseSwaggerForOcelot(app, options);
            app.UseSwaggerUI(c =>
            {
                setupUiAction?.Invoke(c);
                IReadOnlyList<SwaggerEndPointOptions> endPoints = app.ApplicationServices.GetRequiredService<ISwaggerEndPointProvider>().GetAll();
                ChangeDetection(app, c, options);
                AddSwaggerEndPoints(c, endPoints, options.DownstreamSwaggerEndPointBasePath);
            });
            return app;
        }
        private static void UseSwaggerForOcelot(IApplicationBuilder app, SwaggerForOcelotUIOptions options)
            => app.Map(options.PathToSwaggerGenerator, builder => builder.UseMiddleware<GatewaySwaggerMiddleware>(options));
        private static void AddSwaggerEndPoints(SwaggerUIOptions c, IReadOnlyList<SwaggerEndPointOptions> endPoints, string basePath)
        {
            if (endPoints is null || endPoints.Count == 0)
            {
                throw new InvalidOperationException(
                    $"{SwaggerEndPointOptions.ConfigurationSectionName} configuration section is missing or empty.");
            }
            foreach (SwaggerEndPointOptions endPoint in endPoints)
            {
                foreach (SwaggerEndPointConfig config in endPoint.Config)
                {
                    c.SwaggerEndpoint($"{basePath}/{config.Version}/{endPoint.KeyToPath}", GetDescription(config));
                }
            }
        }
        private static void ChangeDetection(IApplicationBuilder app, SwaggerUIOptions c, SwaggerForOcelotUIOptions options)
        {
            IOptionsMonitor<List<SwaggerEndPointOptions>> endpointsChangeMonitor = app.ApplicationServices.GetRequiredService<IOptionsMonitor<List<SwaggerEndPointOptions>>>();
            endpointsChangeMonitor.OnChange((newEndpoints) =>
            {
                c.ConfigObject.Urls = null;
                AddSwaggerEndPoints(c, newEndpoints, options.DownstreamSwaggerEndPointBasePath);
            });
        }
        private static string GetDescription(SwaggerEndPointConfig config)
            => config.IsGatewayItSelf() ? config.Name : $"{config.Name} - {config.Version}";
    }
}
