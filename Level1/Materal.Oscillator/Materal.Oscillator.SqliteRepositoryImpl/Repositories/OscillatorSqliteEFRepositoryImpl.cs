using Materal.TTA.Common;
using Materal.TTA.SqliteRepository;

namespace Materal.Oscillator.SqliteRepositoryImpl.Repositories
{
    public abstract class OscillatorSqliteEFRepositoryImpl<T> : SqliteEFRepositoryImpl<T, Guid>
        where T : class, IEntity<Guid>, new()
    {
    }
}
