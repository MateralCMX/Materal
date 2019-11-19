using System.Threading.Tasks;

namespace Materal.MicroFront
{
    /// <summary>
    /// 微前端发布服务
    /// </summary>
    public interface IMicroFrontServer
    {
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <returns></returns>
        Task RunServerAsync();
    }
}
