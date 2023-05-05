using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;

namespace Materal.Oscillator.SqliteRepository.Repositories
{
    /// <summary>
    /// 调度器任务仓储
    /// </summary>
    public class ScheduleWorkRepositoryImpl : OscillatorRepositoryImpl<ScheduleWork>, IScheduleWorkRepository
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        public ScheduleWorkRepositoryImpl(OscillatorSqliteDBContext dbContext) : base(dbContext)
        {
        }
    }
}
