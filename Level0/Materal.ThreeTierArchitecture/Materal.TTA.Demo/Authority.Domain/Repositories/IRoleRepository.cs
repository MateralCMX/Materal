using Materal.TTA.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authority.Domain.Repositories
{
    /// <summary>
    /// 角色仓储
    /// </summary>
    public interface IRoleRepository : IRepository<Role, Guid>
    {
        /// <summary>
        /// 从缓存中获取所有信息
        /// </summary>
        /// <returns></returns>
        Task<List<Role>> GetAllInfoFromCacheAsync();
        /// <summary>
        /// 清空缓存
        /// </summary>
        void ClearCache();
    }
}
