using Microsoft.AspNetCore.Http;

namespace RC.Deploy.Abstractions.Controllers
{
    /// <summary>
    /// 应用程序控制器
    /// </summary>
    public partial interface IApplicationInfoController
    {
        /// <summary>
        /// 上传新文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPut]
        Task<ResultModel> UploadNewFileAsync(Guid id, IFormFile file);
        /// <summary>
        /// 获得控制台消息数量
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        ResultModel<int> GetMaxConsoleMessageCount();
    }
}
