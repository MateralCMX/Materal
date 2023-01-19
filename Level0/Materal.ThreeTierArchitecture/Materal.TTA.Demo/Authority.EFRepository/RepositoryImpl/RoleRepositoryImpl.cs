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
    /// 角色仓储
    /// </summary>
    public class RoleRepositoryImpl : AuthorityEFRepositoryImpl<Role, Guid>, IRoleRepository
    {
        private const string AllInfoCacheName = "AllRoleInfoCacheName";
        private readonly ICacheManager _cacheManager;
        public RoleRepositoryImpl(AuthorityDbContext dbContext, ICacheManager cacheManager) : base(dbContext)
        {
            _cacheManager = cacheManager;
        }
        public async Task<List<Role>> GetAllInfoFromCacheAsync()
        {
            var result = _cacheManager.Get<List<Role>>(AllInfoCacheName);
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
