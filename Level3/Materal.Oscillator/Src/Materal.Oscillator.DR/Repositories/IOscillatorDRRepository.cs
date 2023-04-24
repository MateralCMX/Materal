using Materal.Oscillator.Abstractions.Domain;
using Materal.TTA.EFRepository;

namespace Materal.Oscillator.DR.Repositories
{
    public interface IOscillatorDRRepository<T> : IEFRepository<T, Guid>
        where T : BaseDomain, IDomain
    {
    }
}
