using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Repositories;
using Materal.TTA.Common;
using Materal.TTA.SqliteRepository;

namespace Materal.Oscillator.SqliteRepositoryImpl
{
    public class OscillatorSqliteUnitOfWorkImpl : SqliteEFUnitOfWorkImpl<OscillatorSqliteDBContext>, IOscillatorUnitOfWork
    {
        public OscillatorSqliteUnitOfWorkImpl(OscillatorSqliteDBContext context) : base(context)
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
