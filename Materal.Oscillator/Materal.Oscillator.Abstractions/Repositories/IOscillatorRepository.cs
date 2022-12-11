using Materal.Oscillator.Abstractions.Domain;
using Materal.TTA.EFRepository;

namespace Materal.Oscillator.Abstractions.Repositories
{
    public interface IOscillatorRepository<T> : IEFRepository<T, Guid>
        where T : BaseDomain, IDomain
    {
    }
}
