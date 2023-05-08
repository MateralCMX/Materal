using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;

namespace Materal.Oscillator.SqliteADONETRepository.Repositories
{
    /// <summary>
    /// 计划仓储
    /// </summary>
    public class PlanRepositoryImpl : OscillatorRepositoryImpl<Plan>, IPlanRepository
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="unitOfWork"></param>
        public PlanRepositoryImpl(OscillatorUnitOfWorkImpl unitOfWork) : base(unitOfWork)
        {
        }
    }
}
