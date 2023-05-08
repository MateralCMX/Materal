using Materal.TTA.Common;
using Materal.TTA.SqliteADONETRepository;

namespace Materal.Oscillator.DRSqliteADONETRepository.Repositories
{
    /// <summary>
    /// Oscillator仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class OscillatorDRRepositoryImpl<T> : SqliteADONETRepositoryImpl<T, Guid>
        where T : class, IEntity<Guid>, new()
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="unitOfWork"></param>
        protected OscillatorDRRepositoryImpl(OscillatorDRUnitOfWorkImpl unitOfWork) : base(unitOfWork)
        {
        }
    }
}
