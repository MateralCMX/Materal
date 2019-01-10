using Materal.ConfigurationHelper;
using Materal.FileHelper;
using Materal.WindowsHelper;
using Materal.WPFCommon;
using Materal.WPFUI.Tools.NuGetTools.Model;
using System.Collections.Generic;
using System.IO;

namespace Materal.WPFUI.Tools.NuGetTools
{
    public sealed class NuGetToolViewModel : NotifyPropertyChanged
    {
        private NuGetToolsConfigTemplateModel _selectedConfigTemplateModel;
        private string _projectAddress;
        private string _targetAddress;
        private bool _debug;
        private bool _release;
        private bool _nuGet;
        private bool _dll;
        private bool _openExplorer = true;

        /// <summary>
        /// 配置模版模型组
        /// </summary>
        public List<NuGetToolsConfigTemplateModel> ConfigTemplateModels { get; set; } = new List<NuGetToolsConfigTemplateModel>();

        /// <summary>
        /// 当前选择的配置模板模型
        /// </summary>
        public NuGetToolsConfigTemplateModel SelectedConfigTemplateModel
        {
            get => _selectedConfigTemplateModel;
            set
            {
                _selectedConfigTemplateModel = value;
                if (_selectedConfigTemplateModel == null) return;
                ProjectAddress = _selectedConfigTemplateModel.ProjectAddress;
                TargetAddress = _selectedConfigTemplateModel.TargetAddress;
                Debug = _selectedConfigTemplateModel.Debug;
                Release = _selectedConfigTemplateModel.Release;
                NuGet = _selectedConfigTemplateModel.NuGet;
                DLL = _selectedConfigTemplateModel.DLL;
            }
        }

        /// <summary>
        /// 项目地址
        /// </summary>
        public string ProjectAddress
        {
            get => _projectAddress;
            set
            {
                _projectAddress = value;
                OnPropertyChanged(nameof(ProjectAddress));
            }
        }

        /// <summary>
        /// 目标地址
        /// </summary>
        public string TargetAddress
        {
            get => _targetAddress;
            set
            {
                _targetAddress = value;
                OnPropertyChanged(nameof(TargetAddress));
            }
        }

        /// <summary>
        /// 采用Debug版本标识
        /// </summary>
        public bool Debug
        {
            get => _debug;
            set
            {
                _debug = value;
                OnPropertyChanged(nameof(Debug));
            }
        }

        /// <summary>
        /// 采用Release版本标识
        /// </summary>
        public bool Release
        {
            get => _release;
            set
            {
                _release = value;
                OnPropertyChanged(nameof(Release));
            }
        }

        /// <summary>
        /// 采用NuGet包
        /// </summary>
        public bool NuGet
        {
            get => _nuGet;
            set
            {
                _nuGet = value;
                OnPropertyChanged(nameof(NuGet));
            }
        }

        /// <summary>
        /// 采用DLL文件
        /// </summary>
        public bool DLL
        {
            get => _dll;
            set
            {
                _dll = value;
                OnPropertyChanged(nameof(DLL));
            }
        }

        /// <summary>
        /// 打开资源管理器
        /// </summary>
        public bool OpenExplorer
        {
            get => _openExplorer;
            set
            {
                _openExplorer = value;
                OnPropertyChanged(nameof(OpenExplorer));
            }
        }

        /// <summary>
        /// 清空目标文件夹
        /// </summary>
        public bool ClearTargetDirectory { get; set; } = true;

        /// <summary>
        /// 删除源文件
        /// </summary>
        public bool DeleteSourceFile { get; set; } = true;

        /// <summary>
        /// 是否可以导出
        /// </summary>
        public bool CanExport
        {
            get
            {
                bool versionReady = Debug || Release;
                bool typeReady = NuGet || DLL;
                bool addressReady = !string.IsNullOrEmpty(ProjectAddress) && !string.IsNullOrEmpty(TargetAddress);
                return versionReady && typeReady && addressReady;
            }
        }

        /// <summary>
        /// 导出中标识
        /// </summary>
        public bool Exporting { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            List<NuGetToolsConfigTemplateModel> models = ApplicationConfig.Configuration.GetArrayObjectValue<NuGetToolsConfigTemplateModel>("Tools:NuGetToos");
            ConfigTemplateModels.AddRange(models);
            OnPropertyChanged(nameof(ConfigTemplateModels));
            OnPropertyChanged(nameof(SelectedConfigTemplateModel));
        }

