using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.PresentationModel.Class.Request
{
    public class EditClassRequestModel : AddClassRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = @"唯一标识不能为空")]
        public Guid ID { get; set; }
    }
}
