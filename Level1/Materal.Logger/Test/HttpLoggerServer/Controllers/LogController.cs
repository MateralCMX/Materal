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
        /// ����ʱ��
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// ��־�ȼ�
        /// </summary>
        public string Level { get; set; } = string.Empty;
        /// <summary>
        /// ����ID
        /// </summary>
        public string ProgressID { get; set; } = string.Empty;
        /// <summary>
        /// �߳�ID
        /// </summary>
        public string ThreadID { get; set; } = string.Empty;
        /// <summary>
        /// ������
        /// </summary>
        public string Scope { get; set; } = string.Empty;
        /// <summary>
        /// ���������
        /// </summary>
        public string? MachineName { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public string? CategoryName { get; set; }
        /// <summary>
        /// Ӧ�ó�������
        /// </summary>
        public string Application { get; set; } = string.Empty;
        /// <summary>
        /// ��Ϣ
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// ����
        /// </summary>
        public string? Error { get; set; }
    }
}