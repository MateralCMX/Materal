using AutoMapper;
using Deploy.DataTransmitModel.ApplicationInfo;
using Deploy.PresentationModel.ApplicationInfo;
using Deploy.Services;
using Deploy.Services.Models.ApplicationInfo;
using Materal.APP.WebAPICore.Controllers;
using Materal.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Deploy.Server.Controllers
{
    /// <summary>
    /// 默认数据控制器
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public class ApplicationInfoController : WebAPIControllerBase
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
        public ResultModel<ApplicationInfoDTO> GetInfoAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            ApplicationInfoDTO result = _applicationInfoService.GetInfo(id);
            return ResultModel<ApplicationInfoDTO>.Success(result, "查询成功");
        }
        /// <summary>
        /// 获得应用程序列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel<List<ApplicationInfoListDTO>> GetList(QueryApplicationInfoFilterRequestModel requestModel)
        {
            var model = _mapper.Map<QueryApplicationInfoFilterModel>(requestModel);
            List<ApplicationInfoListDTO> result = _applicationInfoService.GetList(model);
            return ResultModel<List<ApplicationInfoListDTO>>.Success(result, "查询成功");
        }
        /// <summary>
        /// 获得简易应用程序列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public ResultModel<List<ApplicationInfoSimpleListDTO>> GetSimpleList(QueryApplicationInfoFilterRequestModel requestModel)
        {
            var model = _mapper.Map<QueryApplicationInfoFilterModel>(requestModel);
            List<ApplicationInfoListDTO> applicationInfoList = _applicationInfoService.GetList(model);
            var result = _mapper.Map<List<ApplicationInfoSimpleListDTO>>(applicationInfoList);
            return ResultModel<List<ApplicationInfoSimpleListDTO>>.Success(result, "查询成功");
        }
        /// <summary>
        /// 启动程序
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]

        public ResultModel Start([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            _applicationInfoService.Start(id);
            return ResultModel.Success("应用程序已启动");
        }
        /// <summary>
        /// 停止程序
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        public ResultModel Stop([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            _applicationInfoService.Stop(id);
            return ResultModel.Success("应用程序已停止");
        }
        /// <summary>
        /// 启动所有程序
        /// </summary>
        [HttpPost]
        public ResultModel StartAll()
        {
            _applicationInfoService.StartAll();
            return ResultModel.Success("应用程序已全部启动");
        }
        /// <summary>
        /// 停止所有程序
        /// </summary>
        [HttpPost]
        public ResultModel StopAll()
        {
            _applicationInfoService.StopAll();
            return ResultModel.Success("应用程序已全部停止");
        }
        /// <summary>
        /// 获取控制台消息
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        public ResultModel<ICollection<string>> GetConsoleMessage([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            ICollection<string> consoleMessage = _applicationInfoService.GetConsoleMessage(id);
            return ResultModel<ICollection<string>>.Success(consoleMessage, "查询成功");
        }
        /// <summary>
        /// 上传新文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> UploadNewFileAsync([Required(ErrorMessage = "文件不可以为空")] IFormFile file,[FromQuery] Guid id)
        {
            await _applicationInfoService.SaveFileAsync(file, id);
            return ResultModel.Success("上传成功");
        }
        /// <summary>
        /// 获取上传文件列表
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        public ResultModel<ICollection<string>> GetUploadFileList([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            ICollection<string> uploadFileList = _applicationInfoService.GetUploadFileList(id);
            return ResultModel<ICollection<string>>.Success(uploadFileList, "查询成功");
        }

        /// <summary>
        /// 删除上传文件
        /// </summary>
        /// <param name="requestModel"></param>
        [HttpDelete]
        public ResultModel DeleteUploadFile(DeleteFileRequestModel requestModel)
        {
            _applicationInfoService.DeleteUploadFile(requestModel.ID, requestModel.FileName);
            return ResultModel.Success("删除成功");
        }
        /// <summary>
        /// 清理临时文件
        /// </summary>
        [HttpDelete]
        public Task<ResultModel> ClearTempFile()
        {
            _applicationInfoService.ClearTempFile();
            return Task.FromResult(ResultModel.Success("已清理临时文件"));
        }
        /// <summary>
        /// 清理不活跃的应用程序
        /// </summary>
        [HttpDelete]
        public Task<ResultModel> ClearInactiveApplicationAsync()
        {
            _applicationInfoService.ClearInactiveApplication();
            return Task.FromResult(ResultModel.Success("已清理不活跃的应用程序"));
        }
    }
}
