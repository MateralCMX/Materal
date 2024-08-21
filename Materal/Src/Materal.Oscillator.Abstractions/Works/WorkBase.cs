namespace Materal.Oscillator.Abstractions.Works
{
    /// <summary>
    /// 任务基类
    /// </summary>
    public abstract class WorkBase<TWorkData> : IWork
        where TWorkData : IWorkData, new()
    {
        /// <inheritdoc/>
        public Guid ID => Data.ID;
        /// <inheritdoc/>
        public string TypeName => GetType().FullName ?? throw new OscillatorException("获取任务类型名称失败");
        /// <inheritdoc/>
        public Type DataType => typeof(TWorkData);
        /// <inheritdoc/>
        public IWorkData WorkData => Data;
        /// <summary>
        /// 数据
        /// </summary>
        public TWorkData Data { get; set; } = new();
        /// <inheritdoc/>
        public virtual async Task ExecuteAsync(IOscillatorContext oscillatorContext)
        {
            try
            {
                await OnWorkStartAsync(oscillatorContext);
                await ExcuteWorkAsync(oscillatorContext);
            }
            catch (Exception ex)
            {
                oscillatorContext.WorkException = ex;
                oscillatorContext.IsSuccess = false;
            }
            finally
            {
                await OnWorkEndAsync(oscillatorContext);
            }
        }
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="oscillatorContext"></param>
        /// <returns></returns>
        protected virtual async Task ExcuteWorkAsync(IOscillatorContext oscillatorContext) => await Task.CompletedTask;
        /// <inheritdoc/>
        public virtual async Task SuccessAsync(IOscillatorContext oscillatorContext)
        {
            try
            {
                await OnWorkSuccessStartAsync(oscillatorContext);
                await ExcuteSuccessWorkAsync(oscillatorContext);
            }
            catch (Exception ex)
            {
                oscillatorContext.SuccessWorkException = ex;
            }
            finally
            {
                await OnWorkSuccessEndAsync(oscillatorContext);
            }
        }
        /// <summary>
        /// 执行成功任务
        /// </summary>
        /// <param name="oscillatorContext"></param>
        /// <returns></returns>
        protected virtual async Task ExcuteSuccessWorkAsync(IOscillatorContext oscillatorContext) => await Task.CompletedTask;
        /// <inheritdoc/>
        public virtual async Task FailAsync(IOscillatorContext oscillatorContext)
        {
            try
            {
                await OnWorkFailStartAsync(oscillatorContext);
                await ExcuteFailWorkAsync(oscillatorContext);
            }
            catch (Exception ex)
            {
                oscillatorContext.FailWorkException = ex;
            }
            finally
            {
                await OnWorkFailEndAsync(oscillatorContext);
            }
        }
        /// <summary>
        /// 执行失败任务
        /// </summary>
        /// <param name="oscillatorContext"></param>
        /// <returns></returns>
        protected virtual async Task ExcuteFailWorkAsync(IOscillatorContext oscillatorContext) => await Task.CompletedTask;
        /// <inheritdoc/>
        public async Task SetDataAsync(IWorkData data)
        {
            if (data is not TWorkData trueData) throw new OscillatorException("数据类型错误");
            Data = trueData;
            await InitAsync();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        protected virtual async Task InitAsync() => await Task.CompletedTask;
        /// <summary>
        /// 任务开始
        /// </summary>
        /// <param name="oscillatorContext"></param>
        /// <returns></returns>
        protected async Task OnWorkStartAsync(IOscillatorContext oscillatorContext)
        {
            foreach (IOscillatorListener oscillatorListener in oscillatorContext.Listeners)
            {
                await oscillatorListener.OnWorkStartAsync(oscillatorContext, this);
            }
        }
        /// <summary>
        /// 任务结束
        /// </summary>
        /// <param name="oscillatorContext"></param>
        /// <returns></returns>
        protected async Task OnWorkEndAsync(IOscillatorContext oscillatorContext)
        {
            foreach (IOscillatorListener oscillatorListener in oscillatorContext.Listeners)
            {
                await oscillatorListener.OnWorkEndAsync(oscillatorContext, this);
            }
        }
        /// <summary>
        /// 任务成功开始
        /// </summary>
        /// <param name="oscillatorContext"></param>
        /// <returns></returns>
        protected async Task OnWorkSuccessStartAsync(IOscillatorContext oscillatorContext)
        {
            foreach (IOscillatorListener oscillatorListener in oscillatorContext.Listeners)
            {
                await oscillatorListener.OnWorkSuccessStartAsync(oscillatorContext, this);
            }
        }
        /// <summary>
        /// 任务成功结束
        /// </summary>
        /// <param name="oscillatorContext"></param>
        /// <returns></returns>
        protected async Task OnWorkSuccessEndAsync(IOscillatorContext oscillatorContext)
        {
            foreach (IOscillatorListener oscillatorListener in oscillatorContext.Listeners)
            {
                await oscillatorListener.OnWorkSuccessEndAsync(oscillatorContext, this);
            }
        }
        /// <summary>
        /// 任务失败开始
        /// </summary>
        /// <param name="oscillatorContext"></param>
        /// <returns></returns>
        protected async Task OnWorkFailStartAsync(IOscillatorContext oscillatorContext)
        {
            foreach (IOscillatorListener oscillatorListener in oscillatorContext.Listeners)
            {
                await oscillatorListener.OnWorkFailStartAsync(oscillatorContext, this);
            }
        }
        /// <summary>
        /// 任务失败结束
        /// </summary>
        /// <param name="oscillatorContext"></param>
        /// <returns></returns>
        protected async Task OnWorkFailEndAsync(IOscillatorContext oscillatorContext)
        {
            foreach (IOscillatorListener oscillatorListener in oscillatorContext.Listeners)
            {
                await oscillatorListener.OnWorkFailEndAsync(oscillatorContext, this);
            }
        }
    }
}
