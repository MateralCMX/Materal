namespace RC.Core.Services
{
    public interface IEditServiceModel : IServiceModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
    }
}
