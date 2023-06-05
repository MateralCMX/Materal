using Materal.Abstractions;
using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.SqliteRepository;
using Materal.BusinessFlow.WebAPIControllers.Filters;
using Materal.Logger;
using Materal.TTA.ADONETRepository;
using Materal.TTA.Common;
using Materal.TTA.Common.Model;
using Materal.Utils;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Materal.BusinessFlow.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.Title = "ÒµÎñÁ÷DemoAPI";
            MateralConfig.PageStartNumber = 1;
            var builder = WebApplication.CreateBuilder(args);
            builder.Services
                .AddControllers(options =>
                {
                    options.Filters.Add<ExceptionFilter>();
                })
                .AddApplicationPart(Assembly.Load("Materal.BusinessFlow.WebAPIControllers"))
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                })
                .AddNewtonsoftJson(jsonOptions =>
                {
                    jsonOptions.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMateralUtils();
            builder.Services.AddMateralLogger();
            builder.Services.AddBusinessFlow();
            SqliteConfigModel dbConfig = new() { Source = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BusinessFlow.db") };
            BusinessFlowDBOption oscillatorDB = new(dbConfig);
            builder.Services.AddBusinessFlowSqliteRepository(oscillatorDB);
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(m =>
                {
                    m.AllowAnyHeader();
                    m.AllowAnyMethod();
                    m.AllowAnyOrigin();
                });
            });
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors();
            app.UseAuthorization();
            app.MapControllers();
#if DEBUG
            LoggerManager.Init(option =>
            {
                option.AddConsoleTarget("LifeConsole", null, new Dictionary<LogLevel, ConsoleColor>
                {
                    [LogLevel.Error] = ConsoleColor.DarkRed
                });
                option.AddAllTargetRule(LogLevel.Information, null, new[] { "Microsoft.AspNetCore.*" });
            });
#else
            LoggerManager.Init(option =>
            {
                option.AddConsoleTarget("LifeConsole", "${DateTime}|${Level}:${Message}\r\n${Exception}", new Dictionary<LogLevel, ConsoleColor>
                {
                    [LogLevel.Error] = ConsoleColor.DarkRed
                });
                option.AddAllTargetRule(LogLevel.Information, null, new[] { "Microsoft.AspNetCore.*", "Microsoft.Hosting.Lifetime" });
            });
#endif
            using IServiceScope scope = app.Services.CreateScope();
            IMigrateHelper migrateHelper = scope.ServiceProvider.GetRequiredService<IMigrateHelper<BusinessFlowDBOption>>();
            await migrateHelper.MigrateAsync();
            IBusinessFlowHost host = scope.ServiceProvider.GetRequiredService<IBusinessFlowHost>();
            await host.RunAllAutoNodeAsync();
            await app.RunAsync();
        }
    }
}