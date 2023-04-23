using Materal.BaseCore.CodeGenerator;
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
    [AutoBaseDI]
    public partial class MenuAuthorityServiceImpl : BaseServiceImpl<AddMenuAuthorityModel, EditMenuAuthorityModel, QueryMenuAuthorityModel, MenuAuthorityDTO, MenuAuthorityListDTO, IMenuAuthorityRepository, MenuAuthority>, IMenuAuthorityService
    {
        private readonly IUserRepository _userRepository;
        /// <summary>
        /// 交换位序
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task ExchangeIndexAsync(ExchangeIndexModel model)
            => await ServiceImplHelper.ExchangeIndexAndExchangeParentByGroupPropertiesAsync<IMenuAuthorityRepository, MenuAuthority>(model, DefaultRepository, UnitOfWork, new[] { nameof(MenuAuthority.SubSystemID) }, new[] { nameof(MenuAuthority.SubSystemID) });
        /// <summary>
        /// 更改父级
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task ExchangeParentAsync(ExchangeParentModel model) => await ServiceImplHelper.ExchangeParentByGroupPropertiesAsync<IMenuAuthorityRepository, MenuAuthority>(model, DefaultRepository, UnitOfWork, nameof(MenuAuthority.SubSystemID));
    }
}
