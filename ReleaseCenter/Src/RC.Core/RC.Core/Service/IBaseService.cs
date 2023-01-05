using Materal.Model;
using RC.Core.DataTransmitModel;

namespace RC.Core.Services
{
    /// <summary>
    /// 服务
    /// </summary>
    /// <typeparam name="TAddModel"></typeparam>
    /// <typeparam name="TEditModel"></typeparam>
    /// <typeparam name="TQueryModel"></typeparam>
    /// <typeparam name="TDTO"></typeparam>
    /// <typeparam name="TListDTO"></typeparam>
    public interface IBaseService<TAddModel, TEditModel, TQueryModel, TDTO, TListDTO>
        where TAddModel : class, IServiceModel, new()
        where TEditModel : class, IEditServiceModel, IServiceModel, new()
        where TQueryModel : IQueryServiceModel, IServiceModel, new()
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
        Task<(List<TListDTO> data, PageModel pageInfo)> GetListAsync(TQueryModel model);
    }
}
