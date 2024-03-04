/*
 * Generator Code From MateralMergeBlock=>GeneratorEditRequestModel
 */
namespace RC.Deploy.Abstractions.RequestModel.DefaultData
{
    /// <summary>
    /// 默认数据修改请求模型
    /// </summary>
    public partial class EditDefaultDataRequestModel : IEditRequestModel
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
