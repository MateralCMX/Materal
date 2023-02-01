using Materal.BaseCore.DataTransmitModel;
using System.ComponentModel.DataAnnotations;

namespace MBC.Demo.DataTransmitModel.User
{
    /// <summary>
    /// 用户列表数据传输模型
    /// </summary>
    public class UserListDTO : IListDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "姓名为空")]
        public Guid ID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名为空"), StringLength(100, ErrorMessage = "姓名过长")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "账号为空"), StringLength(100, ErrorMessage = "账号过长")]
        public string Account { get; set; } = string.Empty;
    }
}
