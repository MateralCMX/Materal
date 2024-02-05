using Materal.MergeBlock.Abstractions.WebModule;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Materal.MergeBlock.Swagger
{
    /// <summary>
    /// Swagger模块
    /// </summary>
    public class SwaggerModule : MergeBlockWebModule, IMergeBlockWebModule
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public SwaggerModule() : base("Swagger模块", "Swagger")
        {
        }
        /// <summary>
        /// 配置服务之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IWebConfigServiceContext context)
        {
            GlobalSwaggerConfig swaggerConfig = GetSwaggerConfig(context.Configuration);
            IOptionsMonitor<MergeBlockConfig> mergeBlockConfig = context.ServiceProvider.GetRequiredService<IOptionsMonitor<MergeBlockConfig>>();
            if (!swaggerConfig.Enable) return;
            context.Services.AddSwaggerGen(m => ConfigSwagger(m, mergeBlockConfig));
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 配置Swagger
        /// </summary>
        /// <param name="config"></param>
        /// <param name="mergeBlockConfig"></param>
        private void ConfigSwagger(SwaggerGenOptions config, IOptionsMonitor<MergeBlockConfig> mergeBlockConfig)
        {
            List<string> xmlFiles = [];
            foreach (IModuleInfo moduleInfo in MergeBlockHost.ModuleInfos)
            {
                if (!moduleInfo.ModuleType.IsAssignableTo<IMergeBlockWebModule>()) continue;
                MergeBlockAssemblyAttribute? mergeBlockAssemblyAttribute = moduleInfo.ModuleType.Assembly.GetCustomAttribute<MergeBlockAssemblyAttribute>();
                if (mergeBlockAssemblyAttribute is null) continue;
                if (mergeBlockAssemblyAttribute.HasController)
                {
                    ConfigSwaggerDoc(moduleInfo, config, mergeBlockConfig);
                    xmlFiles.AddRange(GetXMLDoc(moduleInfo));
                }
                if (moduleInfo.Name == "Authorization")
                {
                    OpenAuthorization(config);
                }
            }
            xmlFiles = xmlFiles.Distinct().ToList();
            foreach (string xmlFile in xmlFiles)
            {
                config.IncludeXmlComments(xmlFile);
            }
        }
        /// <summary>
        /// 开启授权
        /// </summary>
        /// <param name="config"></param>
        private static void OpenAuthorization(SwaggerGenOptions config)
        {
            OpenApiSecurityScheme bearerScheme = new()
            {
                Description = "在请求头部加入JWT授权。例子:Authorization:Bearer {token}",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            };
            config.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, bearerScheme);
            OpenApiSecurityRequirement openApiSecurityRequirement = new() { { bearerScheme, new List<string>() } };
            config.AddSecurityRequirement(openApiSecurityRequirement);
        }
        /// <summary>
        /// 获取XML文档
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <returns></returns>
        private static string[] GetXMLDoc(IModuleInfo moduleInfo)
        {
            string? dllPath = moduleInfo.ModuleType.Assembly.Location;
            if (dllPath is null) return [];
            string? dllFileName = Path.GetFileNameWithoutExtension(dllPath);
            if (dllFileName is null) return [];
            string? directoryPath = Path.GetDirectoryName(dllPath);
            if (directoryPath is null) return [];
            string[] xmlFilePaths = Directory.GetFiles(directoryPath, $"*.xml");
            return xmlFilePaths;
        }
        /// <summary>
        /// 配置Swagger文档
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="config"></param>
        /// <param name="mergeBlockConfig"></param>
        private static void ConfigSwaggerDoc(IModuleInfo moduleInfo, SwaggerGenOptions config, IOptionsMonitor<MergeBlockConfig> mergeBlockConfig)
        {
            OpenApiInfo openApiInfo = new()
            {
                Title = moduleInfo.Description ?? mergeBlockConfig.CurrentValue.ApplicationName,
                Version = moduleInfo.Name,
                Description = $"提供WebAPI接口"
            };
            config.SwaggerDoc(openApiInfo.Version, openApiInfo);
        }
        /// <summary>
        /// 配置应用程序
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAsync(IWebApplicationContext context)
        {
            IConfiguration configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
            GlobalSwaggerConfig swaggerConfig = GetSwaggerConfig(configuration);
            if (swaggerConfig.Enable)
            {
                context.WebApplication.UseSwagger();
                context.WebApplication.UseSwaggerUI(m =>
                {
                    foreach (IModuleInfo moduleInfo in MergeBlockHost.ModuleInfos)
                    {
                        if (!moduleInfo.ModuleType.IsAssignableTo<IMergeBlockWebModule>()) continue;
                        MergeBlockAssemblyAttribute? mergeBlockAssemblyAttribute = moduleInfo.ModuleType.Assembly.GetCustomAttribute<MergeBlockAssemblyAttribute>();
                        if (mergeBlockAssemblyAttribute is null || !mergeBlockAssemblyAttribute.HasController) continue;
                        m.SwaggerEndpoint($"/swagger/{moduleInfo.Name}/swagger.json", $"{moduleInfo.Name}");
                    }
                });
            }
            await base.OnApplicationInitAsync(context);
        }
        /// <summary>
        /// 获取Swagger配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static GlobalSwaggerConfig GetSwaggerConfig(IConfiguration configuration)
        {
            GlobalSwaggerConfig swaggerConfig = configuration.GetValueObject<GlobalSwaggerConfig>(GlobalSwaggerConfig.ConfigKey) ?? new GlobalSwaggerConfig();
            return swaggerConfig;
        }
    }
}
