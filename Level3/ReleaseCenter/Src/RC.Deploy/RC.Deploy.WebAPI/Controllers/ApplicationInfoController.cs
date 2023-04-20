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
    }
}
