using Materal.BaseCore.Services;
using Materal.BaseCore.Services.Models;
using Materal.Utils.Model;
using MBC.Demo.DataTransmitModel.MenuAuthority;
using MBC.Demo.Services.Models.MenuAuthority;

namespace MBC.Demo.Services
{
    /// <summary>
    /// 服务
    /// </summary>
    public partial interface IMenuAuthorityService : IBaseService<AddMenuAuthorityModel, EditMenuAuthorityModel, QueryMenuAuthorityModel, MenuAuthorityDTO, MenuAuthorityListDTO>
    {
        /// <summary>
        /// 交换位序
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [DataValidation]
        Task ExchangeIndexAsync(ExchangeIndexModel model);
    }
}
