using Materal.Logger;

namespace WebDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMateralLogger();
            var app = builder.Build();
            #region 配置文件方式
            app.UseMateralLogger(null, builder.Configuration);
            #endregion
            #region 代码方式
            //app.UseMateralLogger(option =>
            //{
            //    const string messageFormat = "${DateTime}|${Application}|${Level}|${Scope}|${CategoryName}\r\n${Message}";
            //    option.AddConsoleTarget("LifeConsole", messageFormat);
            //    //option.AddFileTarget("LevelFile", "${RootPath}\\Logs\\${Date}\\${Level}.log", _messageFormat);
            //    //option.AddSqliteTarget("LocalDB", "${RootPath}\\Logs\\Logger.db");
            //    option.AddAllTargetRule();
            //}, builder.Configuration);
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}