using Materal.ConDep.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Materal.DotNetty.Server.Core.Models;

namespace Materal.ConDep.Services
{
    /// <summary>
    /// 应用程序管理器
    /// </summary>
    public interface IAppService
    {
        /// <summary>
        /// 启动所有应用
        /// </summary>
        /// <returns></returns>
        void StartAllApp();
        /// <summary>
        /// 重启所有应用
        /// </summary>
        /// <returns></returns>
        void RestartAllApp();
        /// <summary>
        /// 停止所有应用
        /// </summary>
        /// <returns></returns>
        void StopAllApp();
        /// <summary>
        /// 根据路径停止
        /// </summary>
        /// <param name="paths"></param>
        void StopAppByPaths(params string[] paths);
        /// <summary>
        /// 添加一个应用
        /// </summary>
        /// <param name="appModel"></param>
        Task AddAppAsync(AppModel appModel);
        /// <summary>
        /// 修改一个应用
        /// </summary>
        /// <param name="appModel"></param>
        /// <returns></returns>
        Task EditAppAsync(AppModel appModel);
        /// <summary>
        /// 删除一个应用
        /// </summary>
        /// <param name="id"></param>
        Task DeleteAppAsync(Guid id);
        /// <summary>
        /// 获得应用信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AppModel GetAppInfo(Guid id);
        /// <summary>
        /// 添加一个Web应用
        /// </summary>
        /// <param name="appModel"></param>
        Task AddWebAppAsync(WebAppModel appModel);
        /// <summary>
        /// 修改一个Web应用
        /// </summary>
        /// <param name="appModel"></param>
        /// <returns></returns>
        Task EditWebAppAsync(WebAppModel appModel);
        /// <summary>
        /// 删除一个Web应用
        /// </summary>
        /// <param name="id"></param>
        Task DeleteWebAppAsync(Guid id);
        /// <summary>
        /// 获得Web应用信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        WebAppModel GetWebAppInfo(Guid id);
        /// <summary>
        /// 启动应用
        /// </summary>
        /// <returns></returns>
        void StartApp(Guid id);
        /// <summary>
        /// 重启应用
        /// </summary>
        /// <returns></returns>
        void RestartApp(Guid id);
        /// <summary>
        /// 停止应用
        /// </summary>
        /// <returns></returns>
        void StopApp(Guid id);
        /// <summary>
        /// 获取应用列表
        /// </summary>
        /// <returns></returns>
        List<AppListModel> GetAppList();
        /// <summary>
        /// 获取Web应用列表
        /// </summary>
        /// <returns></returns>
        List<WebAppModel> GetWebAppList();
        /// <summary>
        /// 获得控制台列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<string> GetConsoleList(Guid id);

        /// <summary>
        /// 上传APP
        /// </summary>
        /// <param name="file"></param>
        Task UpdateAppAsync(IUploadFileModel file);
    }
}
