using Microsoft.Extensions.Options;

namespace Materal.MergeBlock.AccessLog
{
    /// <summary>
    /// 截断大访问日志拦截器
    /// </summary>
    /// <param name="config"></param>
    public class TruncatedBigAccessLogInterceptor(IOptionsMonitor<AccessLogOptions> config) : IBigAccessLogInterceptor
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Handler(string value) => value[..config.CurrentValue.MaxBodySize];
    }
}
