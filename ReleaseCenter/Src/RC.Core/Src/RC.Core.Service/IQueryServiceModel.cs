namespace RC.Core.Services
{
    public interface IQueryServiceModel : IServiceModel
    {
        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }
    }
}
