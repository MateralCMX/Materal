using System;

namespace Demo.DataTransmitModel.Class
{
    /// <summary>
    /// 班级列表数据传输模型
    /// </summary>
    public class ClassListDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
