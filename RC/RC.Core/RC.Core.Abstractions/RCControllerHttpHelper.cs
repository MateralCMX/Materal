using Materal.MergeBlock.Authorization.Abstractions;
using Materal.MergeBlock.Web.Abstractions;
using Materal.Utils.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace RC.Core.Abstractions
{
    /// <summary>
    /// RC控制器HTTP帮助类
    /// </summary>
    public class RCControllerHttpHelper(IHttpHelper httpHelper, ITokenService tokenService, IOptionsMonitor<WebOptions> config, IOptionsMonitor<MergeBlockOptions> mergeBlockConfig, ILoggerFactory? loggerFactory = null) : AuthorizationControllerHttpHelper(httpHelper, tokenService, config, mergeBlockConfig, loggerFactory)
    {
        /// <summary>
        /// 获取URL
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="moduleName"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public override string GetUrl(string projectName, string moduleName, string controllerName, string actionName)
        {
            string action = actionName;
            if (action.EndsWith("Async"))
            {
                action = action[..^5];
            }
            string controller = controllerName;
            controller = controller[1..^10];
            return $"{Config.CurrentValue.BaseUrl}/RC{moduleName}/api/{controller}/{action}";
        }
    }
}
