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
        private readonly IMapper _mapper;
        private readonly IApplicationManage _applicationManage;
        private readonly IDeploySqliteEFUnitOfWork _deploySqliteEFUnitOfWork;
        private readonly IApplicationInfoRepository _applicationInfoRepository;

        public ApplicationInfoServiceImpl(IMapper mapper, IApplicationManage applicationManage, IDeploySqliteEFUnitOfWork deploySqliteEFUnitOfWork, IApplicationInfoRepository applicationInfoRepository)
        {
            _mapper = mapper;
            _applicationManage = applicationManage;
            _deploySqliteEFUnitOfWork = deploySqliteEFUnitOfWork;
            _applicationInfoRepository = applicationInfoRepository;
        }

        public async Task AddAsync(AddApplicationInfoModel model)
        {
            if(DeployConfig.ApplicationNameWhiteList.Any(m=>model.Name.Equals(m, StringComparison.OrdinalIgnoreCase))) throw new DeployException("名称非法");
            if (await _applicationInfoRepository.ExistedAsync(m => m.Name == model.Name && m.ApplicationType == model.ApplicationType))
            {
                throw new DeployException("名称重复");
            }
            var applicationInfo = model.CopyProperties<ApplicationInfo>();
            applicationInfo.ID = Guid.NewGuid();
            _deploySqliteEFUnitOfWork.RegisterAdd(applicationInfo);
            var applicationInfoModel = _mapper.Map<ApplicationInfoModel>(applicationInfo);
            _applicationManage.Add(applicationInfoModel);
            try
            {
                await _deploySqliteEFUnitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                _applicationManage.Delete(applicationInfoModel.ID);
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
            model.CopyProperties(applicationInfoFromDB);
            applicationInfoFromDB.UpdateTime = DateTime.Now;
            _deploySqliteEFUnitOfWork.RegisterEdit(applicationInfoFromDB);
            var applicationInfoModel = _mapper.Map<ApplicationInfoModel>(applicationInfoFromDB);
            _applicationManage.Edit(applicationInfoModel);
            await _deploySqliteEFUnitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            ApplicationInfo applicationInfoFromDB = await _applicationInfoRepository.FirstOrDefaultAsync(m => m.ID == id);
            if (applicationInfoFromDB == null) throw new DeployException("应用程序不存在");
            _deploySqliteEFUnitOfWork.RegisterDelete(applicationInfoFromDB);
            ApplicationInfoModel applicationInfoModel = _applicationManage.Delete(id);
            try
            {
                await _deploySqliteEFUnitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                _applicationManage.Add(applicationInfoModel);
                throw;
            }
        }

        public ApplicationInfoDTO GetInfo(Guid id)
        {
            List<ApplicationInfoModel> allApplicationInfoModes = _applicationManage.GetAllList();
            ApplicationInfoModel applicationInfo = allApplicationInfoModes.FirstOrDefault(m => m.ID == id);
            if (applicationInfo == null) throw new DeployException("应用程序不存在");
            var result = _mapper.Map<ApplicationInfoDTO>(applicationInfo);
            return result;
        }

        public List<ApplicationInfoListDTO> GetList(QueryApplicationInfoFilterModel model)
        {
            List<ApplicationInfoModel> allApplicationInfoModes = _applicationManage.GetAllList();
            Expression<Func<ApplicationInfoModel, bool>> searchExpression = model.GetSearchExpression<ApplicationInfoModel>();
            List<ApplicationInfoModel> applicationInfoList = allApplicationInfoModes.Where(searchExpression.Compile()).ToList();
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

        public async Task SaveFileAsync(IFormFile file)
        {
            string fileName = file.FileName;
            if(string.IsNullOrWhiteSpace(fileName)) throw new DeployException("未识别文件名");
            if (!Path.GetExtension(fileName).Equals(".rar", StringComparison.OrdinalIgnoreCase)) throw new DeployException("只能上传.rar文件");
            string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadFiles");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string filePath = Path.Combine(directoryPath, fileName);
            if (File.Exists(filePath)) File.Delete(filePath);
            var fileStream = new FileStream(filePath, FileMode.CreateNew);
            await file.CopyToAsync(fileStream);
            fileStream.Close();
            fileStream.Dispose();
            UnRarFile(filePath);
        }

        public void ClearUpdateFiles()
        {
            string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadFiles");
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
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

        public bool IsRuningApplication(string path)
        {
            ApplicationRuntimeModel runtimeModel = _applicationManage.GetRuntimeModelByPath(path);
            if (runtimeModel == null) return false;
            return runtimeModel.ApplicationStatus == ApplicationStatusEnum.Runing;
        }

        #region 私有方法
        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="rarFilePath"></param>
        /// <returns></returns>
        private void UnRarFile(string rarFilePath)
        {
            string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadFiles", "Temp", Guid.NewGuid().ToString());
            if (Directory.Exists(directoryPath)) throw new DeployException("解压失败,请重新上传");
            DirectoryInfo directoryInfo = Directory.CreateDirectory(directoryPath);
            ProcessStartInfo processStartInfo = ProcessManager.GetProcessStartInfo("UnRAR.exe", $"x -o+ -y {rarFilePath} {directoryPath}");
            var process = new Process { StartInfo = processStartInfo };
            void DataHandler(object sender, DataReceivedEventArgs e)
            {
                DeployConsoleHelper.WriteLine(e.Data);
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
            foreach (DirectoryInfo item in directoryInfo.GetDirectories())
            {
                CopyApplication(item);
            }
            directoryInfo.Delete(true);
        }
        /// <summary>
        /// 复制应用程序
        /// </summary>
        /// <param name="directoryInfo"></param>
        private void CopyApplication(DirectoryInfo directoryInfo)
        {
            string targetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Application", directoryInfo.Name);
            ApplicationRuntimeModel runtimeModel = _applicationManage.GetRuntimeModelByPath(directoryInfo.Name);
            if (runtimeModel != null && runtimeModel.ApplicationInfo.ApplicationType != ApplicationTypeEnum.StaticDocument)
            {
                runtimeModel.Stop();
                if (runtimeModel.ApplicationStatus == ApplicationStatusEnum.Stop)
                {
                    CopyDirectory(directoryInfo, targetPath);
                }
                else
                {
                    throw new DeployException("应用程序关闭失败");
                }
            }
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
