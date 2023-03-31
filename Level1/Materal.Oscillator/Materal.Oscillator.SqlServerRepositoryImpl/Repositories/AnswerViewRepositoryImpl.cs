using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;

namespace Materal.Oscillator.SqlServerRepositoryImpl.Repositories
{
    public class AnswerViewRepositoryImpl : OscillatorSqlServerEFRepositoryImpl<AnswerView>, IAnswerViewRepository
    {
        public AnswerViewRepositoryImpl(OscillatorSqlServerDBContext dbContext) : base(dbContext)
        {
        }
    }
}
