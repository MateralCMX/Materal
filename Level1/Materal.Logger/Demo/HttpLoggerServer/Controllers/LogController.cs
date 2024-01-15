using Microsoft.AspNetCore.Mvc;

namespace HttpLoggerServer.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class LogController : ControllerBase
    {
        private static int _count;
        [HttpPost]
        public string WriteLog(List<LogMessageModel> logModel)
        {
            _count += logModel.Count;
            ConsoleQueue.WriteLine(_count);
            return "OK";
        }
        [HttpPost]
        public string WriteTrace(List<LogMessageModel> logModel)
        {
            _count += logModel.Count;
            ConsoleQueue.WriteLine(logModel.ToJson(), ConsoleColor.DarkGray);
            return "OK";
        }
        [HttpPost]
        public string WriteDebug(List<LogMessageModel> logModel)
        {
            _count += logModel.Count;
            ConsoleQueue.WriteLine(logModel.ToJson(), ConsoleColor.DarkGreen);
            return "OK";
        }
        [HttpPost]
        public string WriteInformation(List<LogMessageModel> logModel)
        {
            _count += logModel.Count;
            ConsoleQueue.WriteLine(logModel.ToJson(), ConsoleColor.Gray);
            return "OK";
        }
        [HttpPost]
        public string WriteWarning(List<LogMessageModel> logModel)
        {
            _count += logModel.Count;
            ConsoleQueue.WriteLine(logModel.ToJson(), ConsoleColor.DarkYellow);
            return "OK";
        }
        [HttpPost]
        public string WriteError(List<LogMessageModel> logModel)
        {
            _count += logModel.Count;
            ConsoleQueue.WriteLine(logModel.ToJson(), ConsoleColor.DarkRed);
            return "OK";
        }
        [HttpPost]
        public string WriteCritical(List<LogMessageModel> logModel)
        {
            _count += logModel.Count;
            ConsoleQueue.WriteLine(logModel.ToJson(), ConsoleColor.Red);
            return "OK";
        }
    }
}