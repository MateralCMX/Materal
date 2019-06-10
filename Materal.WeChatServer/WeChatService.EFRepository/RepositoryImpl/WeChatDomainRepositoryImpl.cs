using Materal.CacheHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeChatService.Domain;
using WeChatService.Domain.Repositories;
namespace WeChatService.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 微信域名仓储
    /// </summary>
    public class WeChatDomainRepositoryImpl : WeChatServiceEFRepositoryImpl<WeChatDomain, Guid>, IWeChatDomainRepository
    {
        private const string AllInfoCacheName = "AllWeChatDomainInfoCacheName";
        private readonly ICacheManager _cacheManager;
        public WeChatDomainRepositoryImpl(WeChatServiceDbContext dbContext, ICacheManager cacheManager) : base(dbContext)
        {
            _cacheManager = cacheManager;
        }

        public int GetMaxIndex()
        {
            return DBSet.Any() ? DBSet.Max(m => m.Index) : 0;
        }

        public async Task<List<WeChatDomain>> GetAllInfoFromCacheAsync()
        {
            var result = _cacheManager.Get<List<WeChatDomain>>(AllInfoCacheName);
            if (result != null) return result;
            result = await DBSet.Where(m => true).ToAsyncEnumerable().ToList();
            _cacheManager.SetBySliding(AllInfoCacheName, result, 1);
            return result;
        }

        public void ClearCache()
        {
            _cacheManager.Remove(AllInfoCacheName);
        }
    }
}
