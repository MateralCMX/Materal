using Materal.Abstractions;
using Materal.Logger;
using Microsoft.OpenApi.Models;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using WorkflowCore.Interface;
using WorkflowCore.Sample03;

namespace WorkflowWebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            AddWebAPIService(builder.Services);
            var app = builder.Build();
            WebApplicationConfig(app);
            await app.RunAsync();
        }
        /// <summary>
        /// 添加WebAPI服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="swaggerXmlPaths"></param>
        public static IServiceCollection AddWebAPIService(IServiceCollection services)
        {
            services.AddMateralLogger();
            services.AddWorkflow(m=>m.UseSqlite("Data Source=D:\\Project\\达远\\工作流\\WorkflowCore\\Workflow.db;", true));
            #region MVC
            IMvcBuilder mvcBuild = services.AddControllers(mvcOptions =>
            {
                mvcOptions.SuppressAsyncSuffixInActionNames = true;
            })
            .AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All, UnicodeRanges.All);
                jsonOptions.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = new FirstUpperNamingPolicy();
            });
            #endregion
            #region 响应压缩
            services.AddResponseCompression();
            #endregion
            #region Swagger
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title ="WorkflowWebAPI.WebAPI",
                    Version = "v1",
                    Description = "提供WebAPI接口",
                    Contact = new OpenApiContact { Name = "Materal", Email = "cloomcmx1554@hotmail.com" }
                });
            });
            #endregion
            services.AddEndpointsApiExplorer();
            return services;
        }
        /// <summary>
        /// WebApplication配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="consulTag">服务发现标签</param>
        /// <returns></returns>
        public static WebApplication WebApplicationConfig(WebApplication app)
        {
            MateralServices.Services = app.Services;
            var host = app.Services.GetService<IWorkflowHost>() ?? throw new Exception("获取失败");
            host.RegisterWorkflow<PassingDataWorkflow, MyDataClass>();
            host.Start();
            app.Use(async (context, next) =>
            {
                context.Request.EnableBuffering();
                await next.Invoke();
            });
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapControllers();
            app.UseCors();
            return app;
        }
    }
}