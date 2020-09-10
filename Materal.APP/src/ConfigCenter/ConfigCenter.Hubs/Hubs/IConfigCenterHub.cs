using ConfigCenter.PresentationModel.ConfigCenter;
using System.Threading.Tasks;

namespace ConfigCenter.Hubs.Hubs
{
    public interface IConfigCenterHub
    {
        /// <summary>
        /// 获得注册模型
        /// </summary>
        /// <returns></returns>
        RegisterEnvironmentRequestModel GetRegisterModel();
        /// <summary>
        /// 注册环境
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task RegisterEnvironment(RegisterEnvironmentRequestModel requestModel);
    }
}
