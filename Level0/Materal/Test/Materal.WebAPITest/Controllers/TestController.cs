using Materal.Extensions.DependencyInjection;
using Materal.Utils.Consul;
using Materal.Utils.Consul.Models;
using Materal.Utils.Model;
using Materal.WebAPITest.Services;
using Microsoft.AspNetCore.Mvc;

namespace Materal.WebAPITest.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private ITestService _testService => HttpContext.RequestServices.GetRequiredService<ITestService>();
        private IConsulService _consulService => HttpContext.RequestServices.GetRequiredService<IConsulService>();
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

        [HttpGet]
        public async Task<ResultModel<List<ConsulServiceModel>>> GetService()
        {
            List<ConsulServiceModel> result = await _consulService.GetServiceListAsync("http://175.27.194.19:8500/");
            return ResultModel<List<ConsulServiceModel>>.Success(result, "获取成功");
        }
    }
}