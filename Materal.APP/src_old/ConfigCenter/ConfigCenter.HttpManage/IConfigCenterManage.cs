using ConfigCenter.DataTransmitModel.ConfigCenter;
using Materal.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigCenter.PresentationModel.ConfigCenter;

namespace ConfigCenter.HttpManage
{
    public interface IConfigCenterManage
    {
        /// <summary>
        /// 获得环境列表
        /// </summary>
        /// <returns></returns>
        Task<ResultModel<List<EnvironmentListDTO>>> GetEnvironmentListAsync();
        /// <summary>
        /// 同步
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel> SyncAsync(SyncRequestModel requestModel);
    }
}
