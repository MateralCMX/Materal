namespace Materal.Oscillator.Abstractions.Works
{
    /// <summary>
    /// 任务
    /// </summary>
    public interface IWork
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        Guid ID { get; }
        /// <summary>
        /// 类型名称
        /// </summary>
        string TypeName { get; }
        /// <summary>
        /// 数据类型
        /// </summary>
        Type DataType { get; }
        /// <summary>
        /// 任务数据
        /// </summary>
        IWorkData WorkData { get; }
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="oscillatorContext"></param>
        /// <returns></returns>
        Task ExecuteAsync(IOscillatorContext oscillatorContext);
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="oscillatorContext"></param>
        /// <returns></returns>
        Task SuccessAsync(IOscillatorContext oscillatorContext);
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="oscillatorContext"></param>
        /// <returns></returns>
        Task FailAsync(IOscillatorContext oscillatorContext);
        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="data"></param>
        Task SetDataAsync(IWorkData data);
    }
}
