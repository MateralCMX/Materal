using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Materal.Logger.Test
{
    [TestClass]
    public partial class LoggerTest
    {
        private readonly IConfiguration _configuration;
        private const string _textFormat = "${DateTime}|${Application}|${Level}|${Scope}|${CategoryName}\r\n${Message}\r\n${Exception}";
        /// <summary>
        /// ���췽��
        /// </summary>
        public LoggerTest()
        {
            _configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .Build();
        }
        /// <summary>
        /// дTraceListener��־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteTraceListenerLogTestAsync()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddMateralLogger(option =>
            {
                option.SetLoggerLogLevel(LogLevel.Trace);
            });
            IServiceProvider services = serviceCollection.BuildServiceProvider();
            LoggerRuntime loggerRuntime = services.GetRequiredService<LoggerRuntime>();
            WriteLogs(services);
            await loggerRuntime.ShutdownAsync();
            Trace.WriteLine("[Trace]Hello World!");
            Debug.WriteLine("[Debug]Hello World!");
            await Task.Delay(5000);
            await loggerRuntime.ShutdownAsync();
        }

        /// <summary>
        /// д����̨��־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteConsoleLogTestAsync()
            => await WriteLogAsync(option =>
            {
                option.AddConsoleTarget("ConsoleLogger", _textFormat);
            });
        /// <summary>
        /// д�ļ���־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteFileLogTestAsync()
            => await WriteLogAsync(option => option.AddFileTarget("FileLogger", "Logs/${Level}.log", _textFormat));
        /// <summary>
        /// дHttp��־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteHttpLogTestAsync()
            => await WriteLogAsync(option => option.AddHttpTarget("HttpLogger", "http://localhost:5000/api/Log/Write${Level}", HttpMethod.Post));
        /// <summary>
        /// дSqlite��־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteSqliteLogTestAsync()
            => await WriteLogAsync(option => option.AddSqliteTargetFromPath("SqliteLogger", "${RootPath}\\Logs\\MateralLogger.db", "${Level}Log"));
        /// <summary>
        /// дSqlServer��־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteSqlServerLogTestAsync()
            => await WriteLogAsync(option =>
            {
                const string connectionString = "Data Source=82.156.11.176;Database=LogTestDB; User ID=sa; Password=gdb@admin678;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;";
                option.AddSqlServerTarget("SqlServerLogger", connectionString, "${Level}Log");
            });
        /// <summary>
        /// дWebSocket��־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteWebSocketLogTestAsync()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddMateralLogger(option =>
            {
                option.AddWebSocketTarget("WebSocketLogger", 5002);
                option.AddAllTargetRule(LogLevel.Trace);
                option.SetLoggerLogLevel(LogLevel.Trace);
            });
            IServiceProvider services = serviceCollection.BuildServiceProvider();
            LoggerRuntime loggerRuntime = services.GetRequiredService<LoggerRuntime>();
            await WriteLoopLogsAsync(services, 10, 1000);
            await loggerRuntime.ShutdownAsync();
        }
        /// <summary>
        /// д�����ļ���־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteConfigFileLogTestAsync()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(_configuration);
            serviceCollection.AddMateralLogger(option =>
            {
                option.AddCustomConfig("ApplicationName", "MateralLoggerTest");
            });
            IServiceProvider services = serviceCollection.BuildServiceProvider();
            LoggerRuntime loggerRuntime = services.GetRequiredService<LoggerRuntime>();
            await WriteLoopLogsAsync(services, 10, 1000);
            await loggerRuntime.ShutdownAsync();
        }

        /// <summary>
        /// д��־
        /// </summary>
        /// <param name="services"></param>
        /// <param name="loadConfigFile"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task WriteLogAsync(Action<LoggerConfigOptions>? action, bool loadConfigFile = false)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            if (loadConfigFile)
            {
                serviceCollection.AddSingleton(_configuration);
            }
            serviceCollection.AddMateralLogger(option =>
            {
                action?.Invoke(option);
                option.AddAllTargetRule(LogLevel.Trace);
                option.SetLoggerLogLevel(LogLevel.Trace);
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