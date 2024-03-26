/*
 * Generator Code From MateralMergeBlock=>GeneratorControllerMapperCode
 */
using Microsoft.AspNetCore.Http;
using RC.Deploy.Abstractions.DTO.ApplicationInfo;
using RC.Deploy.Abstractions.Services.Models;
using RC.Deploy.Abstractions.RequestModel;

namespace RC.Deploy.Application.Controllers
{
    /// <summary>
    /// 应用程序服务控制器
    /// </summary>
    public partial class ApplicationInfoController
    {
        /// <summary>
        /// 应用最后一个文件
        /// </summary>
        /// <param name="id"></param>
        [HttpPut]
        public ResultModel ApplyLasetFile(Guid id)
        {
            DefaultService.ApplyLasetFile(id);
            return ResultModel.Success("应用最后一个文件成功");
        }
        /// <summary>
        /// 应用文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        [HttpPut]
        public ResultModel ApplyFile(Guid id, string fileName)
        {
            DefaultService.ApplyFile(id, fileName);
            return ResultModel.Success("应用文件成功");
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        [HttpDelete]
        public ResultModel DeleteFile(Guid id, string fileName)
        {
            DefaultService.DeleteFile(id, fileName);
            return ResultModel.Success("删除文件成功");
        }
        /// <summary>
        /// 启动程序
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        public ResultModel Start(Guid id)
        {
            DefaultService.Start(id);
            return ResultModel.Success("启动程序成功");
        }
        /// <summary>
        /// 停止程序
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        public ResultModel Stop(Guid id)
        {
            DefaultService.Stop(id);
            return ResultModel.Success("停止程序成功");
        }
        /// <summary>
        /// 杀死程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> KillAsync(Guid id)
        {
            await DefaultService.KillAsync(id);
            return ResultModel.Success("杀死程序成功");
        }
        /// <summary>
        /// 全部启动
        /// </summary>
        [HttpPost]
        public ResultModel StartAll()
        {
            DefaultService.StartAll();
            return ResultModel.Success("全部启动成功");
        }
        /// <summary>
        /// 全部停止
        /// </summary>
        [HttpPost]
        public ResultModel StopAll()
        {
            DefaultService.StopAll();
            return ResultModel.Success("全部停止成功");
        }
        /// <summary>
        /// 获得控制台信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<ICollection<string>> GetConsoleMessages(Guid id)
        {
            ICollection<string> result = DefaultService.GetConsoleMessages(id);
            return ResultModel<ICollection<string>>.Success(result, "获得控制台信息成功");
        }
        /// <summary>
        /// 清空控制台信息
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public ResultModel ClearConsoleMessages(Guid id)
        {
            DefaultService.ClearConsoleMessages(id);
            return ResultModel.Success("清空控制台信息成功");
        }
        /// <summary>
        /// 获得上传文件列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<List<FileInfoDTO>> GetUploadFiles(Guid id)
        {
            List<FileInfoDTO> result = DefaultService.GetUploadFiles(id);
            return ResultModel<List<FileInfoDTO>>.Success(result, "获得上传文件列表成功");
        }
    }
}
