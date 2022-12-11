using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;

namespace Materal.Oscillator.SqliteRepositoryImpl.Repositories
{
    public class ScheduleWorkViewRepositoryImpl : OscillatorSqliteEFRepositoryImpl<ScheduleWorkView>, IScheduleWorkViewRepository
    {
        public ScheduleWorkViewRepositoryImpl(OscillatorSqliteDBContext dbContext) : base(dbContext)
        {
        }
    }
}
