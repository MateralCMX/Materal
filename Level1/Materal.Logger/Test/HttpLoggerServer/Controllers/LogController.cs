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
            Console.WriteLine(count);
            //Console.WriteLine(logModel.ToJson());
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