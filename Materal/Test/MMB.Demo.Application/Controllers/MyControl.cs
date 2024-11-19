using Materal.EventBus.Abstraction;
using Materal.MergeBlock.Authorization.Abstractions;
using Materal.MergeBlock.Web.Abstractions.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MMB.Demo.Application.EventBus;

namespace MMB.Demo.Application.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class MyController(ITokenService tokenService, IEventBus eventBus) : ControllerBase, IMergeBlockController
    {
        [HttpGet]
        public string Hello(string name) => $"Hello {name}!";
        [HttpGet, AllowAnonymous]
        public string GetToken() => tokenService.GetToken(Guid.NewGuid());
        [HttpGet]
        public void SendEvent(string message) => eventBus.PublishAsync(new TestEvent() { Message = message });
    }
}
