/*
 * Generator Code From MateralMergeBlock=>GeneratorAddModelAsync
 */
namespace RC.Deploy.Abstractions.Services.Models.ApplicationInfo
{
    /// <summary>
    /// 应用程序信息添加模型
    /// </summary>
    public partial class AddApplicationInfoModel : IAddServiceModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称为空")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 根路径
        /// </summary>
        [Required(ErrorMessage = "根路径为空")]
        public string RootPath { get; set; } = string.Empty;
        /// <summary>
        /// 主模块
        /// </summary>
        [Required(ErrorMessage = "主模块为空")]
        public string MainModule { get; set; } = string.Empty;
        /// <summary>
        /// 应用程序类型
        /// </summary>
        [Required(ErrorMessage = "应用程序类型为空")]
        public ApplicationTypeEnum ApplicationType { get; set; }
        /// <summary>
        /// 增量更新
        /// </summary>
        public bool IsIncrementalUpdating { get; set; }
        /// <summary>
        /// 运行参数
        /// </summary>
        public string? RunParams { get; set; }
    }
}
