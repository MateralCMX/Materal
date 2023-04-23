using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;

namespace Materal.Oscillator.SqliteRepositoryImpl.Repositories
{
    public class AnswerViewRepositoryImpl : OscillatorSqliteEFRepositoryImpl<AnswerView>, IAnswerViewRepository
    {
        public AnswerViewRepositoryImpl(OscillatorSqliteDBContext dbContext) : base(dbContext)
        {
        }
    }
}
