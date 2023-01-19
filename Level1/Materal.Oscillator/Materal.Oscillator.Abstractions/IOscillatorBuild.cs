using Materal.Oscillator.Abstractions.Models.Answer;
using Materal.Oscillator.Abstractions.Models.Plan;
using Materal.Oscillator.Abstractions.Models.Schedule;
using Materal.Oscillator.Abstractions.Models.Work;
using Materal.Oscillator.Abstractions.Models.WorkEvent;

namespace Materal.Oscillator.Abstractions
{
    public interface IOscillatorBuild
    {
        /// <summary>
        /// 添加计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IOscillatorBuild AddPlan(PlanModel model);
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IOscillatorBuild AddWork(WorkModel model);
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="workID"></param>
        /// <returns></returns>
        public IOscillatorBuild AddWork(Guid workID);
        /// <summary>
        /// 添加任务事件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IOscillatorBuild AddWorkEvent(WorkEventModel model);
        /// <summary>
        /// 添加回应
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IOscillatorBuild AddAnswer(AnswerModel model);
        /// <summary>
        /// 构建
        /// </summary>
        /// <returns></returns>
        public Task<Guid> BuildAsync();
        /// <summary>
        /// 添加调度器
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public IOscillatorBuild AddSchedule(string name, string? description = null);
        /// <summary>
        /// 添加调度器
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IOscillatorBuild AddSchedule(ScheduleModel model);
    }
}
