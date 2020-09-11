using Deploy.DataTransmitModel.ApplicationInfo;
using Deploy.PresentationModel.ApplicationInfo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Materal.Model;
using Microsoft.AspNetCore.Http;

namespace Deploy.HttpManage
{
    public interface IApplicationInfoManage
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel> AddAsync(AddApplicationInfoRequestModel requestModel);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel> EditAsync(EditApplicationInfoRequestModel requestModel);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResultModel> DeleteAsync(Guid id);

        /// <summary>
        /// 获得信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResultModel<ApplicationInfoDTO>> GetInfoAsync(Guid id);

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel<List<ApplicationInfoListDTO>>> GetListAsync(QueryApplicationInfoFilterRequestModel requestModel);

        /// <summary>
        /// 启动程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResultModel> StartAsync(Guid id);

        /// <summary>
        /// 停止程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResultModel> StopAsync(Guid id);

        /// <summary>
        /// 获得控制台信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResultModel<ICollection<string>>> GetConsoleMessageAsync(Guid id);
        /// <summary>
        /// 全部启动
        /// </summary>
        /// <returns></returns>
        Task<ResultModel> StartAllAsync();
        /// <summary>
        /// 全部停止
        /// </summary>
        /// <returns></returns>
        Task<ResultModel> StopAllAsync();
        /// <summary>
        /// 上传新文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<ResultModel> UploadNewFileAsync(IFormFile file);
        /// <summary>
        /// 清理上传文件
        /// </summary>
        Task<ResultModel> ClearUpdateFilesAsync();
        /// <summary>
        /// 清理不活跃的应用程序
        /// </summary>
        Task<ResultModel> ClearInactiveApplicationAsync();

    }
}
