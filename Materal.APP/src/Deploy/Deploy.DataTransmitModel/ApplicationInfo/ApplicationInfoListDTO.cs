using System;
using Deploy.Enums;
using Materal.Common;

namespace Deploy.DataTransmitModel.ApplicationInfo
{
    /// <summary>
    /// 应用程序信息列表数据传输模型
    /// </summary>
    public class ApplicationInfoListDTO : ApplicationInfoSimpleListDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 停止参数
        /// </summary>
        public string StopParams { get; set; }
        /// <summary>
        /// 其他参数
        /// </summary>
        public string OtherParams { get; set; }
    }
}
