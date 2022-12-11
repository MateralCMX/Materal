using Materal.Oscillator.DR.Domain;

namespace Materal.Oscillator.DR.Repositories
{
    public interface IFlowRepository : IOscillatorDRRepository<Flow>
    {
        Task InitTableAsync();
    }
}
