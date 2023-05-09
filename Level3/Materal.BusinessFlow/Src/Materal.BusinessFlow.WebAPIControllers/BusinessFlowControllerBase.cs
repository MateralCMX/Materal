using Microsoft.AspNetCore.Mvc;

namespace Materal.BusinessFlow.WebAPIControllers
{
    /// <summary>
    /// 业务流控制器基类
    /// </summary>
    [ApiController]
    [Route("bfapi/[controller]/[action]")]
    public class BusinessFlowControllerBase : ControllerBase
    {
    }
}
