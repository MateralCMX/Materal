using Microsoft.Extensions.Logging;

namespace Materal.Tools.Core.MateralPublish.MateralProjects.Level2
{
    /// <summary>
    /// 事件总线
    /// </summary>
    public class EventBusProject(ILoggerFactory? loggerFactory = null) : BaseMateralProject(2, 1, "Materal.EventBus", loggerFactory)
    {
    }
}
