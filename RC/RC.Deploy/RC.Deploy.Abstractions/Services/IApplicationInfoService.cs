﻿using Microsoft.AspNetCore.Http;
using RC.Deploy.Abstractions.DTO.ApplicationInfo;
using RC.Deploy.Abstractions.Services.Models;

namespace RC.Deploy.Abstractions.Services
{
    /// <summary>
    /// 应用程序服务
    /// </summary>
    public partial interface IApplicationInfoService : IMergeBlockController
    {
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        Task SaveFileAsync(Guid id, IFormFile file);
        /// <summary>
        /// 应用最后一个文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MapperController(MapperType.Put)]
        void ApplyLasetFile(Guid id);
        /// <summary>
        /// 应用文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [MapperController(MapperType.Put)]
        void ApplyFile(Guid id, string fileName);
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [MapperController(MapperType.Delete)]
        void DeleteFile(Guid id, string fileName);
        /// <summary>
        /// 启动程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MapperController(MapperType.Post)]
        void Start(Guid id);
        /// <summary>
        /// 停止程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MapperController(MapperType.Post)]
        void Stop(Guid id);
        /// <summary>
        /// 杀死程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MapperController(MapperType.Post)]
        Task KillAsync(Guid id);
        /// <summary>
        /// 全部启动
        /// </summary>
        /// <returns></returns>
        [MapperController(MapperType.Post)]
        void StartAll();
        /// <summary>
        /// 全部停止
        /// </summary>
        /// <returns></returns>
        [MapperController(MapperType.Post)]
        void StopAll();
        /// <summary>
        /// 获得控制台信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MapperController(MapperType.Get)]
        ICollection<string> GetConsoleMessages(Guid id);
        /// <summary>
        /// 清空控制台信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MapperController(MapperType.Delete)]
        void ClearConsoleMessages(Guid id);
        /// <summary>
        /// 应用程序是否在运行
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool IsRunningApplication(string path);
        /// <summary>
        /// 获得应用程序信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IApplicationRuntimeModel? GetApplicationRuntimeModel(string path);
        /// <summary>
        /// 获得上传文件列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MapperController(MapperType.Get)]
        List<FileInfoDTO> GetUploadFiles(Guid id);
    }
}
