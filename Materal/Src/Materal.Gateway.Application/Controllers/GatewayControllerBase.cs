namespace Materal.Gateway.Application.Controllers
{
    /// <summary>
    /// 网关控制器基类
    /// </summary>
    [ApiController, Route("/GatewayAPI/[controller]/[action]")]
    public abstract class GatewayControllerBase : ControllerBase
    {

    }
}
