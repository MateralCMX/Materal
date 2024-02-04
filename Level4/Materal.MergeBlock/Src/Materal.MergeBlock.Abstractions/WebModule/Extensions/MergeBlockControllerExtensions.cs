using Materal.MergeBlock.Abstractions.WebModule.Controllers;

namespace Materal.MergeBlock.Abstractions.WebModule.Extensions
{
    /// <summary>
    /// MergeBlock控制器扩展
    /// </summary>
    public static class MergeBlockControllerExtensions
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="TAddRequestModel"></typeparam>
        /// <typeparam name="TEditRequestModel"></typeparam>
        /// <typeparam name="TQueryRequestModel"></typeparam>
        /// <typeparam name="TDTO"></typeparam>
        /// <typeparam name="TListDTO"></typeparam>
        /// <param name="controller"></param>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockModuleException"></exception>
        public static async Task<ICollection<TListDTO>> GetDataAsync<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO>(this IMergeBlockControllerBase<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO> controller, TQueryRequestModel queryModel)
            where TAddRequestModel : class, IAddRequestModel, new()
            where TEditRequestModel : class, IEditRequestModel, new()
            where TQueryRequestModel : IQueryRequestModel, new()
            where TDTO : class, IDTO, new()
            where TListDTO : class, IListDTO, new()
        {
            PageResultModel<TListDTO> data = await controller.GetListAsync(queryModel);
            if (data.ResultType != ResultTypeEnum.Success) throw new MergeBlockModuleException("从控制器获取数据失败");
            if (data.Data is null) return [];
            return data.Data;
        }
        /// <summary>
        /// 获得第一个或默认
        /// </summary>
        /// <typeparam name="TAddRequestModel"></typeparam>
        /// <typeparam name="TEditRequestModel"></typeparam>
        /// <typeparam name="TQueryRequestModel"></typeparam>
        /// <typeparam name="TDTO"></typeparam>
        /// <typeparam name="TListDTO"></typeparam>
        /// <param name="controller"></param>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockModuleException"></exception>
        public static async Task<TListDTO?> FirstOrDefaultAsync<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO>(this IMergeBlockControllerBase<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO> controller, TQueryRequestModel queryModel)
            where TAddRequestModel : class, IAddRequestModel, new()
            where TEditRequestModel : class, IEditRequestModel, new()
            where TQueryRequestModel : IQueryRequestModel, new()
            where TDTO : class, IDTO, new()
            where TListDTO : class, IListDTO, new()
        {
            ICollection<TListDTO> data = await controller.GetDataAsync(queryModel);
            return data.FirstOrDefault();
        }
        /// <summary>
        /// 获得第一个或默认
        /// </summary>
        /// <typeparam name="TAddRequestModel"></typeparam>
        /// <typeparam name="TEditRequestModel"></typeparam>
        /// <typeparam name="TQueryRequestModel"></typeparam>
        /// <typeparam name="TDTO"></typeparam>
        /// <typeparam name="TListDTO"></typeparam>
        /// <param name="controller"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockModuleException"></exception>
        public static async Task<TListDTO?> FirstOrDefaultAsync<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO>(this IMergeBlockControllerBase<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO> controller, Guid id)
            where TAddRequestModel : class, IAddRequestModel, new()
            where TEditRequestModel : class, IEditRequestModel, new()
            where TQueryRequestModel : IQueryRequestModel, new()
            where TDTO : class, IDTO, new()
            where TListDTO : class, IListDTO, new()
        {
            ICollection<TListDTO> data = await controller.GetDataAsync(new TQueryRequestModel()
            {
                IDs = [id],
                PageIndex = 1,
                PageSize = 1
            });
            return data.FirstOrDefault();
        }
    }
}
