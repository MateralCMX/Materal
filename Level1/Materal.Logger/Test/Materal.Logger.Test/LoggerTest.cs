using Materal.Utils;

namespace Materal.Logger.Test
{
    [TestClass]
    public partial class LoggerTest
    {
        private const LogLevel _minLoggerInfoLevel = LogLevel.Trace;
        /// <summary>
        /// 写控制台日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteConsoleLogTestAsync() => await WriteLogAsync(option
            => option.AddConsoleTarget("ConsoleLog", _textFormat));
        /// <summary>
        /// 写文件日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteFileLogTestAsync() => await WriteLogAsync(option
            => option.AddFileTarget("FileLogger", "${RootPath}\\Logs\\${Level}.log", _textFormat));
        /// <summary>
        /// 写Http日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteHttpLogTestAsync() => await WriteLogAsync(option
            => option.AddHttpTarget("HttpLogger", "http://localhost:5000/api/Log/Write${Level}", HttpMethod.Post));
        /// <summary>
        /// 写Sqlite日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteSqliteLogTestAsync() => await WriteLogAsync(option =>
        {
            const string path = "${RootPath}\\Logs\\Logs.db";
            option.AddSqliteTargetFromPath("SqliteLogger", path, "${Level}Log");
        });
        /// <summary>
        /// 写SqlServer日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteSqlServerLogTestAsync() => await WriteLogAsync(option =>
        {
            const string connectionString = "Data Source=127.0.0.1;Database=Logs; User ID=sa; Password=Materal@1234;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;";
            option.AddSqlServerTarget("SqlServerLogger", connectionString, "${Level}Log");
        });
        /// <summary>
        /// 写MySql日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteMySqlLogTestAsync() => await WriteLogAsync(option =>
        {
            const string connectionString = "Server=127.0.0.1;Port=3306;Database=Logs;Uid=root;Pwd=Materal@1234;AllowLoadLocalInfile=true;";
            option.AddMySqlTarget("MySqlLogger", connectionString, "${Level}Log");
        });
        /// <summary>
        /// 写Oracle日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteOracleLogTestAsync() => await WriteLogAsync(option =>
        {
            const string connectionString = "user id=LOGS; Password=Materal1234; data source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST =127.0.0.1)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = ORCL)));VALIDATE CONNECTION=True;";
            option.AddOracleTarget("OracleLogger", connectionString, "${Level}Log");
        });
        /// <summary>
        /// 写Mongo日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteMongoLogTestAsync() => await WriteLogAsync(option =>
        {
            const string connectionString = "mongodb://localhost:27017/";
            option.AddMongoTarget("MongoLogger", connectionString, "Logs", "${Level}Logs");
        });
        /// <summary>
        /// 写WebSocket日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteWebSocketLogTestAsync() => await WriteLogAsync(option =>
        {
            option.AddWebSocketTarget("WebSocketLogger", 5002);
        }, async services => await WriteLoopLogsAsync(services, 10, 1000));
        /// <summary>
        /// 写WebSocket日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteLoggerListenerLogTestAsync() => await WriteLogAsync(null, services =>
        {
            ILoggerListener loggerListener = services.GetRequiredService<ILoggerListener>();
            using IDisposable observer = loggerListener.Subscribe(log =>
            {
                ConsoleQueue.WriteLine(log.Message);
            });
            WriteLogs(services, 10);
            WriteThreadLogs(services, 100);
            WriteLargeLogs(services, 10000);
        });
        /// <summary>
        /// 写WebSocket日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteConfigLogTestAsync() => await WriteLogAsync(null, true);
    }
}