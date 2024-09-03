namespace Materal.MergeBlock.Abstractions.Services
{
    /// <summary>
    /// 服务
    /// </summary>
    public interface IBaseService
    {
        /// <summary>
        /// 登录用户ID
        /// </summary>
        Guid LoginUserID { get; set; }
    }
    /// <summary>
    /// 服务
    /// </summary>
    /// <typeparam name="TAddModel"></typeparam>
    /// <typeparam name="TEditModel"></typeparam>
    /// <typeparam name="TQueryModel"></typeparam>
    /// <typeparam name="TDTO"></typeparam>
    /// <typeparam name="TListDTO"></typeparam>
    public interface IBaseService<TAddModel, TEditModel, TQueryModel, TDTO, TListDTO> : IBaseService
        where TAddModel : class, IAddServiceModel, new()
        where TEditModel : class, IEditServiceModel, new()
        where TQueryModel : IQueryServiceModel, new()
        where TDTO : class, IDTO
        where TListDTO : class, IListDTO
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Guid> AddAsync(TAddModel model);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task EditAsync(TEditModel model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);
        /// <summary>
        /// 获得信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TDTO> GetInfoAsync(Guid id);
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<(List<TListDTO> data, RangeModel rangeInfo)> GetListAsync(TQueryModel model);
    }
}
