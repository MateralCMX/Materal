using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authority.Domain;
using Authority.Domain.Repositories;
using Materal.CacheHelper;

namespace Authority.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 网页菜单权限仓储
    /// </summary>
    public class WebMenuAuthorityRepositoryImpl : AuthorityEFRepositoryImpl<WebMenuAuthority, Guid>, IWebMenuAuthorityRepository
    {
        private const string AllInfoCacheName = "AllWebMenuAuthorityInfoCacheName";
        private readonly ICacheManager _cacheManager;
        public WebMenuAuthorityRepositoryImpl(AuthorityDbContext dbContext, ICacheManager cacheManager) : base(dbContext)
        {
            _cacheManager = cacheManager;
        }

        public int GetMaxIndex()
        {
            return DBSet.Any() ? DBSet.Max(m => m.Index) : 0;
        }

        public async Task<List<WebMenuAuthority>> GetAllInfoFromCacheAsync()
        {
            var result = _cacheManager.Get<List<WebMenuAuthority>>(AllInfoCacheName);
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
