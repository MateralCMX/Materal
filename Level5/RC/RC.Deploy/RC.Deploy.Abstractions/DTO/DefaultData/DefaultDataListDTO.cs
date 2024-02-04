namespace RC.Deploy.Abstractions.DTO.DefaultData
{
    /// <summary>
    /// 默认数据列表数据传输模型
    /// </summary>
    public partial class DefaultDataListDTO 
    {
        /// <summary>
        /// 应用程序类型文本
        /// </summary>
        [Required(ErrorMessage = "应用程序类型为空")]
        public string ApplicationTypeText => ApplicationType.GetDescription();
    }
}
