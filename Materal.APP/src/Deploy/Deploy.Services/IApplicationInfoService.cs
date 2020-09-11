using Deploy.DataTransmitModel.ApplicationInfo;
using Deploy.Services.Models.ApplicationInfo;
using Materal.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deploy.Services
{
    public interface IApplicationInfoService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [DataValidation]
        Task AddAsync(AddApplicationInfoModel model);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [DataValidation]
        Task EditAsync(EditApplicationInfoModel model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataValidation]
        Task DeleteAsync(Guid id);
        /// <summary>
        /// 获得信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataValidation]
        ApplicationInfoDTO GetInfo(Guid id);
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [DataValidation]
        List<ApplicationInfoListDTO> GetList(QueryApplicationInfoFilterModel model);
        /// <summary>
        /// 启动程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataValidation]
        void Start(Guid id);
        /// <summary>
        /// 停止程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataValidation]
        void Stop(Guid id);
        /// <summary>
        /// 获得控制台信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataValidation]
        ICollection<string> GetConsoleMessage(Guid id);
        /// <summary>
        /// 全部启动
        /// </summary>
        /// <returns></returns>
        void StartAll();
        /// <summary>
        /// 全部停止
        /// </summary>
        /// <returns></returns>
        void StopAll();
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task SaveFileAsync(IFormFile file);
        /// <summary>
        /// 清理上传文件
        /// </summary>
        void ClearUpdateFiles();
        /// <summary>
        /// 清理不活跃的应用程序
        /// </summary>
        void ClearInactiveApplication();
        /// <summary>
        /// 是否为应用程序
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool IsRuningApplication(string path);
    }
}
