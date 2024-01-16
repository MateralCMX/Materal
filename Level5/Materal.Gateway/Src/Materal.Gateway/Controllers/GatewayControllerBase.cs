namespace Materal.Gateway.Controllers
{
    /// <summary>
    /// 网关控制器基类
    /// </summary>
    [ApiController, Route("/api/[controller]/[action]")]
    public abstract class GatewayControllerBase : ControllerBase
    {

    }
}
