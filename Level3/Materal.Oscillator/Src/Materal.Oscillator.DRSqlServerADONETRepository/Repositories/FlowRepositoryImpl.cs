using Materal.Oscillator.Abstractions.DR.Domain;
using Materal.Oscillator.Abstractions.DR.Repositories;

namespace Materal.Oscillator.DRSqlServerADONETRepository.Repositories
{
    /// <summary>
    /// 流程仓储
    /// </summary>
    public class FlowRepositoryImpl : OscillatorDRRepositoryImpl<Flow>, IFlowRepository
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="unitOfWork"></param>
        public FlowRepositoryImpl(OscillatorDRUnitOfWorkImpl unitOfWork) : base(unitOfWork)
        {
        }
    }
}
