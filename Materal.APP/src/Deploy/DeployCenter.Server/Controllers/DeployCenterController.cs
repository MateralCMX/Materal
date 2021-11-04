using Deploy.DataTransmitModel.DeployCenter;
using Deploy.Enums;
using Materal.APP.Core;
using Materal.APP.Core.Models;
using Materal.APP.WebAPICore;
using Materal.APP.WebAPICore.Controllers;
using Materal.APP.WebAPICore.Models;
using Materal.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeployCenter.Server.Controllers
{
    /// <summary>
    /// 配置中心控制器
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public class DeployCenterController : WebAPIControllerBase
    {
        /// <summary>
        /// 获得发布服务列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<List<DeployDTO>>> GetEnvironmentListAsync()
        {
            string tagText = ServiceType.DeployServer.ToString();
            List<ConsulServiceModel> consulServices = await ConsulManage.GetServicesAsync(m => m.Tags.Contains(tagText));
            List<DeployDTO> result = consulServices.Select(m=>new DeployDTO
            {
                Name = m.Tags.LastOrDefault(),
                Key = m.Service
            }).ToList();
            return ResultModel<List<DeployDTO>>.Success(result, "获取成功");
        }
        /// <summary>
        /// 获取所有应用程序类型枚举
        /// </summary>
        [HttpGet, AllowAnonymous]
        public ResultModel<List<KeyValueModel>> GetAllApplicationTypeEnum()
        {
            List<KeyValueModel> result = KeyValueModel.GetAllCode(typeof(ApplicationTypeEnum));
            return ResultModel<List<KeyValueModel>>.Success(result);
        }
    }
}
