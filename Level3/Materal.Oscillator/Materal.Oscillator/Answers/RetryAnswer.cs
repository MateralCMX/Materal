﻿using Materal.Abstractions;
using Materal.Oscillator.Abstractions.Answers;
using Materal.Oscillator.Abstractions.Common;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.QuartZExtend;
using Polly;

namespace Materal.Oscillator.Answers
{
    public class RetryAnswer : AnswerBase, IAnswer
    {
        /// <summary>
        /// 重试次数
        /// </summary>
        public int RetryCount { get; set; }
        /// <summary>
        /// 重试间隔(秒)
        /// </summary>
        public int RetryInterval { get; set; }
        private IRetryAnswerListener? _listener;
        public override async Task InitAsync()
        {
            _listener = MateralServices.GetServiceOrDefatult<IRetryAnswerListener>();
            await base.InitAsync();
        }
        public override async Task<bool> ExcuteAsync(string eventValue, Schedule schedule, ScheduleWorkView scheduleWork, Answer answer, IOscillatorJob job)
        {
            int repetition = RetryCount - 1;
            int repetitionInterval = RetryInterval;
            if (repetition < 0 || repetitionInterval <= 0) return true;
            string newEventValue = eventValue;
            _listener?.RetryTrigger(schedule, scheduleWork, 1, RetryCount, RetryInterval);
            await Task.Delay(repetitionInterval * 1000);
            try
            {
                int index = 0;
                if (repetition > 0)
                {
                    await Policy.Handle<Exception>()
                        .WaitAndRetryAsync(repetition, i =>
                        {
                            index = i + 1;
                            _listener?.RetryTrigger(schedule, scheduleWork, index, RetryCount, RetryInterval);
                            return TimeSpan.FromSeconds(repetitionInterval);
                        })
                        .ExecuteAsync(async () =>
                        {
                            newEventValue = await HandlerRetryJobAsync(eventValue, schedule, scheduleWork, job, index);
                        });
                }
                else
                {
                    newEventValue = await HandlerRetryJobAsync(eventValue, schedule, scheduleWork, job, index);
                }
            }
            catch (Exception)
            {
                _listener?.RetryFail(schedule, scheduleWork, RetryCount, RetryCount, RetryInterval);
                return true;
            }
            if (newEventValue != eventValue)
            {
                _listener?.RetrySuccess(schedule, scheduleWork, RetryCount, RetryCount, RetryInterval);
                await job.SendEventAsync(newEventValue, scheduleWork);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 处理重试任务
        /// </summary>
        /// <param name="scheduleWork"></param>
        /// <param name="jobIndex"></param>
        /// <returns></returns>
        private async Task<string> HandlerRetryJobAsync(string upEventValue, Schedule schedule, ScheduleWorkView scheduleWork, IOscillatorJob job, int nowIndex)
        {
            _listener?.RetryExcute(schedule, scheduleWork, nowIndex, RetryCount, RetryInterval);
            string eventValue = await job.HandlerJobAsync(scheduleWork);
            if (upEventValue == eventValue)
            {
                throw new OscillatorException("任务重试失败");
            }
            return eventValue;
        }
    }
}