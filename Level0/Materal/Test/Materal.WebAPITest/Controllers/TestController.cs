using Materal.Utils.Model;
using Materal.WebAPITest.Services;
using Microsoft.AspNetCore.Mvc;

namespace Materal.WebAPITest.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ITestService _testService;
        public TestController(IServiceProvider serviceProvider, ITestService testService)
        {
            _serviceProvider = serviceProvider;
            ILogger<TestController> logger = _serviceProvider.GetRequiredService<ILogger<TestController>>();
            logger.LogDebug(_serviceProvider.GetType().FullName);
            _testService = testService;
        }
        [HttpGet]
        public ResultModel<string> SayHello()
        {
            string message = _testService.SayHello();
            return ResultModel<string>.Success(message, "获取成功");
        }
        [HttpGet]
        public async Task<ResultModel<string>> SayHello2Async()
        {
            string message = await _testService.SayHelloAsync();
            return ResultModel<string>.Success(message, "获取成功");
        }
        [HttpGet]
        public ResultModel TestErrorAsync() => throw new NotImplementedException();
    }
}