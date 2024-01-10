using Materal.MergeBlock.Application.Controllers;
using Materal.MergeBlock.EventBusTest.Events;
using Materal.TFMS.EventBus;
using Microsoft.AspNetCore.Mvc;

namespace Materal.MergeBlock.EventBusTest.Controllers
{
    /// <summary>
    /// EventBus²âÊÔ¿ØÖÆÆ÷
    /// </summary>
    public class EventBusTestController(IEventBus eventBus) : MergeBlockControllerBase
    {
        [HttpGet]
        public void SendMessage() => eventBus.PublishAsync(new MyEvent { Message = "Hello World!" });
    }
}
