namespace RC.Core.PresentationModel
{
    /// <summary>
    /// 编辑请求模型
    /// </summary>
    public interface IEditRequestModel : IRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
    }
}