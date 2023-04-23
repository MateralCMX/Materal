using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;

namespace Materal.Oscillator.SqlServerRepositoryImpl.Repositories
{
    public class WorkEventViewRepositoryImpl : OscillatorSqlServerEFRepositoryImpl<WorkEventView>, IWorkEventViewRepository
    {
        public WorkEventViewRepositoryImpl(OscillatorSqlServerDBContext dbContext) : base(dbContext)
        {
        }
    }
}
