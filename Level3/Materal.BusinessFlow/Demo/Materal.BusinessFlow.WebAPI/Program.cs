using Materal.Abstractions;
using Materal.BusinessFlow.SqliteRepository;
using Materal.Logger;
using Materal.TTA.ADONETRepository;
using Materal.TTA.Common;
using Materal.TTA.Common.Model;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Materal.BusinessFlow.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            MateralConfig.PageStartNumber = 1;
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers()
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
            LoggerManager.Init(option =>
            {
                option.AddConsoleTarget("LifeConsole", null, new Dictionary<LogLevel, ConsoleColor>
                {
                    [LogLevel.Error] = ConsoleColor.DarkRed
                });
                option.AddAllTargetRule(LogLevel.Information);
            });
            using IServiceScope scope = app.Services.CreateScope();
            IMigrateHelper migrateHelper = scope.ServiceProvider.GetRequiredService<IMigrateHelper<BusinessFlowDBOption>>();
            await migrateHelper.MigrateAsync();
            await app.RunAsync();
        }
    }
}