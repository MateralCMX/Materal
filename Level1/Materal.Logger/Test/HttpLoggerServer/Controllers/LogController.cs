using Materal.Utils;
using Microsoft.AspNetCore.Mvc;

namespace HttpLoggerServer.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class LogController : ControllerBase
    {
        private static int _count;
        [HttpPost]
        public string WriteLog(List<LogModel> logModel)
        {
            _count += logModel.Count;
            ConsoleQueue.WriteLine(_count);
            return "OK";
        }
        [HttpPost]
        public string WriteTrace(List<LogModel> logModel)
        {
            _count += logModel.Count;
            ConsoleQueue.WriteLine(logModel.ToJson(), ConsoleColor.DarkGray);
            return "OK";
        }
        [HttpPost]
        public string WriteDebug(List<LogModel> logModel)
        {
            _count += logModel.Count;
            ConsoleQueue.WriteLine(logModel.ToJson(), ConsoleColor.DarkGreen);
            return "OK";
        }
        [HttpPost]
        public string WriteInformation(List<LogModel> logModel)
        {
            _count += logModel.Count;
            ConsoleQueue.WriteLine(logModel.ToJson(), ConsoleColor.Gray);
            return "OK";
        }
        [HttpPost]
        public string WriteWarning(List<LogModel> logModel)
        {
            _count += logModel.Count;
            ConsoleQueue.WriteLine(logModel.ToJson(), ConsoleColor.DarkYellow);
            return "OK";
        }
        [HttpPost]
        public string WriteError(List<LogModel> logModel)
        {
            _count += logModel.Count;
            ConsoleQueue.WriteLine(logModel.ToJson(), ConsoleColor.DarkRed);
            return "OK";
        }
        [HttpPost]
        public string WriteCritical(List<LogModel> logModel)
        {
            _count += logModel.Count;
            ConsoleQueue.WriteLine(logModel.ToJson(), ConsoleColor.Red);
            return "OK";
        }
    }
    public class LogModel
    {
        /// <summary>
        /// Ψһ��ʶ
        /// </summary>
        public Guid ID { get; set; }
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