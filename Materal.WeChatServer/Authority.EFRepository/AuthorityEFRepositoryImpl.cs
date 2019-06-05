using Materal.TTA.Common;
namespace Authority.EFRepository
{
    /// <summary>
    /// Authority仓储实现
    /// </summary>
    public class AuthorityEFRepositoryImpl<T, TKey> : EFRepositoryImpl<T, TKey> where T : class, IEntity<TKey>, new()
    {
        public AuthorityEFRepositoryImpl(AuthorityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
