using Materal.APP.HttpClient;
using Materal.StringHelper;
using WebAPP.Common;

namespace WebAPP.HttpClientImpl.ConfigCenter
{
    public abstract class ConfigCenterHttpClient : BaseHttpClient
    {
        protected ConfigCenterHttpClient(IAuthorityManage authorityManage) : base(authorityManage)
        {
        }
        protected override string GetUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(UrlManage.ConfigCenterUrl) || !UrlManage.ConfigCenterUrl.IsUrl()) throw new WebAPPException("尚未与发布服务取得联系");
            var result = $"{UrlManage.ConfigCenterUrl}{url}";
            return result;
        }
    }
}
