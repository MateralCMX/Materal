#nullable enable
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.PresentationModel;
using RC.Demo.Enums;

namespace RC.Demo.PresentationModel.User
{
    /// <summary>
    /// 用户修改请求模型
    /// </summary>
    public partial class EditUserRequestModel : IEditRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识为空")]
        public Guid ID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名为空"), StringLength(100, ErrorMessage = "姓名过长")]
        public string Name { get; set; }  = string.Empty;
        /// <summary>
        /// 性别
        /// </summary>
        [Required(ErrorMessage = "性别为空")]
        public SexEnum Sex { get; set; } 
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "账号为空"), StringLength(50, ErrorMessage = "账号过长")]
        public string Account { get; set; }  = string.Empty;
    }
}
