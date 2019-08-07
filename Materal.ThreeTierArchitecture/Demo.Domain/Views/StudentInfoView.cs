using Demo.Common;
using Materal.TTA.Common;
using System;

namespace Demo.Domain.Views
{
    /// <summary>
    /// 学生信息视图
    /// </summary>
    [ViewEntity]
    public class StudentInfoView : IEntity<Guid>
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public byte Age { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public SexEnum Sex { get; set; }
        /// <summary>
        /// 所属班级唯一标识
        /// </summary>
        public Guid BelongClassID { get; set; }
        /// <summary>
        /// 所属班级名称
        /// </summary>
        public string BelongClassName { get; set; }
    }
}
