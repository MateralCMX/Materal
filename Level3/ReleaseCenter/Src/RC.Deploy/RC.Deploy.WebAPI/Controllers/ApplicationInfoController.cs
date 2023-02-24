using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RC.Deploy.WebAPI.Controllers
{
    /// <summary>
    /// 应用程序控制器
    /// </summary>
    public partial class ApplicationInfoController
    {
        /// <summary>
        /// 启动程序
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        public ResultModel Start([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            DefaultService.Start(id);
            return ResultModel.Success("应用程序已启动");
        }
        /// <summary>
        /// 停止程序
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        public ResultModel Stop([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            DefaultService.Stop(id);
            return ResultModel.Success("应用程序已停止");
        }
        /// <summary>
        /// 杀死程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel Kill([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            DefaultService.Kill(id);
            return ResultModel.Success("应用程序已停止");
        }
        /// <summary>
        /// 获取控制台消息
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        public ResultModel<ICollection<string>> GetConsoleMessages([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            ICollection<string> consoleMessage = DefaultService.GetConsoleMessages(id);
            return ResultModel<ICollection<string>>.Success(consoleMessage, "查询成功");
        }
        /// <summary>
        /// 清空控制台消息
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public ResultModel ClearConsoleMessages([Required(ErrorMessage = "唯一标识不能为空")] Guid id)
        {
            DefaultService.ClearConsoleMessages(id);
            return ResultModel.Success("已清空");
        }
        /// <summary>
        /// 启动所有程序
        /// </summary>
        [HttpPost]
        public ResultModel StartAll()
        {
            DefaultService.StartAll();
            return ResultModel.Success("应用程序已全部启动");
        }
        /// <summary>
        /// 停止所有程序
        /// </summary>
        [HttpPost]
        public ResultModel StopAll()
        {
            DefaultService.StopAll();
            return ResultModel.Success("应用程序已全部停止");
        }
        /// <summary>
        /// 上传新文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> UploadNewFileAsync([Required(ErrorMessage = "唯一标识为空")] Guid id, [Required(ErrorMessage = "文件不可以为空")] IFormFile file)
        {
            await DefaultService.SaveFileAsync(id, file);
            return ResultModel.Success("上传成功，正在更新发布文件...");
        }
        /// <summary>
        /// 应用最后一个文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public ResultModel ApplyLasetFile([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            DefaultService.ApplyLasetFile(id);
            return ResultModel.Success("正在更新发布文件...");
        }
        /// <summary>
        /// 应用文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpPut]
        public ResultModel ApplyFile([Required(ErrorMessage = "唯一标识为空")] Guid id, string fileName)
        {
            DefaultService.ApplyFile(id, fileName);
            return ResultModel.Success("正在更新发布文件...");
        }
    }
}
