using Materal.MergeBlock.Application.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Materal.MergeBlock.LoggerTest.Controllers
{
    /// <summary>
    /// Logger²âÊÔ¿ØÖÆÆ÷
    /// </summary>
    public class LoggerTestController(ILogger<LoggerTestController> logger) : MergeBlockControllerBase
    {
        /// <summary>
        /// ËµHello
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public void WriteLogger()
        {
            logger.LogTrace("[Trace]Hello World!");
            logger.LogDebug("[Debug]Hello World!");
            logger.LogInformation("[Information]Hello World!");
            logger.LogWarning("[Warning]Hello World!");
            logger.LogError("[Error]Hello World!");
            logger.LogCritical("[Critical]Hello World!");
        }
    }
}
