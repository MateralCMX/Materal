using Materal.BaseCore.ServiceImpl;
using Materal.BaseCore.Services.Models;
using MBC.Demo.DataTransmitModel.MenuAuthority;
using MBC.Demo.Domain;
using MBC.Demo.Domain.Repositories;
using MBC.Demo.Services;
using MBC.Demo.Services.Models.MenuAuthority;

namespace MBC.Demo.ServiceImpl
{
    /// <summary>
    /// 服务实现
    /// </summary>
    public partial class MenuAuthorityServiceImpl : BaseServiceImpl<AddMenuAuthorityModel, EditMenuAuthorityModel, QueryMenuAuthorityModel, MenuAuthorityDTO, MenuAuthorityListDTO, IMenuAuthorityRepository, MenuAuthority>, IMenuAuthorityService
    {
        /// <summary>
        /// 交换位序
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task ExchangeIndexAsync(ExchangeIndexModel model) => await ServiceImplHelper.ExchangeIndexByGroupPropertiesAsync<IMenuAuthorityRepository, MenuAuthority>(model, DefaultRepository, UnitOfWork, nameof(MenuAuthority.ParentID), nameof(MenuAuthority.SubSystemID));
    }
}
