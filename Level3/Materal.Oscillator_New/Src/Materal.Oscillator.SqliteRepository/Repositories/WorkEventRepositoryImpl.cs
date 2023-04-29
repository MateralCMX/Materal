using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;

namespace Materal.Oscillator.SqliteRepository.Repositories
{
    /// <summary>
    /// 任务事件仓储
    /// </summary>
    public class WorkEventRepositoryImpl : OscillatorRepositoryImpl<WorkEvent>, IWorkEventRepository
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        public WorkEventRepositoryImpl(OscillatorDBContext dbContext) : base(dbContext)
        {
        }
    }
}
