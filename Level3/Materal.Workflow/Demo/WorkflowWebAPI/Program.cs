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
        /// ���WebAPI����
        /// </summary>
        /// <param name="services"></param>
        /// <param name="swaggerXmlPaths"></param>
        public static IServiceCollection AddWebAPIService(IServiceCollection services)
        {
            services.AddMateralLogger();
            services.AddWorkflow(m=>m.UseSqlite("Data Source=D:\\Project\\��Զ\\������\\WorkflowCore\\Workflow.db;", true));
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
            #region ��Ӧѹ��
            services.AddResponseCompression();
            #endregion
            #region Swagger
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title ="WorkflowWebAPI.WebAPI",
                    Version = "v1",
                    Description = "�ṩWebAPI�ӿ�",
                    Contact = new OpenApiContact { Name = "Materal", Email = "cloomcmx1554@hotmail.com" }
                });
            });
            #endregion
            services.AddEndpointsApiExplorer();
            return services;
        }
        /// <summary>
        /// WebApplication����
        /// </summary>
        /// <param name="app"></param>
        /// <param name="consulTag">�����ֱ�ǩ</param>
        /// <returns></returns>
        public static WebApplication WebApplicationConfig(WebApplication app)
        {
            MateralServices.Services = app.Services;
            var host = app.Services.GetService<IWorkflowHost>() ?? throw new Exception("��ȡʧ��");
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