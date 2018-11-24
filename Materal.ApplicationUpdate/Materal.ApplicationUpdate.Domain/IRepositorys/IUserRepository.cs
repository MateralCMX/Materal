using Materal.TTA.Common;
using System;

namespace Materal.ApplicationUpdate.Domain.IRepositorys
{
    /// <summary>
    /// 用户仓储
    /// </summary>
    public interface IUserRepository : IEFRepository<User, Guid>
    {
    }
}
