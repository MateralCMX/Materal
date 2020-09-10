using ConfigCenter.Common;
using ConfigCenter.DataTransmitModel.Project;
using ConfigCenter.Services.Models.Project;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigCenter.Services
{
    public interface IProjectService
    {
        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ConfigCenterException"></exception>
        [DataValidation]
        Task AddProjectAsync(AddProjectModel model);
        /// <summary>
        /// 修改项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ConfigCenterException"></exception>
        [DataValidation]
        Task EditProjectAsync(EditProjectModel model);
        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ConfigCenterException"></exception>
        [DataValidation]
        Task DeleteProjectAsync(Guid id);
        /// <summary>
        /// 获得项目信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ConfigCenterException"></exception>
        [DataValidation]
        Task<ProjectDTO> GetProjectInfoAsync(Guid id);
        /// <summary>
        /// 获得项目列表
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        /// <exception cref="ConfigCenterException"></exception>
        [DataValidation]
        Task<List<ProjectListDTO>> GetProjectListAsync(QueryProjectFilterModel filterModel);
    }
}
