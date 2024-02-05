namespace RC.ServerCenter.Abstractions.RequestModel.Project
{
    /// <summary>
    /// 项目添加请求模型
    /// </summary>
    public partial class AddProjectRequestModel : IAddRequestModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称为空"), StringLength(50, ErrorMessage = "名称过长")]
        public string Name { get; set; }  = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        [Required(ErrorMessage = "描述为空"), StringLength(200, ErrorMessage = "描述过长")]
        public string Description { get; set; }  = string.Empty;
    }
}
