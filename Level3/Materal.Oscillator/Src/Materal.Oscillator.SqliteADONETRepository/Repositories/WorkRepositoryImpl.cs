using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;

namespace Materal.Oscillator.SqliteADONETRepository.Repositories
{
    /// <summary>
    /// 任务仓储
    /// </summary>
    public class WorkRepositoryImpl : OscillatorRepositoryImpl<Work>, IWorkRepository
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="unitOfWork"></param>
        public WorkRepositoryImpl(OscillatorUnitOfWorkImpl unitOfWork) : base(unitOfWork)
        {
        }
    }
}
