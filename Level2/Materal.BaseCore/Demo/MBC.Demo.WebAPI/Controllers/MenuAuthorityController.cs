using Materal.BaseCore.PresentationModel;
using Materal.BaseCore.Services.Models;
using Materal.BaseCore.WebAPI.Controllers;
using Materal.Utils.Model;
using MBC.Demo.DataTransmitModel.MenuAuthority;
using MBC.Demo.PresentationModel.MenuAuthority;
using MBC.Demo.Services;
using MBC.Demo.Services.Models.MenuAuthority;
using Microsoft.AspNetCore.Mvc;

namespace MBC.Demo.WebAPI.Controllers
{
    /// <summary>
    /// 控制器
    /// </summary>
    public partial class MenuAuthorityController : MateralCoreWebAPIServiceControllerBase<AddMenuAuthorityRequestModel, EditMenuAuthorityRequestModel, QueryMenuAuthorityRequestModel, AddMenuAuthorityModel, EditMenuAuthorityModel, QueryMenuAuthorityModel, MenuAuthorityDTO, MenuAuthorityListDTO, IMenuAuthorityService>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        public MenuAuthorityController(IServiceProvider serviceProvider) : base(serviceProvider) { }

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
        /// <param name="requestModel">请求模型</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> ExchangeParentAsync(ExchangeParentRequestModel requestModel)
        {
            ExchangeParentModel model = Mapper.Map<ExchangeParentModel>(requestModel);
            await DefaultService.ExchangeParentAsync(model);
            return ResultModel.Success("已更改父级");
        }
    }
}
