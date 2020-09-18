using Materal.APP.HttpClient;

namespace WebAPP.HttpClientImpl.MateralAPP
{
    public abstract class MateralAPPHttpClient : BaseHttpClient
    {
        protected MateralAPPHttpClient(IAuthorityManage authorityManage) : base(authorityManage)
        {
        }
        protected override string GetUrl(string url)
        {
            return $"{UrlManage.MateralAppUrl}{url}";
        }
    }
}
