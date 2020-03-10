using Materal.ConDep.Services;
using Materal.ConDep.Services.Models;
using Materal.DotNetty.ControllerBus.Attributes;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Materal.ConDep.ControllerCore;
using Materal.ConDep.Controllers.Models;
using Materal.ConvertHelper;

namespace Materal.ConDep.Controllers
{
    public class AppController : ConDepBaseController
    {
        private readonly IAppService _appService;
        public AppController(IAppService appService)
        {
            _appService = appService;
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
                List<AppListModel> appList = _appService.GetAppList();
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
                _appService.StartAllApp();
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
                _appService.RestartAllApp();
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
                _appService.StopAllApp();
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
        public async Task<ResultModel> AddApp(AddAppRequestModel requestModel)
        {
            try
            {
                var appModel = requestModel.CopyProperties<AppModel>();
                await _appService.AddAppAsync(appModel);
                return ResultModel.Success("已添加应用");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 修改一个应用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> EditApp(EditAppRequestModel requestModel)
        {
            try
            {
                var appModel = requestModel.CopyProperties<AppModel>();
                await _appService.EditAppAsync(appModel);
                return ResultModel.Success("已修改应用");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 删除一个应用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel> DeleteApp(Guid id)
        {
            try
            {
                await _appService.DeleteAppAsync(id);
                return ResultModel.Success("已删除应用");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
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
                AppModel result = _appService.GetAppInfo(id);
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
                _appService.StartApp(id);
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
                _appService.RestartApp(id);
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
                _appService.StopApp(id);
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
                List<string> result = _appService.GetConsoleList(id);
                return ResultModel<List<string>>.Success(result, "查询成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<List<string>>.Fail(ex.Message);
            }
        }
        [HttpGet]
        public ResultModel UploadFile()
        {
            return ResultModel.Success("上传成功");
        }
    }
}
