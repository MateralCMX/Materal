using Materal.TTA.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authority.Domain.Repositories
{
    /// <summary>
    /// 网页菜单权限仓储
    /// </summary>
    public interface IWebMenuAuthorityRepository : IRepository<WebMenuAuthority, Guid>
    {
        /// <summary>
        /// 从缓存中获取所有信息
        /// </summary>
        /// <returns></returns>
        Task<List<WebMenuAuthority>> GetAllInfoFromCacheAsync();
        /// <summary>
        /// 清空缓存
        /// </summary>
        void ClearCache();
    }
}
