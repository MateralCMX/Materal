using Materal.Oscillator.Abstractions.DR.Domain;
using Materal.Oscillator.Abstractions.DR.Repositories;

namespace Materal.Oscillator.DRSqliteEFRepository.Repositories
{
    /// <summary>
    /// 流程仓储
    /// </summary>
    public class FlowRepositoryImpl : OscillatorDRRepositoryImpl<Flow>, IFlowRepository
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        public FlowRepositoryImpl(OscillatorDRDBContext dbContext) : base(dbContext)
        {
        }
    }
}
