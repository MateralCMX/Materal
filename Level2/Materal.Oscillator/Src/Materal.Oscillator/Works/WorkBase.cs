using Materal.Oscillator.Abstractions.Works;

namespace Materal.Oscillator.Works
{
    /// <summary>
    /// 任务基类
    /// </summary>
    public abstract class WorkBase<TData> : IWork
        where TData : IWorkData, new()
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName => GetType().Name;
        /// <summary>
        /// 数据
        /// </summary>
        public TData Data { get; set; } = new();
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="workContext"></param>
        /// <returns></returns>
        public abstract Task ExecuteAsync(IWorkContext workContext);
        /// <summary>
        /// 成功执行
        /// </summary>
        /// <param name="workContext"></param>
        /// <returns></returns>
        public virtual async Task SuccessExecuteAsync(IWorkContext workContext) => await Task.CompletedTask;
        /// <summary>
        /// 失败执行
        /// </summary>
        /// <param name="workContext"></param>
        /// <returns></returns>
        public virtual async Task FailExecuteAsync(IWorkContext workContext) => await Task.CompletedTask;
        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="OscillatorException"></exception>
        public void SetData(IWorkData data)
        {
            if (data.WorkTypeName != TypeName || data is not TData trueData) throw new OscillatorException("数据类型错误");
            Data = trueData;
        }
    }
}
