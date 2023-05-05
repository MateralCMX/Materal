using Materal.Oscillator.Abstractions.Domain;
using Materal.TTA.Common;

namespace Materal.Oscillator.DR.Repositories
{
    public interface IOscillatorDRRepository<T> : IRepository<T, Guid>
        where T : BaseDomain, IDomain
    {
    }
}
