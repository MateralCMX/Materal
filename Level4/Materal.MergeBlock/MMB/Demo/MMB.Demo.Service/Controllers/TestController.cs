using Materal.MergeBlock.Authorization.Abstractions;
using Materal.TFMS.EventBus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MMB.Demo.Abstractions.Controllers;
using MMB.Demo.Abstractions.Events;

namespace MMB.Demo.WebAPI.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    [ApiController, Route("/api/[controller]/[action]")]
    public class TestController(ITokenService tokenService, IEventBus eventBus) : ControllerBase, ITestController
    {
        /// <summary>
        /// 说Hello
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel SayHello() => ResultModel.Success("Hello World!");
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public ResultModel<string> GetToken()
        {
            string token = tokenService.GetToken(Guid.NewGuid());
            return ResultModel<string>.Success(token, "获取成功");
        }
        /// <summary>
        /// 发送事件总线消息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel> SendEventBusMessageAsync()
        {
            MyEvent @event = new() { Message = "Hello EventBus!" };
            await eventBus.PublishAsync(@event);
            return ResultModel.Success("发送成功");
        }
    }
}
