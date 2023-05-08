using Materal.TTA.Common;
using Materal.TTA.SqlServerEFRepository;

namespace Materal.Oscillator.DRSqlServerEFRepository.Repositories
{
    /// <summary>
    /// 容灾仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class OscillatorDREFRepositoryImpl<T> : SqlServerEFRepositoryImpl<T, Guid, OscillatorDRDBContext>
        where T : class, IEntity<Guid>, new()
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        protected OscillatorDREFRepositoryImpl(OscillatorDRDBContext dbContext) : base(dbContext)
        {
        }
    }
}
