using Materal.Gateway.OcelotExtension.Services;

namespace Materal.Gateway.WebAPI.Controllers
{
    /// <summary>
    /// 同步控制器
    /// </summary>
    public class SyncController(IOcelotConfigService ocelotConfigService) : GatewayControllerBase
    {
    }
}
