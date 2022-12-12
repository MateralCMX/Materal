using Materal.Oscillator.Abstractions;
using Materal.TTA.Common;
using Materal.TTA.SqlServerRepository;

namespace Materal.Oscillator.SqlServerRepositoryImpl.Repositories
{
    public abstract class OscillatorSqlServerEFRepositoryImpl<T> : SqlServerEFRepositoryImpl<T, Guid>
        where T : class, IEntity<Guid>, new()
    {
        protected OscillatorSqlServerEFRepositoryImpl(OscillatorSqlServerDBContext dbContext) : base(dbContext)
        {
        }
    }
}
