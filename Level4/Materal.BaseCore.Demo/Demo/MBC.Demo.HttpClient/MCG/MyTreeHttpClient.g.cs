#nullable enable
using MBC.Core.HttpClient;
using Materal.Utils.Model;
using Materal.BaseCore.PresentationModel;
using MBC.Demo.DataTransmitModel.MyTree;
using MBC.Demo.PresentationModel.MyTree;

namespace MBC.Demo.HttpClient
{
    public partial class MyTreeHttpClient : HttpClientBase<AddMyTreeRequestModel, EditMyTreeRequestModel, QueryMyTreeRequestModel, MyTreeDTO, MyTreeListDTO>
    {
        public MyTreeHttpClient(IServiceProvider serviceProvider) : base("MBC.Demo", serviceProvider) { }
        /// <summary>
        /// 交换位序
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task ExchangeIndexAsync(ExchangeIndexRequestModel requestModel) => await GetResultModelByPutAsync("MyTree/ExchangeIndex", null, requestModel);
        /// <summary>
        /// 更改父级
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task ExchangeParentAsync(ExchangeParentRequestModel requestModel) => await GetResultModelByPutAsync("MyTree/ExchangeParent", null, requestModel);
        /// <summary>
        /// 查询树列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<List<MyTreeTreeListDTO>?> GetTreeListAsync(QueryMyTreeTreeListRequestModel requestModel) => await GetResultModelByPostAsync<List<MyTreeTreeListDTO>>("MyTree/GetTreeList", null, requestModel);
    }
}
