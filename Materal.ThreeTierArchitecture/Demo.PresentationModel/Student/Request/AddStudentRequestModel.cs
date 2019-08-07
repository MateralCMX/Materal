using Demo.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.PresentationModel.Student.Request
{
    public class AddStudentRequestModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = @"唯一标识不能为空"), StringLength(100, ErrorMessage = @"名称不能超过100个字符")]
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        [Required(ErrorMessage = @"年龄不能为空"), Range(byte.MinValue, byte.MaxValue, ErrorMessage = @"年龄格式错误")]
        public byte Age { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Required(ErrorMessage = @"性别不能为空")]
        public SexEnum Sex { get; set; }
        /// <summary>
        /// 所属班级唯一标识
        /// </summary>
        [Required(ErrorMessage = @"所属班级唯一标识不能为空")]
        public Guid BelongClassID { get; set; }
    }
}
