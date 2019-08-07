using Demo.Common;
using System;
using TTA.Core.Domain;

namespace Demo.Domain
{
    public class Student : BaseEntity<Guid>
    {
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
    }
}
