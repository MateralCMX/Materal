﻿/*
 * Generator Code From MateralMergeBlock=>GeneratorEditRequestModelAsync
 */
namespace RC.ServerCenter.Abstractions.RequestModel.Project
{
    /// <summary>
    /// 项目修改请求模型
    /// </summary>
    public partial class EditProjectRequestModel : IEditRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识为空")]
        public Guid ID { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Required(ErrorMessage = "描述为空"), StringLength(200, ErrorMessage = "描述过长")]
        public string Description { get; set; } = string.Empty;
    }
}
