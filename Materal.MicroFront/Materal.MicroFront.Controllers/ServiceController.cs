using System;
using Materal.Model;
using Materal.Services.Models;
using Materal.WebSocket.Http.Attributes;
using System.Collections.Generic;
using Materal.Services;

namespace Materal.MicroFront.Controllers
{
    public class ServiceController
    {
        private readonly IWebFileService _webFileService;

        public ServiceController(IWebFileService webFileService)
        {
            _webFileService = webFileService;
        }

        [HttpPost]
        public ResultModel<List<ServiceModel>> GetServiceModel()
        {
            List<ServiceModel> result =_webFileService.GetServices();
            return ResultModel<List<ServiceModel>>.Success(result, "获取成功");
        }

        [HttpPost]
        public ResultModel DeleteService(string serviceName)
        {
            try
            {
                _webFileService.DeleteService(serviceName);
                return ResultModel.Success("删除成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
    }
}
