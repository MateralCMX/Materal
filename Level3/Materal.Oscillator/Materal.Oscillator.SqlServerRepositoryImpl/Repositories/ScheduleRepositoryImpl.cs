using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;

namespace Materal.Oscillator.SqlServerRepositoryImpl.Repositories
{
    public class ScheduleRepositoryImpl : OscillatorSqlServerEFRepositoryImpl<Schedule>, IScheduleRepository
    {
        public ScheduleRepositoryImpl(OscillatorSqlServerDBContext dbContext) : base(dbContext)
        {
        }
    }
}
