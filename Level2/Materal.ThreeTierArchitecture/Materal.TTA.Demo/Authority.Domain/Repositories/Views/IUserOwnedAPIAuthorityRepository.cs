using Materal.TTA.Common;
using System;
using Authority.Domain.Views;
namespace Authority.Domain.Repositories.Views
{
    /// <summary>
    /// 用户拥有的API权限仓储
    /// </summary>
    public interface IUserOwnedAPIAuthorityRepository : IRepository<UserOwnedAPIAuthority, Guid>
    {
    }
}
