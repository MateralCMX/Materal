#nullable enable
using Materal.BaseCore.PresentationModel;
using Materal.BaseCore.Services.Models;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.CodeGenerator;
using Microsoft.AspNetCore.Http;
using RC.Deploy.DataTransmitModel.ApplicationInfo;

namespace RC.Deploy.WebAPI.Controllers
{
    public partial class ApplicationInfoController
    {
        /// <summary>
        /// 应用最后一个文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public ResultModel ApplyLasetFile([Required(ErrorMessage = "id不能为空")] Guid id)
        {
            DefaultService.ApplyLasetFile(id);
            return ResultModel.Success();
        }
        /// <summary>
        /// 应用文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpPut]
        public ResultModel ApplyFile([Required(ErrorMessage = "id不能为空")] Guid id,[Required(ErrorMessage = "fileName不能为空")] string fileName)
        {
            DefaultService.ApplyFile(id,fileName);
            return ResultModel.Success();
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpDelete]
        public ResultModel DeleteFile([Required(ErrorMessage = "id不能为空")] Guid id,[Required(ErrorMessage = "fileName不能为空")] string fileName)
        {
            DefaultService.DeleteFile(id,fileName);
            return ResultModel.Success();
        }
        /// <summary>
        /// 启动程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel Start([Required(ErrorMessage = "id不能为空")] Guid id)
        {
            DefaultService.Start(id);
            return ResultModel.Success();
        }
        /// <summary>
        /// 停止程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel Stop([Required(ErrorMessage = "id不能为空")] Guid id)
        {
            DefaultService.Stop(id);
            return ResultModel.Success();
        }
        /// <summary>
        /// 杀死程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel Kill([Required(ErrorMessage = "id不能为空")] Guid id)
        {
            DefaultService.Kill(id);
            return ResultModel.Success();
        }
        /// <summary>
        /// 全部启动
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ResultModel StartAll()
        {
            DefaultService.StartAll();
            return ResultModel.Success();
        }
        /// <summary>
        /// 全部停止
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ResultModel StopAll()
        {
            DefaultService.StopAll();
            return ResultModel.Success();
        }
        /// <summary>
        /// 获得控制台信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<ICollection<string>> GetConsoleMessages([Required(ErrorMessage = "id不能为空")] Guid id)
        {
            var result = DefaultService.GetConsoleMessages(id);
            return ResultModel<ICollection<string>>.Success(result);
        }
        /// <summary>
        /// 清空控制台信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ResultModel ClearConsoleMessages([Required(ErrorMessage = "id不能为空")] Guid id)
        {
            DefaultService.ClearConsoleMessages(id);
            return ResultModel.Success();
        }
        /// <summary>
        /// 获得上传文件列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<List<FileInfoDTO>> GetUploadFiles([Required(ErrorMessage = "id不能为空")] Guid id)
        {
            var result = DefaultService.GetUploadFiles(id);
            return ResultModel<List<FileInfoDTO>>.Success(result);
        }
    }
}
