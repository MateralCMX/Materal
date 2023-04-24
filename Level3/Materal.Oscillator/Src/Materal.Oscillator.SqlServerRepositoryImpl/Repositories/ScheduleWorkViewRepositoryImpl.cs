using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;

namespace Materal.Oscillator.SqlServerRepositoryImpl.Repositories
{
    public class ScheduleWorkViewRepositoryImpl : OscillatorSqlServerEFRepositoryImpl<ScheduleWorkView>, IScheduleWorkViewRepository
    {
        public ScheduleWorkViewRepositoryImpl(OscillatorSqlServerDBContext dbContext) : base(dbContext)
        {
        }
    }
}
