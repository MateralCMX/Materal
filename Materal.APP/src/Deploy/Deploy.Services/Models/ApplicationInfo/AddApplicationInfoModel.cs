using System.ComponentModel.DataAnnotations;
using Deploy.Enums;

namespace Deploy.Services.Models.ApplicationInfo
{
    /// <summary>
    /// 添加应用程序信息模型
    /// </summary>
    public class AddApplicationInfoModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空"), StringLength(100, ErrorMessage = "名称长度不能超过100")]
        public string Name { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        [Required(ErrorMessage = "路径不能为空")]
        public string Path { get; set; }
        /// <summary>
        /// 主模块
        /// </summary>
        [Required(ErrorMessage = "主模块不能为空")]
        public string MainModule { get; set; }
        /// <summary>
        /// 应用程序类型
        /// </summary>
        public ApplicationTypeEnum ApplicationType { get; set; }
        /// <summary>
        /// 运行参数
        /// </summary>
        public string RunParams { get; set; }
    }
}
