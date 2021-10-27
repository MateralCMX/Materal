using AutoMapper;
using ConfigCenter.DataTransmitModel.Namespace;
using ConfigCenter.HttpManage;
using ConfigCenter.PresentationModel.Namespace;
using ConfigCenter.Server.Hubs;
using ConfigCenter.Services;
using ConfigCenter.Services.Models.Namespace;
using Materal.APP.WebAPICore;
using Materal.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ConfigCenter.Hubs.Clients;
using Microsoft.AspNetCore.SignalR;

namespace ConfigCenter.Server.Controllers
{
    /// <summary>
    /// 命名空间控制器
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public class NamespaceController : WebAPIControllerBase, INamespaceManage
    {
        private readonly IMapper _mapper;
        private readonly INamespaceService _namespaceService;
        private readonly IHubContext<ConfigCenterHub, IConfigCenterClient> _configCenterHubContext;
        /// <summary>
        /// 命名空间控制器
        /// </summary>
        public NamespaceController(INamespaceService namespaceService, IMapper mapper, IHubContext<ConfigCenterHub, IConfigCenterClient> configCenterHubContext)
        {
            _namespaceService = namespaceService;
            _mapper = mapper;
            _configCenterHubContext = configCenterHubContext;
        }
        /// <summary>
        /// 添加命名空间
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> AddNamespaceAsync(AddNamespaceRequestModel requestModel)
        {
            var model = _mapper.Map<AddNamespaceModel>(requestModel);
            await _namespaceService.AddNamespaceAsync(model);
            return ResultModel.Success("添加成功");
        }
        /// <summary>
        /// 修改命名空间
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> EditNamespaceAsync(EditNamespaceRequestModel requestModel)
        {
            var model = _mapper.Map<EditNamespaceModel>(requestModel);
            await _namespaceService.EditNamespaceAsync(model);
            return ResultModel.Success("修改成功");
        }
        /// <summary>
        /// 删除命名空间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResultModel> DeleteNamespaceAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            await _configCenterHubContext.Clients.All.DeleteNamespace(id);
            await _namespaceService.DeleteNamespaceAsync(id);
            return ResultModel.Success("删除成功");
        }
        /// <summary>
        /// 获得命名空间信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<NamespaceDTO>> GetNamespaceInfoAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            NamespaceDTO result = await _namespaceService.GetNamespaceInfoAsync(id);
            return ResultModel<NamespaceDTO>.Success(result, "查询成功");
        }
        /// <summary>
        /// 获得命名空间列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel<List<NamespaceListDTO>>> GetNamespaceListAsync(QueryNamespaceFilterRequestModel requestModel)
        {
            var model = _mapper.Map<QueryNamespaceFilterModel>(requestModel);
            List<NamespaceListDTO> result = await _namespaceService.GetNamespaceListAsync(model);
            return ResultModel<List<NamespaceListDTO>>.Success(result, "查询成功");
        }
    }
}
