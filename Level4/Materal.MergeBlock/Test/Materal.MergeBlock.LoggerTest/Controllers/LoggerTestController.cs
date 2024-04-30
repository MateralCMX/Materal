using Materal.MergeBlock.Application.WebModule.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Materal.MergeBlock.LoggerTest.Controllers
{
    /// <summary>
    /// Logger测试控制器
    /// </summary>
    public class LoggerTestController(ILogger<LoggerTestController> logger) : MergeBlockControllerBase
    {
        /// <summary>
        /// 写日志
        /// </summary>
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
