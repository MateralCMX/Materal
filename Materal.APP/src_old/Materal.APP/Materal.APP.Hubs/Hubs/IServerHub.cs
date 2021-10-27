using Materal.APP.PresentationModel.Server;
using System.Threading.Tasks;

namespace Materal.APP.Hubs.Hubs
{
    public interface IServerHub
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task Register(RegisterRequestModel requestModel);
    }
}
