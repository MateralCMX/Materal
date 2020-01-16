using Materal.ConDep.Manager;
using Materal.ConDep.Manager.Models;
using Materal.Model;
using Materal.WebSocket.Http.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.ConDep.Controllers
{
    public class AppController
    {
        private readonly IAppManager _appManager;
        public AppController(IAppManager appManager)
        {
            _appManager = appManager;
        }
        /// <summary>
        /// 获取应用列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<List<AppListModel>> GetAppList()
        {
            try
            {
                List<AppListModel> appList = _appManager.GetAppList();
                return ResultModel<List<AppListModel>>.Success(appList, "获取成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<List<AppListModel>>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 启动所有应用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel StartAllApp()
        {
            try
            {
                _appManager.StartAllApp();
                return ResultModel.Success("任务已分配");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 重启所有应用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel RestartAllApp()
        {
            try
            {
                _appManager.RestartAllApp();
                return ResultModel.Success("任务已分配");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 停止所有应用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel StopAllApp()
        {
            try
            {
                _appManager.StopAllApp();
                return ResultModel.Success("任务已分配");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 添加一个应用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ResultModel AddApp(AppModel appModel)
        {
            Task<ResultModel> task = Task.Run(async () =>
            {
                try
                {
                    await _appManager.AddAppAsync(appModel);
                    return ResultModel.Success("已添加应用");
                }
                catch (InvalidOperationException ex)
                {
                    return ResultModel.Fail(ex.Message);
                }
            });
            return task.Result;
        }
        /// <summary>
        /// 修改一个应用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ResultModel EditApp(AppModel appModel)
        {
            Task<ResultModel> task = Task.Run(async () =>
            {
                try
                {
                    await _appManager.EditAppAsync(appModel);
                    return ResultModel.Success("已修改应用");
                }
                catch (InvalidOperationException ex)
                {
                    return ResultModel.Fail(ex.Message);
                }
            });
            return task.Result;
        }
        /// <summary>
        /// 删除一个应用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel DeleteApp(Guid id)
        {
            Task<ResultModel> task = Task.Run(async () =>
            {
                try
                {
                    await _appManager.DeleteAppAsync(id);
                    return ResultModel.Success("已删除应用");
                }
                catch (InvalidOperationException ex)
                {
                    return ResultModel.Fail(ex.Message);
                }
            });
            return task.Result;
        }
        /// <summary>
        /// 获得应用信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<AppModel> GetAppInfo(Guid id)
        {
            try
            {
                AppModel result = _appManager.GetAppInfo(id);
                return ResultModel<AppModel>.Success(result, "查询成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<AppModel>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 启动应用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel StartApp(Guid id)
        {
            try
            {
                _appManager.StartApp(id);
                return ResultModel.Success("任务已分配");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 重启应用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel RestartApp(Guid id)
        {
            try
            {
                _appManager.RestartApp(id);
                return ResultModel.Success("任务已分配");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 停止应用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel StopApp(Guid id)
        {
            try
            {
                _appManager.StopApp(id);
                return ResultModel.Success("任务已分配");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获得控制台列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<List<string>> GetConsoleList(Guid id)
        {
            try
            {
                List<string> result = _appManager.GetConsoleList(id);
                return ResultModel<List<string>>.Success(result, "查询成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<List<string>>.Fail(ex.Message);
            }
        }
    }
}
