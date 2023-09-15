using Materal.Utils;
using Microsoft.AspNetCore.Mvc;

namespace HttpLoggerServer.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class LogController : ControllerBase
    {
        public static int count = 0;
        [HttpPost]
        public string WriteLog(List<LogModel> logModel)
        {
            count += logModel.Count;
            ConsoleQueue.WriteLine(count);
            return "OK";
        }
        [HttpPost]
        public string WriteTrace(List<LogModel> logModel)
        {
            count += logModel.Count;
            ConsoleQueue.WriteLine(logModel.ToJson(), ConsoleColor.DarkGray);
            return "OK";
        }
        [HttpPost]
        public string WriteDebug(List<LogModel> logModel)
        {
            count += logModel.Count;
            ConsoleQueue.WriteLine(logModel.ToJson(), ConsoleColor.DarkGreen);
            return "OK";
        }
        [HttpPost]
        public string WriteInformation(List<LogModel> logModel)
        {
            count += logModel.Count;
            ConsoleQueue.WriteLine(logModel.ToJson(), ConsoleColor.Gray);
            return "OK";
        }
        [HttpPost]
        public string WriteWarning(List<LogModel> logModel)
        {
            count += logModel.Count;
            ConsoleQueue.WriteLine(logModel.ToJson(), ConsoleColor.DarkYellow);
            return "OK";
        }
        [HttpPost]
        public string WriteError(List<LogModel> logModel)
        {
            count += logModel.Count;
            ConsoleQueue.WriteLine(logModel.ToJson(), ConsoleColor.DarkRed);
            return "OK";
        }
        [HttpPost]
        public string WriteCritical(List<LogModel> logModel)
        {
            count += logModel.Count;
            ConsoleQueue.WriteLine(logModel.ToJson(), ConsoleColor.Red);
            return "OK";
        }
    }
    public class LogModel
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 日志等级
        /// </summary>
        public string Level { get; set; } = string.Empty;
        /// <summary>
        /// 进程ID
        /// </summary>
        public string ProgressID { get; set; } = string.Empty;
        /// <summary>
        /// 线程ID
        /// </summary>
        public string ThreadID { get; set; } = string.Empty;
        /// <summary>
        /// 作用域
        /// </summary>
        public string Scope { get; set; } = string.Empty;
        /// <summary>
        /// 计算机名称
        /// </summary>
        public string? MachineName { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string? CategoryName { get; set; }
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string Application { get; set; } = string.Empty;
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// 错误
        /// </summary>
        public string? Error { get; set; }
    }
}