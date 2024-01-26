namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// MergeBlockController扩展
    /// </summary>
    public static class IMergeBlockControllerBaseExtensions
    {
        /// <summary>
        /// 获取第一个或者默认
        /// </summary>
        /// <typeparam name="TAddRequestModel"></typeparam>
        /// <typeparam name="TEditRequestModel"></typeparam>
        /// <typeparam name="TQueryRequestModel"></typeparam>
        /// <typeparam name="TDTO"></typeparam>
        /// <typeparam name="TListDTO"></typeparam>
        /// <param name="controller"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<TListDTO?> FirstOrDefaultAsync<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO>(this IMergeBlockControllerBase<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO> controller, Guid id)
            where TAddRequestModel : class, IAddRequestModel, new()
            where TEditRequestModel : class, IEditRequestModel, new()
            where TQueryRequestModel : IQueryRequestModel, new()
            where TDTO : class, IDTO, new()
            where TListDTO : class, IListDTO, new()
        {
            PageResultModel<TListDTO> data = await controller.GetListAsync(new TQueryRequestModel { PageIndex = 1, PageSize = 1, IDs = [id] });
            return data.Data?.FirstOrDefault();
        }
    }
}
