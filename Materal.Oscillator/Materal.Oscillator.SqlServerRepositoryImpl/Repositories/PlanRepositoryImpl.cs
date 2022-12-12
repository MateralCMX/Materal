using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;

namespace Materal.Oscillator.SqlServerRepositoryImpl.Repositories
{
    public class PlanRepositoryImpl : OscillatorSqlServerEFRepositoryImpl<Plan>, IPlanRepository
    {
        public PlanRepositoryImpl(OscillatorSqlServerDBContext dbContext) : base(dbContext)
        {
        }
    }
}
