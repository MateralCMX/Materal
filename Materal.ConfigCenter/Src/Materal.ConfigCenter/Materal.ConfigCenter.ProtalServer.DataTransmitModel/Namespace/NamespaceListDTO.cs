using System;

namespace Materal.ConfigCenter.ProtalServer.DataTransmitModel.Namespace
{
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
