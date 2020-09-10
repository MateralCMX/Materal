using ConfigCenter.DataTransmitModel.Project;
using ConfigCenter.PresentationModel.Project;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigCenter.HttpManage
{
    public interface IProjectManage
    {
        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel> AddProjectAsync(AddProjectRequestModel requestModel);

        /// <summary>
        /// 修改项目
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel> EditProjectAsync(EditProjectRequestModel requestModel);

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResultModel> DeleteProjectAsync(Guid id);

        /// <summary>
        /// 获得项目信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResultModel<ProjectDTO>> GetProjectInfoAsync(Guid id);

        /// <summary>
        /// 获得项目列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel<List<ProjectListDTO>>> GetProjectListAsync(QueryProjectFilterRequestModel requestModel);
    }
}
