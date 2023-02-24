using Materal.Abstractions;
using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Answers;
using Materal.Oscillator.Abstractions.Common;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Enums;
using Materal.Oscillator.Abstractions.Models.Answer;
using Materal.Oscillator.Abstractions.QuartZExtend;
using Materal.Oscillator.Abstractions.Repositories;
using Materal.Oscillator.Abstractions.Works;
using Materal.Oscillator.AutoMapperProfile;
using Materal.Oscillator.DR;
using Materal.Oscillator.DR.Domain;
using Quartz;

namespace Materal.Oscillator.QuartZExtend
{
    public class OscillatorJob : IOscillatorJob, IJob
    {
        /// <summary>
        /// 调度器数据Key
        /// </summary>
        public const string ScheduleDataMapKey = "Schedule";
        /// <summary>
        /// 任务组数据Key
        /// </summary>
        public const string WorksDataMapKey = "Works";
        /// <summary>
        /// 流程数据Key
        /// </summary>
        public const string FlowMapKey = "Flow";
        private IWorkEventBus? _jobBus;
        private IOscillatorListener? _oscillatorListener;
        private IOscillatorDR? _oscillatorDR;
        private IWorkEventRepository? _workEventRepository;
        private IAnswerRepository? _answerRepository;
        /// <summary>
        /// 调度器
        /// </summary>
        private Schedule? _schedule;
        /// <summary>
        /// 任务组
        /// </summary>
        private ScheduleWorkView[]? _scheduleWorks;
        /// <summary>
        /// 任务链位序
        /// </summary>
        private int _nowWorkIndex = 0;
        /// <summary>
        /// 任务链结束标志
        /// </summary>
        private bool _isOver = false;
        /// <summary>
        /// 容灾流程对象
        /// </summary>
        private Flow? _flow;
        /// <summary>
        /// 任务结果
        /// </summary>
        private readonly List<WorkResultModel> _workResults = new();
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                #region 获取服务
                _jobBus = MateralServices.GetService<IWorkEventBus>();
                _workEventRepository = MateralServices.GetService<IWorkEventRepository>();
                _answerRepository = MateralServices.GetService<IAnswerRepository>();
                _oscillatorListener = MateralServices.GetServiceOrDefatult<IOscillatorListener>();
                _oscillatorDR = MateralServices.GetServiceOrDefatult<IOscillatorDR>();
                #endregion
                #region 设置数据
                JobDataMap dataMap = context.JobDetail.JobDataMap;
                if (dataMap[ScheduleDataMapKey] is not Schedule schedule) return;
                _schedule = schedule;
                if (dataMap[WorksDataMapKey] is not ScheduleWorkView[] scheduleWorks || scheduleWorks.Length <= 0) return;
                if (scheduleWorks.Any(m => m.ScheduleID != _schedule.ID)) return;
                _scheduleWorks = scheduleWorks.OrderBy(m => m.OrderIndex).ToArray();
                if (dataMap.ContainsKey(FlowMapKey) && dataMap[FlowMapKey] is Flow flow)
                {
                    _flow = flow;
                    if(_flow.WorkResults != null)
                    {
                        List<string?> workResults = _flow.WorkResults.JsonToObject<List<string?>>();
                        for (; _nowWorkIndex < workResults.Count && _nowWorkIndex < scheduleWorks.Length; _nowWorkIndex++)
                        {
                            ScheduleWorkView scheduleWork = scheduleWorks[_nowWorkIndex];
                            _workResults.Add(new WorkResultModel
                            {
                                Result = workResults[_nowWorkIndex],
                                ScheduleWork = scheduleWork
                            });
                        }
                    }
                }
                #endregion
                #region 订阅事件
                string[] eventValues = await _workEventRepository.GetAllWorkEventValuesAsync(_schedule.ID);
                foreach (string item in eventValues)
                {
                    if (item == DefaultWorkEventEnum.Next.ToString())
                    {
                        _jobBus?.Subscribe(DefaultWorkEventEnum.Next.ToString(), (_, work) => HandlerNextAsync(work));
                    }
                    else if (item == DefaultWorkEventEnum.Success.ToString())
                    {
                        _jobBus?.Subscribe(DefaultWorkEventEnum.Success.ToString(), (_, work) => HandlerSuccessEventAsync(work));
                    }
                    else if (item == DefaultWorkEventEnum.Fail.ToString())
                    {
                        _jobBus?.Subscribe(DefaultWorkEventEnum.Fail.ToString(), (_, work) => HandlerFailEventAsync(work));
                    }
                    else
                    {
                        _jobBus?.Subscribe(item, DefaultHandlerEventAsync);
                    }
                }
                #endregion
                await HandlerJobAsync();
            }
            catch (Exception ex)
            {
                if (_oscillatorListener != null && _schedule != null)
                {
                    await _oscillatorListener.ScheduleErrorAsync(_schedule, ex);
                }
            }
        }
        /// <summary>
        /// 处理任务
        /// </summary>
        /// <returns></returns>
        private async Task HandlerJobAsync()
        {
            if (_scheduleWorks == null || _scheduleWorks.Length <= 0) return;
            if (_nowWorkIndex >= _scheduleWorks.Length)
            {
                await SendEventAsync(DefaultWorkEventEnum.Success.ToString(), _scheduleWorks.Last());
                return;
            }
            ScheduleWorkView scheduleWork = _scheduleWorks[_nowWorkIndex];
            if(_schedule != null)
            {
                if (_oscillatorDR != null)
                {
                    if(_flow == null)
                    {
                        await _oscillatorDR.WorkExecuteAsync(_schedule, scheduleWork);
                    }
                    else
                    {
                        await _oscillatorDR.WorkExecuteAsync(_flow, scheduleWork);
                    }
                }
                if (_oscillatorListener != null)
                {
                    await _oscillatorListener.WorkExecuteAsync(_schedule, scheduleWork);
                }
            }
            string? eventValue = await HandlerJobAsync(scheduleWork);
            if (!string.IsNullOrWhiteSpace(eventValue))
            {
                await SendEventAsync(eventValue, scheduleWork);
                return;
            }
        }
        /// <summary>
        /// 处理任务
        /// </summary>
        /// <param name="scheduleWork"></param>
        /// <returns></returns>
        public async Task<string> HandlerJobAsync(ScheduleWorkView scheduleWork)
        {
            if (_schedule == null) return scheduleWork.FailEvent;
            IWork? workData = OscillatorConvertHelper.ConvertToInterface<IWork>(scheduleWork.WorkType, scheduleWork.WorkData);
            if (workData == null) throw new OscillatorException("获取任务数据失败");
            string eventValue = scheduleWork.FailEvent;
            string? workResult = null;
            if(_workResults.Count >= _nowWorkIndex)//当前任务是否已经执行完毕
            {
                try
                {
                    await workData.InitAsync();
                    workResult = await workData.ExcuteAsync(_workResults, _nowWorkIndex, _schedule, scheduleWork);
                    _workResults.Add(new WorkResultModel
                    {
                        ScheduleWork = scheduleWork,
                        Result = workResult
                    });
                    eventValue = scheduleWork.SuccessEvent;
                }
                catch (Exception ex)
                {
                    if (_oscillatorListener != null && _schedule != null)
                    {
                        await _oscillatorListener.WorkErrorAsync(_schedule, scheduleWork, ex);
                    }
                    return eventValue;
                }
                finally
                {
                    if (_oscillatorDR != null)
                    {
                        if (_flow == null)
                        {
                            await _oscillatorDR.WorkExecutedAsync(_schedule, scheduleWork, workResult);
                        }
                        else
                        {
                            await _oscillatorDR.WorkExecutedAsync(_flow, scheduleWork, workResult);
                        }
                    }
                    if (_oscillatorListener != null)
                    {
                        if (eventValue == scheduleWork.SuccessEvent)
                        {
                            await _oscillatorListener.WorkSuccessAsync(_schedule, scheduleWork, eventValue, workResult);
                        }
                        else if (eventValue == scheduleWork.FailEvent)
                        {
                            await _oscillatorListener.WorkFailAsync(_schedule, scheduleWork, eventValue, workResult);
                        }
                        await _oscillatorListener.WorkExecutedAsync(_schedule, scheduleWork, eventValue, workResult);
                    }
                }
            }
            else
            {
                eventValue = scheduleWork.SuccessEvent;
            }
            return eventValue;
        }
        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="eventValue"></param>
        /// <param name="scheduleWork"></param>
        public async Task SendEventAsync(string eventValue, ScheduleWorkView scheduleWork)
        {
            if (_jobBus == null) return;
            if (_isOver && eventValue == DefaultWorkEventEnum.Complete.ToString()) return;
            await _jobBus.SendEventAsync(eventValue, scheduleWork);
            if (eventValue == DefaultWorkEventEnum.Complete.ToString())
            {
                _isOver = true;
            }
        }
        #region 事件处理
        /// <summary>
        /// 处理下一个
        /// </summary>
        /// <param name="scheduleWork"></param>
        /// <returns></returns>
        private async Task HandlerNextAsync(ScheduleWorkView scheduleWork)
        {
            if (_oscillatorListener != null && _schedule != null)
            {
                await _oscillatorListener.WorkEventTriggerAsync(_schedule, scheduleWork, DefaultWorkEventEnum.Next.ToString());
            }
            _nowWorkIndex++;
            await HandlerJobAsync();
        }
        /// <summary>
        /// 处理成功
        /// </summary>
        /// <param name="scheduleWork"></param>
        /// <returns></returns>
        private async Task HandlerSuccessEventAsync(ScheduleWorkView scheduleWork)
        {
            if (_oscillatorListener != null && _schedule != null)
            {
                await _oscillatorListener.WorkEventTriggerAsync(_schedule, scheduleWork, DefaultWorkEventEnum.Success.ToString());
            }
            await HandlerEventAsync(DefaultWorkEventEnum.Success.ToString(), scheduleWork);
            await SendEventAsync(DefaultWorkEventEnum.Complete.ToString(), scheduleWork);
        }
        /// <summary>
        /// 处理失败
        /// </summary>
        /// <param name="scheduleWork"></param>
        /// <returns></returns>
        private async Task HandlerFailEventAsync(ScheduleWorkView scheduleWork)
        {
            if (_oscillatorListener != null && _schedule != null)
            {
                await _oscillatorListener.WorkEventTriggerAsync(_schedule, scheduleWork, DefaultWorkEventEnum.Fail.ToString());
            }
            await HandlerEventAsync(DefaultWorkEventEnum.Fail.ToString(), scheduleWork);
            await SendEventAsync(DefaultWorkEventEnum.Complete.ToString(), scheduleWork);
        }
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="eventValue"></param>
        /// <param name="scheduleWork"></param>
        /// <returns></returns>
        private async Task HandlerEventAsync(string eventValue, ScheduleWorkView scheduleWork)
        {
            if (_answerRepository == null) return;
            if (_schedule == null) return;
            List<Answer> answers = (await _answerRepository.FindAsync(new QueryAnswerManagerModel
            {
                WorkEvent = eventValue,
                ScheduleID = _schedule.ID,
                Enable = true
            })).OrderBy(m => m.OrderIndex).ToList();
            foreach (Answer answer in answers)
            {
                if (_oscillatorListener != null)
                {
                    await _oscillatorListener.AnswerExecuteAsync(_schedule, scheduleWork, answer);
                }
                IAnswer? answerHandle = OscillatorConvertHelper.ConvertToInterface<IAnswer>(answer.AnswerType, answer.AnswerData);
                if (answerHandle == null) continue;
                try
                {
                    await answerHandle.InitAsync();
                    bool answerResult = await answerHandle.ExcuteAsync(eventValue, _schedule, scheduleWork, answer, this);
                    if (_oscillatorListener != null)
                    {
                        await _oscillatorListener.AnswerSuccessAsync(_schedule, scheduleWork, answer, answerResult);
                    }
                    if (!answerResult) return;
                }
                catch (Exception ex)
                {
                    if (_oscillatorListener != null)
                    {
                        await _oscillatorListener.AnswerFailAsync(_schedule, scheduleWork, answer, ex);
                    }
                    return;
                }
                finally
                {
                    if (_oscillatorListener != null)
                    {
                        await _oscillatorListener.AnswerExecutedAsync(_schedule, scheduleWork, answer);
                    }
                }
            }
        }
        /// <summary>
        /// 默认处理事件
        /// </summary>
        /// <param name="eventValue"></param>
        /// <param name="scheduleWork"></param>
        private async Task DefaultHandlerEventAsync(string eventValue, ScheduleWorkView scheduleWork)
        {
            if (_oscillatorListener != null && _schedule != null)
            {
                await _oscillatorListener.WorkEventTriggerAsync(_schedule, scheduleWork, eventValue);
            }
            await HandlerEventAsync(eventValue, scheduleWork);
        }
        #endregion
    }
}
