using Materal.Abstractions;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.QuartZExtend;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.Oscillator.Abstractions.Answers
{
    /// <summary>
    /// 响应 
    /// </summary>
    public abstract class AnswerBase : IAnswer
    {
        /// <summary>
        /// DI域
        /// </summary>
        protected readonly IServiceScope ServiceScope;
        /// <summary>
        /// DI域
        /// </summary>
        protected readonly IServiceProvider ServiceProvider;
        /// <summary>
        /// 构造方法
        /// </summary>
        protected AnswerBase()
        {
            if (MateralServices.Services == null) throw new OscillatorException("获取DI容器失败");
            ServiceScope = MateralServices.Services.CreateScope();
            ServiceProvider = ServiceScope.ServiceProvider;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public virtual Task InitAsync() => Task.CompletedTask;
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="answerData"></param>
        /// <returns></returns>
        public virtual IAnswer Deserialization(string answerData) => (IAnswer)answerData.JsonToObject(GetType());
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="eventValue"></param>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <param name="answer"></param>
        /// <param name="job"></param>
        /// <returns></returns>
        public abstract Task<bool> ExcuteAsync(string eventValue, Schedule schedule, ScheduleWork scheduleWork, Work work, Answer answer, IOscillatorJob job);
        /// <summary>
        /// 释放
        /// </summary>
        public virtual void Dispose()
        {
            ServiceScope.Dispose();
        }
    }
}
