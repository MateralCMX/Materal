using Materal.Utils.MongoDB.Extensions;

namespace Materal.Logger.Test
{
    [TestClass]
    public class LoggerTest
    {
        private readonly IConfiguration _configuration;
        private const string _textFormat = "${DateTime}|${Application}|${Level}|${Scope}|${CategoryName}\r\n${Message}\r\n${Exception}";
        /// <summary>
        /// ���췽��
        /// </summary>
        public LoggerTest()
        {
            _configuration = new ConfigurationBuilder()
                        .AddJsonFile("MateralLogger.json", optional: true, reloadOnChange: true)
                        .Build();
        }
        /// <summary>
        /// д������־����
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteErrorLogTestAsync() => await WriteLogAsync(option =>
        {
            option.AddConsoleTarget("ConsoleLogger", _textFormat);
        }, services =>
        {
            ILogger<LoggerTest> logger = services.GetRequiredService<ILogger<LoggerTest>>();
            Exception exception = new NotImplementedException("������Ϣ");
            logger.LogError(exception, "����һ��������־��Ϣ");
            try
            {
                throw new NotImplementedException("������Ϣ");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "����һ��������־��Ϣ");
            }
        });
        /// <summary>
        /// д�༶��־����
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteMultiLevelTextLogTestAsync() => await WriteLogAsync(option =>
        {
            option.AddConsoleTarget("ConsoleLogger", _textFormat);
        }, services =>
        {
            ILogger<LoggerTest> logger = services.GetRequiredService<ILogger<LoggerTest>>();
            Dictionary<string, object?> data = new()
            {
                ["NullValue"] = null,
                ["StringValue"] = "StringValue",
                ["IntValue"] = 1,
                ["DecimalValue"] = 1.1m,
                ["DateTimeValue"] = DateTime.Now,
                ["GuidValue"] = Guid.NewGuid(),
                ["ListValue"] = new List<object> { "0", 1, "22" },
                ["ArrayValue"] = new object[] { "0", 1, "22" },
                ["DictionaryValue"] = new Dictionary<string, object?>
                {
                    ["NullValue"] = null,
                    ["StringValue"] = "StringValue",
                    ["IntValue"] = 1,
                    ["DecimalValue"] = 1.1m,
                    ["DateTimeValue"] = DateTime.Now,
                    ["GuidValue"] = Guid.NewGuid()
                },
                ["ObjectValue"] = new
                {
                    StringValue = "StringValue",
                    IntValue = 1,
                    DecimalValue = 1.1m,
                    DateTimeValue = DateTime.Now,
                    GuidValue = Guid.NewGuid(),
                    ObjectValue = new { DateTimeValue = DateTime.Now },
                    DictionaryValue = new Dictionary<string, object?>
                    {
                        ["NullValue"] = null,
                        ["StringValue"] = "StringValue",
                        ["IntValue"] = 1,
                        ["DecimalValue"] = 1.1m,
                        ["DateTimeValue"] = DateTime.Now,
                        ["GuidValue"] = Guid.NewGuid()
                    },
                }
            };
            using IDisposable? scope = logger.BeginScope(new AdvancedScope("CustomScope", data));
            logger.LogInformation("${ObjectValue.DictionaryValue}");
            logger.LogInformation("${NullValue}");
            logger.LogInformation("${DictionaryValue.NullValue}");
            logger.LogInformation("${ObjectValue}");
            logger.LogInformation("${ObjectValue.ObjectValue}");
            logger.LogInformation("${ListValue}");
            logger.LogInformation("${StringValue}|${IntValue}|${DecimalValue}|${DateTimeValue}|${GuidValue}");
            logger.LogInformation("${ListValue[0]}|${ListValue[1]}|${ListValue[2]}");
            logger.LogInformation("${ArrayValue[0]}|${ArrayValue[1]}|${ArrayValue[2]}");
            logger.LogInformation("${DictionaryValue.StringValue}|${DictionaryValue.IntValue}|${DictionaryValue.DecimalValue}|${DictionaryValue.DateTimeValue}|${DictionaryValue.GuidValue}");
            logger.LogInformation("${ObjectValue.StringValue}|${ObjectValue.IntValue}|${ObjectValue.DecimalValue}|${ObjectValue.DateTimeValue}|${ObjectValue.GuidValue}");
        });
        /// <summary>
        /// д����̨��־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteConsoleLogTestAsync() => await WriteLogAsync(option =>
        {
            option.AddConsoleTarget("ConsoleLogger", _textFormat);
        });
        /// <summary>
        /// д�ļ���־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteFileLogTestAsync() => await WriteLogAsync(option =>
        {
            option.AddFileTarget("FileLogger", "${RootPath}\\Logs\\${Level}.log", _textFormat);
        });
        /// <summary>
        /// дHttp��־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteHttpLogTestAsync() => await WriteLogAsync(option =>
        {
            option.AddHttpTarget("HttpLogger", "http://localhost:5000/api/Log/Write${Level}", HttpMethod.Post);
        });
        /// <summary>
        /// дSqlite��־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteSqliteLogTestAsync() => await WriteLogAsync(option =>
        {
            option.AddSqliteTargetFromPath("SqliteLogger", "${RootPath}\\Logs\\MateralLogger.db", "${Level}Log");
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
            option.AddOracleTarget("OracleLogger", connectionString, "TestLogger");
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
        /// <summary>
        /// д�����ļ���־
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteConfigFileLogTestAsync() => await WriteLogAsync(option =>
        {
            option.AddCustomConfig("ApplicationName", "MateralLoggerTest");
        }, true);
        #region ��ݷ���
        /// <summary>
        /// д��־
        /// </summary>
        /// <param name="action"></param>
        /// <param name="testFunc"></param>
        /// <param name="loadConfigFile"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task WriteLogAsync(Action<LoggerConfig>? action, bool loadConfigFile = false) => await WriteLogAsync(action, services =>
        {
            WriteLogs(services, 10);
            WriteThreadLogs(services, 100);
            WriteLargeLogs(services, LogLevel.Information, 10000);
        }, loadConfigFile);
        /// <summary>
        /// д��־
        /// </summary>
        /// <param name="action"></param>
        /// <param name="testFunc"></param>
        /// <param name="loadConfigFile"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task WriteLogAsync(Action<LoggerConfig>? action, Action<IServiceProvider> testFunc, bool loadConfigFile = false)
        {
            IServiceProvider services = await InitAsync(action, loadConfigFile);
            testFunc.Invoke(services);
            await LoggerHost.ShutdownAsync();
        }
        /// <summary>
        /// д��־
        /// </summary>
        /// <param name="action"></param>
        /// <param name="testFunc"></param>
        /// <param name="loadConfigFile"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task WriteLogAsync(Action<LoggerConfig>? action, Func<IServiceProvider, Task> testFunc, bool loadConfigFile = false)
        {
            IServiceProvider services = await InitAsync(action, loadConfigFile);
            await testFunc.Invoke(services);
            await LoggerHost.ShutdownAsync();
        }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="action"></param>
        /// <param name="loadConfigFile"></param>
        /// <returns></returns>
        private async Task<IServiceProvider> InitAsync(Action<LoggerConfig>? action, bool loadConfigFile = false)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
            {
                if (loadConfigFile)
                {
                    builder.AddMateralLogger(_configuration);
                }
                else
                {
                    builder.AddMateralLogger();
                }
                builder.AddMateralLoggerConfig(option =>
                {
                    action?.Invoke(option);
                    if (!loadConfigFile)
                    {
                        option.AddAllTargetsRule();
                    }
                });
            });
            serviceCollection.AddMongoUtils();
            IServiceProvider services = serviceCollection.BuildServiceProvider();
            await services.UseMateralLoggerAsync();
            return services;
        }
        /// <summary>
        /// д��־
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        private static void WriteLogs(ILogger logger, string message)
        {
            logger.LogTrace(message);
            logger.LogDebug(message);
            logger.LogInformation(message);
            logger.LogWarning(message);
            logger.LogError(message);
            logger.LogCritical(message);
        }
        /// <summary>
        /// д��־
        /// </summary>
        /// <param name="message"></param>
        private static void WriteLog(IServiceProvider services, LogLevel logLevel, string message)
        {
            ILogger<LoggerTest> logger = services.GetRequiredService<ILogger<LoggerTest>>();
            logger.Log(logLevel, message);
        }
        /// <summary>
        /// д��־
        /// </summary>
        /// <param name="services"></param>
        /// <param name="count"></param>
        public static void WriteLogs(IServiceProvider services, int count = 1)
        {
            ILogger<LoggerTest> logger = services.GetRequiredService<ILogger<LoggerTest>>();
            for (int i = 0; i < count; i++)
            {
                WriteLogs(logger, $"{i}����һ����־��Ϣ");
            }
            using (IDisposable? scope = logger.BeginScope("CustomScope"))
            {
                for (int i = 0; i < count; i++)
                {
                    WriteLogs(logger, $"{i}����һ����־��Ϣ");
                }
            }
            AdvancedScope advancedScope = new("AdvancedScope", new Dictionary<string, object?>
            {
                ["UserID"] = Guid.NewGuid()
            });
            using (IDisposable? scope = logger.BeginScope(advancedScope))
            {
                for (int i = 0; i < count; i++)
                {
                    advancedScope.ScopeData["CustomData"] = Guid.NewGuid();
                    WriteLogs(logger, $"{i}����һ���߼���������־��Ϣ[${{UserID}}]");
                }
            }
        }
        /// <summary>
        /// дѭ����־
        /// </summary>
        /// <param name="services"></param>
        /// <param name="loopCount"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static async Task WriteLoopLogsAsync(IServiceProvider services, int loopCount = 10, int interval = 1000)
        {
            ILogger<LoggerTest> logger = services.GetRequiredService<ILogger<LoggerTest>>();
            for (int i = 0; i < loopCount; i++)
            {
                await Task.Delay(interval);
                WriteLogs(logger, $"����һ��ѭ����־[{i}]");
            }
        }
        /// <summary>
        /// д������־
        /// </summary>
        /// <param name="services"></param>
        /// <param name="logLevel"></param>
        /// <param name="count"></param>
        public static void WriteLargeLogs(IServiceProvider services, LogLevel logLevel = LogLevel.Information, int count = 10000)
        {
            ILogger<LoggerTest> logger = services.GetRequiredService<ILogger<LoggerTest>>();
            for (int i = 1; i <= count; i++)
            {
                logger.Log(logLevel, $"[{i:00000}]����һ��������¼����־");
            }
        }
        /// <summary>
        /// д���߳���־
        /// </summary>
        /// <param name="services"></param>
        /// <param name="threadCount"></param>
        public static void WriteThreadLogs(IServiceProvider services, int threadCount = 100)
        {
            ILoggerFactory loggerFactory = services.GetRequiredService<ILoggerFactory>();
            List<Task> tasks = [];
            for (int i = 0; i < threadCount; i++)
            {
                string index = i.ToString();
                Task task = Task.Run(() =>
                {
                    ILogger logger = loggerFactory.CreateLogger($"Thread{index}");
                    for (int i = 0; i < 10; i++)
                    {
                        WriteLogs(logger, $"[Thread{index}][{i}]����һ�����߳���־��Ϣ");
                    }
                });
                tasks.Add(task);
            }
            Task.WaitAll([.. tasks]);
        }
        #endregion
    }
}