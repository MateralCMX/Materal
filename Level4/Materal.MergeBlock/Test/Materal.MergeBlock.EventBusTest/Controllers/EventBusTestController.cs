using Materal.EventBus.Abstraction;
using Materal.MergeBlock.Application.WebModule.Controllers;
using Materal.MergeBlock.EventBusTest.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Materal.MergeBlock.EventBusTest.Controllers
{
    /// <summary>
    /// EventBus���Կ�����
    /// </summary>
    [ApiExplorerSettings(GroupName = "EventBusTest")]
    public class EventBusTestController(IEventBus eventBus) : MergeBlockControllerBase
    {
        /// <summary>
        /// ������Ϣ
        /// </summary>
        [HttpGet]
        public void SendMessage() => eventBus.Publish(new MyEvent { Message = "Hello World!" });
    }
}
