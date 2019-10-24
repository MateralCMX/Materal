using System.Threading.Tasks;

namespace Materal.MicroFront
{
    /// <summary>
    /// 持续部署服务
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
