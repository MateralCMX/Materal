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
    /// API权限仓储
    /// </summary>
    public class APIAuthorityRepositoryImpl : AuthorityEFRepositoryImpl<APIAuthority, Guid>, IAPIAuthorityRepository
    {
        private const string AllInfoCacheName = "AllAPIAuthorityInfoCacheName";
        private readonly ICacheManager _cacheManager;
        public APIAuthorityRepositoryImpl(AuthorityDbContext dbContext, ICacheManager cacheManager) : base(dbContext)
        {
            _cacheManager = cacheManager;
        }

        public async Task<List<APIAuthority>> GetAllInfoFromCacheAsync()
        {
            var result = _cacheManager.Get<List<APIAuthority>>(AllInfoCacheName);
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
