using Materal.BaseCore.PresentationModel;
using Materal.BaseCore.Services.Models;
using Materal.BaseCore.WebAPI.Controllers;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;
using MBC.Demo.DataTransmitModel.MyTree;
using MBC.Demo.PresentationModel.MyTree;
using MBC.Demo.Services;
using MBC.Demo.Services.Models.MyTree;

namespace MBC.Demo.WebAPI.Controllers
{
    /// <summary>
    /// 我的树控制器
    /// </summary>
    public partial class MyTreeController : MateralCoreWebAPIServiceControllerBase<AddMyTreeRequestModel, EditMyTreeRequestModel, QueryMyTreeRequestModel, AddMyTreeModel, EditMyTreeModel, QueryMyTreeModel, MyTreeDTO, MyTreeListDTO, IMyTreeService>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        public MyTreeController(IServiceProvider serviceProvider) : base(serviceProvider) { }
        /// <summary>
        /// 交换位序
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> ExchangeIndexAsync(ExchangeIndexRequestModel requestModel)
        {
            ExchangeIndexModel model = Mapper.Map<ExchangeIndexModel>(requestModel);
            await DefaultService.ExchangeIndexAsync(model);
            return ResultModel.Success("交换位序成功");
        }
        /// <summary>
        /// 更改父级
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> ExchangeParentAsync(ExchangeParentRequestModel requestModel)
        {
            ExchangeParentModel model = Mapper.Map<ExchangeParentModel>(requestModel);
            await DefaultService.ExchangeParentAsync(model);
            return ResultModel.Success("更改父级成功");
        }
        /// <summary>
        /// 查询树列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel<List<MyTreeTreeListDTO>>> GetTreeListAsync(QueryMyTreeTreeListRequestModel requestModel)
        {
            QueryMyTreeTreeListModel model = Mapper.Map<QueryMyTreeTreeListModel>(requestModel);
            List<MyTreeTreeListDTO> result = await DefaultService.GetTreeListAsync(model);
            return ResultModel<List<MyTreeTreeListDTO>>.Success(result, "查询成功");
        }
    }
}
