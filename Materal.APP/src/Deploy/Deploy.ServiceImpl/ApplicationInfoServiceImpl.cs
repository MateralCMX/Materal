using AutoMapper;
using Deploy.Common;
using Deploy.DataTransmitModel.ApplicationInfo;
using Deploy.Domain;
using Deploy.Domain.Repositories;
using Deploy.Enums;
using Deploy.ServiceImpl.Manage;
using Deploy.ServiceImpl.Models;
using Deploy.Services;
using Deploy.Services.Models.ApplicationInfo;
using Deploy.SqliteEFRepository;
using Materal.ConvertHelper;
using Materal.WindowsHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Deploy.ServiceImpl
{
    public class ApplicationInfoServiceImpl : IApplicationInfoService
    {
        private readonly ILogger<ApplicationInfoServiceImpl> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationManage _applicationManage;
        private readonly IDeploySqliteEFUnitOfWork _deploySqliteEFUnitOfWork;
        private readonly IApplicationInfoRepository _applicationInfoRepository;

        public ApplicationInfoServiceImpl(ILogger<ApplicationInfoServiceImpl> logger, IMapper mapper, IDeploySqliteEFUnitOfWork deploySqliteEFUnitOfWork, IApplicationInfoRepository applicationInfoRepository, ApplicationManage applicationManage)
        {
            _logger = logger;
            _mapper = mapper;
            _deploySqliteEFUnitOfWork = deploySqliteEFUnitOfWork;
            _applicationInfoRepository = applicationInfoRepository;
            _applicationManage = applicationManage;
        }
        public async Task AddAsync(AddApplicationInfoModel model)
        {
            if (DeployConfig.ApplicationNameWhiteList.Any(m => model.Name.Equals(m, StringComparison.OrdinalIgnoreCase))) throw new DeployException("名称非法");
            if (await _applicationInfoRepository.ExistedAsync(m => m.Name == model.Name && m.ApplicationType == model.ApplicationType))
            {
                throw new DeployException("名称重复");
            }
            var applicationInfo = model.CopyProperties<ApplicationInfo>();
            applicationInfo.ID = Guid.NewGuid();
            _deploySqliteEFUnitOfWork.RegisterAdd(applicationInfo);
            _applicationManage.Add(applicationInfo);
            try
            {
                await _deploySqliteEFUnitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                _applicationManage.Delete(applicationInfo.ID);
                throw;
            }
        }

        public async Task EditAsync(EditApplicationInfoModel model)
        {
            if (await _applicationInfoRepository.ExistedAsync(m => m.ID != model.ID && m.Name == model.Name && m.ApplicationType == model.ApplicationType))
            {
                throw new DeployException("名称重复");
            }
            ApplicationInfo applicationInfoFromDB = await _applicationInfoRepository.FirstOrDefaultAsync(m => m.ID == model.ID);
            if (applicationInfoFromDB == null) throw new DeployException("应用程序不存在");
            string oldName = applicationInfoFromDB.Name;
            model.CopyProperties(applicationInfoFromDB);
            applicationInfoFromDB.UpdateTime = DateTime.Now;
            _deploySqliteEFUnitOfWork.RegisterEdit(applicationInfoFromDB);
            _applicationManage.Edit(applicationInfoFromDB);
            string oldDirectoryPath = GetDirectoryPathByApplicationInfo(oldName);
            string newDirectoryPath = GetDirectoryPathByApplicationInfo(applicationInfoFromDB.Name);
            if (oldName != applicationInfoFromDB.Name)
            {
                if (Directory.Exists(newDirectoryPath)) throw new DeployException("应用程序目录已存在");
                if (Directory.Exists(oldDirectoryPath))
                {
                    Directory.Move(oldDirectoryPath, newDirectoryPath);
                }
            }
            try
            {
                await _deploySqliteEFUnitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                if (oldName != applicationInfoFromDB.Name)
                {
                    Directory.Move(newDirectoryPath, oldDirectoryPath);
                }
                throw;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            ApplicationInfo applicationInfoFromDB = await _applicationInfoRepository.FirstOrDefaultAsync(m => m.ID == id);
            if (applicationInfoFromDB == null) throw new DeployException("应用程序不存在");
            _deploySqliteEFUnitOfWork.RegisterDelete(applicationInfoFromDB);
            _applicationManage.Delete(id);
            string deleteDirectoryPath = GetDirectoryPathByApplicationInfo(applicationInfoFromDB.Name);
            string deleteDirectoryTempPath = deleteDirectoryPath + $"_{Guid.NewGuid()}";
            if (Directory.Exists(deleteDirectoryPath))
            {
                Directory.Move(deleteDirectoryPath, deleteDirectoryTempPath);
            }
            try
            {
                await _deploySqliteEFUnitOfWork.CommitAsync();
                Directory.Delete(deleteDirectoryTempPath);
            }
            catch (Exception)
            {
                _applicationManage.Add(applicationInfoFromDB);
                throw;
            }
        }

        public ApplicationInfoDTO GetInfo(Guid id)
        {
            List<ApplicationInfoDTO> allApplicationInfoModes = _applicationManage.GetAllList();
            ApplicationInfoDTO applicationInfo = allApplicationInfoModes.FirstOrDefault(m => m.ID == id);
            if (applicationInfo == null) throw new DeployException("应用程序不存在");
            return applicationInfo;
        }

        public List<ApplicationInfoListDTO> GetList(QueryApplicationInfoFilterModel model)
        {
            List<ApplicationInfoDTO> allApplicationInfoModes = _applicationManage.GetAllList();
            Expression<Func<ApplicationInfoDTO, bool>> searchExpression = model.GetSearchExpression<ApplicationInfoDTO>();
            List<ApplicationInfoDTO> applicationInfoList = allApplicationInfoModes.Where(searchExpression.Compile()).ToList();
            var result = _mapper.Map<List<ApplicationInfoListDTO>>(applicationInfoList);
            return result;
        }

        public void Start(Guid id)
        {
            _applicationManage.Start(id);
        }

        public void Stop(Guid id)
        {
            _applicationManage.Stop(id);
        }

        public ICollection<string> GetConsoleMessage(Guid id)
        {
            ICollection<string> consoleMessages = _applicationManage.GetConsoleMessage(id);
            return consoleMessages;
        }

        public void StartAll()
        {
            _applicationManage.StartAll();
        }

        public void StopAll()
        {
            _applicationManage.StopAll();
        }

        public async Task SaveFileAsync(IFormFile file, Guid id)
        {
            ApplicationInfoDTO applicationInfo = GetInfo(id);
            if(applicationInfo.Status != ApplicationStatusEnum.Stop) throw new DeployException("应用程序尚未停止");
            applicationInfo.Status = ApplicationStatusEnum.Release;
            string fileName = file.FileName;
            if (string.IsNullOrWhiteSpace(fileName)) throw new DeployException("未识别文件名");
            if (!Path.GetExtension(fileName).Equals(".rar", StringComparison.OrdinalIgnoreCase)) throw new DeployException("只能上传.rar文件");
            string directoryPath = GetDirectoryPathByApplicationInfo(applicationInfo.Name);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string filePath = Path.Combine(directoryPath, fileName);
            if (File.Exists(filePath)) File.Delete(filePath);
            await using var fileStream = new FileStream(filePath, FileMode.CreateNew);
            await file.CopyToAsync(fileStream);
            fileStream.Close();
            UnRarFile(filePath, applicationInfo);
            applicationInfo.Status = ApplicationStatusEnum.Stop;
        }

        public List<string> GetUploadFileList(Guid id)
        {
            ApplicationInfoDTO applicationInfo = GetInfo(id);
            string directoryPath = GetDirectoryPathByApplicationInfo(applicationInfo.Name);
            if (!Directory.Exists(directoryPath)) return new List<string>();
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            List<string> result = fileInfos.OrderByDescending(m => m.CreationTime).Select(m => m.Name).ToList();
            return result;
        }

        public void DeleteUploadFile(Guid id, string fileName)
        {
            ApplicationInfoDTO applicationInfo = GetInfo(id);
            string directoryPath = GetDirectoryPathByApplicationInfo(applicationInfo.Name);
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            FileInfo fileInfo = fileInfos.FirstOrDefault(m => m.Name == fileName);
            if (fileInfo == null) throw new DeployException("文件不存在");
            try
            {
                fileInfo.Delete();
            }
            catch (Exception exception)
            {
                throw new DeployException("文件删除失败", exception);
            }
        }

        public void ClearTempFile()
        {
            string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadFiles");
            if (!Directory.Exists(directoryPath))
            {
                return;
            }
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
            foreach (DirectoryInfo tempDirectoryInfo in directoryInfos)
            {
                tempDirectoryInfo.Delete(true);
            }
        }

        public void ClearInactiveApplication()
        {
            string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Application");
            if (!Directory.Exists(directoryPath)) return;
            var directoryInfo = new DirectoryInfo(directoryPath);
            foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
            {
                ApplicationRuntimeModel runtimeModel = _applicationManage.GetRuntimeModelByPath(directory.Name);
                if (runtimeModel == null)
                {
                    directory.Delete(true);
                }
            }
        }

        public bool IsRunningApplication(string path)
        {
            ApplicationRuntimeModel runtimeModel = _applicationManage.GetRuntimeModelByPath(path);
            if (runtimeModel == null) return false;
            return runtimeModel.Status == ApplicationStatusEnum.Running;
        }

        #region 私有方法

        private string GetDirectoryPathByApplicationInfo(string applicationName)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadFiles", applicationName);
        }
        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="rarFilePath"></param>
        /// <param name="applicationInfo"></param>
        /// <returns></returns>
        private void UnRarFile(string rarFilePath, ApplicationInfoDTO applicationInfo)
        {
            string fileName = Path.GetFileNameWithoutExtension(rarFilePath);
            string directoryPath = Path.Combine(GetDirectoryPathByApplicationInfo(applicationInfo.Name), fileName);
            if (Directory.Exists(directoryPath)) Directory.Delete(directoryPath, true);
            DirectoryInfo directoryInfo = Directory.CreateDirectory(directoryPath);
            var winRarPath = Path.Combine(DeployConfig.WinRarPath, "UnRaR.exe");
            string directoryTempPath = $"x -o+ -y \"{rarFilePath}\" \"{directoryPath}\"";
            ProcessStartInfo processStartInfo = ProcessManager.GetProcessStartInfo(winRarPath, directoryTempPath);
            _logger.LogDebug(directoryTempPath);
            var process = new Process { StartInfo = processStartInfo };
            void DataHandler(object sender, DataReceivedEventArgs e)
            {
                if (string.IsNullOrWhiteSpace(e.Data)) return;
                _logger.LogInformation(e.Data);
            }
            process.OutputDataReceived += DataHandler;
            process.ErrorDataReceived += DataHandler;
            if (process.Start())
            {
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
                process.Dispose();
            }
            try
            {
                foreach (DirectoryInfo item in directoryInfo.GetDirectories())
                {
                    CopyApplication(item);
                }
            }
            finally
            {
                directoryInfo.Delete(true);
            }
        }
        /// <summary>
        /// 复制应用程序
        /// </summary>
        /// <param name="directoryInfo"></param>
        private void CopyApplication(DirectoryInfo directoryInfo)
        {
            string targetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Application", directoryInfo.Name);
            ApplicationRuntimeModel runtimeModel = _applicationManage.GetRuntimeModelByPath(directoryInfo.Name);
            if(runtimeModel == null) throw new DeployException("应用序程不存在");
            CopyDirectory(directoryInfo, targetPath);
        }
        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="targetPath"></param>
        private void CopyDirectory(DirectoryInfo directoryInfo, string targetPath)
        {
            if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
            #region CopyFile
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            foreach (FileInfo fileInfo in fileInfos)
            {
                string filePath = Path.Combine(targetPath, fileInfo.Name);
                _logger.LogInformation($"开始复制文件\"{fileInfo.FullName}\"到\"{filePath}\"");
                if (File.Exists(filePath)) File.Delete(filePath);
                File.Move(fileInfo.FullName, filePath);
            }
            #endregion
            #region CopyChildDirectory
            DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
            foreach (DirectoryInfo info in directoryInfos)
            {
                string dirPath = Path.Combine(targetPath, info.Name);
                if (Directory.Exists(dirPath)) Directory.Delete(dirPath, true);
                Directory.Move(info.FullName, dirPath);
            }
            #endregion
        }
        #endregion
    }
}
