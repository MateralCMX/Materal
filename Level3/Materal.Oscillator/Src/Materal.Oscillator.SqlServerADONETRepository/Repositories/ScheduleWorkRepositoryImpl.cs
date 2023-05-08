using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;

namespace Materal.Oscillator.SqlServerADONETRepository.Repositories
{
    /// <summary>
    /// 调度器任务仓储
    /// </summary>
    public class ScheduleWorkRepositoryImpl : OscillatorRepositoryImpl<ScheduleWork>, IScheduleWorkRepository
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ScheduleWorkRepositoryImpl(OscillatorUnitOfWorkImpl unitOfWork) : base(unitOfWork)
        {
        }
    }
}
