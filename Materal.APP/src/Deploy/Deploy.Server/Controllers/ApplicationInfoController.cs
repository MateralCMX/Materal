using AutoMapper;
using Deploy.DataTransmitModel.ApplicationInfo;
using Deploy.HttpManage;
using Deploy.PresentationModel.ApplicationInfo;
using Deploy.Services;
using Deploy.Services.Models.ApplicationInfo;
using Materal.APP.WebAPICore;
using Materal.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Deploy.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Deploy.Server.Controllers
{
    /// <summary>
    /// 应用程序控制器
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public class ApplicationInfoController : WebAPIControllerBase, IApplicationInfoManage
    {
        private readonly IMapper _mapper;
        private readonly IApplicationInfoService _applicationInfoService;
        /// <summary>
        /// 应用程序控制器
        /// </summary>
        public ApplicationInfoController(IMapper mapper, IApplicationInfoService applicationInfoService)
        {
            _mapper = mapper;
            _applicationInfoService = applicationInfoService;
        }
        /// <summary>
        /// 添加应用程序
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> AddAsync(AddApplicationInfoRequestModel requestModel)
        {
            var model = _mapper.Map<AddApplicationInfoModel>(requestModel);
            await _applicationInfoService.AddAsync(model);
            return ResultModel.Success("添加成功");
        }
        /// <summary>
        /// 修改应用程序
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> EditAsync(EditApplicationInfoRequestModel requestModel)
        {
            var model = _mapper.Map<EditApplicationInfoModel>(requestModel);
            await _applicationInfoService.EditAsync(model);
            return ResultModel.Success("修改成功");
        }
        /// <summary>
        /// 删除应用程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResultModel> DeleteAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            await _applicationInfoService.DeleteAsync(id);
            return ResultModel.Success("删除成功");
        }
        /// <summary>
        /// 获得应用程序信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<ResultModel<ApplicationInfoDTO>> GetInfoAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            ApplicationInfoDTO result = _applicationInfoService.GetInfo(id);
            return Task.FromResult(ResultModel<ApplicationInfoDTO>.Success(result, "查询成功"));
        }
        /// <summary>
        /// 获得应用程序列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultModel<List<ApplicationInfoListDTO>>> GetListAsync(QueryApplicationInfoFilterRequestModel requestModel)
        {
            var model = _mapper.Map<QueryApplicationInfoFilterModel>(requestModel);
            List<ApplicationInfoListDTO> result = _applicationInfoService.GetList(model);
            return Task.FromResult(ResultModel<List<ApplicationInfoListDTO>>.Success(result, "查询成功"));
        }
        /// <summary>
        /// 获取运行中的静态文件列表
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public Task<ResultModel<List<ApplicationInfoListDTO>>> GetRuningStaticListAsync()
        {
            var model = new QueryApplicationInfoFilterModel
            {
                ApplicationStatus = ApplicationStatusEnum.Runing,
                ApplicationType = ApplicationTypeEnum.StaticDocument,
                MainModule = "index.html"
            };
            List<ApplicationInfoListDTO> result = _applicationInfoService.GetList(model);
            return Task.FromResult(ResultModel<List<ApplicationInfoListDTO>>.Success(result, "查询成功"));
        }
        /// <summary>
        /// 启动程序
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]

        public Task<ResultModel> StartAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            _applicationInfoService.Start(id);
            return Task.FromResult(ResultModel.Success("应用程序已启动"));
        }
        /// <summary>
        /// 停止程序
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        public Task<ResultModel> StopAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            _applicationInfoService.Stop(id);
            return Task.FromResult(ResultModel.Success("应用程序已停止"));
        }
        /// <summary>
        /// 获取控制台消息
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        public Task<ResultModel<ICollection<string>>> GetConsoleMessageAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            ICollection<string> consoleMessage = _applicationInfoService.GetConsoleMessage(id);
            return Task.FromResult(ResultModel<ICollection<string>>.Success(consoleMessage, "查询成功"));
        }
        /// <summary>
        /// 启动所有程序
        /// </summary>
        [HttpPost]
        public Task<ResultModel> StartAllAsync()
        {
            _applicationInfoService.StartAll();
            return Task.FromResult(ResultModel.Success("应用程序已全部启动"));
        }
        /// <summary>
        /// 停止所有程序
        /// </summary>
        [HttpPost]
        public Task<ResultModel> StopAllAsync()
        {
            _applicationInfoService.StopAll();
            return Task.FromResult(ResultModel.Success("应用程序已全部停止"));
        }
        /// <summary>
        /// 上传新文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> UploadNewFileAsync([Required(ErrorMessage = "文件不可以为空")] IFormFile file)
        {
            await _applicationInfoService.SaveFileAsync(file);
            return ResultModel.Success("上传成功");
        }
        /// <summary>
        /// 清理上传文件
        /// </summary>
        [HttpDelete]
        public Task<ResultModel> ClearUpdateFilesAsync()
        {
            _applicationInfoService.ClearUpdateFiles();
            return Task.FromResult(ResultModel.Success("已清理"));
        }
        /// <summary>
        /// 清理不活跃的应用程序
        /// </summary>
        [HttpDelete]
        public Task<ResultModel> ClearInactiveApplicationAsync()
        {
            _applicationInfoService.ClearInactiveApplication();
            return Task.FromResult(ResultModel.Success("已清理"));
        }
    }
}
