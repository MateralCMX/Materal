﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Materal.ConfigCenter.ControllerCore;
using Materal.ConfigCenter.ProtalServer.DataTransmitModel.Project;
using Materal.ConfigCenter.ProtalServer.PresentationModel.Project;
using Materal.ConfigCenter.ProtalServer.Services;
using Materal.DotNetty.ControllerBus.Attributes;
using Materal.Model;

namespace Materal.ConfigCenter.ProtalServer.Controllers
{
    public class ProjectController : ConfigCenterBaseController
    {
        private readonly IProjectService projectService;
        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }
        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> AddProject(AddProjectModel model)
        {
            try
            {
                await projectService.AddProjectAsync(model);
                return ResultModel.Success("添加成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 修改项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> EditProject(EditProjectModel model)
        {
            try
            {
                await projectService.EditProjectAsync(model);
                return ResultModel.Success("修改成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel> DeleteProject(Guid id)
        {
            try
            {
                await projectService.DeleteProjectAsync(id);
                return ResultModel.Success("删除成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得项目信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<ProjectDTO>> GetProjectInfo(Guid id)
        {
            try
            {
                ProjectDTO result = await projectService.GetProjectInfoAsync(id);
                return ResultModel<ProjectDTO>.Success(result, "查询成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel<ProjectDTO>.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel<ProjectDTO>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得项目列表
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResultModel<ProjectListDTO>> GetProjectList(QueryProjectFilterModel filterModel)
        {
            try
            {
                (List<ProjectListDTO> result, PageModel pageModel) = await projectService.GetProjectListAsync(filterModel);
                return PageResultModel<ProjectListDTO>.Success(result, pageModel, "查询成功");
            }
            catch (AspectInvocationException ex)
            {
                return PageResultModel<ProjectListDTO>.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return PageResultModel<ProjectListDTO>.Fail(ex.Message);
            }
        }
    }
}
