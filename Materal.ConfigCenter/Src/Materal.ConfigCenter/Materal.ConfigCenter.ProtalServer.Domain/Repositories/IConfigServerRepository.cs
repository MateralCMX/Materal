using System.Threading.Tasks;

namespace Materal.ConfigCenter.ProtalServer.Domain.Repositories
{
    public interface IConfigServerRepository
    {
        /// <summary>
        /// 健康检查
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        Task HealthAsync(string address);
    }
}
