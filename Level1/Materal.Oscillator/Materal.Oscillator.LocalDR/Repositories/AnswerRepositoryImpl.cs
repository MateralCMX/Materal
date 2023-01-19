using Materal.Oscillator.DR.Domain;
using Materal.Oscillator.DR.Repositories;

namespace Materal.Oscillator.LocalDR.Repositories
{
    public class FlowRepositoryImpl : OscillatorLocalDREFRepositoryImpl<Flow>, IFlowRepository
    {
        public FlowRepositoryImpl(OscillatorLocalDRDBContext dbContext) : base(dbContext)
        {
        }
    }
}
