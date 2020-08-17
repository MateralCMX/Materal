using System.Threading.Tasks;

namespace Materal.ConDep.Services
{
    public interface IServerManage
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <returns></returns>
        public Task RegisterServerAsync();
    }
}
