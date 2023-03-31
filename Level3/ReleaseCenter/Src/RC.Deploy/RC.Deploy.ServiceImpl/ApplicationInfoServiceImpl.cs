using Materal.Abstractions;
using Materal.BaseCore.EFRepository;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Http;
using RC.Core.Common;
using RC.Deploy.DataTransmitModel.ApplicationInfo;
using RC.Deploy.Domain;
using RC.Deploy.Domain.Repositories;
using RC.Deploy.Enums;
using RC.Deploy.ServiceImpl.Models;
using RC.Deploy.Services.Models.ApplicationInfo;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace RC.Deploy.ServiceImpl
{
    public partial class ApplicationInfoServiceImpl
    {
        static ApplicationInfoServiceImpl()
        {
            IMateralCoreUnitOfWork unitOfWork = MateralServices.GetService<IMateralCoreUnitOfWork>();
            IApplicationInfoRepository applicationInfoRepository = unitOfWork.GetRepository<IApplicationInfoRepository>();
            List<ApplicationInfo> allApplicationInfos = applicationInfoRepository.Find(m => true, m => m.Name, SortOrder.Ascending);
            foreach (ApplicationInfo applicationInfo in allApplicationInfos)
            {
                ApplicationRuntimeModel model = new(applicationInfo);
                ApplicationRuntimeManage.ApplicationRuntimes.TryAdd(applicationInfo.ID, model);
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
            ApplicationRuntimeManage.ApplicationRuntimes.TryAdd(result, new ApplicationRuntimeModel(domain));
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
            if (ApplicationRuntimeManage.ApplicationRuntimes[model.ID].ApplicationStatus != ApplicationStatusEnum.Stop) throw new RCException("应用程序尚未停止");
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
            ApplicationRuntimeManage.ApplicationRuntimes[model.ID].ApplicationInfo = domainFromDB;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task DeleteAsync([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            if (ApplicationRuntimeManage.ApplicationRuntimes[id].ApplicationStatus != ApplicationStatusEnum.Stop) throw new RCException("应用程序尚未停止");
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
            ApplicationRuntimeManage.ApplicationRuntimes.TryRemove(domain.ID, out _);
        }
        /// <summary>
        /// 获得信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="RCException"></exception>
        public override Task<ApplicationInfoDTO> GetInfoAsync([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            if (!ApplicationRuntimeManage.ApplicationRuntimes.ContainsKey(id)) throw new RCException("应用程序不存在");
            ApplicationRuntimeModel application = ApplicationRuntimeManage.ApplicationRuntimes[id];
            ApplicationInfoDTO result = Mapper.Map<ApplicationInfoDTO>(application);
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
        public override Task<(List<ApplicationInfoListDTO> data, PageModel pageInfo)> GetListAsync(QueryApplicationInfoModel model)
        {
            List<ApplicationRuntimeModel> allApplications = ApplicationRuntimeManage.ApplicationRuntimes.Select(m => m.Value).ToList();
            if (model.ApplicationStatus != null)
            {
                allApplications = allApplications.Where(m => m.ApplicationStatus == model.ApplicationStatus.Value).ToList();
            }
            Guid[] targetApplicationIDs = allApplications.Select(m => m.ApplicationInfo).Where(model.GetSearchDelegate<ApplicationInfo>()).Select(m => m.ID).ToArray();
            allApplications = allApplications.Where(m => targetApplicationIDs.Contains(m.ApplicationInfo.ID)).OrderBy(m => m.ApplicationInfo.MainModule).ToList();
            PageModel pageInfo = new(model, allApplications.Count);
            allApplications = allApplications.Skip(model.Skip).Take(model.Take).ToList();
            List<ApplicationInfoListDTO> result = Mapper.Map<List<ApplicationInfoListDTO>>(allApplications);
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
            if (!ApplicationRuntimeManage.ApplicationRuntimes.ContainsKey(id)) throw new RCException("应用程序信息不存在");
            ApplicationRuntimeModel applicationInfo = ApplicationRuntimeManage.ApplicationRuntimes[id];
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
            if (!ApplicationRuntimeManage.ApplicationRuntimes.ContainsKey(id)) throw new RCException("应用程序信息不存在");
            ApplicationRuntimeModel applicationInfo = ApplicationRuntimeManage.ApplicationRuntimes[id];
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
            if (!ApplicationRuntimeManage.ApplicationRuntimes.ContainsKey(id)) throw new RCException("应用程序信息不存在");
            ApplicationRuntimeModel applicationInfo = ApplicationRuntimeManage.ApplicationRuntimes[id];
            applicationInfo.ExecuteApplyFileTask(fileName);
        }
        /// <summary>
        /// 应用文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public void DeleteFile([Required(ErrorMessage = "唯一标识为空")] Guid id, string fileName)
        {
            if (!ApplicationRuntimeManage.ApplicationRuntimes.ContainsKey(id)) throw new RCException("应用程序信息不存在");
            ApplicationRuntimeModel applicationInfo = ApplicationRuntimeManage.ApplicationRuntimes[id];
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
        /// <exception cref="NotImplementedException"></exception>
        public void Start([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            if (!ApplicationRuntimeManage.ApplicationRuntimes.ContainsKey(id)) throw new RCException("应用程序信息不存在");
            ApplicationRuntimeManage.ApplicationRuntimes[id].ExecuteStartTask();
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Stop([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            if (!ApplicationRuntimeManage.ApplicationRuntimes.ContainsKey(id)) throw new RCException("应用程序信息不存在");
            ApplicationRuntimeManage.ApplicationRuntimes[id].ExecuteStopTask();
        }
        /// <summary>
        /// 杀死程序
        /// </summary>
        /// <param name="id"></param>
        public void Kill([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            if (!ApplicationRuntimeManage.ApplicationRuntimes.ContainsKey(id)) throw new RCException("应用程序信息不存在");
            ApplicationRuntimeManage.ApplicationRuntimes[id].Kill();
        }
        /// <summary>
        /// 启动所有
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void StartAll()
        {
            foreach (KeyValuePair<Guid, ApplicationRuntimeModel> item in ApplicationRuntimeManage.ApplicationRuntimes)
            {
                if (item.Value.ApplicationStatus != ApplicationStatusEnum.Stop) continue;
                item.Value.ExecuteStartTask();
            }
        }
        /// <summary>
        /// 停止所有
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void StopAll()
        {
            foreach (KeyValuePair<Guid, ApplicationRuntimeModel> item in ApplicationRuntimeManage.ApplicationRuntimes)
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
        /// <exception cref="NotImplementedException"></exception>
        public ICollection<string> GetConsoleMessages([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            if (!ApplicationRuntimeManage.ApplicationRuntimes.ContainsKey(id)) throw new RCException("应用程序信息不存在");
            return ApplicationRuntimeManage.ApplicationRuntimes[id].GetConsoleMessages();
        }
        /// <summary>
        /// 清空控制台消息
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="RCException"></exception>
        public void ClearConsoleMessages([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            if (!ApplicationRuntimeManage.ApplicationRuntimes.ContainsKey(id)) throw new RCException("应用程序信息不存在");
            ApplicationRuntimeManage.ApplicationRuntimes[id].ClearConsoleMessage();
        }
        /// <summary>
        /// 应用程序是否在运行
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool IsRunningApplication(string path)
        {
            ApplicationRuntimeModel? runtimeModel = ApplicationRuntimeManage.ApplicationRuntimes.Select(m => m.Value).FirstOrDefault(m => m.ApplicationInfo.RootPath.Equals(path, StringComparison.OrdinalIgnoreCase));
            if (runtimeModel == null) return false;
            return runtimeModel.ApplicationStatus == ApplicationStatusEnum.Runing;
        }
        /// <summary>
        /// 获得上传文件列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="RCException"></exception>
        public List<FileInfoDTO> GetUploadFiles([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            if (!ApplicationRuntimeManage.ApplicationRuntimes.ContainsKey(id)) throw new RCException("应用程序信息不存在");
            ApplicationRuntimeModel application = ApplicationRuntimeManage.ApplicationRuntimes[id];
            FileInfo[]? fileInfos = application.GetRarFileNames();
            if (fileInfos == null) return new();
            List<FileInfoDTO> result = Mapper.Map<List<FileInfoDTO>>(fileInfos);
            result = result.OrderByDescending(m => m.LastWriteTime).ToList();
            foreach (FileInfoDTO fileInfo in result)
            {
                fileInfo.DownloadUrl = $"/UploadFiles/{application.ApplicationInfo.Name}/{fileInfo.Name}";
            }
            return result;
        }
    }
}
