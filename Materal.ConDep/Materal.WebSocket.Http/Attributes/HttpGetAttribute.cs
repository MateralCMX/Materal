using System;

namespace Materal.WebSocket.Http.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpGetAttribute : Attribute, IHttpMethodAttribute
    {
        public T GetParams<T>()
        {
            throw new NotImplementedException();
        }
    }
}
