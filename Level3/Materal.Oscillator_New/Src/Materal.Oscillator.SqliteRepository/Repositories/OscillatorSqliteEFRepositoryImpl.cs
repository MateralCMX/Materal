using Materal.TTA.Common;
using Materal.TTA.SqliteRepository;

namespace Materal.Oscillator.SqliteRepository.Repositories
{
    /// <summary>
    /// Oscillator仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class OscillatorRepositoryImpl<T> : SqliteEFRepositoryImpl<T, Guid, OscillatorDBContext>
        where T : class, IEntity<Guid>, new()
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        protected OscillatorRepositoryImpl(OscillatorDBContext dbContext) : base(dbContext)
        {
        }
    }
}
