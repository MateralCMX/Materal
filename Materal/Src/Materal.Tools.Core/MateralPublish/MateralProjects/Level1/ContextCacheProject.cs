using Microsoft.Extensions.Logging;

namespace Materal.Tools.Core.MateralPublish.MateralProjects.Level1
{
    /// <summary>
    /// 上下文缓存项目
    /// </summary>
    /// <param name="loggerFactory"></param>
    public class ContextCacheProject(ILoggerFactory? loggerFactory = null) : BaseMateralProject(1, 1, "Materal.ContextCache", loggerFactory)
    {
    }
}
