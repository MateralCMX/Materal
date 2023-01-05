namespace RC.Core.Services
{
    /// <summary>
    /// 修改模型
    /// </summary>
    public interface IEditServiceModel : IServiceModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
    }
}
