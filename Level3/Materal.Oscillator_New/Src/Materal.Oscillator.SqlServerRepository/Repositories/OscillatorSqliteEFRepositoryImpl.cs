using Materal.TTA.Common;
using Materal.TTA.SqlServerEFRepository;

namespace Materal.Oscillator.SqlServerRepository.Repositories
{
    /// <summary>
    /// Oscillator仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class OscillatorRepositoryImpl<T> : SqlServerEFRepositoryImpl<T, Guid, OscillatorSqlServerDBContext>
        where T : class, IEntity<Guid>, new()
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        protected OscillatorRepositoryImpl(OscillatorSqlServerDBContext dbContext) : base(dbContext)
        {
        }
    }
}
