﻿using AutoMapper;
using ConfigCenter.DataTransmitModel.Project;
using ConfigCenter.PresentationModel.Project;
using ConfigCenter.Services;
using ConfigCenter.Services.Models.Project;
using Materal.APP.WebAPICore.Controllers;
using Materal.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ConfigCenter.Server.Controllers
{
    /// <summary>
    /// 项目控制器
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public class ProjectController : WebAPIControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectService _projectService; 
        /// <summary>
        /// 项目控制器
        /// </summary>
        public ProjectController(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }
        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> AddProjectAsync(AddProjectRequestModel requestModel)
        {
            var model = _mapper.Map<AddProjectModel>(requestModel);
            await _projectService.AddProjectAsync(model);
            return ResultModel.Success("添加成功");
        }
        /// <summary>
        /// 修改项目
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> EditProjectAsync(EditProjectRequestModel requestModel)
        {
            var model = _mapper.Map<EditProjectModel>(requestModel);
            await _projectService.EditProjectAsync(model);
            return ResultModel.Success("修改成功");
        }
        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResultModel> DeleteProjectAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            //todo:删除所有环境的该项目配置
            //await _configCenterHubContext.Clients.All.DeleteProject(id);
            await _projectService.DeleteProjectAsync(id);
            return ResultModel.Success("删除成功");
        }
        /// <summary>
        /// 获得项目信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<ProjectDTO>> GetProjectInfoAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            ProjectDTO result = await _projectService.GetProjectInfoAsync(id);
            return ResultModel<ProjectDTO>.Success(result, "查询成功");
        }
        /// <summary>
        /// 获得项目列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel<List<ProjectListDTO>>> GetProjectListAsync(QueryProjectFilterRequestModel requestModel)
        {
            var model = _mapper.Map<QueryProjectFilterModel>(requestModel);
            List<ProjectListDTO> result = await _projectService.GetProjectListAsync(model);
            return ResultModel<List<ProjectListDTO>>.Success(result, "查询成功");
        }
    }
}