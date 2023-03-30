using Materal.TTA.Common;
using MBC.Core.EFRepository;

namespace MBC.Demo.EFRepository
{
    public abstract class DemoRepositoryImpl<T> : MBCEFRepositoryImpl<T, DemoDBContext>
        where T : class, IEntity<Guid>, new()
    {
        protected DemoRepositoryImpl(DemoDBContext dbContext) : base(dbContext)
        {
        }
    }
}
