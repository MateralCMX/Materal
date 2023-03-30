using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;

namespace Materal.Oscillator.SqliteRepositoryImpl.Repositories
{
    public class ScheduleRepositoryImpl : OscillatorSqliteEFRepositoryImpl<Schedule>, IScheduleRepository
    {
    }
}
