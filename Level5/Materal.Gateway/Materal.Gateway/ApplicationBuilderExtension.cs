using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Ocelot.Configuration.Creator;
using Ocelot.Configuration.File;
using Ocelot.Configuration.Repository;
using Ocelot.Configuration.Setter;
using Ocelot.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Logging;
using Ocelot.Middleware;
using Ocelot.Responses;
using System.Diagnostics;
using Materal.Gateway.OcelotExtension.Middleware;

namespace Materal.Gateway
{
    /// <summary>
    /// 应用程序构建器扩展
    /// </summary>
    public static partial class ApplicationBuilderExtension
    {
        /// <summary>
        /// 使用Materal网关
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static async Task<IApplicationBuilder> UseMateralGatewayAsync(this IApplicationBuilder builder)
        {
            await builder.UseMateralGatewayAsync(new OcelotPipelineConfiguration());
            return builder;
        }
        /// <summary>
        /// 使用Materal网关
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="pipelineConfiguration"></param>
        /// <returns></returns>
        public static async Task<IApplicationBuilder> UseMateralGatewayAsync(this IApplicationBuilder builder, Action<OcelotPipelineConfiguration> pipelineConfiguration)
        {
            OcelotPipelineConfiguration config = new();
            pipelineConfiguration?.Invoke(config);
            return await builder.UseMateralGatewayAsync(config);
        }
        /// <summary>
        /// 使用Materal网关
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="pipelineConfiguration"></param>
        /// <returns></returns>
        public static async Task<IApplicationBuilder> UseMateralGatewayAsync(this IApplicationBuilder builder, OcelotPipelineConfiguration pipelineConfiguration)
        {
            await CreateConfiguration(builder);
            ConfigureDiagnosticListener(builder);
            return CreateGatewayPipeline(builder, pipelineConfiguration);
        }
        /// <summary>
        /// 使用Materal网关
        /// </summary>
        /// <param name="app"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        public static Task<IApplicationBuilder> UseMateralGatewayAsync(this IApplicationBuilder app, Action<IApplicationBuilder, OcelotPipelineConfiguration> builderAction)
            => UseMateralGatewayAsync(app, builderAction, new OcelotPipelineConfiguration());
        /// <summary>
        /// 使用Materal网关
        /// </summary>
        /// <param name="app"></param>
        /// <param name="builderAction"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task<IApplicationBuilder> UseMateralGatewayAsync(this IApplicationBuilder app, Action<IApplicationBuilder, OcelotPipelineConfiguration> builderAction, OcelotPipelineConfiguration configuration)
        {
            await CreateConfiguration(app);
            ConfigureDiagnosticListener(app);
            builderAction?.Invoke(app, configuration ?? new OcelotPipelineConfiguration());
            app.Properties["analysis.NextMiddlewareName"] = "TransitionToOcelotMiddleware";
            return app;
        }
        /// <summary>
        /// 创建网关管道
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="pipelineConfiguration"></param>
        /// <returns></returns>
        private static IApplicationBuilder CreateGatewayPipeline(IApplicationBuilder builder, OcelotPipelineConfiguration pipelineConfiguration)
        {
            builder.BuildGatewayPipeline(pipelineConfiguration);
            /*
            inject first delegate into first piece of asp.net middleware..maybe not like this
            then because we are updating the http context in ocelot it comes out correct for
            rest of asp.net..
            */
            builder.Properties["analysis.NextMiddlewareName"] = "TransitionToOcelotMiddleware";
            return builder;
        }
        /// <summary>
        /// 创建配置
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        private static async Task<IInternalConfiguration> CreateConfiguration(IApplicationBuilder builder)
        {
            // make configuration from file system?
            // earlier user needed to add ocelot files in startup configuration stuff, asp.net will map it to this
            IOptionsMonitor<FileConfiguration> fileConfig = builder.ApplicationServices.GetRequiredService<IOptionsMonitor<FileConfiguration>>();
            // now create the config
            IInternalConfigurationCreator internalConfigCreator = builder.ApplicationServices.GetRequiredService<IInternalConfigurationCreator>();
            Response<IInternalConfiguration> internalConfig = await internalConfigCreator.Create(fileConfig.CurrentValue);
            //Configuration error, throw error message
            if (internalConfig.IsError)
            {
                throw ThrowToStopOcelotStarting(internalConfig);
            }
            // now save it in memory
            IInternalConfigurationRepository internalConfigRepo = builder.ApplicationServices.GetRequiredService<IInternalConfigurationRepository>();
            internalConfigRepo.AddOrReplace(internalConfig.Data);
            fileConfig.OnChange(async (config) =>
            {
                Response<IInternalConfiguration> newInternalConfig = await internalConfigCreator.Create(config);
                internalConfigRepo.AddOrReplace(newInternalConfig.Data);
            });
            IAdministrationPath? adminPath = builder.ApplicationServices.GetService<IAdministrationPath>();
            IEnumerable<OcelotMiddlewareConfigurationDelegate> configurations = builder.ApplicationServices.GetServices<OcelotMiddlewareConfigurationDelegate>();
            // Todo - this has just been added for consul so far...will there be an ordering problem in the future? Should refactor all config into this pattern?
            foreach (var configuration in configurations)
            {
                await configuration(builder);
            }
            if (AdministrationApiInUse(adminPath))
            {
                //We have to make sure the file config is set for the ocelot.env.json and ocelot.json so that if we pull it from the
                //admin api it works...boy this is getting a spit spags boll.
                IFileConfigurationSetter fileConfigSetter = builder.ApplicationServices.GetRequiredService<IFileConfigurationSetter>();
                await SetFileConfig(fileConfigSetter, fileConfig);
            }
            return GetOcelotConfigAndReturn(internalConfigRepo);
        }
        private static bool AdministrationApiInUse(IAdministrationPath? adminPath) => adminPath is not null;
        private static async Task SetFileConfig(IFileConfigurationSetter fileConfigSetter, IOptionsMonitor<FileConfiguration> fileConfig)
        {
            Response response = await fileConfigSetter.Set(fileConfig.CurrentValue);
            if (IsError(response))
            {
                throw ThrowToStopOcelotStarting(response);
            }
        }
        private static bool IsError(Response response)
        {
            return response == null || response.IsError;
        }
        private static IInternalConfiguration GetOcelotConfigAndReturn(IInternalConfigurationRepository provider)
        {
            Response<IInternalConfiguration> ocelotConfiguration = provider.Get();
            if (ocelotConfiguration == null || ocelotConfiguration?.Data == null || ocelotConfiguration.IsError)
            {
                throw ThrowToStopOcelotStarting(ocelotConfiguration);
            }
            return ocelotConfiguration.Data;
        }
        private static Exception ThrowToStopOcelotStarting(Response? config)
        {
            if(config is null)
            {
                return new Exception($"Unable to start Ocelot");
            }
            else
            {
                return new Exception($"Unable to start Ocelot, errors are: {string.Join(',', config.Errors.Select(x => x.ToString()))}");
            }
        }
        private static void ConfigureDiagnosticListener(IApplicationBuilder builder)
        {
            OcelotDiagnosticListener listener = builder.ApplicationServices.GetRequiredService<OcelotDiagnosticListener>();
            DiagnosticListener diagnosticListener = builder.ApplicationServices.GetRequiredService<DiagnosticListener>();
            diagnosticListener.SubscribeWithAdapter(listener);
        }
    }
}
