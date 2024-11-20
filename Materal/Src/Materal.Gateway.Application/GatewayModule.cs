using Materal.Gateway.ConfigModel;
using Materal.Gateway.DependencyInjection;
using Materal.Gateway.Middleware;
using Materal.Gateway.Service;
using Materal.Gateway.Service.Models.Route;
using Materal.Utils.Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using MMLib.SwaggerForOcelot.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace Materal.Gateway.Application
{
    /// <summary>
    /// 网关模块
    /// </summary>
    public partial class GatewayModule() : MergeBlockModule("网关模块")
    {
        [GeneratedRegex("\\{.+\\}")]
        private static partial Regex UpstreamPathTemplateRegex();
        /// <summary>
        /// 配置服务前
        /// </summary>
        /// <param name="context"></param>
        public override void OnPreConfigureServices(ServiceConfigurationContext context)
        {
            string filePath = Path.Combine(GetType().Assembly.GetDirectoryPath(), "Ocelot.json");
            if (context.Configuration is ConfigurationManager configuration)
            {
                configuration.AddJsonFile(filePath, false, true);
            }
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            if (context.Configuration is not null)
            {
                context.Services.Configure<ApplicationConfig>(context.Configuration);
            }
            context.Services.AddMateralConsulUtils();
            context.Services.AddSwaggerForOcelot(context.Configuration);
            context.Services.Configure<SwaggerForOcelotUIOptions>(options =>
            {
                options.ReConfigureUpstreamSwaggerJson = ReConfigureUpstreamSwaggerJson;
            });
            context.Services.AddGateway();
        }
        private static string ReConfigureUpstreamSwaggerJson(HttpContext context, string jsonDocument)
        {
            string key = context.Request.Path.ToString().Split('/').Last();
            IOcelotConfigService ocelotConfigService = context.RequestServices.GetRequiredService<IOcelotConfigService>();
            List<RouteConfigModel> routeConfigs = ocelotConfigService.GetRouteList(new QueryRouteModel
            {
                SwaggerKey = key
            });
            if (routeConfigs.Count == 0) return jsonDocument;
            RouteConfigModel configModel = routeConfigs.First();
            string upstreamPathTemplate = configModel.UpstreamPathTemplate;
            Match match = UpstreamPathTemplateRegex().Match(upstreamPathTemplate);
            upstreamPathTemplate = match.Success ? upstreamPathTemplate[..match.Index] : upstreamPathTemplate;
            if (upstreamPathTemplate[0] == '/')
            {
                upstreamPathTemplate = upstreamPathTemplate[1..];
            }
            if (upstreamPathTemplate.Last() == '/')
            {
                upstreamPathTemplate = upstreamPathTemplate[..^1];
            }
            JToken jToken = JToken.Parse(jsonDocument);
            if (jToken["paths"] is JObject paths)
            {
                Dictionary<string, JToken?> newPaths = [];
                foreach (KeyValuePair<string, JToken?> path in paths)
                {
                    string[] pathUrls = path.Key.Split('/');
                    if (pathUrls.Length <= 1)
                    {
                        newPaths.Add(path.Key, path.Value);
                    }
                    else
                    {
                        pathUrls[1] = upstreamPathTemplate;
                        newPaths.Add(string.Join('/', pathUrls), path.Value);
                    }
                }
                paths.RemoveAll();
                foreach (KeyValuePair<string, JToken?> newPath in newPaths)
                {
                    paths.Add(newPath.Key, newPath.Value);
                }
            }
            string result = JsonConvert.SerializeObject(jToken);
            return result;
        }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
        {
            AdvancedContext advancedContext = context.ServiceProvider.GetRequiredService<AdvancedContext>();
            if (advancedContext.App is WebApplication webApplication)
            {
                webApplication.UseSwaggerForOcelotUI();
                await webApplication.UseGateway();
                string managementPath = Path.Combine(GetType().Assembly.GetDirectoryPath(), "GatewayManagement");
                DirectoryInfo managementDirectoryInfo = new(managementPath);
                if (!managementDirectoryInfo.Exists)
                {
                    managementDirectoryInfo.Create();
                    managementDirectoryInfo.Refresh();
                }
                StaticFileOptions staticFileOptions = new()
                {
                    FileProvider = new PhysicalFileProvider(managementDirectoryInfo.FullName),
                    RequestPath = $"/{managementDirectoryInfo.Name}",
                };
                webApplication.UseStaticFiles(staticFileOptions);
                context.ServiceProvider.GetService<ILogger<GatewayModule>>()?.LogInformation($"已启用网关管理界面:/{managementDirectoryInfo.Name}/Index.html");
            }
            context.ServiceProvider.GetRequiredService<IOcelotConfigService>();
        }
    }
}
