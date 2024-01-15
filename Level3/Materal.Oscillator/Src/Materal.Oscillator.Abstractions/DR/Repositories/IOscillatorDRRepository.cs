using Materal.Oscillator.Abstractions.Domain;

namespace Materal.Oscillator.Abstractions.DR.Repositories
{
    /// <summary>
    /// 容灾仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IOscillatorDRRepository<T> : IRepository<T, Guid>
        where T : BaseDomain, IDomain
    {
    }
}
