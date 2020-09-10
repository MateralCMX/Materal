using System;

namespace ConfigCenter.DataTransmitModel.Project
{
    /// <summary>
    /// 项目列表数据传输模型
    /// </summary>
    public class ProjectListDTO
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
    }
}
