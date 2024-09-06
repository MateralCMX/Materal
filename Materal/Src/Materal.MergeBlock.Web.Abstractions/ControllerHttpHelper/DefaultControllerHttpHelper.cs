using Materal.MergeBlock.Web.Abstractions;
using Materal.MergeBlock.Web.Abstractions.Controllers;
using Materal.Utils.Http;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Materal.MergeBlock.Web.Abstractions.ControllerHttpHelper
{
    /// <summary>
    /// 默认控制器Http帮助类
    /// </summary>
    public class DefaultControllerHttpHelper : IControllerHttpHelper
    {
        /// <summary>
        /// Http帮助类
        /// </summary>
        protected readonly IHttpHelper HttpHelper;
        /// <summary>
        /// Web配置
        /// </summary>
        protected readonly IOptionsMonitor<WebOptions> Config;
        /// <summary>
        /// MergeBlock配置
        /// </summary>
        protected readonly IOptionsMonitor<MergeBlockOptions> MergeBlockConfig;
        /// <summary>
        /// 日志
        /// </summary>
        protected readonly ILogger? Logger;
        /// <summary>
        /// 构造方法
        /// </summary>
        public DefaultControllerHttpHelper(IHttpHelper httpHelper, IOptionsMonitor<WebOptions> config, IOptionsMonitor<MergeBlockOptions> mergeBlockConfig, ILoggerFactory? loggerFactory = null)
        {
            HttpHelper = httpHelper;
            Config = config;
            MergeBlockConfig = mergeBlockConfig;
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
            where TController : IMergeBlockController
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
                    "EditAsync" => HttpMethod.Put,
                    "DeleteAsync" => HttpMethod.Delete,
                    "GetInfoAsync" => HttpMethod.Get,
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
                    if (resultModel is TResult tResult)
                    {
                        result = tResult;
                    }
                }
            }
            if (result is null) throw new MergeBlockException(message);
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
        public virtual string GetServiceName() => MergeBlockConfig.CurrentValue.ApplicationName;
    }
}
