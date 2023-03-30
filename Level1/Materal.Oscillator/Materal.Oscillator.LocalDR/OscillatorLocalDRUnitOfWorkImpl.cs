using Materal.Oscillator.DR.Repositories;
using Materal.Oscillator.LocalDR;
using Materal.TTA.Common;
using Materal.TTA.SqliteRepository;

namespace Materal.Oscillator.SqliteRepositoryImpl
{
    public class OscillatorLocalDRUnitOfWorkImpl : SqliteEFUnitOfWorkImpl<OscillatorLocalDRDBContext>, IOscillatorDRUnitOfWork
    {
        public OscillatorLocalDRUnitOfWorkImpl(OscillatorLocalDRDBContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
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
