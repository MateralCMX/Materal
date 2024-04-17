namespace Materal.Logger.Test
{
    [TestClass]
    public partial class LoggerTest
    {
        /// <summary>
        /// д����̨��־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteConsoleLogTestAsync() => await WriteLogAsync(option
            => option.AddConsoleTarget("ConsoleLog", _textFormat));
        /// <summary>
        /// д�ļ���־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteFileLogTestAsync() => await WriteLogAsync(option
            => option.AddFileTarget("FileLogger", "${RootPath}\\Logs\\${Level}.log", _textFormat));
        /// <summary>
        /// дHttp��־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteHttpLogTestAsync() => await WriteLogAsync(option
            => option.AddHttpTarget("HttpLogger", "http://localhost:5000/api/Log/Write${Level}", HttpMethod.Post));
        /// <summary>
        /// д������־
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
        /// дSqlite��־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteSqliteLogTestAsync() => await WriteLogAsync(option =>
        {
            const string path = "${RootPath}\\Logs\\MateralLogger.db";
            option.AddSqliteTargetFromPath("SqliteLogger", path, "${Level}Log");
        });
        /// <summary>
        /// дSqlServer��־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteSqlServerLogTestAsync() => await WriteLogAsync(option =>
        {
            const string connectionString = "Data Source=127.0.0.1;Database=MateralLoggerTestDB; User ID=sa; Password=Materal@1234;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;";
            option.AddSqlServerTarget("SqlServerLogger", connectionString, "${Level}Log");
        });
        /// <summary>
        /// дMySql��־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteMySqlLogTestAsync() => await WriteLogAsync(option =>
        {
            const string connectionString = "Server=127.0.0.1;Port=3306;Database=MateralLoggerTestDB;Uid=root;Pwd=Materal@1234;AllowLoadLocalInfile=true;";
            option.AddMySqlTarget("MySqlLogger", connectionString, "${Level}Log");
        });
        /// <summary>
        /// дOracle��־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteOracleLogTestAsync() => await WriteLogAsync(option =>
        {
            const string connectionString = "user id=MATERAL; Password=Materal1234; data source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST =127.0.0.1)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = ORCL)));VALIDATE CONNECTION=True;";
            option.AddOracleTarget("OracleLogger", connectionString, "${Level}Log");
        });
        /// <summary>
        /// дMongo��־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteMongoLogTestAsync() => await WriteLogAsync(option =>
        {
            const string connectionString = "mongodb://localhost:27017/";
            option.AddMongoTarget("MongoLogger", connectionString);
        });
        /// <summary>
        /// дWebSocket��־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteWebSocketLogTestAsync() => await WriteLogAsync(option =>
        {
            option.AddWebSocketTarget("WebSocketLogger", 5002);
        }, async services => await WriteLoopLogsAsync(services, 10, 1000));
    }
}