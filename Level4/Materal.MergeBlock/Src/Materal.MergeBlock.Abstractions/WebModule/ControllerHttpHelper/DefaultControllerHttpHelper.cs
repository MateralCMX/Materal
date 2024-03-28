using Materal.MergeBlock.Abstractions.WebModule.Controllers;
using Materal.Utils.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Materal.MergeBlock.Abstractions.WebModule.ControllerHttpHelper
{
    /// <summary>
    /// 默认控制器Http帮助类
    /// </summary>
    public class DefaultControllerHttpHelper : IControllerHttpHelper
    {
        /// <summary>
        /// MergeBlock配置
        /// </summary>
        protected readonly IHttpHelper HttpHelper;
        /// <summary>
        /// MergeBlock配置
        /// </summary>
        protected readonly IOptionsMonitor<MergeBlockConfig> Config;
        /// <summary>
        /// 日志
        /// </summary>
        protected readonly ILogger? Logger;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpHelper"></param>
        /// <param name="config"></param>
        /// <param name="loggerFactory"></param>
        public DefaultControllerHttpHelper(IHttpHelper httpHelper, IOptionsMonitor<MergeBlockConfig> config, ILoggerFactory? loggerFactory = null)
        {
            HttpHelper = httpHelper;
            Config = config;
            Logger = loggerFactory?.CreateLogger(GetType());
        }
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <typeparam name="TController"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="projectName"></param>
        /// <param name="moduleName"></param>
        /// <param name="methodName"></param>
        /// <param name="queryArgs"></param>
        /// <param name="datas"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockModuleException"></exception>
        public virtual async Task<TResult> SendAsync<TController, TResult>(string projectName, string moduleName, string methodName, Dictionary<string, string> queryArgs, params object[] datas)
            where TController : IMergeBlockControllerBase
        {
            var controllerType = typeof(TController);
            var methodInfo = controllerType.GetMethods().FirstOrDefault(m => m.Name == methodName);
            HttpMethod httpMethod;
            bool hasToken = true;
            if (methodInfo is not null)
            {
                var attributes = methodInfo.GetCustomAttributes();
                httpMethod = HttpMethod.Post;
                foreach (var item in attributes)
                {
                    string attributeName = item.GetType().Name;
                    switch (attributeName)
                    {
                        case nameof(HttpGetAttribute):
                            httpMethod = HttpMethod.Get;
                            break;
                        case nameof(HttpPostAttribute):
                            httpMethod = HttpMethod.Post;
                            break;
                        case nameof(HttpPutAttribute):
                            httpMethod = HttpMethod.Put;
                            break;
                        case nameof(HttpDeleteAttribute):
                            httpMethod = HttpMethod.Delete;
                            break;
                        case nameof(HttpPatchAttribute):
                            httpMethod = HttpMethod.Patch;
                            break;
                        case nameof(AllowAnonymousAttribute):
                            hasToken = false;
                            break;
                    }
                }
            }
            else
            {
                httpMethod = methodName switch
                {
                    "AddAsync" => HttpMethod.Put,
                    "DeleteAsync" => HttpMethod.Delete,
                    "GetInfoAsync" => HttpMethod.Get,
                    "GetListAsync" => HttpMethod.Post,
                    _ => HttpMethod.Post,
                };
            }
            string url = GetUrl(projectName, moduleName, controllerType.Name, methodName);
            var headers = GetHeaders(hasToken);
            string message = $"向{typeof(TController).FullName}发送Http[{url}]请求失败";
            TResult? result = default;
            try
            {
                if (datas.Length == 0)
                {
                    result = await HttpHelper.SendAsync<TResult>(url, httpMethod, queryArgs, null, headers);
                }
                else if (datas.Length == 1)
                {
                    result = await HttpHelper.SendAsync<TResult>(url, httpMethod, queryArgs, datas[0], headers);
                }
                else
                {
                    result = await HttpHelper.SendAsync<TResult>(url, httpMethod, queryArgs, datas.ToExpandoObject(), headers);
                }
            }
            catch (Exception ex)
            {
                Logger?.LogWarning(ex, message);
                Type tType = typeof(TResult);
                if (tType.IsAssignableTo<ResultModel>())
                {
                    ResultModel resultModel = tType.Instantiation<ResultModel>();
                    resultModel.ResultType = ResultTypeEnum.Fail;
                    resultModel.Message = message;
                    if(resultModel is TResult tResult)
                    {
                        result = tResult;
                    }
                }
            }
            if(result is null) throw new MergeBlockException(message);
            return result;
        }
        /// <summary>
        /// 获取请求头
        /// </summary>
        /// <param name="hasToken"></param>
        /// <returns></returns>
        public virtual Dictionary<string, string> GetHeaders(bool hasToken)
        {
            Dictionary<string, string> result = [];
            return result;
        }
        /// <summary>
        /// 获取地址
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="moduleName"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public virtual string GetUrl(string projectName, string moduleName, string controllerName, string actionName)
        {
            string action = actionName;
            if (action.EndsWith("Async"))
            {
                action = action[0..^5];
            }
            string controller = controllerName[1..^10];
            return $"{Config.CurrentValue.BaseUrl}/api/{controller}/{action}";
        }
        /// <summary>
        /// 获取服务名称
        /// </summary>
        /// <returns></returns>
        public virtual string GetServiceName() => Config.CurrentValue.ApplicationName;
    }
}
