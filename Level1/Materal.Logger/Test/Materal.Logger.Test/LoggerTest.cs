namespace Materal.Logger.Test
{
    [TestClass]
    public partial class LoggerTest
    {
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
        /// 写总线日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteBusLogTestAsync()
        {
            TestLogMonitor testLogMonitor = new();
            BusLoggerWriter.Subscribe(testLogMonitor);
            await WriteLogAsync(option => option.AddBusTarget("BusLog"));
        }
        /// <summary>
        /// 写Sqlite日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteSqliteLogTestAsync() => await WriteLogAsync(option =>
        {
            const string path = "${RootPath}\\Logs\\MateralLogger.db";
            option.AddSqliteTargetFromPath("SqliteLogger", path, "${Level}Log");
        });
        /// <summary>
        /// 写SqlServer日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteSqlServerLogTestAsync() => await WriteLogAsync(option =>
        {
            const string connectionString = "Data Source=127.0.0.1;Database=MateralLoggerTestDB; User ID=sa; Password=Materal@1234;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;";
            option.AddSqlServerTarget("SqlServerLogger", connectionString, "${Level}Log");
        });
        /// <summary>
        /// 写MySql日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteMySqlLogTestAsync() => await WriteLogAsync(option =>
        {
            const string connectionString = "Server=127.0.0.1;Port=3306;Database=MateralLoggerTestDB;Uid=root;Pwd=Materal@1234;AllowLoadLocalInfile=true;";
            option.AddMySqlTarget("MySqlLogger", connectionString, "${Level}Log");
        });
        /// <summary>
        /// 写Oracle日志
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteOracleLogTestAsync() => await WriteLogAsync(option =>
        {
            const string connectionString = "user id=MATERAL; Password=Materal1234; data source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST =127.0.0.1)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = ORCL)));VALIDATE CONNECTION=True;";
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
            option.AddMongoTarget("MongoLogger", connectionString);
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
    }
}