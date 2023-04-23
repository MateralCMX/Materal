#nullable enable
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.DataTransmitModel;
using MBC.Demo.Enums;

namespace MBC.Demo.DataTransmitModel.User
{
    /// <summary>
    /// 用户列表数据传输模型
    /// </summary>
    public partial class UserListDTO: IListDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识为空")]
        public Guid ID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required(ErrorMessage = "创建时间为空")]
        public DateTime CreateTime { get; set; }
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
