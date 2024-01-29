using Materal.MergeBlock.Abstractions.WebModule.ControllerHttpHelper;
using Materal.Utils.Http;
using Microsoft.Extensions.Options;

namespace Materal.MergeBlock.Abstractions.WebModule.Authorization
{
    /// <summary>
    /// 默认控制器Http帮助类
    /// </summary>
    public class AuthorizationControllerHttpHelper(IHttpHelper httpHelper, ITokenService tokenService, IOptionsMonitor<MergeBlockConfig> config) : DefaultControllerHttpHelper(httpHelper, config), IControllerHttpHelper
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
