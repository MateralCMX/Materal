using Materal.ApplicationUpdate.Common;
using Microsoft.Extensions.Configuration;
using NLog;

namespace Materal.ApplicationUpdate.SystemLog
{
    public sealed class NLogHelper
    {
        public static void InitialNlog(string appName)
        {
            LogManager.Configuration.Variables["NlogConnectionString"] = ApplicationConfig.Configuration["ConnectionStrings:ApplicationUpdateDB"];
            LogManager.Configuration.Variables["AppName"] = appName;
        }
    }
}
