using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Materal.Logger.Test
{
    [TestClass]
    public partial class LoggerTest
    {
        //private readonly IConfiguration _configuration;
        private const string _textFormat = "${DateTime}|${Application}|${Level}|${Scope}|${CategoryName}\r\n${Message}\r\n${Exception}";
        ///// <summary>
        ///// 构造方法
        ///// </summary>
        //public LoggerTest()
        //{
        //    _configuration = new ConfigurationBuilder()
        //                .AddJsonFile("appsetting.json", optional: true, reloadOnChange: true)
        //                .Build();
        //}
        /// <summary>
        /// 写控制台日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteConsoleLogTestAsync()
            => await WriteLogAsync(option =>
            {
                //option.AddConsoleTarget("ConsoleLogger", _textFormat);
                option.AddConsoleTarget("ConsoleLogger1", _textFormat);
                option.AddConsoleTarget("ConsoleLogger2", _textFormat);
            });
        /// <summary>
        /// 写文件日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteFileLogTestAsync()
            => await WriteLogAsync(option => option.AddFileTarget("FileLogger", "Logs/${Level}.log", _textFormat));
        /// <summary>
        /// 写Http日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteHttpLogTestAsync()
            => await WriteLogAsync(option => option.AddHttpTarget("HttpLogger", "http://localhost:5000/api/Log/Write${Level}", HttpMethod.Post));
        /// <summary>
        /// 写Sqlite日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteSqliteLogTestAsync()
            => await WriteLogAsync(option => option.AddSqliteTargetFromPath("SqliteLogger", "${RootPath}\\Logs\\MateralLogger.db", "${Level}Log"));
        /// <summary>
        /// 写SqlServer日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteSqlServerLogTestAsync()
            => await WriteLogAsync(option =>
            {
                const string connectionString = "Data Source=82.156.11.176;Database=LogTestDB; User ID=sa; Password=gdb@admin678;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;";
                option.AddSqlServerTarget("SqlServerLogger", connectionString, "${Level}Log");
            });
        ///// <summary>
        ///// 写SqlServer日志
        ///// </summary>
        ///// <returns></returns>
        //[TestMethod]
        //public async Task WriteWebSocketLogTestAsync()
        //    => await WriteLogAsync(option =>
        //    {
        //        const string connectionString = "Data Source=82.156.11.176;Database=LogTestDB; User ID=sa; Password=gdb@admin678;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;";
        //        option.AddWebSocketTarget("SqlServerLogger", connectionString, "${Level}Log");
        //    });
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static async Task WriteLogAsync(Action<LoggerConfigOptions> action)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddMateralLogger(option =>
            {
                action(option);
                option.AddAllTargetRule(LogLevel.Trace);
                option.SetLoggerLogLevel(LogLevel.Warning);
            });
            IServiceProvider services = serviceCollection.BuildServiceProvider();
            LoggerRuntime loggerRuntime = services.GetRequiredService<LoggerRuntime>();
            WriteLogs(services);
            //WriteLargeLogs(services, LogLevel.Information, 10000);
            //WriteThreadLogs(services, 100);
            await loggerRuntime.ShutdownAsync();
        }
    }
}