using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;

namespace Materal.Oscillator.SqlServerEFRepository.Repositories
{
    /// <summary>
    /// 调度器仓储
    /// </summary>
    public class ScheduleRepositoryImpl : OscillatorRepositoryImpl<Schedule>, IScheduleRepository
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        public ScheduleRepositoryImpl(OscillatorSqlServerDBContext dbContext) : base(dbContext)
        {
        }
    }
}
