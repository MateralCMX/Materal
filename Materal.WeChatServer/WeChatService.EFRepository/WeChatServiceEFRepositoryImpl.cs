using Materal.TTA.Common;
namespace WeChatService.EFRepository
{
    /// <summary>
    /// WeChatService仓储实现
    /// </summary>
    public class WeChatServiceEFRepositoryImpl<T, TKey> : EFRepositoryImpl<T, TKey> where T : class, IEntity<TKey>, new()
    {
        public WeChatServiceEFRepositoryImpl(WeChatServiceDbContext dbContext) : base(dbContext)
        {
        }
    }
}
