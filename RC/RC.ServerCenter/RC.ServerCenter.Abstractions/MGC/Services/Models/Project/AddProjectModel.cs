﻿/*
 * Generator Code From MateralMergeBlock=>GeneratorAddModelAsync
 */
namespace RC.ServerCenter.Abstractions.Services.Models.Project
{
    /// <summary>
    /// 项目添加模型
    /// </summary>
    public partial class AddProjectModel : IAddServiceModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称为空"), StringLength(50, ErrorMessage = "名称过长")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        [Required(ErrorMessage = "描述为空"), StringLength(200, ErrorMessage = "描述过长")]
        public string Description { get; set; } = string.Empty;
    }
}
