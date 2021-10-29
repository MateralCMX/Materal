using System;

namespace ConfigCenter.DataTransmitModel.Namespace
{
    /// <summary>
    /// 命名空间列表数据传输模型
    /// </summary>
    public class NamespaceListDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 项目唯一标识
        /// </summary>
        public Guid ProjectID { get; set; }
    }
}
