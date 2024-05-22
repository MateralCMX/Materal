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
        /// 唯一标识
        /// </summary>
        public Guid ID { get => Data.ID; set => Data.ID = value; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get => Data.Name; set => Data.Name = value; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName => GetType().Name;
        /// <summary>
        /// 数据
        /// </summary>
        public TData Data { get; set; } = new();
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="name"></param>
        public WorkBase(string name = "新任务") => Data.Name = name;
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
        /// <summary>
        /// 获得数据
        /// </summary>
        /// <returns></returns>
        public IWorkData GetData() => Data;
    }
}
