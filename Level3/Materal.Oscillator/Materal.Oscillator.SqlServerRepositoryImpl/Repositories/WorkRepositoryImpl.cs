using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;

namespace Materal.Oscillator.SqlServerRepositoryImpl.Repositories
{
    public class WorkRepositoryImpl : OscillatorSqlServerEFRepositoryImpl<Work>, IWorkRepository
    {
        public WorkRepositoryImpl(OscillatorSqlServerDBContext dbContext) : base(dbContext)
        {
        }
    }
}
