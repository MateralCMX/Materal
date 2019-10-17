using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using DotNetty.Common.Utilities;
using Materal.ConDep.Authority;
using Materal.ConDep.Common;
using Materal.ConvertHelper;
using Materal.Model;
using Materal.WebSocket.Http;
using Materal.WebSocket.Http.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Materal.ConDep
{
    public class HttpHandler
    {
        /// <summary>
        /// 获得返回
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DefaultFullHttpResponse GetResponse(IFullHttpRequest request)
        {
            return request.Uri.LastIndexOf("/api", StringComparison.Ordinal) == 0 ? GetAPIResponse(request) : GetFileResponse(request);
        }
        #region 私有方法
        /// <summary>
        /// 获得API返回
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private DefaultFullHttpResponse GetAPIResponse(IFullHttpRequest request)
        {
            try
            {
                string[] names = request.Uri.Substring(5).Split('/');
                var controllerBus = ApplicationData.GetService<IControllerBus>();
                object controller = controllerBus.GetController($"{names[0]}Controller");
                if (controller == null) return GetResponse(HttpResponseStatus.NotFound, "未找到控制器");
                string actionName = names[1].Split('?')[0];
                return InvokeAction(request, controller, actionName);
            }
            catch (Exception ex)
            {
                return GetFailResult(ex.Message);
            }
        }
        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="request"></param>
        /// <param name="controller"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        private DefaultFullHttpResponse InvokeAction(IFullHttpRequest request, object controller, string actionName)
        {
            Type controllerType = controller.GetType();
            MethodInfo action = controllerType.GetMethod(actionName);
            if (action == null) return GetResponse(HttpResponseStatus.NotFound, "未找到方法");
            if (!CanMethodSuccess(request, action)) return GetResponse(HttpResponseStatus.BadRequest, "Method错误");
            if (!CanLoginSuccess(action, request.Headers)) return GetResponse(HttpResponseStatus.Unauthorized, "未登录");
            string bodyParams = GetBodyParams(request);
            Dictionary<string, string> urlParams = GetUrlParams(request);
            ParameterInfo[] parameters = action.GetParameters();
            var objects = new object[parameters.Length];
            object actionResult;
            if (parameters.Length > 0)
            {
                for (var i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i].ParameterType.IsClass && parameters[i].ParameterType != typeof(string))
                    {
                        objects[i] = bodyParams.JsonToObject(parameters[i].ParameterType);
                    }
                    else
                    {
                        objects[i] = urlParams[parameters[i].Name].ConvertTo(parameters[i].ParameterType);
                    }
                }
                actionResult = action.Invoke(controller, objects);
            }
            else
            {
                actionResult = action.Invoke(controller, null);
            }
            switch (actionResult)
            {
                case ResultModel resultModel:
                    return GetResponse(HttpResponseStatus.OK, resultModel.ToJson());
            }
            return GetFailResult("返回错误");
        }
        /// <summary>
        /// Method验证
        /// </summary>
        /// <param name="request"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private bool CanMethodSuccess(IFullHttpRequest request, MethodInfo action)
        {
            Attribute[] attribute = action.GetCustomAttributes().ToArray();
            if (request.Method.Name == "GET" && attribute.Any(m => m is HttpGetAttribute) ||
                request.Method.Name == "POST" && attribute.Any(m => m is HttpPostAttribute))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 是否登录
        /// </summary>
        /// <returns></returns>
        private bool CanLoginSuccess(MemberInfo action, HttpHeaders headers)
        {
            Attribute[] attribute = action.GetCustomAttributes().ToArray();
            if (attribute.Any(m => m is AllowAnonymousAttribute)) return true;
            IList<ICharSequence> authorizations = headers.GetAll(HttpHeaderNames.Authorization);
            var authorityService = ApplicationData.GetService<IAuthorityService>();
            foreach (ICharSequence authorization in authorizations)
            {
                string authorizationString = authorization.ToString();
                if (!authorizationString.Substring(0, 7).Equals("Bearer ")) continue;
                string token = authorizationString.Substring(7);
                if (authorityService.IsLogin(token))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 获得参数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string GetBodyParams(IFullHttpRequest request)
        {
            string @params = request.Content.ReadString(request.Content.Capacity, Encoding.UTF8);
            return @params;
        }
        /// <summary>
        /// 获得字典参数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetUrlParams(IFullHttpRequest request)
        {
            var @params = new Dictionary<string, string>();
            string[] tempString = request.Uri.Split('?');
            if (tempString.Length <= 1) return @params;
            string[] paramsString = tempString[1].Split('&');
            foreach (string item in paramsString)
            {
                if (string.IsNullOrEmpty(item)) continue;
                string[] values = item.Split("=");
                if (values.Length !=2 || @params.ContainsKey(values[0])) continue;
                @params.Add(values[0], values[1]);
            }
            return @params;
        }
        /// <summary>
        /// 获得文件返回
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private DefaultFullHttpResponse GetFileResponse(IFullHttpRequest request)
        {
#if DEBUG
            const string basePath = @"E:/Project/Materal/Project/Materal.ConDep/Materal.ConDep/";
#else
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
#endif
            string fileName = request.Uri.Split('?')[0];
            bool isHtml = !fileName.Contains(".");
            string filePath = isHtml ? $"{basePath}HtmlPages{fileName}.html" : $"{basePath}HtmlPages{fileName}";
            if (!File.Exists(filePath))
            {
                if (isHtml) filePath = basePath + "/HtmlPages/404.html";
                else
                {
                    return GetResponse(HttpResponseStatus.NotFound);
                }
            }
            using (var streamReader = new StreamReader(filePath))
            {
                string fileText = streamReader.ReadToEnd();
                if (isHtml) return GetResponse(HttpResponseStatus.OK, fileText);
                if (fileName.Contains(".js")) return GetResponse(HttpResponseStatus.OK, fileText, "application/javascript");
                if (fileName.Contains(".css")) return GetResponse(HttpResponseStatus.OK, fileText, "text/css");
            }
            return GetResponse(HttpResponseStatus.NotFound);
        }
        /// <summary>
        /// 获得失败返回
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private DefaultFullHttpResponse GetFailResult(string message)
        {
            return GetResponse(HttpResponseStatus.OK, ResultModel.Fail(message).ToJson());
        }
        /// <summary>
        /// 获得返回
        /// </summary>
        /// <param name="status"></param>
        /// <param name="bodyString"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        private DefaultFullHttpResponse GetResponse(HttpResponseStatus status, string bodyString = "", string contentType = "text/html; charset=UTF-8")
        {
            IByteBuffer body = Unpooled.WrappedBuffer(Encoding.UTF8.GetBytes(bodyString));
            var response = new DefaultFullHttpResponse(HttpVersion.Http11, status, body);
            response.Headers.Set(HttpHeaderNames.AccessControlAllowOrigin, "*");
            response.Headers.Set(HttpHeaderNames.ContentType, $"{contentType}");
            response.Headers.Set(HttpHeaderNames.ContentLength, body.ReadableBytes);
            response.Headers.Set(HttpHeaderNames.Date, DateTime.Now);
            response.Headers.Set(HttpHeaderNames.Server, "Materal.ConDep");
            return response;
        }
        #endregion
    }
}
