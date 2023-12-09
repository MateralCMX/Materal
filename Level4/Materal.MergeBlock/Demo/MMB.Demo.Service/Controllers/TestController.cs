using Materal.MergeBlock.Authorization.Abstractions;
using Materal.MergeBlock.Repository;
using Materal.TFMS.EventBus;
using Materal.TTA.Common;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MMB.Demo.Abstractions.Enums;
using MMB.Demo.Abstractions.Events;
using MMB.Demo.Domain;
using MMB.Demo.Domain.Repositories;

namespace MMB.Demo.WebAPI.Controllers
{
    [ApiController, Route("/api/[controller]/[action]")]
    public class TestController(ITokenService tokenService, IEventBus eventBus, IUserRepository userRepository, IMateralCoreUnitOfWork unitOfWork) : ControllerBase
    {
        [HttpGet]
        public async Task<ResultModel> SayHelloAsync()
        {
            User user = new()
            {
                Account = "admin",
                Password = "123456",
                Name = "管理员",
                Sex = SexEnum.Woman
            };
            unitOfWork.RegisterAdd(user);
            await unitOfWork.CommitAsync();
            User? a = await userRepository.FirstOrDefaultAsync(m => m.ID == user.ID);
            return ResultModel.Success("Hello World!");
        }
        [HttpGet, AllowAnonymous]
        public ResultModel<string> GetToken()
        {
            string token = tokenService.GetToken(Guid.NewGuid());
            return ResultModel<string>.Success(token, "获取成功");
        }
        [HttpGet]
        public async Task<ResultModel> SendMessageAsync()
        {
            MyEvent @event = new() { Message = "Hello EventBus!" };
            await eventBus.PublishAsync(@event);
            return ResultModel.Success("发送成功");
        }
    }
}
