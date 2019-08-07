using Materal.TTA.Common;

namespace Demo.EFRepository
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class DemoUnitOfWorkImpl : EFUnitOfWorkImpl<DemoDbContext>, IDemoUnitOfWork
    {
        public DemoUnitOfWorkImpl(DemoDbContext context) : base(context)
        {
        }
    }
}
