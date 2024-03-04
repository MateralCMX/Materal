/*
 * Generator Code From MateralMergeBlock=>GeneratorAddModel
 */
namespace RC.Deploy.Abstractions.Services.Models.DefaultData
{
    /// <summary>
    /// 默认数据添加模型
    /// </summary>
    public partial class AddDefaultDataModel : IAddServiceModel
    {
        /// <summary>
        /// 应用程序类型
        /// </summary>
        [Required(ErrorMessage = "应用程序类型为空")]
        public ApplicationTypeEnum ApplicationType { get; set; }
        /// <summary>
        /// 键
        /// </summary>
        [Required(ErrorMessage = "数据为空")]
        public string Key { get; set; } = string.Empty;
        /// <summary>
        /// 数据
        /// </summary>
        [Required(ErrorMessage = "数据为空")]
        public string Data { get; set; } = string.Empty;
    }
}
