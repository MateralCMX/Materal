using Materal.TTA.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeChatService.Domain.Repositories
{
    /// <summary>
    /// 微信域名仓储
    /// </summary>
    public interface IWeChatDomainRepository : IRepository<WeChatDomain, Guid>
    {
        /// <summary>
        /// 获得最大位序
        /// </summary>
        /// <returns></returns>
        int GetMaxIndex();
        /// <summary>
        /// 从缓存中获取所有信息
        /// </summary>
        /// <returns></returns>
        Task<List<WeChatDomain>> GetAllInfoFromCacheAsync();
        /// <summary>
        /// 清空缓存
        /// </summary>
        void ClearCache();
    }
}
