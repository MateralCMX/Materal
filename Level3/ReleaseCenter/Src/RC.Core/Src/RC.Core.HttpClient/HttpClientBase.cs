﻿using Materal.BaseCore.DataTransmitModel;
using Materal.BaseCore.HttpClient;
using Materal.BaseCore.PresentationModel;

namespace RC.Core.HttpClient
{
    public class HttpClientBase : MateralCoreHttpClientBase
    {
        private string _appName;
        public HttpClientBase(string projectName)
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
        protected override string GetTrueUrl(string url) => $"{HttpClientConfig.HttpClienUrltConfig.BaseUrl}RC{_appName}{HttpClientConfig.HttpClienUrltConfig.Suffix}/{url}";
    }
    public class HttpClientBase<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO> : MateralCoreHttpClientBase<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO>
        where TAddRequestModel : class, IAddRequestModel, new()
        where TEditRequestModel : class, IEditRequestModel, new()
        where TQueryRequestModel : IQueryRequestModel, new()
        where TDTO : class, IDTO, new()
        where TListDTO : class, IListDTO, new()
    {
        private string _appName;
        public HttpClientBase(string projectName)
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
        protected override string GetTrueUrl(string url) => $"{HttpClientConfig.HttpClienUrltConfig.BaseUrl}RC{_appName}{HttpClientConfig.HttpClienUrltConfig.Suffix}/api/{url}";
    }
}