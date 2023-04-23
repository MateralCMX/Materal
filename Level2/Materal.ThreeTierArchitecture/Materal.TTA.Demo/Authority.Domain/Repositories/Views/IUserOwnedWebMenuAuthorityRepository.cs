using Materal.TTA.Common;
using System;
using Authority.Domain.Views;
namespace Authority.Domain.Repositories.Views
{
    /// <summary>
    /// 用户拥有的网页菜单权限仓储
    /// </summary>
    public interface IUserOwnedWebMenuAuthorityRepository : IRepository<UserOwnedWebMenuAuthority, Guid>
    {
    }
}
