﻿namespace Materal.Oscillator.Abstractions.Works
{
    /// <summary>
    /// 任务
    /// </summary>
    public interface IWork
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        string TypeName { get; }
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="workContext"></param>
        /// <returns></returns>
        Task ExecuteAsync(IWorkContext workContext);
        /// <summary>
        /// 成功执行
        /// </summary>
        /// <param name="workContext"></param>
        /// <returns></returns>
        Task SuccessExecuteAsync(IWorkContext workContext);
        /// <summary>
        /// 失败执行
        /// </summary>
        /// <param name="workContext"></param>
        /// <returns></returns>
        Task FailExecuteAsync(IWorkContext workContext);
        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="data"></param>
        void SetData(IWorkData data);
    }
}