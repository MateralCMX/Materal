using Materal.BaseCore.CodeGenerator;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Http;
using RC.Deploy.DataTransmitModel.ApplicationInfo;

namespace RC.Deploy.Services
{
    /// <summary>
    /// 应用程序服务
    /// </summary>
    public partial interface IApplicationInfoService
    {
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [DataValidation]
        Task SaveFileAsync(Guid id, IFormFile file);
        /// <summary>
        /// 应用最后一个文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataValidation, MapperController(MapperType.Put)]
        void ApplyLasetFile(Guid id);
        /// <summary>
        /// 应用文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [DataValidation, MapperController(MapperType.Put)]
        void ApplyFile(Guid id, string fileName);
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [DataValidation, MapperController(MapperType.Delete)]
        void DeleteFile(Guid id, string fileName);
        /// <summary>
        /// 启动程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataValidation, MapperController(MapperType.Post)]
        void Start(Guid id);
        /// <summary>
        /// 停止程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataValidation, MapperController(MapperType.Post)]
        void Stop(Guid id);
        /// <summary>
        /// 杀死程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataValidation, MapperController(MapperType.Post)]
        void Kill(Guid id);
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
        [DataValidation, MapperController(MapperType.Get)]
        ICollection<string> GetConsoleMessages(Guid id);
        /// <summary>
        /// 清空控制台信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataValidation, MapperController(MapperType.Delete)]
        void ClearConsoleMessages(Guid id);
        /// <summary>
        /// 应用程序是否在运行
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [DataValidation]
        bool IsRunningApplication(string path);
        /// <summary>
        /// 获得上传文件列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataValidation, MapperController(MapperType.Get)]
        List<FileInfoDTO> GetUploadFiles(Guid id);
    }
}
