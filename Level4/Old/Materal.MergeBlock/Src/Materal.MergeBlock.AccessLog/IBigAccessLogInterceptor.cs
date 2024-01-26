namespace Materal.MergeBlock.AccessLog
{
    /// <summary>
    /// 大访问日志拦截器
    /// </summary>
    public interface IBigAccessLogInterceptor
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string Handler(string value);
    }
}
