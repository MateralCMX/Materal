using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.PresentationModel.Student.Request
{
    public class EditStudentRequestModel : AddStudentRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = @"唯一标识不能为空")]
        public Guid ID { get; set; }
    }
}
