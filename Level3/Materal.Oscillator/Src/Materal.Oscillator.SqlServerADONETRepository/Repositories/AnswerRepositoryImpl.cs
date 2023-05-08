using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;

namespace Materal.Oscillator.SqlServerADONETRepository.Repositories
{
    /// <summary>
    /// 响应仓储
    /// </summary>
    public class AnswerRepositoryImpl : OscillatorRepositoryImpl<Answer>, IAnswerRepository
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="unitOfWork"></param>
        public AnswerRepositoryImpl(OscillatorUnitOfWorkImpl unitOfWork) : base(unitOfWork)
        {
        }
    }
}
