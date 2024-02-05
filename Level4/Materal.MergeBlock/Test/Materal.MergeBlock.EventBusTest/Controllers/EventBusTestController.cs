using Materal.EventBus.Abstraction;
using Materal.MergeBlock.Application.WebModule.Controllers;
using Materal.MergeBlock.EventBusTest.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Materal.MergeBlock.EventBusTest.Controllers
{
    /// <summary>
    /// EventBus测试控制器
    /// </summary>
    [ApiExplorerSettings(GroupName = "EventBusTest")]
    public class EventBusTestController(IEventBus eventBus) : MergeBlockControllerBase
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        [HttpGet]
        public void SendMessage() => eventBus.Publish(new MyEvent { Message = "Hello World!" });
    }
}
