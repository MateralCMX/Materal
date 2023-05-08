using Materal.TTA.Common;
using Materal.TTA.SqliteADONETRepository;

namespace Materal.Oscillator.SqliteADONETRepository.Repositories
{
    /// <summary>
    /// Oscillator仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class OscillatorRepositoryImpl<T> : SqliteADONETRepositoryImpl<T, Guid>
        where T : class, IEntity<Guid>, new()
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="unitOfWork"></param>
        protected OscillatorRepositoryImpl(OscillatorUnitOfWorkImpl unitOfWork) : base(unitOfWork)
        {
        }
    }
}
