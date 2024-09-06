using Materal.MergeBlock.Abstractions;
using Materal.MergeBlock.Web.Abstractions;
using Materal.MergeBlock.Web.Abstractions.ControllerHttpHelper;
using Materal.Utils.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Materal.MergeBlock.Authorization.Abstractions
{
    /// <summary>
    /// 默认控制器Http帮助类
    /// </summary>
    public class AuthorizationControllerHttpHelper(IHttpHelper httpHelper, ITokenService tokenService, IOptionsMonitor<WebOptions> config, IOptionsMonitor<MergeBlockOptions> mergeBlockConfig, ILoggerFactory? loggerFactory = null) : DefaultControllerHttpHelper(httpHelper, config, mergeBlockConfig, loggerFactory), IControllerHttpHelper
    {
        /// <summary>
        /// 获取请求头
        /// </summary>
        /// <param name="hasToken"></param>
        /// <returns></returns>
        public override Dictionary<string, string> GetHeaders(bool hasToken)
        {
            Dictionary<string, string> result = base.GetHeaders(hasToken);
            if (hasToken)
            {
                string serviceName = GetServiceName();
                string token = tokenService.GetToken(serviceName);
                result.Add("Authorization", $"Bearer {token}");
            }
            return result;
        }
    }
}
