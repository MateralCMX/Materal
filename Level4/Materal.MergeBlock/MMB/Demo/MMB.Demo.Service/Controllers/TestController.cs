using Materal.MergeBlock.Authorization.Abstractions;
using Materal.TFMS.EventBus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MMB.Demo.Abstractions.Controllers;
using MMB.Demo.Abstractions.Events;

namespace MMB.Demo.WebAPI.Controllers
{
    /// <summary>
    /// ���Կ�����
    /// </summary>
    [ApiController, Route("/api/[controller]/[action]")]
    public class TestController(ITokenService tokenService, IEventBus eventBus) : ControllerBase, ITestController
    {
        /// <summary>
        /// ˵Hello
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel SayHello() => ResultModel.Success("Hello World!");
        /// <summary>
        /// ��ȡToken
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public ResultModel<string> GetToken()
        {
            string token = tokenService.GetToken(Guid.NewGuid());
            return ResultModel<string>.Success(token, "��ȡ�ɹ�");
        }
        /// <summary>
        /// �����¼�������Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel> SendEventBusMessageAsync()
        {
            MyEvent @event = new() { Message = "Hello EventBus!" };
            await eventBus.PublishAsync(@event);
            return ResultModel.Success("���ͳɹ�");
        }
    }
}
