using ConfigCenter.DataTransmitModel.ConfigCenter;
using Materal.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigCenter.HttpManage
{
    public interface IConfigCenterManage
    {
        /// <summary>
        /// 获得环境列表
        /// </summary>
        /// <returns></returns>
        Task<ResultModel<List<EnvironmentListDTO>>> GetNamespaceList();
    }
}
