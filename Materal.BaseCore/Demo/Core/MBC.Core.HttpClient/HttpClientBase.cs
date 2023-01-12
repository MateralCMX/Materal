using Materal.BaseCore.DataTransmitModel;
using Materal.BaseCore.HttpClient;
using Materal.BaseCore.PresentationModel;

namespace MBC.Core.HttpClient
{
    public class HttpClientBase : MateralCoreHttpClientBase
    {
        /// <summary>
        /// 获得默认头
        /// </summary>
        /// <returns></returns>
        protected override Dictionary<string, string> GetDefaultHeaders() => HttpClientHelper.GetDefaultHeaders();
    }
    public class HttpClientBase<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO> : MateralCoreHttpClientBase<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO>
        where TAddRequestModel : class, IAddRequestModel, new()
        where TEditRequestModel : class, IEditRequestModel, new()
        where TQueryRequestModel : IQueryRequestModel, new()
        where TDTO : class, IDTO, new()
        where TListDTO : class, IListDTO, new()
    {
        /// <summary>
        /// 获得默认头
        /// </summary>
        /// <returns></returns>
        protected override Dictionary<string, string> GetDefaultHeaders() => HttpClientHelper.GetDefaultHeaders();
    }
}