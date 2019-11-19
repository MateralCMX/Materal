using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using Materal.CacheHelper;
using Materal.ConvertHelper;
using Materal.MicroFront.Common;
using Materal.Model;
using Materal.WebSocket.Http;
using Materal.WebSocket.Http.Attributes;

namespace Materal.Services
{
    public class HttpHandler
    {
        private readonly ICacheManager _cacheManager;
        private const string _cacheKey = "FileCacheKey";
        public HttpHandler()
        {
            _cacheManager = ApplicationData.GetService<ICacheManager>();
        }
        /// <summary>
        /// 获得返回
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DefaultFullHttpResponse GetResponse(IFullHttpRequest request)
        {
            if (request.Uri.Contains("."))
            {
                return GetFileResponse(request);
            }
            if (request.Uri.IndexOf("/api", StringComparison.Ordinal) == 0)
            {
                return GetAPIResponse(request);
            }
            request.SetUri("/Portal/Index.html");
            return GetFileResponse(request);
        }
        /// <summary>
        /// 清空缓存
        /// </summary>
        public void ClearCache()
        {
            List<string> cacheKeys = _cacheManager.GetCacheKeys();
            foreach (string cacheKey in cacheKeys)
            {
                if (cacheKey.StartsWith(_cacheKey))
                {
                    _cacheManager.Remove(cacheKey);
                }
            }
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
                string[] values = item.Split('=');
                if (values.Length != 2 || @params.ContainsKey(values[0])) continue;
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
            string fileName = request.Uri.Split('?')[0];
            if (fileName == "/") fileName = "/Index";
            DefaultFullHttpResponse result;
            if (fileName.EndsWith(".html") || fileName.EndsWith(".css") || fileName.EndsWith(".js") || fileName.EndsWith(".js.map"))
            {
                result = GetTxtResponse(fileName);
            }
            else
            {
                result = GetFileResponse(fileName);
            }
            return result;
        }
        /// <summary>
        /// 获得文件返回
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private DefaultFullHttpResponse GetFileResponse(string fileName)
        {
            var body = _cacheManager.Get<byte[]>($"{_cacheKey}{fileName}");
            if (body == null)
            {
                string filePath = GetFilePath(fileName);
                if (filePath == null) return GetResponse(HttpResponseStatus.NotFound);
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    body = new byte[fileStream.Length];
                    var index = 0;
                    while (fileStream.CanRead && index < fileStream.Length)
                    {
                        int length = index + 2048 > fileStream.Length
                            ? Convert.ToInt32(fileStream.Length - index)
                            : 2048;
                        fileStream.Read(body, index, length);
                        index += length;
                    }
                    _cacheManager.SetBySliding($"{_cacheKey}{fileName}", body, 1);
                }
            }
            if (fileName.Contains(".png")) return GetResponse(HttpResponseStatus.OK, body, "image/png");
            if (fileName.Contains(".gif")) return GetResponse(HttpResponseStatus.OK, body, "image/gif");
            if (fileName.Contains(".jpeg")) return GetResponse(HttpResponseStatus.OK, body, "image/jpeg");
            if (fileName.Contains(".jpg")) return GetResponse(HttpResponseStatus.OK, body, "image/jpeg");
            if (fileName.Contains(".woff")) return GetResponse(HttpResponseStatus.OK, body, "application/font-woff");
            if (fileName.Contains(".ttf")) return GetResponse(HttpResponseStatus.OK, body, "application/font-ttf");
            return GetResponse(HttpResponseStatus.OK, body, "image/jpeg");
        }
        /// <summary>
        /// 获得文本返回
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private DefaultFullHttpResponse GetTxtResponse(string fileName)
        {
            var fileText = _cacheManager.Get<string>($"{_cacheKey}{fileName}");
            if (string.IsNullOrEmpty(fileText))
            {
                string filePath = GetFilePath(fileName);
                if (filePath == null) return GetResponse(HttpResponseStatus.NotFound);
                using (var streamReader = new StreamReader(filePath))
                {
                    fileText = streamReader.ReadToEnd();
                    _cacheManager.SetBySliding($"{_cacheKey}{fileName}", fileText, 1);
                }
            }
            if (fileName.EndsWith(".html")) return GetResponse(HttpResponseStatus.OK, fileText);
            if (fileName.Contains(".js")) return GetResponse(HttpResponseStatus.OK, fileText, "application/javascript");
            if (fileName.Contains(".css")) return GetResponse(HttpResponseStatus.OK, fileText, "text/css");
            return GetResponse(HttpResponseStatus.NotFound);
        }
        /// <summary>
        /// 获得文件路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetFilePath(string fileName)
        {
            //const string basePath = @"E:/Project/Materal/Project/Materal.MicroFront/Materal.MicroFront/";
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = GetFilePath(basePath, fileName, "");
            if (File.Exists(filePath)) return filePath;
            filePath = GetFilePath(basePath, fileName, "Portal");
            if (File.Exists(filePath)) return filePath;
            var htmlPagesDirectoryInfo = new DirectoryInfo($"{basePath}HtmlPages");
            DirectoryInfo[] directoryInfos = htmlPagesDirectoryInfo.GetDirectories();
            foreach (DirectoryInfo directoryInfo in directoryInfos)
            {
                switch (directoryInfo.Name)
                {
                    case "Portal":
                    case "Manager":
                        break;
                    default:
                        filePath = GetFilePath(basePath, fileName, directoryInfo.Name);
                        if (File.Exists(filePath)) return filePath;
                        break;
                }
            }
            return null;
        }
        /// <summary>
        /// 获得文件路径
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="fileName"></param>
        /// <param name="projectName"></param>
        /// <returns></returns>
        private string GetFilePath(string basePath, string fileName, string projectName)
        {
            if (string.IsNullOrEmpty(projectName))
            {
                return $"{basePath}HtmlPages{fileName}";
            }
            return $"{basePath}HtmlPages/{projectName}{fileName}";
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
            byte[] body = Encoding.UTF8.GetBytes(bodyString);
            DefaultFullHttpResponse response = GetResponse(status, body, contentType);
            return response;
        }
        /// <summary>
        /// 获得返回
        /// </summary>
        /// <param name="status"></param>
        /// <param name="body"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        private DefaultFullHttpResponse GetResponse(HttpResponseStatus status, byte[] body, string contentType = "text/html; charset=UTF-8")
        {
            IByteBuffer bodyBuffer = Unpooled.WrappedBuffer(body);
            var response = new DefaultFullHttpResponse(HttpVersion.Http11, status, bodyBuffer);
            response.Headers.Set(HttpHeaderNames.AccessControlAllowOrigin, "*");
            response.Headers.Set(HttpHeaderNames.ContentType, $"{contentType}");
            response.Headers.Set(HttpHeaderNames.ContentLength, bodyBuffer.ReadableBytes);
            response.Headers.Set(HttpHeaderNames.Date, DateTime.Now);
            response.Headers.Set(HttpHeaderNames.Server, "Materal.MicroFront");
            return response;
        }
        #endregion
    }
}
