using Materal.APP.DataTransmitModel;
using Materal.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.APP.HttpManage
{
    public interface IServerManage
    {
        /// <summary>
        /// 获得服务列表
        /// </summary>
        /// <returns></returns>
        Task<ResultModel<List<ServerListDTO>>> GetServerListAsync();
    }
}
