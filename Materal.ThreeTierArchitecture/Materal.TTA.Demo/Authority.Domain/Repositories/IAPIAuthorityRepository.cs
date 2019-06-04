using Materal.TTA.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authority.Domain.Repositories
{
    /// <summary>
    /// API权限仓储
    /// </summary>
    public interface IAPIAuthorityRepository : IRepository<APIAuthority, Guid>
    {
        /// <summary>
        /// 从缓存中获取所有信息
        /// </summary>
        /// <returns></returns>
        Task<List<APIAuthority>> GetAllInfoFromCacheAsync();
        /// <summary>
        /// 清空缓存
        /// </summary>
        void ClearCache();
    }
}
