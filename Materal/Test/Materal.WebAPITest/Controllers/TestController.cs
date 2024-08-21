using Materal.Utils.Model;
using Materal.WebAPITest.Models;
using Materal.WebAPITest.Services;
using Microsoft.AspNetCore.Mvc;

namespace Materal.WebAPITest.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class TestController(ITestService testService) : ControllerBase
    {
        [HttpGet]
        public ResultModel<string> SayHello()
        {
            string message = testService.SayHello();
            return ResultModel<string>.Success(message, "获取成功");
        }
        [HttpGet]
        public async Task<ResultModel<string>> SayHello2Async()
        {
            string message = await testService.SayHelloAsync();
            return ResultModel<string>.Success(message, "获取成功");
        }
        [HttpPost]
        public ResultModel<string> SayHello3(User user)
        {
            string message = testService.SayHello(user.Name);
            return ResultModel<string>.Success(message, "获取成功");
        }
        [HttpGet]
        public ResultModel TestError() => throw new NotImplementedException("测试异常");
        [HttpGet]
        public async Task<ResultModel> TestAsyncErrorAsync()
        {
            await Task.CompletedTask;
            throw new NotImplementedException("测试异常");
        }
    }
}