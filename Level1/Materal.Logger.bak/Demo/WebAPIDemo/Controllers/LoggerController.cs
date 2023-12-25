using Microsoft.AspNetCore.Mvc;

namespace WebAPIDemo.Controllers
{
    [ApiController, Route("/api/[controller]/[action]")]
    public class LoggerController : ControllerBase
    {
        private readonly ILogger<LoggerController> _logger;
        public LoggerController(ILogger<LoggerController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public void WriteLog(LogLevel logLevel, string message) => _logger.Log(logLevel, message);
        [HttpGet]
        public void WriteTraceLog(string message) => _logger.LogTrace(message);
        [HttpGet]
        public void WriteDebugLog(string message) => _logger.LogDebug(message);
        [HttpGet]
        public void WriteInformationLog(string message) => _logger.LogInformation(message);
        [HttpGet]
        public void WriteWarningLog(string message) => _logger.LogWarning(message);
        [HttpGet]
        public void WriteErrorLog(string message) => _logger.LogError(message);
        [HttpGet]
        public void WriteCriticalLog(string message) => _logger.LogCritical(message);
    }
}