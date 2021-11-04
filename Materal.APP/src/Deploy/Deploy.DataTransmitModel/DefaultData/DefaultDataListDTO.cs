using Deploy.Enums;
using Materal.Common;
using System;

namespace Deploy.DataTransmitModel.DefaultData
{
    /// <summary>
    /// 默认数据列表数据传输模型
    /// </summary>
    public class DefaultDataListDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public ApplicationTypeEnum ApplicationType { get; set; }
        /// <summary>
        /// 类型字符
        /// </summary>
        public string ApplicationTypeString => ApplicationType.GetDescription();
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public string Data { get; set; }
    }
}
