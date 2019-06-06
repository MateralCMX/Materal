using Materal.TTA.Common;
namespace WeChatService.EFRepository
{
    /// <summary>
    /// WeChatService工作单元
    /// </summary>
    public class WeChatServiceUnitOfWorkImpl : EFUnitOfWorkImpl<WeChatServiceDbContext>, IWeChatServiceUnitOfWork
    {
        public WeChatServiceUnitOfWorkImpl(WeChatServiceDbContext context) : base(context)
        {
        }
    }
}
