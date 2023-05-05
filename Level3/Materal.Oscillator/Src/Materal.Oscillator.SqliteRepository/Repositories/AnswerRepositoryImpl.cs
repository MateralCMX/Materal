using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;

namespace Materal.Oscillator.SqliteRepository.Repositories
{
    /// <summary>
    /// 响应仓储
    /// </summary>
    public class AnswerRepositoryImpl : OscillatorRepositoryImpl<Answer>, IAnswerRepository
    {
        /// <summary>
        /// 响应仓储
        /// </summary>
        /// <param name="dbContext"></param>
        public AnswerRepositoryImpl(OscillatorSqliteDBContext dbContext) : base(dbContext)
        {
        }
    }
}
