using Materal.TTA.Common;
using Materal.TTA.SqliteRepository;

namespace Materal.Oscillator.LocalDR.Repositories
{
    public abstract class OscillatorLocalDREFRepositoryImpl<T> : SqliteEFRepositoryImpl<T, Guid, OscillatorLocalDRDBContext>
        where T : class, IEntity<Guid>, new()
    {
        protected OscillatorLocalDREFRepositoryImpl(OscillatorLocalDRDBContext dbContext) : base(dbContext)
        {
        }
    }
}
