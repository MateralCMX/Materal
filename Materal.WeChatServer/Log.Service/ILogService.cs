using Log.DataTransmitModel.Log;
using Log.Service.Model.Log;
using Materal.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Log.Service
{
    public interface ILogService
    {
        /// <summary>
        /// 获得日志信息
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<LogDTO> GetLogInfoAsync(int id);
        /// <summary>
        /// 获得日志列表
        /// </summary>
        /// <param name="filterModel">查询模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<(List<LogListDTO> result, PageModel pageModel)> GetLogListAsync(QueryLogFilterModel filterModel);
    }
}
