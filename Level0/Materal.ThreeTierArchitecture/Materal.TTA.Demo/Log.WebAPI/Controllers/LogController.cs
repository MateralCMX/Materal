using AutoMapper;
using Log.DataTransmitModel.Log;
using Log.PresentationModel.Log.Request;
using Log.Service;
using Log.Service.Model.Log;
using Materal.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Log.WebAPI.Controllers
{
    /// <summary>
    /// 日志控制器
    /// </summary>
    [ApiController, Route("api/[controller]/[action]")]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;
        private readonly IMapper _mapper;
        /// <summary>
        /// 构造方法
        /// </summary>
        public LogController(ILogService logService, IMapper mapper)
        {
            _logService = logService;
            _mapper = mapper;
        }

        /// <summary>
        /// 获取日志信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<LogDTO>> GetLogInfo(int id)
        {
            try
            {
                LogDTO result = await _logService.GetLogInfoAsync(id);
                return ResultModel<LogDTO>.Success(result, "查询成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<LogDTO>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获取日志列表信息
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResultModel<LogListDTO>> GetLogList(QueryLogFilterRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<QueryLogFilterModel>(requestModel);
                (List<LogListDTO> result, PageModel pageModel) = await _logService.GetLogListAsync(model);
                return PageResultModel<LogListDTO>.Success(result, pageModel, "查询成功");
            }
            catch (InvalidOperationException ex)
            {
                return PageResultModel<LogListDTO>.Fail(ex.Message);
            }
        }
    }
}