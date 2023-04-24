using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Repositories;
using Materal.TTA.Common;
using Materal.TTA.SqlServerRepository;

namespace Materal.Oscillator.SqlServerRepositoryImpl
{
    public class OscillatorSqlServerUnitOfWorkImpl : SqlServerEFUnitOfWorkImpl<OscillatorSqlServerDBContext>, IOscillatorUnitOfWork
    {
        public OscillatorSqlServerUnitOfWorkImpl(OscillatorSqlServerDBContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
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
