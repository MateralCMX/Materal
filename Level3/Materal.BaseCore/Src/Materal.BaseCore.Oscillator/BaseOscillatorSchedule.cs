using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Models;
using Materal.Oscillator.Abstractions.Works;

namespace Materal.BaseCore.Oscillator
{
    public abstract class BaseOscillatorSchedule<T> : IOscillatorSchedule
        where T : class, IWorkData, new()
    {
        public abstract string Name { get; }
        /// <summary>
        /// 调度器名称
        /// </summary>
        public virtual string ScheduleName => $"{Name}调度器";
        /// <summary>
        /// 调度器描述
        /// </summary>
        public virtual string? ScheduleDescription => $"{Name}调度器";
        /// <summary>
        /// 任务名称
        /// </summary>
        public virtual string WorkName => $"{Name}任务";
        /// <summary>
        /// 任务描述
        /// </summary>
        public virtual string? WorkDescription => $"{Name}任务";
        /// <summary>
        /// 获得计划组
        /// </summary>
        /// <returns></returns>
        public virtual List<AddPlanModel> GetPlans()
        {
            List<AddPlanModel> result = new();
            AddPlanModel addPlanModel = GetPlan();
            addPlanModel.Description ??= addPlanModel.PlanTriggerData.GetDescriptionText();
            result.Add(addPlanModel);
            return result;
        }
        /// <summary>
        /// 获得计划
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual AddPlanModel GetPlan() => throw new NotImplementedException();
        public virtual async Task<Guid> AddSchedule(IOscillatorHost host)
        {
            Guid workID = await host.AddWorkAsync(new AddWorkModel
            {
                Name = WorkName,
                Description = WorkDescription,
                WorkData = new T()
            });
            Guid scheduleID = await host.AddScheduleAsync(new AddScheduleModel
            {
                Name = ScheduleName,
                Plans = GetPlans(),
                Enable = true,
                Answers = new(),
                Description = ScheduleDescription,
                Works = new() { new() { WorkID = workID } }
            });
            return scheduleID;
        }
    }
}
