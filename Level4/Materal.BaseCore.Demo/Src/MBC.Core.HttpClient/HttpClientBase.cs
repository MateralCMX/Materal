using Materal.BaseCore.DataTransmitModel;
using Materal.BaseCore.HttpClient;
using Materal.BaseCore.PresentationModel;

namespace MBC.Core.HttpClient
{
    public class HttpClientBase : MateralCoreHttpClientBase
    {
        private readonly string _appName;
        public HttpClientBase(string projectName, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            string[] temp = projectName.Split('.');
            if (temp.Length > 1)
            {
                _appName = temp[1];
            }
            else
            {
                _appName = projectName;
            }
        }
        protected override Dictionary<string, string> GetDefaultHeaders() => HttpClientHelper.GetDefaultHeaders();
        protected override string GetTrueUrl(string url)
        {
            if (HttpClientHelper.GetUrl == null) return $"{HttpClientConfig.HttpClienUrltConfig.BaseUrl}MBC{_appName}{HttpClientConfig.HttpClienUrltConfig.Suffix}/api/{url}";
            return HttpClientHelper.GetUrl(url, _appName);
        }
    }
    public class HttpClientBase<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO> : MateralCoreHttpClientBase<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO>
    where TAddRequestModel : class, IAddRequestModel, new()
    where TEditRequestModel : class, IEditRequestModel, new()
    where TQueryRequestModel : IQueryRequestModel, new()
    where TDTO : class, IDTO, new()
    where TListDTO : class, IListDTO, new()
    {
        private readonly string _appName;
        public HttpClientBase(string projectName, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            string[] temp = projectName.Split('.');
            if (temp.Length > 1)
            {
                _appName = temp[1];
            }
            else
            {
                _appName = projectName;
            }
        }
        protected override Dictionary<string, string> GetDefaultHeaders() => HttpClientHelper.GetDefaultHeaders();
        protected override string GetTrueUrl(string url)
        {
            if (HttpClientHelper.GetUrl == null) return $"{HttpClientConfig.HttpClienUrltConfig.BaseUrl}MBC{_appName}{HttpClientConfig.HttpClienUrltConfig.Suffix}/api/{url}";
            return HttpClientHelper.GetUrl(url, _appName);
        }
    }
}