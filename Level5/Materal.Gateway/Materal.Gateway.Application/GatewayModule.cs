using Materal.Gateway.OcelotExtension;
using Materal.Gateway.OcelotExtension.ConfigModel;
using Materal.Gateway.Service;
using Materal.Gateway.Service.Models.Route;
using Materal.MergeBlock.Abstractions.WebModule;
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
    public class GatewayModule() : MergeBlockWebModule("网关模块", "Gateway")
    {
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task OnConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            string filePath = Path.Combine(GetType().Assembly.GetDirectoryPath(), "Ocelot.json");
            if (context.Configuration is ConfigurationManager configuration)
            {
                configuration.AddJsonFile(filePath, false, true);
            }
            return base.OnConfigServiceBeforeAsync(context);
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IWebConfigServiceContext context)
        {
            context.Services.Configure<ApplicationConfig>(context.Configuration);
            context.Services.AddMateralConsulUtils();
            context.Services.AddSwaggerForOcelot(context.Configuration);
            context.Services.Configure<SwaggerForOcelotUIOptions>(options =>
            {
                options.ReConfigureUpstreamSwaggerJson = ReConfigureUpstreamSwaggerJson;
            });
            context.Services.AddOcelotGateway();
            await base.OnConfigServiceAsync(context);
        }
        private string ReConfigureUpstreamSwaggerJson(HttpContext context, string jsonDocument)
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
            Match match = Regex.Match(upstreamPathTemplate, "\\{.+\\}");
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
        /// 应用程序初始化之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitBeforeAsync(IWebApplicationContext context)
        {
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
            context.WebApplication.UseStaticFiles(staticFileOptions);
            context.ServiceProvider.GetService<ILogger<GatewayModule>>()?.LogInformation($"已启用网关管理界面:/{managementDirectoryInfo.Name}/Index.html");
            await base.OnApplicationInitBeforeAsync(context);
        }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAsync(IWebApplicationContext context)
        {
            context.WebApplication.UseSwaggerForOcelotUI();
            await context.WebApplication.UseOcelotGatewayAsync(true);
            IOcelotConfigService ocelotConfigService = context.ServiceProvider.GetRequiredService<IOcelotConfigService>();
            await ocelotConfigService.InitAsync();
        }
    }
}
