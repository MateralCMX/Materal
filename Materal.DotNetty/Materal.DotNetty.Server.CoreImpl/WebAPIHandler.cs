using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;
using Materal.ConvertHelper;
using Materal.DotNetty.ControllerBus;
using Materal.DotNetty.ControllerBus.Filters;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Materal.DotNetty.Common;
using Materal.DotNetty.Server.Core;
using Materal.DotNetty.Server.Core.Models;

namespace Materal.DotNetty.Server.CoreImpl
{
    public class WebAPIHandler : HttpHandlerContext
    {
        private readonly IControllerBus _controllerBus;
        public WebAPIHandler(IControllerBus controllerBus)
        {
            _controllerBus = controllerBus;
        }

        public override async Task HandlerAsync(IChannelHandlerContext ctx, IByteBufferHolder byteBufferHolder)
        {
            try
            {
                await HandlerAsync<IFullHttpRequest>(ctx, byteBufferHolder, HandlerRequestAsync);
            }
            catch (Exception exception)
            {
                OnException?.Invoke(exception);
                await HandlerExceptionAsync(ctx, byteBufferHolder, exception);
            }
        }
        #region 私有方法
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="byteBufferHolder"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        protected virtual async Task HandlerExceptionAsync(IChannelHandlerContext ctx, IByteBufferHolder byteBufferHolder, Exception exception)
        {
            Dictionary<AsciiString, object> headers = HttpResponseHelper.GetDefaultHeaders("text/plain;charset=utf-8");
            IFullHttpResponse response = HttpResponseHelper.GetHttpResponse(HttpResponseStatus.InternalServerError, exception.Message, headers);
            IFilter[] globalFilters = _controllerBus.GetGlobalFilters();
            List<IExceptionFilter> exceptionFilters = globalFilters.OfType<IExceptionFilter>().ToList();
            List<IExceptionAsyncFilter> exceptionAsyncFilters = globalFilters.OfType<IExceptionAsyncFilter>().ToList();
            if (exceptionFilters.Count > 0 || exceptionAsyncFilters.Count > 0)
            {
                foreach (IExceptionFilter exceptionFilter in exceptionFilters)
                {
                    response = exceptionFilter.HandlerException(byteBufferHolder, exception);
                }
                foreach (IExceptionAsyncFilter exceptionFilter in exceptionAsyncFilters)
                {
                    response = await exceptionFilter.HandlerExceptionAsync(byteBufferHolder, exception);
                }
            }
            await SendHttpResponseAsync(ctx, byteBufferHolder, response);
            StopHandler();
        }
        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        protected virtual async Task HandlerRequestAsync(IChannelHandlerContext ctx, IFullHttpRequest request)
        {
            IFullHttpResponse response = null;
            IFilter[] globalFilters = _controllerBus.GetGlobalFilters();
            if (!request.Uri.StartsWith("/api/"))
            {
                if (CanNext) return;
                response = HttpResponseHelper.GetHttpResponse(HttpResponseStatus.NotFound);
            }
            else
            {
                string[] urls = request.Uri.Split('/');
                if (urls.Length < 4) return;
                string controllerKey = urls[2];
                try
                {
                    BaseController baseController = _controllerBus.GetController(controllerKey);
                    baseController.Request = request;
                    string actionKey = urls[3].Split('?')[0];
                    ActionInfo action = baseController.GetAction(actionKey);
                    if (!HandlerOptions(ctx, request, action, ref response))
                    {
                        response = await baseController.HandlerControllerBeforeFilterAsync(globalFilters);
                        if (response.Status.Code == HttpResponseStatus.OK.Code)
                        {
                            response = await GetResponseAsync(request, baseController, action, globalFilters);
                            await baseController.HandlerControllerAfterFilterAsync(response, globalFilters);
                        }
                    }
                }
                catch (DotNettyServerException exception)
                {
                    ResultModel resultModel = ResultModel.Fail(exception.Message);
                    response = HttpResponseHelper.GetHttpResponse(HttpResponseStatus.NotFound, resultModel.ToJson());
                }
            }
            await SendHttpResponseAsync(ctx, request, response);
            StopHandler();
        }
        /// <summary>
        /// 处理Options
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="request"></param>
        /// <param name="action"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        protected virtual bool HandlerOptions(IChannelHandlerContext ctx, IFullHttpRequest request, ActionInfo action, ref IFullHttpResponse response)
        {
            if (request.Method.Name != HttpMethod.Options.Name) return false;
            response = GetOptionsResponse(action.GetMethodName());
            return true;
        }
        /// <summary>
        /// 获得Response
        /// </summary>
        /// <param name="request"></param>
        /// <param name="baseController"></param>
        /// <param name="action"></param>
        /// <param name="globalFilters"></param>
        /// <returns></returns>
        protected virtual async Task<IFullHttpResponse> GetResponseAsync(IFullHttpRequest request, BaseController baseController, ActionInfo action, IFilter[] globalFilters)
        {
            IFullHttpResponse response = action.HandlerMethod(request);
            if (response.Status.Code != HttpResponseStatus.OK.Code) return response;
            response = await action.HandlerAuthorityFilterAsync(request, globalFilters);
            if (response.Status.Code != HttpResponseStatus.OK.Code) return response;
            response = await action.HandlerActionBeforeFilterAsync(request, globalFilters);
            if (response.Status.Code != HttpResponseStatus.OK.Code) return response;
            response = await HandlerParamsAsync(request, baseController, action);
            await action.HandlerActionAfterFilterAsync(request, response, globalFilters);
            return response;
        }
        /// <summary>
        /// 处理参数
        /// </summary>
        /// <param name="request"></param>
        /// <param name="baseController"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        protected virtual async Task<IFullHttpResponse> HandlerParamsAsync(IFullHttpRequest request, BaseController baseController, ActionInfo action)
        {
            IFullHttpResponse response = null;
            ParameterInfo[] parameters = action.Action.GetParameters();
            if (parameters.Length > 0)
            {
                ICharSequence typeCharSequence = request.Headers.Get(HttpHeaderNames.ContentType, null);
                string type = string.Empty;
                if (typeCharSequence != null)
                {
                    type = typeCharSequence.ToString();
                }
                object[] @params;
                if (type.Contains("multipart/form-data"))
                {
                    @params = HandlerFormDataParams(request, parameters, ref response);
                    if (@params == null) return response;
                }
                else
                {
                    @params = HandlerBodyAndUrlParams(request, parameters, ref response);
                    if (@params == null) return response;
                }
                response = await GetResponseAsync(baseController, action, @params);
            }
            else
            {
                response = await GetResponseAsync(baseController, action, null);
            }
            return response;
        }
        /// <summary>
        /// 处理FormData
        /// </summary>
        /// <param name="request"></param>
        /// <param name="parameters"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        protected virtual object[] HandlerFormDataParams(IFullHttpRequest request, IReadOnlyList<ParameterInfo> parameters, ref IFullHttpResponse response)
        {
            if (parameters.Count == 1 && parameters[0].ParameterType == typeof(IUploadFileModel))
            {
                var @params = new object[parameters.Count];
                @params[0] = new DefaultUploadFileModel(request);
                return @params;
            }
            ResultModel resultModel = ResultModel.Fail("参数错误,multipart/form-data只能接收一个byte[]");
            response = HttpResponseHelper.GetHttpResponse(HttpResponseStatus.BadRequest, resultModel.ToJson());
            return null;
        }
        /// <summary>
        /// 处理Body和Url参数
        /// </summary>
        /// <param name="request"></param>
        /// <param name="parameters"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        protected virtual object[] HandlerBodyAndUrlParams(IFullHttpRequest request, IReadOnlyList<ParameterInfo> parameters, ref IFullHttpResponse response)
        {
            string bodyParams = GetBodyParams(request);
            Dictionary<string, string> urlParams = GetUrlParams(request);
            if (parameters.Count <= 0) return null;
            var @params = new object[parameters.Count];
            for (var i = 0; i < parameters.Count; i++)
            {
                if (!string.IsNullOrEmpty(bodyParams) && parameters[i].ParameterType.IsClass && parameters[i].ParameterType != typeof(string))
                {
                    @params[i] = bodyParams.JsonToObject(parameters[i].ParameterType);
                    continue;
                }
                if (urlParams.ContainsKey(parameters[i].Name ?? throw new DotNettyException("参数为空")))
                {
                    @params[i] = urlParams[parameters[i].Name].ConvertTo(parameters[i].ParameterType);
                    continue;
                }
                if (@params[i] == null)
                {
                    ResultModel resultModel = ResultModel.Fail($"参数{parameters[i].Name}错误");
                    response = HttpResponseHelper.GetHttpResponse(HttpResponseStatus.BadRequest, resultModel.ToJson());
                    return null;
                }
            }
            return @params;
        }
        /// <summary>
        /// 获得Response
        /// </summary>
        /// <param name="baseController"></param>
        /// <param name="action"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        protected virtual async Task<IFullHttpResponse> GetResponseAsync(BaseController baseController, ActionInfo action, object[] @params)
        {
            object actionResult = action.Action.Invoke(baseController, @params);
            if(actionResult is Task task)
            {
                dynamic taskObj = task;
                actionResult = await taskObj;
            }
            IFullHttpResponse result;
            if(actionResult != null)
            {
                if(actionResult is Stream stream)
                {
                    result = await GetStreamResponseAsync(stream);
                }
                else if (actionResult.GetType().IsClass && !(actionResult is string))
                {
                    result = GetJsonResponse(actionResult);
                }
                else
                {
                    result = GetTextResponse(actionResult);
                }
            }
            else
            {
                result = HttpResponseHelper.GetHttpResponse(HttpResponseStatus.OK);
            }
            return result;
        }
        /// <summary>
        /// 获取流返回
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        protected virtual async Task<IFullHttpResponse> GetStreamResponseAsync(Stream stream)
        {
            var bytes = new byte[stream.Length];
            await stream.ReadAsync(bytes, 0, bytes.Length);
            stream.Close();
            stream.Dispose();
            Dictionary<AsciiString, object> headers = HttpResponseHelper.GetDefaultHeaders("application/octet-stream");
            IFullHttpResponse result = HttpResponseHelper.GetHttpResponse(HttpResponseStatus.OK, bytes, headers);
            return result;
        }
        /// <summary>
        /// 获取Json返回
        /// </summary>
        /// <param name="actionResult"></param>
        /// <returns></returns>
        protected virtual IFullHttpResponse GetJsonResponse(object actionResult)
        {
            Dictionary<AsciiString, object> headers = HttpResponseHelper.GetDefaultHeaders("application/json;charset=utf-8");
            IFullHttpResponse result = HttpResponseHelper.GetHttpResponse(HttpResponseStatus.OK, actionResult.ToJson(), headers);
            return result;
        }
        /// <summary>
        /// 获取文本返回
        /// </summary>
        /// <param name="actionResult"></param>
        /// <returns></returns>
        protected virtual IFullHttpResponse GetTextResponse(object actionResult)
        {
            Dictionary<AsciiString, object> headers = HttpResponseHelper.GetDefaultHeaders("text/plain;charset=utf-8");
            IFullHttpResponse result = HttpResponseHelper.GetHttpResponse(HttpResponseStatus.OK, actionResult.ToString(), headers);
            return result;
        }

        /// <summary>
        /// 获得Body参数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected virtual string GetBodyParams(IFullHttpRequest request)
        {
            string result = request.Content.ReadString(request.Content.Capacity, Encoding.UTF8);
            return result;
        }
        /// <summary>
        /// 获取Url参数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected virtual Dictionary<string,string> GetUrlParams(IFullHttpRequest request)
        {
            var result = new Dictionary<string, string>();
            string[] tempString = request.Uri.Split('?');
            if (tempString.Length <= 1) return result;
            string[] paramsString = tempString[1].Split('&');
            foreach (string param in paramsString)
            {
                if (string.IsNullOrEmpty(param)) continue;
                string[] values = param.Split('=');
                if (values.Length != 2 || result.ContainsKey(values[0])) continue;
                result.Add(values[0], values[1]);
            }
            return result;
        }
        #endregion
    }
}
