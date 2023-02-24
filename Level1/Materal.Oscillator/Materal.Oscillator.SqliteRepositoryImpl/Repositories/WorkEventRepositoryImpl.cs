using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Enums;
using Materal.Oscillator.Abstractions.Models.WorkEvent;
using Materal.Oscillator.Abstractions.Repositories;
using Materal.Utils.Model;

namespace Materal.Oscillator.SqliteRepositoryImpl.Repositories
{
    public class WorkEventRepositoryImpl : OscillatorSqliteEFRepositoryImpl<WorkEvent>, IWorkEventRepository
    {
        public WorkEventRepositoryImpl(OscillatorSqliteDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<string[]> GetAllWorkEventValuesAsync(Guid scheduleID)
        {
            string[] defaultEventValues = GetDefaultWorkEventValues();
            string[] eventValues = await GetWorkEventValuesAsync(scheduleID);
            List<string> result = new();
            result.AddRange(defaultEventValues);
            result.AddRange(eventValues);
            return result.ToArray();
        }
        public string[] GetDefaultWorkEventValues()
        {
            string[] result = KeyValueModel<DefaultWorkEventEnum>.GetAllCode().Select(m => m.Key.ToString()).ToArray();
            return result;
        }
        public async Task<string[]> GetWorkEventValuesAsync(Guid scheduleID)
        {
            IList<WorkEvent> workEvent = await FindAsync(new QueryWorkEventManagerModel
            {
                ScheduleID = scheduleID
            });
            string[] result = workEvent.Select(m => m.Value).ToArray();
            return result;
        }
    }
}
