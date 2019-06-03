using Materal.Common;
using System;

namespace Log.Service.Model.Log
{
    /// <summary>
    /// 查询日志过滤器模型
    /// </summary>
    public class QueryLogFilterModel : PageRequestModel
    {
        /// <summary>
        /// 应用程序
        /// </summary>
        public string Application { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 最小时间
        /// </summary>
        public DateTime? MinTime { get; set; }
        /// <summary>
        /// 最大时间
        /// </summary>
        public DateTime? MaxTime { get; set; }
    }
}
