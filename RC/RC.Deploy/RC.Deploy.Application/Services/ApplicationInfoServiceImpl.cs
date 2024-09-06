using Microsoft.AspNetCore.Http;
using RC.Deploy.Abstractions.DTO.ApplicationInfo;
using RC.Deploy.Abstractions.Services.Models;
using RC.Deploy.Abstractions.Services.Models.ApplicationInfo;
using RC.Deploy.Application.Services.Models;

namespace RC.Deploy.Application.Services
{
    /// <summary>
    /// 应用程序信息服务
    /// </summary>
    public partial class ApplicationInfoServiceImpl(IServiceProvider serviceProvider, IOptionsMonitor<ApplicationConfig> config)
    {
        static ApplicationInfoServiceImpl()
        {
            if (MateralServices.Services == null) throw new RCException("获取DI容器失败");
            using IServiceScope scope = MateralServices.ServiceProvider.CreateScope();
            IServiceProvider services = scope.ServiceProvider;
            IApplicationInfoRepository applicationInfoRepository = services.GetRequiredService<IApplicationInfoRepository>();
            IOptionsMonitor<ApplicationConfig> config = services.GetRequiredService<IOptionsMonitor<ApplicationConfig>>();
            List<ApplicationInfo> allApplicationInfos = applicationInfoRepository.Find(m => true, m => m.Name, SortOrderEnum.Ascending);
            foreach (ApplicationInfo applicationInfo in allApplicationInfos)
            {
                ApplicationRuntimeModel model = new(services, applicationInfo, config);
                ApplicationRuntimeHost.ApplicationRuntimes.TryAdd(applicationInfo.ID, model);
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="RCException"></exception>
        public override async Task<Guid> AddAsync(AddApplicationInfoModel model)
        {
            if (await DefaultRepository.ExistedAsync(m => m.Name == model.Name)) throw new RCException("名称重复");
            if (await DefaultRepository.ExistedAsync(m => m.RootPath == model.RootPath)) throw new RCException("根路径重复");
            return await base.AddAsync(model);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override async Task<Guid> AddAsync(ApplicationInfo domain, AddApplicationInfoModel model)
        {
            Guid result = await base.AddAsync(domain, model);
            ApplicationRuntimeHost.ApplicationRuntimes.TryAdd(result, new ApplicationRuntimeModel(serviceProvider, domain, config));
            return result;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="RCException"></exception>
        public override async Task EditAsync(EditApplicationInfoModel model)
        {
            if (ApplicationRuntimeHost.ApplicationRuntimes[model.ID].ApplicationStatus != ApplicationStatusEnum.Stop) throw new RCException("应用程序尚未停止");
            await base.EditAsync(model);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="domainFromDB"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override async Task EditAsync(ApplicationInfo domainFromDB, EditApplicationInfoModel model)
        {
            await base.EditAsync(domainFromDB, model);
            ApplicationRuntimeHost.ApplicationRuntimes[model.ID].ApplicationInfo = domainFromDB;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task DeleteAsync([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            if (ApplicationRuntimeHost.ApplicationRuntimes[id].ApplicationStatus != ApplicationStatusEnum.Stop) throw new RCException("应用程序尚未停止");
            await base.DeleteAsync(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        protected override async Task DeleteAsync(ApplicationInfo domain)
        {
            await base.DeleteAsync(domain);
            ApplicationRuntimeHost.ApplicationRuntimes.TryRemove(domain.ID, out _);
        }
        /// <summary>
        /// 获得信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="RCException"></exception>
        public override Task<ApplicationInfoDTO> GetInfoAsync([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            if (!ApplicationRuntimeHost.ApplicationRuntimes.TryGetValue(id, out ApplicationRuntimeModel? value)) throw new RCException("应用程序不存在");
            ApplicationRuntimeModel application = value;
            ApplicationInfoDTO result = Mapper.Map<ApplicationInfoDTO>(application) ?? throw new RCException("映射失败");
            DirectoryInfo rarFilesDirectoryInfo = new(application.RarFilesDirectoryPath);
            if (rarFilesDirectoryInfo.Exists)
            {
                result.UploadFileNames = rarFilesDirectoryInfo.GetFiles().Select(m => m.Name).ToArray();
            }
            return Task.FromResult(result);
        }
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override Task<(List<ApplicationInfoListDTO> data, RangeModel rangeInfo)> GetListAsync(QueryApplicationInfoModel model)
        {
            List<ApplicationRuntimeModel> allApplications = ApplicationRuntimeHost.ApplicationRuntimes.Select(m => m.Value).ToList();
            if (model.ApplicationStatus != null)
            {
                allApplications = allApplications.Where(m => m.ApplicationStatus == model.ApplicationStatus.Value).ToList();
            }
            Guid[] targetApplicationIDs = allApplications.Select(m => m.ApplicationInfo).Where(model.GetSearchDelegate<ApplicationInfo>()).Select(m => m.ID).ToArray();
            allApplications =
            [
                .. allApplications.Where(m => targetApplicationIDs.Contains(m.ApplicationInfo.ID)).OrderBy(m => m.ApplicationInfo.MainModule),
            ];
            RangeModel pageInfo = new(model.SkipInt, model.TakeInt, allApplications.Count);
            allApplications = allApplications.Skip(model.SkipInt).Take(model.TakeInt).ToList();
            List<ApplicationInfoListDTO> result = Mapper.Map<List<ApplicationInfoListDTO>>(allApplications) ?? throw new RCException("映射失败");
            return Task.FromResult((result, pageInfo));
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="RCException"></exception>
        public async Task SaveFileAsync([Required(ErrorMessage = "唯一标识为空")] Guid id, [Required(ErrorMessage = "文件为空")] IFormFile file)
        {
            string fileName = file.FileName;
            if (string.IsNullOrWhiteSpace(fileName)) throw new RCException("未识别文件名");
            if (!Path.GetExtension(fileName).Equals(".rar", StringComparison.OrdinalIgnoreCase)) throw new RCException("只能上传.rar文件");
            if (!ApplicationRuntimeHost.ApplicationRuntimes.TryGetValue(id, out ApplicationRuntimeModel? value)) throw new RCException("应用程序信息不存在");
            ApplicationRuntimeModel applicationInfo = value;
            string directoryPath = applicationInfo.RarFilesDirectoryPath;
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string filePath = Path.Combine(directoryPath, fileName);
            if (File.Exists(filePath)) File.Delete(filePath);
            await using (FileStream fileStream = new(filePath, FileMode.CreateNew))
            {
                await file.CopyToAsync(fileStream);
            }
            applicationInfo.ExecuteApplyLatestFileTask();
        }
        /// <summary>
        /// 应用最后一个文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void ApplyLasetFile([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            if (!ApplicationRuntimeHost.ApplicationRuntimes.TryGetValue(id, out ApplicationRuntimeModel? value)) throw new RCException("应用程序信息不存在");
            ApplicationRuntimeModel applicationInfo = value;
            applicationInfo.ExecuteApplyLatestFileTask();
        }
        /// <summary>
        /// 应用文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public void ApplyFile([Required(ErrorMessage = "唯一标识为空")] Guid id, string fileName)
        {
            if (!ApplicationRuntimeHost.ApplicationRuntimes.TryGetValue(id, out ApplicationRuntimeModel? value)) throw new RCException("应用程序信息不存在");
            ApplicationRuntimeModel applicationInfo = value;
            applicationInfo.ExecuteApplyFileTask(fileName);
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public void DeleteFile([Required(ErrorMessage = "唯一标识为空")] Guid id, string fileName)
        {
            if (!ApplicationRuntimeHost.ApplicationRuntimes.TryGetValue(id, out ApplicationRuntimeModel? value)) throw new RCException("应用程序信息不存在");
            ApplicationRuntimeModel applicationInfo = value;
            FileInfo[]? fileInfos = applicationInfo.GetRarFileNames() ?? throw new RCException("文件不存在");
            FileInfo? fileInfo = fileInfos.FirstOrDefault(m => m.Name == fileName);
            if (fileInfo == null || !fileInfo.Exists) throw new RCException("文件不存在");
            try
            {
                fileInfo.Delete();
            }
            catch (Exception ex)
            {
                throw new RCException($"文件删除失败:{ex.Message}", ex);
            }
        }
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="id"></param>
        public void Start([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            if (!ApplicationRuntimeHost.ApplicationRuntimes.TryGetValue(id, out ApplicationRuntimeModel? value)) throw new RCException("应用程序信息不存在");
            value.ExecuteStartTask();
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="id"></param>
        public void Stop([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            if (!ApplicationRuntimeHost.ApplicationRuntimes.TryGetValue(id, out ApplicationRuntimeModel? value)) throw new RCException("应用程序信息不存在");
            value.ExecuteStopTask();
        }
        /// <summary>
        /// 杀死程序
        /// </summary>
        /// <param name="id"></param>
        public async Task KillAsync([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            if (!ApplicationRuntimeHost.ApplicationRuntimes.TryGetValue(id, out ApplicationRuntimeModel? value)) throw new RCException("应用程序信息不存在");
            await value.KillAsync();
        }
        /// <summary>
        /// 启动所有
        /// </summary>
        public void StartAll()
        {
            foreach (KeyValuePair<Guid, ApplicationRuntimeModel> item in ApplicationRuntimeHost.ApplicationRuntimes)
            {
                if (item.Value.ApplicationStatus != ApplicationStatusEnum.Stop) continue;
                item.Value.ExecuteStartTask();
            }
        }
        /// <summary>
        /// 停止所有
        /// </summary>
        public void StopAll()
        {
            foreach (KeyValuePair<Guid, ApplicationRuntimeModel> item in ApplicationRuntimeHost.ApplicationRuntimes)
            {
                if (item.Value.ApplicationStatus != ApplicationStatusEnum.Runing) continue;
                item.Value.ExecuteStopTask();
            }
        }
        /// <summary>
        /// 获得控制台消息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ICollection<string> GetConsoleMessages([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            if (!ApplicationRuntimeHost.ApplicationRuntimes.TryGetValue(id, out ApplicationRuntimeModel? value)) throw new RCException("应用程序信息不存在");
            return value.GetConsoleMessages();
        }
        /// <summary>
        /// 清空控制台消息
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="RCException"></exception>
        public void ClearConsoleMessages([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            if (!ApplicationRuntimeHost.ApplicationRuntimes.TryGetValue(id, out ApplicationRuntimeModel? value)) throw new RCException("应用程序信息不存在");
            value.ClearConsoleMessage();
        }
        /// <summary>
        /// 应用程序是否在运行
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool IsRunningApplication(string path)
        {
            ApplicationRuntimeModel? runtimeModel = ApplicationRuntimeHost.ApplicationRuntimes.Select(m => m.Value).FirstOrDefault(m => m.ApplicationInfo.RootPath.Equals(path, StringComparison.OrdinalIgnoreCase));
            if (runtimeModel == null) return false;
            return runtimeModel.ApplicationStatus == ApplicationStatusEnum.Runing;
        }
        /// <summary>
        /// 获得应用程序信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IApplicationRuntimeModel? GetApplicationRuntimeModel(string path)
        {
            ApplicationRuntimeModel? result = ApplicationRuntimeHost.ApplicationRuntimes.Select(m => m.Value).FirstOrDefault(m => m.ApplicationInfo.RootPath.Equals(path, StringComparison.OrdinalIgnoreCase));
            return result;
        }
        /// <summary>
        /// 获得上传文件列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="RCException"></exception>
        public List<FileInfoDTO> GetUploadFiles([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            if (!ApplicationRuntimeHost.ApplicationRuntimes.TryGetValue(id, out ApplicationRuntimeModel? value)) throw new RCException("应用程序信息不存在");
            ApplicationRuntimeModel application = value;
            FileInfo[]? fileInfos = application.GetRarFileNames();
            if (fileInfos == null) return [];
            List<FileInfoDTO> result = Mapper.Map<List<FileInfoDTO>>(fileInfos) ?? throw new RCException("映射失败");
            result = [.. result.OrderByDescending(m => m.LastWriteTime)];
            foreach (FileInfoDTO fileInfo in result)
            {
                fileInfo.DownloadUrl = $"/UploadFiles/{application.ApplicationInfo.Name}/{fileInfo.Name}";
            }
            return result;
        }
    }
}
