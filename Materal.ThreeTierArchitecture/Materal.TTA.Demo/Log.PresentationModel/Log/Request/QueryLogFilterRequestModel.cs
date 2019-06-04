using System;
using System.Collections.Generic;
using System.Text;
using Materal.Common;

namespace Log.PresentationModel.Log.Request
{
    /// <summary>
    /// 查询日志过滤器请求模型
    /// </summary>
    public class QueryLogFilterRequestModel : PageRequestModel
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