        /// <summary>
        /// 导出
        /// </summary>
        public void Export()
        {
            try
            {
                Exporting = true;
                if (!CanExport) throw new MateralWPFException("导出配置错误");
                if (!Directory.Exists(ProjectAddress)) throw new MateralWPFException("项目文件夹不存在");
                var projectDirectoryInfo = new DirectoryInfo(ProjectAddress);
                DirectoryInfo targetDirectoryInfo = Directory.Exists(TargetAddress) ? new DirectoryInfo(TargetAddress) : Directory.CreateDirectory(TargetAddress);
                if (ClearTargetDirectory) targetDirectoryInfo.Clear();
                Export(projectDirectoryInfo, targetDirectoryInfo);
                if (OpenExplorer) ExplorerManager.OpenExplorer(TargetAddress);
            }
            finally
            {
                Exporting = false;
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="projectDirectoryInfo">项目文件夹</param>
        /// <param name="targetDirectoryInfo">目标文件夹</param>
        private void Export(DirectoryInfo projectDirectoryInfo, DirectoryInfo targetDirectoryInfo)
        {
            DirectoryInfo[] directoryInfos = projectDirectoryInfo.GetDirectories();
            foreach (DirectoryInfo directoryInfo in directoryInfos)
            {
                if (directoryInfo.Name.ToLower().Equals("bin"))
                {
                    DirectoryInfo[] generateDirectoryInfos = directoryInfo.GetDirectories();
                    foreach (DirectoryInfo generateDirectoryInfo in generateDirectoryInfos)
                    {
                        if (Release && generateDirectoryInfo.Name.ToLower().Equals(nameof(Release).ToLower()))
                        {
                            if(NuGet) ExportNuGet(generateDirectoryInfo, targetDirectoryInfo, nameof(Release));
                            if(DLL) ExportDLL(generateDirectoryInfo, targetDirectoryInfo, nameof(Release));
                        }
                        if (Debug && generateDirectoryInfo.Name.ToLower().Equals(nameof(Debug).ToLower()))
                        {
                            if (NuGet) ExportNuGet(generateDirectoryInfo, targetDirectoryInfo, nameof(Debug));
                            if (DLL) ExportDLL(generateDirectoryInfo, targetDirectoryInfo, nameof(Debug));
                        }
                    }
                }
                else
                {
                    Export(directoryInfo, targetDirectoryInfo);
                }
            }
        }

        /// <summary>
        /// 导出NuGet包文件
        /// </summary>
        /// <param name="projectDirectoryInfo">项目文件夹</param>
        /// <param name="targetDirectoryInfo">目标文件夹</param>
        /// <param name="subDirectoryName">子文件夹名称</param>
        private void ExportNuGet(DirectoryInfo projectDirectoryInfo, DirectoryInfo targetDirectoryInfo, string subDirectoryName)
        {
            ExportFile(projectDirectoryInfo, targetDirectoryInfo, $"NuGet/{subDirectoryName}", "*.nupkg");
        }

        /// <summary>
        /// 导出DLL文件
        /// </summary>
        /// <param name="projectDirectoryInfo">项目文件夹</param>
        /// <param name="targetDirectoryInfo">目标文件夹</param>
        /// <param name="subDirectoryName">子文件夹名称</param>
        private void ExportDLL(DirectoryInfo projectDirectoryInfo, DirectoryInfo targetDirectoryInfo, string subDirectoryName)
        {
            ExportFile(projectDirectoryInfo, targetDirectoryInfo, $"DLL/{subDirectoryName}", "*.dll");
        }

        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="projectDirectoryInfo">项目文件夹</param>
        /// <param name="targetDirectoryInfo">目标文件夹</param>
        /// <param name="subDirectoryName">子文件夹名称</param>
        /// <param name="fileExpression">文件表达式</param>
        private void ExportFile(DirectoryInfo projectDirectoryInfo, DirectoryInfo targetDirectoryInfo, string subDirectoryName, string fileExpression)
        {
            DirectoryInfo directoryInfo = targetDirectoryInfo.CreateSubdirectory(subDirectoryName);
            DirectoryInfo[] directoryInfos = projectDirectoryInfo.GetDirectories();
            ExportFile(projectDirectoryInfo, directoryInfo, fileExpression);
            foreach (DirectoryInfo item in directoryInfos)
            {
                ExportFile(item, directoryInfo, fileExpression);
            }
        }

        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="projectDirectoryInfo">项目文件夹</param>
        /// <param name="targetDirectoryInfo">目标文件夹</param>
        /// <param name="fileExpression">文件表达式</param>
        private void ExportFile(DirectoryInfo projectDirectoryInfo, FileSystemInfo targetDirectoryInfo, string fileExpression)
        {
            DirectoryInfo[] directoryInfos = projectDirectoryInfo.GetDirectories();
            foreach (DirectoryInfo item in directoryInfos)
            {
                ExportFile(item, targetDirectoryInfo, fileExpression);
            }
            FileInfo[] fileInfos = projectDirectoryInfo.GetFiles(fileExpression);
            foreach (FileInfo fileInfo in fileInfos)
            {
                string targetFileName = $"{targetDirectoryInfo.FullName}/{fileInfo.Name}";
                if (File.Exists(targetFileName))
                {
                    File.Delete(targetFileName);
                }
                if (DeleteSourceFile)
                {
                    fileInfo.MoveTo(targetFileName);
                }
                else
                {
                    fileInfo.CopyTo($"{targetDirectoryInfo.FullName}/{fileInfo.Name}", true);
                }
            }
        }
    }
}
