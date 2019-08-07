using System.ComponentModel.DataAnnotations;

namespace Demo.PresentationModel.Class.Request
{
    public class AddClassRequestModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = @"唯一标识不能为空"), StringLength(100, ErrorMessage = @"名称不能超过100个字符")]
        public string Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
