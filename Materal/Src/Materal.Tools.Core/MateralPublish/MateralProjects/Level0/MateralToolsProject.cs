using Microsoft.Extensions.Logging;

namespace Materal.Tools.Core.MateralPublish.MateralProjects.Level0
{
    /// <summary>
    /// MateralTools项目
    /// </summary>
    public class MateralToolsProject(ILoggerFactory? loggerFactory = null) : BaseMateralProject(0, 0, "Materal.Tools", loggerFactory)
    {
    }
}
