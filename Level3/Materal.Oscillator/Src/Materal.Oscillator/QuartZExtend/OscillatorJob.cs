using Dy.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Answers;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.DR;
using Materal.Oscillator.Abstractions.DR.Domain;
using Materal.Oscillator.Abstractions.Enums;
using Materal.Oscillator.Abstractions.Helper;
using Materal.Oscillator.Abstractions.QuartZExtend;
using Materal.Oscillator.Abstractions.Repositories;
using Materal.Oscillator.Abstractions.Works;
using Quartz;

namespace Materal.Oscillator.QuartZExtend
{
    /// <summary>
    /// 调度器作业
    /// </summary>
    public class OscillatorJob : IOscillatorJob, IJob, IDisposable
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
        private readonly IServiceScope _serviceScope;
        private readonly IServiceProvider _serviceProvider;
        private readonly IOscillatorListener? _oscillatorListener;
        private readonly IOscillatorDR? _oscillatorDR;
        private readonly IOscillatorUnitOfWork _unitOfWork;
        private readonly IWorkRepository _workRepository;
        private readonly IAnswerRepository _answerRepository;
        /// <summary>
        /// 调度器
        /// </summary>
        private Schedule? _schedule;
        /// <summary>
        /// 调度器任务组
        /// </summary>
        private ScheduleWork[]? _scheduleWorks;
        /// <summary>
        /// 任务组
        /// </summary>
        private Work[]? _works;
        /// <summary>
        /// 任务链位序
        /// </summary>
        private int _nowWorkIndex = 0;
        /// <summary>
        /// 容灾流程对象
        /// </summary>
        private Flow? _flow;
        /// <summary>
        /// 任务结果
        /// </summary>
        private readonly List<WorkResultModel> _workResults = new();
        /// <summary>
        /// 构造方法
        /// </summary>
        public OscillatorJob()
        {
            if (OscillatorServices.Services == null) throw new OscillatorException("获取DI容器失败");
            _serviceScope = OscillatorServices.Services.CreateScope();
            _serviceProvider = _serviceScope.ServiceProvider;
            _oscillatorListener = _serviceProvider.GetService<IOscillatorListener>();
            _oscillatorDR = _serviceProvider.GetService<IOscillatorDR>();
            _unitOfWork = _serviceProvider.GetRequiredService<IOscillatorUnitOfWork>();
            _workRepository = _unitOfWork.GetRepository<IWorkRepository>();
            _answerRepository = _unitOfWork.GetRepository<IAnswerRepository>();
        }
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            _serviceScope.Dispose();
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                await InitAsync(context.JobDetail.JobDataMap);
                await HandlerJobAsync();
            }
            catch (Exception ex)
            {
                if (_oscillatorListener == null || _schedule == null) return;
                await _oscillatorListener.ScheduleErrorAsync(_schedule, ex);
            }
        }
        /// <summary>
        /// 处理作业
        /// </summary>
        /// <param name="scheduleWork"></param>
        /// <returns></returns>
        public async Task<string> HandlerJobAsync(ScheduleWork scheduleWork)
        {
            if (_schedule == null || _works is null || _works.Length <= 0) return scheduleWork.FailEvent;
            Work work = _works.First(m => m.ID == scheduleWork.WorkID);
            IWorkData workData = OscillatorConvertHelper.ConvertToInterface<IWorkData>(work.WorkType, work.WorkData) ?? throw new OscillatorException("获取任务数据失败");
            string eventValue = scheduleWork.FailEvent;
            string? workResult = null;
            if (_workResults.Count >= _nowWorkIndex)//当前任务是否已经执行完毕
            {
                try
                {
                    Type workDataType = workData.GetType();
                    Type workBaseType = typeof(IWork<>);
                    workBaseType = workBaseType.MakeGenericType(workDataType);
                    object? workObj = _serviceProvider.GetService(workBaseType);
                    if (workObj is null || workObj is not IWork workExcute) throw new OscillatorException("获取任务失败");
                    workResult = await workExcute.ExcuteAsync(workData, _workResults, _nowWorkIndex, _schedule, scheduleWork, work);
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
                        await _oscillatorListener.WorkErrorAsync(_schedule, scheduleWork, work, ex);
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
                            await _oscillatorListener.WorkSuccessAsync(_schedule, scheduleWork, work, eventValue, workResult);
                        }
                        else if (eventValue == scheduleWork.FailEvent)
                        {
                            await _oscillatorListener.WorkFailAsync(_schedule, scheduleWork, work, eventValue, workResult);
                        }
                        await _oscillatorListener.WorkExecutedAsync(_schedule, scheduleWork, work, eventValue, workResult);
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
        /// <returns></returns>
        public async Task SendEventAsync(string eventValue, ScheduleWork scheduleWork)
        {
            if (eventValue == DefaultWorkEventEnum.Next.ToString())
            {
                await HandlerNextEventAsync(scheduleWork);
            }
            else if (eventValue == DefaultWorkEventEnum.Success.ToString())
            {
                await HandlerSuccessEventAsync(scheduleWork);
            }
            else if (eventValue == DefaultWorkEventEnum.Fail.ToString())
            {
                await HandlerFailEventAsync(scheduleWork);
            }
            else
            {
                await DefaultHandlerEventAsync(eventValue, scheduleWork);
            }
        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dataMap"></param>
        private async Task InitAsync(JobDataMap dataMap)
        {
            if (dataMap[ScheduleDataMapKey] is not Schedule schedule) return;
            _schedule = schedule;
            if (dataMap[WorksDataMapKey] is not ScheduleWork[] scheduleWorks || scheduleWorks.Length <= 0) return;
            if (scheduleWorks.Any(m => m.ScheduleID != _schedule.ID)) return;
            _scheduleWorks = scheduleWorks.OrderBy(m => m.Index).ToArray();
            Guid[] workIDs = _scheduleWorks.Select(m => m.WorkID).Distinct().ToArray();
            _works = (await _workRepository.FindAsync(m => workIDs.Contains(m.ID))).ToArray();
            if (dataMap.ContainsKey(FlowMapKey) && dataMap[FlowMapKey] is Flow flow)
            {
                _flow = flow;
                if (_flow.WorkResults != null)
                {
                    List<string?> workResults = _flow.WorkResults.JsonToObject<List<string?>>();
                    for (; _nowWorkIndex < workResults.Count && _nowWorkIndex < scheduleWorks.Length; _nowWorkIndex++)
                    {
                        ScheduleWork scheduleWork = scheduleWorks[_nowWorkIndex];
                        _workResults.Add(new WorkResultModel
                        {
                            Result = workResults[_nowWorkIndex],
                            ScheduleWork = scheduleWork
                        });
                    }
                }
            }
        }
        /// <summary>
        /// 处理作业
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
            ScheduleWork scheduleWork = _scheduleWorks[_nowWorkIndex];
            if (_schedule != null)
            {
                if (_oscillatorDR != null)
                {
                    if (_flow == null)
                    {
                        await _oscillatorDR.WorkExecuteAsync(_schedule, scheduleWork);
                    }
                    else
                    {
                        await _oscillatorDR.WorkExecuteAsync(_flow, scheduleWork);
                    }
                }
                if (_oscillatorListener != null && _works is not null && _works.Length > 0)
                {
                    Work work = _works.First(m => m.ID == scheduleWork.WorkID);
                    await _oscillatorListener.WorkExecuteAsync(_schedule, scheduleWork, work);
                }
            }
            string? eventValue = await HandlerJobAsync(scheduleWork);
            if (!string.IsNullOrWhiteSpace(eventValue))
            {
                await SendEventAsync(eventValue, scheduleWork);
                return;
            }
        }
        #region 事件处理
        /// <summary>
        /// 处理下一个事件
        /// </summary>
        /// <param name="scheduleWork"></param>
        /// <returns></returns>
        private async Task HandlerNextEventAsync(ScheduleWork scheduleWork)
        {
            if (_works is null || _works.Length <= 0) return;
            Work work = _works.First(m => m.ID == scheduleWork.WorkID);
            if (_oscillatorListener != null && _schedule != null)
            {
                await _oscillatorListener.WorkEventTriggerAsync(_schedule, scheduleWork, work, DefaultWorkEventEnum.Next.ToString());
            }
            _nowWorkIndex++;
            await HandlerJobAsync();
        }
        /// <summary>
        /// 处理成功
        /// </summary>
        /// <param name="scheduleWork"></param>
        /// <returns></returns>
        private async Task HandlerSuccessEventAsync(ScheduleWork scheduleWork)
        {
            if (_works is null || _works.Length <= 0) return;
            Work work = _works.First(m => m.ID == scheduleWork.WorkID);
            if (_oscillatorListener != null && _schedule != null)
            {
                await _oscillatorListener.WorkEventTriggerAsync(_schedule, scheduleWork, work, DefaultWorkEventEnum.Success.ToString());
            }
            await HandlerEventAsync(DefaultWorkEventEnum.Success.ToString(), scheduleWork, work);
            await SendEventAsync(DefaultWorkEventEnum.Complete.ToString(), scheduleWork);
        }
        /// <summary>
        /// 处理失败
        /// </summary>
        /// <param name="scheduleWork"></param>
        /// <returns></returns>
        private async Task HandlerFailEventAsync(ScheduleWork scheduleWork)
        {
            if (_works is null || _works.Length <= 0) return;
            Work work = _works.First(m => m.ID == scheduleWork.WorkID);
            if (_oscillatorListener != null && _schedule != null)
            {
                await _oscillatorListener.WorkEventTriggerAsync(_schedule, scheduleWork, work, DefaultWorkEventEnum.Fail.ToString());
            }
            await HandlerEventAsync(DefaultWorkEventEnum.Fail.ToString(), scheduleWork, work);
            await SendEventAsync(DefaultWorkEventEnum.Complete.ToString(), scheduleWork);
        }
        /// <summary>
        /// 默认处理事件
        /// </summary>
        /// <param name="eventValue"></param>
        /// <param name="scheduleWork"></param>
        private async Task DefaultHandlerEventAsync(string eventValue, ScheduleWork scheduleWork)
        {
            if (_works is null || _works.Length <= 0) return;
            Work work = _works.First(m => m.ID == scheduleWork.WorkID);
            if (_oscillatorListener != null && _schedule != null)
            {
                await _oscillatorListener.WorkEventTriggerAsync(_schedule, scheduleWork, work, eventValue);
            }
            await HandlerEventAsync(eventValue, scheduleWork, work);
        }
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="eventValue"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        private async Task HandlerEventAsync(string eventValue, ScheduleWork scheduleWork, Work work)
        {
            if (_schedule == null) return;
            List<Answer> answers = await _answerRepository.FindAsync(m => m.WorkEvent == eventValue && m.ScheduleID == _schedule.ID, m => m.Index, SortOrderEnum.Ascending);
            foreach (Answer answer in answers)
            {
                if (_oscillatorListener != null)
                {
                    await _oscillatorListener.AnswerExecuteAsync(_schedule, scheduleWork, work, answer);
                }
                using IAnswer? answerHandle = OscillatorConvertHelper.ConvertToInterface<IAnswer>(answer.AnswerType, answer.AnswerData);
                if (answerHandle == null) continue;
                try
                {
                    await answerHandle.InitAsync();
                    bool answerResult = await answerHandle.ExcuteAsync(eventValue, _schedule, scheduleWork, work, answer, this);
                    if (_oscillatorListener != null)
                    {
                        await _oscillatorListener.AnswerSuccessAsync(_schedule, scheduleWork, work, answer, answerResult);
                    }
                    if (!answerResult) return;
                }
                catch (Exception ex)
                {
                    if (_oscillatorListener != null)
                    {
                        await _oscillatorListener.AnswerFailAsync(_schedule, scheduleWork, work, answer, ex);
                    }
                    return;
                }
                finally
                {
                    if (_oscillatorListener != null)
                    {
                        await _oscillatorListener.AnswerExecutedAsync(_schedule, scheduleWork, work, answer);
                    }
                }
            }
        }
        #endregion
        #endregion
    }
}
