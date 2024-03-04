/*
 * Generator Code From MateralMergeBlock=>GeneratorEditModel
 */
namespace RC.Deploy.Abstractions.Services.Models.DefaultData
{
    /// <summary>
    /// 默认数据修改模型
    /// </summary>
    public partial class EditDefaultDataModel : IEditServiceModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识为空")]
        public Guid ID { get; set; }
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
