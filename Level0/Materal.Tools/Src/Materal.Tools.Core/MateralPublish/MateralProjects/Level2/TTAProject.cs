using Microsoft.Extensions.Logging;

namespace Materal.Tools.Core.MateralPublish.MateralProjects.Level2
{
    /// <summary>
    /// TTA框架
    /// </summary>
    public class TTAProject(ILoggerFactory? loggerFactory = null) : BaseMateralProject(2, 0, "Materal.ThreeTierArchitecture", loggerFactory)
    {
    }
}
