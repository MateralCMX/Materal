using Materal.MergeBlock.Swagger.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Materal.MergeBlock.Swagger
{
    /// <summary>
    /// Swagger模块
    /// </summary>
    [DependsOn(typeof(Web.WebModule))]
    public class SwaggerModule() : MergeBlockModule("Swagger模块")
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            IMvcBuilder? mvcBuilder = context.Services.GetSingletonInstance<IMvcBuilder>();
            if (mvcBuilder is null) return;
            MergeBlockContext? mergeBlockContext = context.Services.GetSingletonInstance<MergeBlockContext>();
            if (mergeBlockContext is null) return;
            SwaggerControllerModelConvention swaggerControllerModelConvention = new(mergeBlockContext);
            mvcBuilder.AddMvcOptions(options =>
            {
                options.Conventions.Add(swaggerControllerModelConvention);
            });
        }
        /// <summary>
        /// 配置服务之后
        /// </summary>
        /// <param name="context"></param>
        public override void OnPostConfigureServices(ServiceConfigurationContext context)
        {
            GlobalSwaggerOptions swaggerConfig = GetSwaggerConfig(context.Configuration);
            if (!swaggerConfig.Enable) return;
            MergeBlockContext? mergeBlockContext = context.Services.GetSingletonInstance<MergeBlockContext>();
            if (mergeBlockContext is null) return;
            IConfigurationSection? section = context.Configuration?.GetSection("MergeBlock:ApplicationName");
            string applicationName = "MateralMergeBlock应用程序";
            if (section is not null && !string.IsNullOrWhiteSpace(section.Value))
            {
                applicationName = section.Value;
            }
            context.Services.AddSwaggerGen(m => ConfigSwagger(m, mergeBlockContext, applicationName));
        }
        /// <summary>
        /// 配置Swagger
        /// </summary>
        /// <param name="config"></param>
        /// <param name="context"></param>
        /// <param name="applicationName"></param>
        private static void ConfigSwagger(SwaggerGenOptions config, MergeBlockContext context, string applicationName)
        {
            List<string> xmlFiles = [];
            foreach (ModuleDescriptor moduleDescriptor in context.ModuleDescriptors)
            {
                if (moduleDescriptor.Type.Assembly.GetName().Name == "Materal.MergeBlock.Authorization")
                {
                    OpenAuthorization(config);
                }
                Type? type = moduleDescriptor.Type.Assembly.GetTypeByFilter(m => m.IsAssignableTo<ControllerBase>());
                if (type is null) continue;
                ConfigSwaggerDoc(moduleDescriptor, config, applicationName);
                xmlFiles.AddRange(GetXMLDoc(moduleDescriptor));
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
        /// <param name="moduleDescriptor"></param>
        /// <returns></returns>
        private static string[] GetXMLDoc(ModuleDescriptor moduleDescriptor)
        {
            string? dllPath = moduleDescriptor.Type.Assembly.Location;
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
        /// <param name="moduleDescriptor"></param>
        /// <param name="config"></param>
        /// <param name="applicationName"></param>
        private static void ConfigSwaggerDoc(ModuleDescriptor moduleDescriptor, SwaggerGenOptions config, string applicationName)
        {
            string? groupName = SwaggerControllerModelConvention.GetGroupName(moduleDescriptor.Type.Assembly);
            if (string.IsNullOrWhiteSpace(groupName)) return;
            OpenApiInfo openApiInfo = new()
            {
                Title = moduleDescriptor.Instance.Name,
                Version = groupName,
                Description = $"提供WebAPI接口"
            };
            config.SwaggerDoc(groupName, openApiInfo);
        }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            IOptionsMonitor<GlobalSwaggerOptions> swaggerConfig = context.ServiceProvider.GetRequiredService<IOptionsMonitor<GlobalSwaggerOptions>>();
            if (!swaggerConfig.CurrentValue.Enable) return;
            AdvancedContext advancedContext = context.ServiceProvider.GetRequiredService<AdvancedContext>();
            MergeBlockContext mergeBlockContext = context.ServiceProvider.GetRequiredService<MergeBlockContext>();
            if (advancedContext.App is not WebApplication app) return;
            app.UseSwagger();
            app.UseSwaggerUI(m =>
            {
                foreach (ModuleDescriptor moduleDescriptor in mergeBlockContext.ModuleDescriptors)
                {
                    Type? type = moduleDescriptor.Type.Assembly.GetTypeByFilter(m => m.IsAssignableTo<ControllerBase>());
                    if (type is null) continue;
                    string? groupName = SwaggerControllerModelConvention.GetGroupName(moduleDescriptor.Type.Assembly);
                    if (string.IsNullOrWhiteSpace(groupName)) return;
                    m.SwaggerEndpoint($"/swagger/{groupName}/swagger.json", $"{groupName}");
                }
                IEnumerable<ISwaggerConfigService> swaggerConfigServices = context.ServiceProvider.GetServices<ISwaggerConfigService>();
                foreach (ISwaggerConfigService swaggerConfigService in swaggerConfigServices)
                {
                    swaggerConfigService.ConfigSwagger(m);
                }
            });
        }
        /// <summary>
        /// 获取Swagger配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static GlobalSwaggerOptions GetSwaggerConfig(IConfiguration? configuration)
        {
            GlobalSwaggerOptions swaggerConfig = configuration?.GetConfigItem<GlobalSwaggerOptions>(GlobalSwaggerOptions.ConfigKey) ?? new GlobalSwaggerOptions();
            return swaggerConfig;
        }
    }
}
