using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;

namespace Materal.Oscillator.SqliteRepositoryImpl.Repositories
{
    public class WorkRepositoryImpl : OscillatorSqliteEFRepositoryImpl<Work>, IWorkRepository
    {
        public WorkRepositoryImpl(OscillatorSqliteDBContext dbContext) : base(dbContext)
        {
        }
    }
}
