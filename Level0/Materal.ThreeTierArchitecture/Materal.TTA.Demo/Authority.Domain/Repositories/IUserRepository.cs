using Materal.TTA.Common;
using System;
namespace Authority.Domain.Repositories
{
    /// <summary>
    /// 用户仓储
    /// </summary>
    public interface IUserRepository : IRepository<User, Guid>
    {
    }
}
