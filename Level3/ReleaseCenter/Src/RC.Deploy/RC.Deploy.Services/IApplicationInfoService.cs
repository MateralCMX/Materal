using Materal.Utils.Model;
using Microsoft.AspNetCore.Http;

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
        [DataValidation]
        void ApplyLasetFile(Guid id);
        /// <summary>
        /// 应用文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [DataValidation]
        void ApplyFile(Guid id, string fileName);
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
        /// 杀死程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataValidation]
        void Kill(Guid id);
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
        /// 获得控制台信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataValidation]
        ICollection<string> GetConsoleMessages(Guid id);
        /// <summary>
        /// 清空控制台信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataValidation]
        void ClearConsoleMessages(Guid id);
        /// <summary>
        /// 应用程序是否在运行
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool IsRunningApplication(string path);
    }
}
