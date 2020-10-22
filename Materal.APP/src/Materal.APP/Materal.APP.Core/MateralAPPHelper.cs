using Materal.APP.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Materal.APP.Core
{
    public static class MateralAPPHelper
    {
        /// <summary>
        /// 处理传入参数
        /// </summary>
        /// <param name="args"></param>
        /// <param name="serverInfo"></param>
        /// <returns></returns>
        public static string[] HandlerArgs(string[] args, ServerInfoModel serverInfo)
        {
            string[] inputArgs = args;
            string urlsArg = inputArgs.FirstOrDefault(m => m.StartsWith("--urls="));
            if (string.IsNullOrWhiteSpace(urlsArg))
            {
                List<string> temp = inputArgs.ToList();
                temp.Add($"--urls={serverInfo.Url}");
                inputArgs = temp.ToArray();
                ApplicationConfig.Url = serverInfo.Url.Split(';')[0];
            }
            else
            {
                string[] tempArgs = urlsArg.Split('=');
                if (tempArgs.Length == 2)
                {
                    ApplicationConfig.Url = tempArgs[1].Split(';')[0];
                }
            }
            ApplicationConfig.PublicUrl = serverInfo.PublicUrl ?? ApplicationConfig.Url;
            return inputArgs;
        }
    }
}
