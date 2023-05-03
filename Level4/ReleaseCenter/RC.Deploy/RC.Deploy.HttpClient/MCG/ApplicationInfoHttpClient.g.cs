#nullable enable
using RC.Core.HttpClient;
using Materal.Utils.Model;
using Materal.BaseCore.PresentationModel;
using RC.Deploy.DataTransmitModel.ApplicationInfo;
using RC.Deploy.PresentationModel.ApplicationInfo;
using System.ComponentModel.DataAnnotations;

namespace RC.Deploy.HttpClient
{
    public partial class ApplicationInfoHttpClient : HttpClientBase<AddApplicationInfoRequestModel, EditApplicationInfoRequestModel, QueryApplicationInfoRequestModel, ApplicationInfoDTO, ApplicationInfoListDTO>
    {
        public ApplicationInfoHttpClient() : base("RC.Deploy") { }
        /// <summary>
        /// 应用最后一个文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task ApplyLasetFileAsync([Required(ErrorMessage = "id不能为空")] Guid id) => await GetResultModelByPutAsync("ApplicationInfo/ApplyLasetFile", new Dictionary<string, string> { [nameof(id)] = id.ToString()});
        /// <summary>
        /// 应用文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task ApplyFileAsync([Required(ErrorMessage = "id不能为空")] Guid id, [Required(ErrorMessage = "fileName不能为空")] string fileName) => await GetResultModelByPutAsync("ApplicationInfo/ApplyFile", new Dictionary<string, string> { [nameof(id)] = id.ToString(), [nameof(fileName)] = fileName});
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task DeleteFileAsync([Required(ErrorMessage = "id不能为空")] Guid id, [Required(ErrorMessage = "fileName不能为空")] string fileName) => await GetResultModelByDeleteAsync("ApplicationInfo/DeleteFile", new Dictionary<string, string> { [nameof(id)] = id.ToString(), [nameof(fileName)] = fileName});
        /// <summary>
        /// 启动程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task StartAsync([Required(ErrorMessage = "id不能为空")] Guid id) => await GetResultModelByPostAsync("ApplicationInfo/Start", new Dictionary<string, string> { [nameof(id)] = id.ToString()});
        /// <summary>
        /// 停止程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task StopAsync([Required(ErrorMessage = "id不能为空")] Guid id) => await GetResultModelByPostAsync("ApplicationInfo/Stop", new Dictionary<string, string> { [nameof(id)] = id.ToString()});
        /// <summary>
        /// 杀死程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task KillAsync([Required(ErrorMessage = "id不能为空")] Guid id) => await GetResultModelByPostAsync("ApplicationInfo/Kill", new Dictionary<string, string> { [nameof(id)] = id.ToString()});
        /// <summary>
        /// 全部启动
        /// </summary>
        /// <returns></returns>
        public async Task StartAllAsync() => await GetResultModelByPostAsync("ApplicationInfo/StartAll");
        /// <summary>
        /// 全部停止
        /// </summary>
        /// <returns></returns>
        public async Task StopAllAsync() => await GetResultModelByPostAsync("ApplicationInfo/StopAll");
        /// <summary>
        /// 获得控制台信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ICollection<string>?> GetConsoleMessagesAsync([Required(ErrorMessage = "id不能为空")] Guid id) => await GetResultModelByGetAsync<ICollection<string>>("ApplicationInfo/GetConsoleMessages", new Dictionary<string, string> { [nameof(id)] = id.ToString()});
        /// <summary>
        /// 清空控制台信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task ClearConsoleMessagesAsync([Required(ErrorMessage = "id不能为空")] Guid id) => await GetResultModelByDeleteAsync("ApplicationInfo/ClearConsoleMessages", new Dictionary<string, string> { [nameof(id)] = id.ToString()});
        /// <summary>
        /// 获得上传文件列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<FileInfoDTO>?> GetUploadFilesAsync([Required(ErrorMessage = "id不能为空")] Guid id) => await GetResultModelByGetAsync<List<FileInfoDTO>>("ApplicationInfo/GetUploadFiles", new Dictionary<string, string> { [nameof(id)] = id.ToString()});
    }
}
