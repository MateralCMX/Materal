using Materal.APP.HttpClient;
using Materal.StringHelper;
using WebAPP.Common;

namespace WebAPP.HttpClientImpl.Authority
{
    public abstract class AuthorityHttpClient : BaseHttpClient
    {
        protected AuthorityHttpClient(IAuthorityManage authorityManage) : base(authorityManage)
        {
        }
        protected override string GetUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(UrlManage.AuthorityUrl) || !UrlManage.AuthorityUrl.IsUrl()) throw new WebAPPException("尚未与权限服务取得联系");
            return $"{UrlManage.AuthorityUrl}{url}";
        }
    }
}
