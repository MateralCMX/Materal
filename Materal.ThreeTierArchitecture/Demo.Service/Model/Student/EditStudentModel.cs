using System;

namespace Demo.Service.Model.Student
{
    /// <summary>
    /// 修改学生模型
    /// </summary>
    public class EditStudentModel : AddStudentModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
    }
}
