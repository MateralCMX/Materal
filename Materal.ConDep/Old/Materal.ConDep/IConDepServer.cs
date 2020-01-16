using System.Threading.Tasks;

namespace Materal.ConDep
{
    /// <summary>
    /// 持续部署服务
    /// </summary>
    public interface IConDepServer
    {
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <returns></returns>
        Task RunServerAsync();
    }
}
