using Materal.APP.HttpClient;
using Materal.StringHelper;
using WebAPP.Common;

namespace WebAPP.HttpClientImpl.Environment
{
    public abstract class EnvironmentHttpClient : BaseHttpClient
    {
        protected EnvironmentHttpClient(IAuthorityManage authorityManage) : base(authorityManage)
        {
        }
        protected override string GetUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(UrlManage.EnvironmentUrl) || !UrlManage.EnvironmentUrl.IsUrl()) throw new WebAPPException("尚未与发布服务取得联系");
            return $"{UrlManage.EnvironmentUrl}{url}";
        }
    }
}
