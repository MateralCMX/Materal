using Materal.BusinessFlow.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.WebAPIControllers.Models.User
{
    public class AddUserModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(40)]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 数据模型唯一标识
        /// </summary>
        [Required]
        public Guid DataModelID { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        [Required]
        public DataTypeEnum DataType { get; set; } = DataTypeEnum.String;
        /// <summary>
        /// 数据
        /// </summary>
        public string? Data { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
    }
}
