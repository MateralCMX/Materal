#nullable enable
using RC.Core.HttpClient;
using RC.Deploy.DataTransmitModel.ApplicationInfo;
using RC.Deploy.PresentationModel.ApplicationInfo;
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.Common.Utils.TreeHelper;
using Materal.BaseCore.Common.Utils.IndexHelper;
using Materal.BaseCore.PresentationModel;
using Materal.Utils.Model;

namespace RC.Deploy.HttpClient
{
    public partial class ApplicationInfoHttpClient : HttpClientBase<AddApplicationInfoRequestModel, EditApplicationInfoRequestModel, QueryApplicationInfoRequestModel, ApplicationInfoDTO, ApplicationInfoListDTO>
    {
        public ApplicationInfoHttpClient() : base("RC.Deploy") { }
        /// <summary>
        /// 启动程序
        /// </summary>
        /// <param name="id"></param>
        public async Task StartAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id) => await GetResultModelByPostAsync("ApplicationInfo/Start", new Dictionary<string, string> { [nameof(id)] = id.ToString()});
        /// <summary>
        /// 停止程序
        /// </summary>
        /// <param name="id"></param>
        public async Task StopAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id) => await GetResultModelByPostAsync("ApplicationInfo/Stop", new Dictionary<string, string> { [nameof(id)] = id.ToString()});
        /// <summary>
        /// 杀死程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task KillAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id) => await GetResultModelByPostAsync("ApplicationInfo/Kill", new Dictionary<string, string> { [nameof(id)] = id.ToString()});
        /// <summary>
        /// 获取控制台消息
        /// </summary>
        /// <param name="id"></param>
        public async Task<ICollection<string>?> GetConsoleMessagesAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id) => await GetResultModelByGetAsync<ICollection<string>>("ApplicationInfo/GetConsoleMessages", new Dictionary<string, string> { [nameof(id)] = id.ToString()});
        /// <summary>
        /// 清空控制台消息
        /// </summary>
        /// <param name="id"></param>
        public async Task ClearConsoleMessagesAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id) => await GetResultModelByDeleteAsync("ApplicationInfo/ClearConsoleMessages", new Dictionary<string, string> { [nameof(id)] = id.ToString()});
        /// <summary>
        /// 启动所有程序
        /// </summary>
        public async Task StartAllAsync() => await GetResultModelByPostAsync("ApplicationInfo/StartAll");
        /// <summary>
        /// 停止所有程序
        /// </summary>
        public async Task StopAllAsync() => await GetResultModelByPostAsync("ApplicationInfo/StopAll");
        /// <summary>
        /// 应用最后一个文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task ApplyLasetFileAsync([Required(ErrorMessage = "唯一标识为空")] Guid id) => await GetResultModelByPutAsync("ApplicationInfo/ApplyLasetFile", new Dictionary<string, string> { [nameof(id)] = id.ToString()});
        /// <summary>
        /// 应用文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task ApplyFileAsync([Required(ErrorMessage = "唯一标识为空")] Guid id,  string fileName) => await GetResultModelByPutAsync("ApplicationInfo/ApplyFile", new Dictionary<string, string> { [nameof(id)] = id.ToString(), [nameof(fileName)] = fileName});
    }
}
