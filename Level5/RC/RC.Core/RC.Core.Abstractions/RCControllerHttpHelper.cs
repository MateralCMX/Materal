using Materal.MergeBlock.Abstractions.Config;
using Materal.MergeBlock.Authorization.Abstractions;
using Materal.Utils.Http;
using Microsoft.Extensions.Options;

namespace RC.Core.Abstractions
{
    /// <summary>
    /// RC控制器HTTP帮助类
    /// </summary>
    /// <param name="httpHelper"></param>
    /// <param name="tokenService"></param>
    /// <param name="config"></param>
    public class RCControllerHttpHelper(IHttpHelper httpHelper, ITokenService tokenService, IOptionsMonitor<MergeBlockConfig> config) : AuthorizationControllerHttpHelper(httpHelper, tokenService, config)
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
            return $"{Config.CurrentValue.BaseUrl}/api/{controller}/{action}";
        }
    }
}
