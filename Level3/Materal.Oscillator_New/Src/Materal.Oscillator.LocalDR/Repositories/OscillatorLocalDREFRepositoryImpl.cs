using Materal.TTA.Common;
using Materal.TTA.SqliteRepository;

namespace Materal.Oscillator.LocalDR.Repositories
{
    /// <summary>
    /// 本地容灾仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class OscillatorLocalDREFRepositoryImpl<T> : SqliteEFRepositoryImpl<T, Guid, OscillatorLocalDRDBContext>
        where T : class, IEntity<Guid>, new()
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        protected OscillatorLocalDREFRepositoryImpl(OscillatorLocalDRDBContext dbContext) : base(dbContext)
        {
        }
    }
}
