using DotNetty.Codecs.Http;
using System;

namespace Materal.DotNetty.ControllerBus.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public abstract class HttpMethodAttribute : Attribute
    {
        public string Name { get; }
        protected HttpMethodAttribute(string name)
        {
            Name = name;
        }
        /// <summary>
        /// 是否匹配
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        protected bool IsMatch(string method)
        {
            return Name.Equals(method, StringComparison.CurrentCultureIgnoreCase);
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public void Handler(IFullHttpRequest request, IFullHttpResponse response)
        {
            if (!IsMatch(request.Method.Name))
            {
                response.SetStatus(HttpResponseStatus.BadRequest);
            }
        }
    }
}
