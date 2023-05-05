using Materal.Oscillator.Abstractions.DR.Domain;
using Materal.Oscillator.Abstractions.DR.Repositories;

namespace Materal.Oscillator.LocalDR.Repositories
{
    /// <summary>
    /// 流程仓储
    /// </summary>
    public class FlowRepositoryImpl : OscillatorLocalDREFRepositoryImpl<Flow>, IFlowRepository
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        public FlowRepositoryImpl(OscillatorLocalDRDBContext dbContext) : base(dbContext)
        {
        }
    }
}
