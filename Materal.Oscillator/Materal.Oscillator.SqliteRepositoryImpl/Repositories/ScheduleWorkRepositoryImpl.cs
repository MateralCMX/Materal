using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;

namespace Materal.Oscillator.SqliteRepositoryImpl.Repositories
{
    public class ScheduleWorkRepositoryImpl : OscillatorSqliteEFRepositoryImpl<ScheduleWork>, IScheduleWorkRepository
    {
        public ScheduleWorkRepositoryImpl(OscillatorSqliteDBContext dbContext) : base(dbContext)
        {
        }
    }
}
