using Microsoft.Extensions.Logging;

namespace Materal.Tools.Core.MateralPublish.MateralProjects.Level2
{
    /// <summary>
    /// 调度器
    /// </summary>
    public class OscillatorProject(ILoggerFactory? loggerFactory = null) : BaseMateralProject(2, 2, "Materal.Oscillator", loggerFactory)
    {
    }
}
