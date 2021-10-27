using System.Threading.Tasks;

namespace Materal.APP.Hubs.Clients
{
    /// <summary>
    /// 服务Hub客户端
    /// </summary>
    public interface IServerClient
    {
        /// <summary>
        /// 注册结果
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task RegisterResult(bool isSuccess, string message);
    }
}
