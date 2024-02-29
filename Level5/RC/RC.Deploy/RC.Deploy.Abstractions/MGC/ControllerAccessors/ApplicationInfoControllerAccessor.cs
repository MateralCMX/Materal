using Microsoft.AspNetCore.Http;
using RC.Deploy.Abstractions.DTO.ApplicationInfo;
using RC.Deploy.Abstractions.RequestModel.ApplicationInfo;

namespace RC.Deploy.Abstractions.ControllerAccessors
{
    /// <summary>
    /// 应用程序控制器访问器
    /// </summary>
    public partial class ApplicationInfoControllerAccessor(IServiceProvider serviceProvider) : BaseControllerAccessor<IApplicationInfoController, AddApplicationInfoRequestModel, EditApplicationInfoRequestModel, QueryApplicationInfoRequestModel, ApplicationInfoDTO, ApplicationInfoListDTO>(serviceProvider), IApplicationInfoController
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public override string ProjectName => "RC";
        /// <summary>
        /// 模块名称
        /// </summary>
        public override string ModuleName => "Deploy";
        /// <summary>
        /// 上传新文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<ResultModel> UploadNewFileAsync(Guid id, IFormFile file)
            => await HttpHelper.SendAsync<IApplicationInfoController, ResultModel>(ProjectName, ModuleName, nameof(UploadNewFileAsync), new() {[nameof(id)] = id.ToString()}, file);
        /// <summary>
        /// 获得控制台消息数量
        /// </summary>
        /// <returns></returns>
        public ResultModel<int> GetMaxConsoleMessageCount()
            => HttpHelper.SendAsync<IApplicationInfoController, ResultModel<int>>(ProjectName, ModuleName, nameof(GetMaxConsoleMessageCount), []).Result;
        /// <summary>
        /// 应用最后一个文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultModel ApplyLasetFile(Guid id)
            => HttpHelper.SendAsync<IApplicationInfoController, ResultModel>(ProjectName, ModuleName, nameof(ApplyLasetFile), new() {[nameof(id)] = id.ToString()}).Result;
        /// <summary>
        /// 应用文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ResultModel ApplyFile(Guid id, string fileName)
            => HttpHelper.SendAsync<IApplicationInfoController, ResultModel>(ProjectName, ModuleName, nameof(ApplyFile), new() {[nameof(id)] = id.ToString(), [nameof(fileName)] = fileName}).Result;
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ResultModel DeleteFile(Guid id, string fileName)
            => HttpHelper.SendAsync<IApplicationInfoController, ResultModel>(ProjectName, ModuleName, nameof(DeleteFile), new() {[nameof(id)] = id.ToString(), [nameof(fileName)] = fileName}).Result;
        /// <summary>
        /// 启动程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultModel Start(Guid id)
            => HttpHelper.SendAsync<IApplicationInfoController, ResultModel>(ProjectName, ModuleName, nameof(Start), new() {[nameof(id)] = id.ToString()}).Result;
        /// <summary>
        /// 停止程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultModel Stop(Guid id)
            => HttpHelper.SendAsync<IApplicationInfoController, ResultModel>(ProjectName, ModuleName, nameof(Stop), new() {[nameof(id)] = id.ToString()}).Result;
        /// <summary>
        /// 杀死程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResultModel> KillAsync(Guid id)
            => await HttpHelper.SendAsync<IApplicationInfoController, ResultModel>(ProjectName, ModuleName, nameof(KillAsync), new() {[nameof(id)] = id.ToString()});
        /// <summary>
        /// 全部启动
        /// </summary>
        /// <returns></returns>
        public ResultModel StartAll()
            => HttpHelper.SendAsync<IApplicationInfoController, ResultModel>(ProjectName, ModuleName, nameof(StartAll), []).Result;
        /// <summary>
        /// 全部停止
        /// </summary>
        /// <returns></returns>
        public ResultModel StopAll()
            => HttpHelper.SendAsync<IApplicationInfoController, ResultModel>(ProjectName, ModuleName, nameof(StopAll), []).Result;
        /// <summary>
        /// 获得控制台信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultModel<ICollection<string>> GetConsoleMessages(Guid id)
            => HttpHelper.SendAsync<IApplicationInfoController, ResultModel<ICollection<string>>>(ProjectName, ModuleName, nameof(GetConsoleMessages), new() {[nameof(id)] = id.ToString()}).Result;
        /// <summary>
        /// 清空控制台信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultModel ClearConsoleMessages(Guid id)
            => HttpHelper.SendAsync<IApplicationInfoController, ResultModel>(ProjectName, ModuleName, nameof(ClearConsoleMessages), new() {[nameof(id)] = id.ToString()}).Result;
        /// <summary>
        /// 获得上传文件列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultModel<List<FileInfoDTO>> GetUploadFiles(Guid id)
            => HttpHelper.SendAsync<IApplicationInfoController, ResultModel<List<FileInfoDTO>>>(ProjectName, ModuleName, nameof(GetUploadFiles), new() {[nameof(id)] = id.ToString()}).Result;
    }
}
