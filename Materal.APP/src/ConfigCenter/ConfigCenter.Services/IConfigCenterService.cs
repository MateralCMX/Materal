using System.Collections.Generic;
using ConfigCenter.DataTransmitModel.ConfigCenter;
using ConfigCenter.Services.Models.ConfigCenter;

namespace ConfigCenter.Services
{
    public interface IConfigCenterService
    {
        /// <summary>
        /// 注册环境
        /// </summary>
        /// <param name="key"></param>
        /// <param name="model"></param>
        void RegisterEnvironment(string key, RegisterEnvironmentModel model);
        /// <summary>
        /// 反注册环境
        /// </summary>
        /// <param name="key"></param>
        void UnRegisterEnvironment(string key);
        /// <summary>
        /// 获得环境列表
        /// </summary>
        /// <returns></returns>
        List<EnvironmentListDTO> GetEnvironmentList();
    }
}
