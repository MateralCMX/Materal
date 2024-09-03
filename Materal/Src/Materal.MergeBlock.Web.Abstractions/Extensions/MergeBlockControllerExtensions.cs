using Materal.MergeBlock.Abstractions;
using Materal.MergeBlock.Abstractions.Models;
using Materal.MergeBlock.Web.Abstractions.Controllers;
using Materal.Utils.Model;
using System.Reflection;

namespace Materal.MergeBlock.Web.Abstractions.Extensions
{
    /// <summary>
    /// MergeBlock控制器扩展
    /// </summary>
    public static class MergeBlockControllerExtensions
    {
        /// <summary>
        /// 设置分页信息
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        private static void SetPageInfo(this object queryModel, int pageSize, int pageIndex)
        {
            Type queryType = queryModel.GetType();
            queryType.GetProperty("PageIndex")?.SetValue(queryModel, pageIndex);
            queryType.GetProperty("PageSize")?.SetValue(queryModel, pageSize);
        }
        /// <summary>
        /// 获得查询模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputModel"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        private static (bool isQuery, T queryModel) GetQueryModel<T>(this object inputModel, int pageSize = 10, int pageIndex = 1)
            where T : new()
        {
            bool isQuery = false;
            T queryModel = new();
            Type inputType = inputModel.GetType();
            Type queryType = queryModel.GetType();
            foreach (PropertyInfo inputPropertyInfo in inputType.GetProperties())
            {
                QueryPropertyAttribute? queryPropertyAttribute = inputPropertyInfo.GetCustomAttribute<QueryPropertyAttribute>();
                if (queryPropertyAttribute == null) continue;
                if (!string.IsNullOrWhiteSpace(queryPropertyAttribute.TypeName) && queryPropertyAttribute.TypeName != queryType.Name) continue;
                PropertyInfo? queryPropertyInfo = queryType.GetProperty(queryPropertyAttribute.TargetProperty);
                if (queryPropertyInfo == null) continue;
                if (queryPropertyInfo.PropertyType != inputPropertyInfo.PropertyType) continue;
                object? value = inputPropertyInfo.GetValue(inputModel);
                if (value == null || value.IsNullOrWhiteSpaceString()) continue;
                queryPropertyInfo.SetValue(queryModel, value);
                isQuery = true;
            }
            if (isQuery)
            {
                queryModel.SetPageInfo(pageSize, pageIndex);
            }
            return (isQuery, queryModel);
        }
        /// <summary>
        /// 获得数据
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="requestModel"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static async Task<(bool isQuery, ICollection<TListDTO>? data)> GetDataAsync<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO>(this IMergeBlockController<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO> controller, object requestModel, int pageSize = 10, int pageIndex = 1)
            where TAddRequestModel : class, IAddRequestModel, new()
            where TEditRequestModel : class, IEditRequestModel, new()
            where TQueryRequestModel : IQueryRequestModel, new()
            where TDTO : class, IDTO, new()
            where TListDTO : class, IListDTO, new()
        {
            (bool isQuery, TQueryRequestModel queryModel) = requestModel.GetQueryModel<TQueryRequestModel>(pageSize, pageIndex);
            if (isQuery)
            {
                return (isQuery, await controller.GetDataAsync(queryModel));
            }
            return (false, []);
        }
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
        public static async Task<ICollection<TListDTO>> GetDataAsync<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO>(this IMergeBlockController<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO> controller, TQueryRequestModel queryModel)
            where TAddRequestModel : class, IAddRequestModel, new()
            where TEditRequestModel : class, IEditRequestModel, new()
            where TQueryRequestModel : IQueryRequestModel, new()
            where TDTO : class, IDTO, new()
            where TListDTO : class, IListDTO, new()
        {
            CollectionResultModel<TListDTO> data = await controller.GetListAsync(queryModel);
            if (data.ResultType != ResultTypeEnum.Success) return [];
            if (data.Data is null) return [];
            return data.Data;
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="TAddRequestModel"></typeparam>
        /// <typeparam name="TEditRequestModel"></typeparam>
        /// <typeparam name="TQueryRequestModel"></typeparam>
        /// <typeparam name="TDTO"></typeparam>
        /// <typeparam name="TListDTO"></typeparam>
        /// <param name="controller"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockModuleException"></exception>
        public static async Task<ICollection<TListDTO>> GetDataAsync<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO>(this IMergeBlockController<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO> controller, IEnumerable<Guid> ids)
            where TAddRequestModel : class, IAddRequestModel, new()
            where TEditRequestModel : class, IEditRequestModel, new()
            where TQueryRequestModel : IQueryRequestModel, new()
            where TDTO : class, IDTO, new()
            where TListDTO : class, IListDTO, new()
        {
            TQueryRequestModel requestModel = new()
            {
                IDs = [.. ids]
            };
            requestModel.SetPageInfo(requestModel.IDs.Count, 1);
            return await controller.GetDataAsync(requestModel);
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
        public static async Task<TListDTO?> FirstOrDefaultAsync<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO>(this IMergeBlockController<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO> controller, TQueryRequestModel queryModel)
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
        public static async Task<TListDTO?> FirstOrDefaultAsync<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO>(this IMergeBlockController<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO> controller, Guid id)
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
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <typeparam name="TAddRequestModel"></typeparam>
        /// <typeparam name="TEditRequestModel"></typeparam>
        /// <typeparam name="TQueryRequestModel"></typeparam>
        /// <typeparam name="TDTO"></typeparam>
        /// <typeparam name="TListDTO"></typeparam>
        /// <param name="controller"></param>
        /// <param name="id"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task BindDataAsync<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO>(this IMergeBlockController<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO> controller, Guid id, Action<TListDTO> action)
            where TAddRequestModel : class, IAddRequestModel, new()
            where TEditRequestModel : class, IEditRequestModel, new()
            where TQueryRequestModel : IQueryRequestModel, new()
            where TDTO : class, IDTO, new()
            where TListDTO : class, IListDTO, new()
        {
            TListDTO? dto = await controller.FirstOrDefaultAsync(id);
            if (dto is null) return;
            action(dto);
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <typeparam name="TAddRequestModel"></typeparam>
        /// <typeparam name="TEditRequestModel"></typeparam>
        /// <typeparam name="TQueryRequestModel"></typeparam>
        /// <typeparam name="TDTO"></typeparam>
        /// <typeparam name="TListDTO"></typeparam>
        /// <param name="controller"></param>
        /// <param name="requestModel"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task BindDataAsync<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO>(this IMergeBlockController<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO> controller, TQueryRequestModel requestModel, Action<TListDTO> action)
            where TAddRequestModel : class, IAddRequestModel, new()
            where TEditRequestModel : class, IEditRequestModel, new()
            where TQueryRequestModel : IQueryRequestModel, new()
            where TDTO : class, IDTO, new()
            where TListDTO : class, IListDTO, new()
        {
            ICollection<TListDTO> dtoList = await controller.GetDataAsync(requestModel);
            foreach (TListDTO dto in dtoList)
            {
                action(dto);
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <typeparam name="TAddRequestModel"></typeparam>
        /// <typeparam name="TEditRequestModel"></typeparam>
        /// <typeparam name="TQueryRequestModel"></typeparam>
        /// <typeparam name="TDTO"></typeparam>
        /// <typeparam name="TListDTO"></typeparam>
        /// <param name="controller"></param>
        /// <param name="ids"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task BindDataAsync<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO>(this IMergeBlockController<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO> controller, IEnumerable<Guid> ids, Action<TListDTO> action)
            where TAddRequestModel : class, IAddRequestModel, new()
            where TEditRequestModel : class, IEditRequestModel, new()
            where TQueryRequestModel : IQueryRequestModel, new()
            where TDTO : class, IDTO, new()
            where TListDTO : class, IListDTO, new()
        {
            ICollection<TListDTO> dtoList = await controller.GetDataAsync(ids);
            foreach (TListDTO dto in dtoList)
            {
                action(dto);
            }
        }
    }
}
