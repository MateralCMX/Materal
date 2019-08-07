using System;
using Demo.Common;

namespace Demo.Service.Model.Student
{
    /// <summary>
    /// 添加学生模型
    /// </summary>
    public class AddStudentModel
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
