using Microsoft.AspNetCore.Http;
using RC.Deploy.Abstractions.DTO.ApplicationInfo;
using RC.Deploy.Abstractions.RequestModel;

namespace RC.Deploy.Abstractions.Controllers
{
    /// <summary>
    /// 应用程序服务控制器
    /// </summary>
    public partial interface IApplicationInfoController : IMergeBlockControllerBase
    {
        /// <summary>
        /// 应用最后一个文件
        /// </summary>
        /// <param name="id"></param>
        [HttpPut]
        ResultModel ApplyLasetFile(Guid id);
        /// <summary>
        /// 应用文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        [HttpPut]
        ResultModel ApplyFile(Guid id, string fileName);
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        [HttpDelete]
        ResultModel DeleteFile(Guid id, string fileName);
        /// <summary>
        /// 启动程序
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        ResultModel Start(Guid id);
        /// <summary>
        /// 停止程序
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        ResultModel Stop(Guid id);
        /// <summary>
        /// 杀死程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        Task<ResultModel> KillAsync(Guid id);
        /// <summary>
        /// 全部启动
        /// </summary>
        [HttpPost]
        ResultModel StartAll();
        /// <summary>
        /// 全部停止
        /// </summary>
        [HttpPost]
        ResultModel StopAll();
        /// <summary>
        /// 获得控制台信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        ResultModel<ICollection<string>> GetConsoleMessages(Guid id);
        /// <summary>
        /// 清空控制台信息
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        ResultModel ClearConsoleMessages(Guid id);
        /// <summary>
        /// 获得上传文件列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        ResultModel<List<FileInfoDTO>> GetUploadFiles(Guid id);
    }
}
