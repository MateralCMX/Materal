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