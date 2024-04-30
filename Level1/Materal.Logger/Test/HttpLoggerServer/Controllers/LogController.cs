using Microsoft.AspNetCore.Mvc;

namespace HttpLoggerServer.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class LogController : ControllerBase
    {
        private static int _count;
        [HttpPost]
        public string WriteLog(List<Log> logModel)
        {
            _count += logModel.Count;
            ConsoleQueue.WriteLine(_count);
            return "OK";
        }
        [HttpPost]
        public string WriteTrace(List<Log> logModel)
        {
            _count += logModel.Count;
            ConsoleQueue.WriteLine(logModel.ToJson(), ConsoleColor.DarkGray);
            return "OK";
        }
        [HttpPost]
        public string WriteDebug(List<Log> logModel)
        {
            _count += logModel.Count;
            ConsoleQueue.WriteLine(logModel.ToJson(), ConsoleColor.DarkGreen);
            return "OK";
        }
        [HttpPost]
        public string WriteInformation(List<Log> logModel)
        {
            _count += logModel.Count;
            ConsoleQueue.WriteLine(logModel.ToJson(), ConsoleColor.Gray);
            return "OK";
        }
        [HttpPost]
        public string WriteWarning(List<Log> logModel)
        {
            _count += logModel.Count;
            ConsoleQueue.WriteLine(logModel.ToJson(), ConsoleColor.DarkYellow);
            return "OK";
        }
        [HttpPost]
        public string WriteError(List<Log> logModel)
        {
            _count += logModel.Count;
            ConsoleQueue.WriteLine(logModel.ToJson(), ConsoleColor.DarkRed);
            return "OK";
        }
        [HttpPost]
        public string WriteCritical(List<Log> logModel)
        {
            _count += logModel.Count;
            ConsoleQueue.WriteLine(logModel.ToJson(), ConsoleColor.Red);
            return "OK";
        }
    }
}