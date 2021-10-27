using Materal.APP.Core;
using Materal.APP.HttpClient;

namespace ConfigCenter.Environment.HttpClientImpl.MateralAPP
{
    public abstract class MateralAPPHttpClient : BaseHttpClient
    {
        protected MateralAPPHttpClient(IAuthorityManage authorityManage) : base(authorityManage)
        {
        }
        protected override string GetUrl(string url)
        {
            return $"{ApplicationConfig.ServerInfo.Url}{url}";
        }
    }
}
