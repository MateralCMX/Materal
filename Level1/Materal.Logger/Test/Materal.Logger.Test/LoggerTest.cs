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
        public async Task WriteSqliteLogTestAsync() => await WriteLogAsync(option
            => option.AddSqliteTargetFromPath("SqliteLogger", "${RootPath}\\Logs\\MateralLogger.db", "${Level}Log"));
    }
}