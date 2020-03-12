namespace Materal.WebSocket.Http.Attributes
{
    /// <summary>
    /// HttpMethod特性
    /// </summary>
    public interface IHttpMethodAttribute
    {
        /// <summary>
        /// 获得参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetParams<T>();
    }
}
