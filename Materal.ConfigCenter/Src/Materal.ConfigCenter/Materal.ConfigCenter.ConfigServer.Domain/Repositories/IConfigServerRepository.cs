using System.Threading.Tasks;

namespace Materal.ConfigCenter.ConfigServer.Domain.Repositories
{
    public interface IConfigServerRepository
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        Task RegisterAsync();
        /// <summary>
        /// 健康检查
        /// </summary>
        /// <returns></returns>
        Task HealthAsync();
    }
}
