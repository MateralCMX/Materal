using Materal.MergeBlock.Authorization.Abstractions;
using Materal.TFMS.EventBus;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MMB.Demo.Abstractions.Events;

namespace MMB.Demo.WebAPI.Controllers
{
    [ApiController, Route("/api/[controller]/[action]")]
    public class TestController(ITokenService tokenService, IEventBus eventBus) : ControllerBase
    {
        [HttpGet]
        public ResultModel SayHello()
        {
            return ResultModel.Success("Hello World!");
        }
        [HttpGet, AllowAnonymous]
        public ResultModel<string> GetToken()
        {
            string token = tokenService.GetToken(Guid.NewGuid());
            return ResultModel<string>.Success(token, "��ȡ�ɹ�");
        }
        [HttpGet]
        public async Task<ResultModel> SendMessageAsync()
        {
            MyEvent @event = new() { Message = "Hello EventBus!" };
            await eventBus.PublishAsync(@event);
            return ResultModel.Success("���ͳɹ�");
        }
    }
}
