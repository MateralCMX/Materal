using Materal.TTA.Common;
using Materal.TTA.EFRepository;

namespace Materal.TTA.Demo
{
    public class DemoUnitOfWorkImpl : EFUnitOfWorkImpl<TTADemoDBContext>, IDemoUnitOfWork
    {
        public DemoUnitOfWorkImpl(TTADemoDBContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }
        public void RegisterAdd<TEntity>(TEntity obj)
            where TEntity : class, IEntity<Guid>
        {
            RegisterAdd<TEntity, Guid>(obj);
        }

        public void RegisterDelete<TEntity>(TEntity obj)
            where TEntity : class, IEntity<Guid>
        {
            RegisterDelete<TEntity, Guid>(obj);
        }

        public void RegisterEdit<TEntity>(TEntity obj)
            where TEntity : class, IEntity<Guid>
        {
            RegisterEdit<TEntity, Guid>(obj);
        }
    }
}
