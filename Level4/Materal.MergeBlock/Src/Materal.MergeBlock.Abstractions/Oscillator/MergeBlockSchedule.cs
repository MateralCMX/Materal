//using Materal.Oscillator.Abstractions;
//using Materal.Oscillator.Abstractions.Models;
//using Materal.Oscillator.Abstractions.Works;

//namespace Materal.MergeBlock.Abstractions.Oscillator
//{
//    /// <summary>
//    /// MergeBlock调度器
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    public abstract class MergeBlockSchedule<T> : IOscillatorSchedule
//        where T : class, IWorkData, new()
//    {
//        /// <summary>
//        /// 名称
//        /// </summary>
//        public abstract string Name { get; }
//        /// <summary>
//        /// 调度器名称
//        /// </summary>
//        public virtual string ScheduleName => $"{Name}调度器";
//        /// <summary>
//        /// 调度器描述
//        /// </summary>
//        public virtual string? ScheduleDescription => $"{Name}调度器";
//        /// <summary>
//        /// 任务名称
//        /// </summary>
//        public virtual string WorkName => $"{Name}任务";
//        /// <summary>
//        /// 任务描述
//        /// </summary>
//        public virtual string? WorkDescription => $"{Name}任务";
//        /// <summary>
//        /// 获得计划组
//        /// </summary>
//        /// <returns></returns>
//        public virtual List<AddPlanModel> GetPlans()
//        {
//            List<AddPlanModel> result = [];
//            var addPlanModel = GetPlan();
//            addPlanModel.Description ??= addPlanModel.PlanTriggerData.GetDescriptionText();
//            result.Add(addPlanModel);
//            return result;
//        }
//        /// <summary>
//        /// 获得计划
//        /// </summary>
//        /// <returns></returns>
//        public abstract AddPlanModel GetPlan();
//        /// <summary>
//        /// 添加调度器
//        /// </summary>
//        /// <param name="host"></param>
//        /// <returns></returns>
//        public virtual async Task<Guid> AddSchedule(IOscillatorHost host)
//        {
//            var workID = await host.AddWorkAsync(new AddWorkModel
//            {
//                Name = WorkName,
//                Description = WorkDescription,
//                WorkData = new T()
//            });
//            var scheduleID = await host.AddScheduleAsync(new AddScheduleModel
//            {
//                Name = ScheduleName,
//                Plans = GetPlans(),
//                Enable = true,
//                Answers = [],
//                Description = ScheduleDescription,
//                Works = [new() { WorkID = workID }]
//            });
//            return scheduleID;
//        }
//    }
//}
