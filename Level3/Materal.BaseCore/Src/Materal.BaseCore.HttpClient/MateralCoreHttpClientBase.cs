using Materal.Abstractions;
using Materal.BaseCore.DataTransmitModel;
using Materal.BaseCore.HttpClient.Extensions;
using Materal.BaseCore.PresentationModel;
using Materal.Utils.Http;
using Materal.Utils.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Materal.BaseCore.HttpClient
{
    /// <summary>
    /// Http客户端基类
    /// </summary>
    public abstract class MateralCoreHttpClientBase
    {
        /// <summary>
        /// 日志
        /// </summary>
        protected readonly ILogger? Logger;
        protected readonly IHttpHelper httpHelper;
        /// <summary>
        /// 默认编码
        /// </summary>
        protected readonly Encoding DefaultEncoding = Encoding.UTF8;
        protected MateralCoreHttpClientBase(IServiceProvider serviceProvider)
        {
            Logger = serviceProvider.GetService<ILogger<MateralCoreHttpClientBase>>();
            httpHelper = serviceProvider.GetRequiredService<IHttpHelper>();
        }
        /// <summary>
        /// 获得真实Url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected virtual string GetTrueUrl(string url) => $"{HttpClientConfig.HttpClienUrltConfig.BaseUrl}api/{url}";
        /// <summary>
        /// 获得默认头部
        /// </summary>
        /// <returns></returns>
        protected virtual Dictionary<string, string> GetDefaultHeaders()
        {
            Dictionary<string, string> result = new()
            {
                ["Content-Type"] = "application/json"
            };
            return result;
        }
        /// <summary>
        /// 获得Post返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task<string> GetResultByPostAsync(string url, Dictionary<string, string>? queryParams = null, object? data = null)
        {
            data?.Validation();
            Dictionary<string, string> httpHeaders = GetDefaultHeaders();
            string trueUrl = GetTrueUrl(url);
            string httpReslt = await httpHelper.SendPostAsync(trueUrl, queryParams, data, httpHeaders, DefaultEncoding);
            if (string.IsNullOrWhiteSpace(httpReslt)) throw new MateralHttpException("返回值内容为空");
            return httpReslt;
        }
        /// <summary>
        /// 获得Put返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task<string> GetResultByPutAsync(string url, Dictionary<string, string>? queryParams = null, object? data = null)
        {
            data?.Validation();
            Dictionary<string, string> httpHeaders = GetDefaultHeaders();
            string trueUrl = GetTrueUrl(url);
            string httpReslt = await httpHelper.SendPutAsync(trueUrl, queryParams, data, httpHeaders, DefaultEncoding);
            if (string.IsNullOrWhiteSpace(httpReslt)) throw new MateralHttpException("返回值内容为空");
            return httpReslt;
        }
        /// <summary>
        /// 获得Patch返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task<string> GetResultByPathAsync(string url, Dictionary<string, string>? queryParams = null, object? data = null)
        {
            data?.Validation();
            Dictionary<string, string> httpHeaders = GetDefaultHeaders();
            string trueUrl = GetTrueUrl(url);
            string httpReslt = await httpHelper.SendPatchAsync(trueUrl, queryParams, data, httpHeaders, DefaultEncoding);
            if (string.IsNullOrWhiteSpace(httpReslt)) throw new MateralHttpException("返回值内容为空");
            return httpReslt;
        }
        /// <summary>
        /// 获得Delete返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task<string> GetResultByDeleteAsync(string url, Dictionary<string, string>? queryParams = null, object? data = null)
        {
            data?.Validation();
            Dictionary<string, string> httpHeaders = GetDefaultHeaders();
            string trueUrl = GetTrueUrl(url);
            string httpReslt = await httpHelper.SendDeleteAsync(trueUrl, queryParams, data, httpHeaders, DefaultEncoding);
            if (string.IsNullOrWhiteSpace(httpReslt)) throw new MateralHttpException("返回值内容为空");
            return httpReslt;
        }
        /// <summary>
        /// 获得Get返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task<string> GetResultByGetAsync(string url, Dictionary<string, string>? queryParams = null, object? data = null)
        {
            Dictionary<string, string> httpHeaders = GetDefaultHeaders();
            string trueUrl = GetTrueUrl(url);
            string httpReslt = await httpHelper.SendGetAsync(trueUrl, queryParams, data, httpHeaders, DefaultEncoding);
            if (string.IsNullOrWhiteSpace(httpReslt)) throw new MateralHttpException("返回值内容为空");
            return httpReslt;
        }
        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task<T> SendPostAsync<T>(string url, Dictionary<string, string>? queryParams = null, object? data = null)
        {
            string httpReslt = await GetResultByPostAsync(url, queryParams, data);
            T result = httpReslt.JsonToObject<T>();
            return result;
        }
        /// <summary>
        /// 发送Put请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task<T> SendPutAsync<T>(string url, Dictionary<string, string>? queryParams = null, object? data = null)
        {
            string httpReslt = await GetResultByPutAsync(url, queryParams, data);
            T result = httpReslt.JsonToObject<T>();
            return result;
        }
        /// <summary>
        /// 发送Patch请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task<T> SendPatchAsync<T>(string url, Dictionary<string, string>? queryParams = null, object? data = null)
        {
            string httpReslt = await GetResultByPathAsync(url, queryParams, data);
            T result = httpReslt.JsonToObject<T>();
            return result;
        }
        /// <summary>
        /// 发送Delete请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task<T> SendDeleteAsync<T>(string url, Dictionary<string, string>? queryParams = null, object? data = null)
        {
            string httpReslt = await GetResultByDeleteAsync(url, queryParams, data);
            T result = httpReslt.JsonToObject<T>();
            return result;
        }
        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task<T> SendGetAsync<T>(string url, Dictionary<string, string>? queryParams = null)
        {
            string httpReslt = await GetResultByGetAsync(url, queryParams);
            T result = httpReslt.JsonToObject<T>();
            return result;
        }
        /// <summary>
        /// 获得Post返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task GetResultModelByPostAsync(string url, Dictionary<string, string>? queryParams = null, object? data = null)
        {
            ResultModel result = await SendPostAsync<ResultModel>(url, queryParams, data);
            if (result.ResultType != ResultTypeEnum.Success) throw new MateralHttpException(result.Message ?? string.Empty);
        }
        /// <summary>
        /// 获得Post返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task<T?> GetResultModelByPostAsync<T>(string url, Dictionary<string, string>? queryParams = null, object? data = null)
        {
            ResultModel<T> result = await SendPostAsync<ResultModel<T>>(url, queryParams, data);
            if (result.ResultType != ResultTypeEnum.Success) throw new MateralHttpException(result.Message ?? string.Empty);
            return result.Data;
        }
        /// <summary>
        /// 获得Post返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task<(List<T>? data, PageModel pageInfo)> GetPageResultModelByPostAsync<T>(string url, Dictionary<string, string>? queryParams = null, object? data = null)
        {
            PageResultModel<T> result = await SendPostAsync<PageResultModel<T>>(url, queryParams, data);
            if (result.ResultType != ResultTypeEnum.Success) throw new MateralHttpException(result.Message ?? string.Empty);
            if (result.Data != null && result.PageModel != null)
            {
                return (result.Data.ToList(), result.PageModel);
            }
            return (null, new PageModel());
        }
        /// <summary>
        /// 获得Put返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task GetResultModelByPutAsync(string url, Dictionary<string, string>? queryParams = null, object? data = null)
        {
            ResultModel result = await SendPutAsync<ResultModel>(url, queryParams, data);
            if (result.ResultType != ResultTypeEnum.Success) throw new MateralHttpException(result.Message ?? string.Empty);
        }
        /// <summary>
        /// 获得Put返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task<T?> GetResultModelByPutAsync<T>(string url, Dictionary<string, string>? queryParams = null, object? data = null)
        {
            ResultModel<T> result = await SendPutAsync<ResultModel<T>>(url, queryParams, data);
            if (result.ResultType != ResultTypeEnum.Success) throw new MateralHttpException(result.Message ?? string.Empty);
            return result.Data;
        }
        /// <summary>
        /// 获得Put返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task<(List<T>? data, PageModel pageInfo)> GetPageResultModelByPutAsync<T>(string url, Dictionary<string, string>? queryParams = null, object? data = null)
        {
            PageResultModel<T> result = await SendPutAsync<PageResultModel<T>>(url, queryParams, data);
            if (result.ResultType != ResultTypeEnum.Success) throw new MateralHttpException(result.Message ?? string.Empty);
            if (result.Data != null && result.PageModel != null)
            {
                return (result.Data.ToList(), result.PageModel);
            }
            return (null, new PageModel());
        }
        /// <summary>
        /// 获得Patch返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task GetResultModelByPatchAsync(string url, Dictionary<string, string>? queryParams = null, object? data = null)
        {
            ResultModel result = await SendPatchAsync<ResultModel>(url, queryParams, data);
            if (result.ResultType != ResultTypeEnum.Success) throw new MateralHttpException(result.Message ?? string.Empty);
        }
        /// <summary>
        /// 获得Patch返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task<T?> GetResultModelByPatchAsync<T>(string url, Dictionary<string, string>? queryParams = null, object? data = null)
        {
            ResultModel<T> result = await SendPatchAsync<ResultModel<T>>(url, queryParams, data);
            if (result.ResultType != ResultTypeEnum.Success) throw new MateralHttpException(result.Message ?? string.Empty);
            return result.Data;
        }
        /// <summary>
        /// 获得Patch返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task<(List<T>? data, PageModel pageInfo)> GetPageResultModelByPatchAsync<T>(string url, Dictionary<string, string>? queryParams = null, object? data = null)
        {
            PageResultModel<T> result = await SendPatchAsync<PageResultModel<T>>(url, queryParams, data);
            if (result.ResultType != ResultTypeEnum.Success) throw new MateralHttpException(result.Message ?? string.Empty);
            if (result.Data != null && result.PageModel != null)
            {
                return (result.Data.ToList(), result.PageModel);
            }
            return (null, new PageModel());
        }
        /// <summary>
        /// 获得Delete返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task GetResultModelByDeleteAsync(string url, Dictionary<string, string>? queryParams = null, object? data = null)
        {
            ResultModel result = await SendDeleteAsync<ResultModel>(url, queryParams, data);
            if (result.ResultType != ResultTypeEnum.Success) throw new MateralHttpException(result.Message ?? string.Empty);
        }
        /// <summary>
        /// 获得Delete返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task<T?> GetResultModelByDeleteAsync<T>(string url, Dictionary<string, string>? queryParams = null, object? data = null)
        {
            ResultModel<T> result = await SendDeleteAsync<ResultModel<T>>(url, queryParams, data);
            if (result.ResultType != ResultTypeEnum.Success) throw new MateralHttpException(result.Message ?? string.Empty);
            return result.Data;
        }
        /// <summary>
        /// 获得Delete返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task<(List<T>? data, PageModel pageInfo)> GetPageResultModelByDeleteAsync<T>(string url, Dictionary<string, string>? queryParams = null, object? data = null)
        {
            PageResultModel<T> result = await SendDeleteAsync<PageResultModel<T>>(url, queryParams, data);
            if (result.ResultType != ResultTypeEnum.Success) throw new MateralHttpException(result.Message ?? string.Empty);
            if (result.Data != null && result.PageModel != null)
            {
                return (result.Data.ToList(), result.PageModel);
            }
            return (null, new PageModel());
        }
        /// <summary>
        /// 获得Get返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task GetResultModelByGetAsync(string url, Dictionary<string, string>? queryParams = null)
        {
            ResultModel result = await SendGetAsync<ResultModel>(url, queryParams);
            if (result.ResultType != ResultTypeEnum.Success) throw new MateralHttpException(result.Message ?? string.Empty);
        }
        /// <summary>
        /// 获得Get返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task<T?> GetResultModelByGetAsync<T>(string url, Dictionary<string, string>? queryParams = null)
        {
            ResultModel<T> result = await SendGetAsync<ResultModel<T>>(url, queryParams);
            if (result.ResultType != ResultTypeEnum.Success) throw new MateralHttpException(result.Message ?? string.Empty);
            return result.Data;
        }
        /// <summary>
        /// 获得Get返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        protected virtual async Task<(List<T>? data, PageModel pageInfo)> GetPageResultModelByGetAsync<T>(string url, Dictionary<string, string>? queryParams = null)
        {
            PageResultModel<T> result = await SendGetAsync<PageResultModel<T>>(url, queryParams);
            if (result.ResultType != ResultTypeEnum.Success) throw new MateralHttpException(result.Message ?? string.Empty);
            if (result.Data != null && result.PageModel != null)
            {
                return (result.Data.ToList(), result.PageModel);
            }
            return (null, new PageModel());
        }
    }
    /// <summary>
    /// Http客户端基类
    /// </summary>
    /// <typeparam name="TAddRequestModel"></typeparam>
    /// <typeparam name="TEditRequestModel"></typeparam>
    /// <typeparam name="TQueryRequestModel"></typeparam>
    /// <typeparam name="TDTO"></typeparam>
    /// <typeparam name="TListDTO"></typeparam>
    public abstract class MateralCoreHttpClientBase<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO> : MateralCoreHttpClientBase
        where TAddRequestModel : class, IAddRequestModel, new()
        where TEditRequestModel : class, IEditRequestModel, new()
        where TQueryRequestModel : IQueryRequestModel, new()
        where TDTO : class, IDTO, new()
        where TListDTO : class, IListDTO, new()
    {
        protected readonly string ControllerName;
        protected MateralCoreHttpClientBase(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            ControllerName = typeof(TDTO).Name[0..^3];
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public virtual async Task<Guid> AddAsync(TAddRequestModel requestModel) => await GetResultModelByPostAsync<Guid>($"{ControllerName}/Add", null, requestModel);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public virtual async Task EditAsync(TEditRequestModel requestModel) => await GetResultModelByPutAsync($"{ControllerName}/Edit", null, requestModel);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync([Required(ErrorMessage = "唯一标识为空")] Guid id) => await GetResultModelByDeleteAsync($"{ControllerName}/Delete", new Dictionary<string, string> { [nameof(id)] = id.ToString() });
        /// <summary>
        /// 获得信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TDTO?> GetInfoAsync([Required(ErrorMessage = "唯一标识为空")] Guid id) => await GetResultModelByGetAsync<TDTO>($"{ControllerName}/GetInfo", new Dictionary<string, string> { [nameof(id)] = id.ToString() });
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<bool> ExistedAsync([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            TDTO? result = await GetResultModelByGetAsync<TDTO>($"{ControllerName}/GetInfo", new Dictionary<string, string> { [nameof(id)] = id.ToString() });
            return result != null;
        }
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<TListDTO>? data, PageModel pageInfo)> GetListAsync(TQueryRequestModel requestModel) => await GetPageResultModelByPostAsync<TListDTO>($"{ControllerName}/GetList", null, requestModel);
        /// <summary>
        /// 获得数据
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<List<TListDTO>?> GetDataAsync(TQueryRequestModel requestModel)
        {
            List<TListDTO> result = new();
            try
            {
                if (requestModel == null) return result;
                (List<TListDTO>? data, PageModel pageInfo) = await GetListAsync(requestModel);
                if(data != null)
                {
                    result = data;
                }
            }
            catch (Exception ex)
            {
                Logger?.LogWarning(ex, "获取数据失败");
            }
            return result;
        }
        /// <summary>
        /// 获得数据
        /// </summary>
        /// <param name="requestModel"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<(bool isQuery, List<TListDTO>? data)> GetDataAsync(object requestModel, int pageSize = 10, int pageIndex = 1)
        {
            try
            {
                (bool isQuery, TQueryRequestModel queryModel) = requestModel.GetQueryModel<TQueryRequestModel>(pageSize, pageIndex);
                if (isQuery)
                {
                    return (isQuery, await GetDataAsync(queryModel));
                }
            }
            catch (Exception ex)
            {
                Logger?.LogWarning(ex, "获取数据失败");
            }
            return (false, new());
        }
        /// <summary>
        /// 获得数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<List<TListDTO>?> GetDataAsync(params Guid[] ids)
        {
            try
            {
                TQueryRequestModel queryModel = new();
                Type queryType = queryModel.GetType();
                List<Guid> allIDs = ids.Distinct().ToList();
                queryType.GetProperty("IDs")?.SetValue(queryModel, allIDs);
                queryModel.SetPageInfo(allIDs.Count, 1);
                List<TListDTO>? data = await GetDataAsync(queryModel);
                return data;
            }
            catch (Exception ex)
            {
                Logger?.LogWarning(ex, "获取数据失败");
            }
            return new List<TListDTO>();
        }
        /// <summary>
        /// 获得数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TListDTO?> FirstDataAsync(Guid id)
        {
            try
            {
                List<TListDTO>? data = await GetDataAsync(new Guid[] { id });
                if (data == null) return default;
                return data.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger?.LogWarning(ex, "获取数据失败");
            }
            return default;
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="bindAction"></param>
        /// <returns></returns>
        public async Task BindDataAsync(ICollection<Guid> ids, Action<TListDTO> bindAction)
        {
            try
            {
                List<TListDTO>? data = await GetDataAsync(ids.ToArray());
                if (data == null) return;
                foreach (var item in data)
                {
                    bindAction(item);
                }
            }
            catch (Exception ex)
            {
                Logger?.LogWarning(ex, "绑定数据失败");
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="bindAction"></param>
        /// <returns></returns>
        public async Task BindDataAsync(Guid id, Action<TListDTO> bindAction)
        {
            try
            {
                TListDTO? data = await FirstDataAsync(id);
                if (data == null) return;
                bindAction(data);
            }
            catch (Exception ex)
            {
                Logger?.LogWarning(ex, "绑定数据失败");
            }
        }
    }
}
