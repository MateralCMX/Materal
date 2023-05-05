using Materal.Oscillator.Abstractions.Domain;
using Materal.TTA.Common;

namespace Materal.Oscillator.Abstractions.Repositories
{
    /// <summary>
    /// Oscillator仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IOscillatorRepository<T> : IRepository<T, Guid>
        where T : BaseDomain, IDomain
    {
    }
}
