﻿using Microsoft.Extensions.Logging;

namespace Materal.Tools.Core.MateralPublish.MateralProjects.Level1
{
    /// <summary>
    /// 日志项目
    /// </summary>
    public class LoggerProject(ILoggerFactory? loggerFactory = null) : BaseMateralProject(1, 0, "Materal.Logger", loggerFactory)
    {
    }
}
