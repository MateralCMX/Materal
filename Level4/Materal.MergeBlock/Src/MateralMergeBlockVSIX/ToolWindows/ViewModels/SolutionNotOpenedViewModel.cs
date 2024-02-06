#nullable enable
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell.Interop;
using System.IO;
using System.Reflection;
using System.Text;

namespace MateralMergeBlockVSIX.ToolWindows.ViewModels
{
    public partial class SolutionNotOpenedViewModel : ObservableObject
    {
        private string? _projectName = "MMB";
        public string? ProjectName { get => _projectName; set { _projectName = value; NotifyPropertyChanged(); } }
        //private string _moduleName = "NewModule";
        private string _moduleName = "Authority";
        public string ModuleName { get => _moduleName; set { _moduleName = value; NotifyPropertyChanged(); } }
        //private string _projectPath = @"C:\Project";
        private string _projectPath = @"D:\Project\Test\Materal.MergeBlockTest\MMB";
        public string ProjectPath { get => _projectPath; set { _projectPath = value; NotifyPropertyChanged(); } }
        public SolutionNotOpenedViewModel() => PropertyChanged += SolutionNotOpenedControlViewModel_PropertyChanged;
        private void SolutionNotOpenedControlViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ProjectPath))
            {
                OnProjectPathChanged();
            }
        }
        private void OnProjectPathChanged()
        {
            if (string.IsNullOrEmpty(ProjectPath)) return;
            if (ProjectPath.EndsWith("\\"))
            {
                ProjectPath = ProjectPath[0..^1];
            }
            int index = ProjectPath.LastIndexOf("\\");
            if (index == -1) return;
            ProjectName = ProjectPath[(index + 1)..];
        }
        public void CreateModule()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (ProjectName is not null && !string.IsNullOrWhiteSpace(ProjectName))
            {
                CreateCoreSolution();
            }
            string slnFilePath = CreateModuleSolution();
            OpenSln(slnFilePath);
        }
        #region 核心方法
        /// <summary>
        /// 创建核心解决方案
        /// </summary>
        private void CreateCoreSolution()
        {
            string coreDirectoryPath = Path.Combine(ProjectPath, $"{ProjectName}.Core");
            DirectoryInfo coreDirectoryInfo = new(coreDirectoryPath);
            if (coreDirectoryInfo.Exists) return;
            coreDirectoryInfo.Create();
            coreDirectoryInfo.Refresh();
            string coreSolutionPath = Path.Combine(coreDirectoryPath, $"{ProjectName}.Core.sln");
            FileInfo coreSolutionFileInfo = new(coreSolutionPath);
            CreateCoreSolutionFile(coreSolutionFileInfo);
            CreateCoreProjects(coreDirectoryInfo);
            DirectoryInfo demoDirectoryInfo = new(Path.Combine(coreDirectoryPath, "Demo"));
            CreateDemoProjects(demoDirectoryInfo);
        }
        #region Core
        /// <summary>
        /// 创建核心解决方案文件
        /// </summary>
        /// <param name="coreSolutionFileInfo"></param>
        private void CreateCoreSolutionFile(FileInfo coreSolutionFileInfo)
        {
            if (coreSolutionFileInfo.Exists) return;
            ApplyTemplate(coreSolutionFileInfo.FullName, "Core", "Solution");
        }
        /// <summary>
        /// 创建核心项目
        /// </summary>
        /// <param name="coreDirectoryInfo"></param>
        private void CreateCoreProjects(DirectoryInfo coreDirectoryInfo)
        {
            if (!coreDirectoryInfo.Exists)
            {
                coreDirectoryInfo.Create();
                coreDirectoryInfo.Refresh();
            }
            CreateCoreAbstractionsProject(coreDirectoryInfo);
            CreateCoreApplicationProject(coreDirectoryInfo);
            CreateCoreRepositoryProject(coreDirectoryInfo);
        }
        /// <summary>
        /// 创建核心抽象项目
        /// </summary>
        /// <param name="coreDirectoryInfo"></param>
        private void CreateCoreAbstractionsProject(DirectoryInfo coreDirectoryInfo)
        {
            string projectName = $"{ProjectName}.Core.Abstractions";
            CreateNewProject(coreDirectoryInfo, projectName, "Core", "Abstractions", "Project");
            string directoryPath = Path.Combine(coreDirectoryInfo.FullName, projectName);
            ApplyTemplate(Path.Combine(directoryPath, $"{ProjectName}Exception.cs"), "Core", "Abstractions", "Exception");
            ApplyTemplate(Path.Combine(directoryPath, $"GlobalUsings.cs"), "Core", "Abstractions", "GlobalUsings");
            ApplyTemplate(Path.Combine(directoryPath, $"I{ProjectName}CacheRepository.cs"), "Core", "Abstractions", "ICacheRepository");
            ApplyTemplate(Path.Combine(directoryPath, $"I{ProjectName}Repository.cs"), "Core", "Abstractions", "IRepository");
            ApplyTemplate(Path.Combine(directoryPath, $"I{ProjectName}UnitOfWork.cs"), "Core", "Abstractions", "IUnitOfWork");
        }
        /// <summary>
        /// 创建核心抽象项目
        /// </summary>
        /// <param name="coreDirectoryInfo"></param>
        private void CreateCoreApplicationProject(DirectoryInfo coreDirectoryInfo)
        {
            string projectName = $"{ProjectName}.Core.Application";
            CreateNewProject(coreDirectoryInfo, projectName, "Core", "Application", "Project");
            string directoryPath = Path.Combine(coreDirectoryInfo.FullName, projectName);
            ApplyTemplate(Path.Combine(directoryPath, $"{ProjectName}Module.cs"), "Core", "Application", "Module");
        }
        /// <summary>
        /// 创建核心仓储项目
        /// </summary>
        /// <param name="coreDirectoryInfo"></param>
        private void CreateCoreRepositoryProject(DirectoryInfo coreDirectoryInfo)
        {
            string projectName = $"{ProjectName}.Core.Repository";
            CreateNewProject(coreDirectoryInfo, projectName, "Core", "Repository", "Project");
            string directoryPath = Path.Combine(coreDirectoryInfo.FullName, projectName);
            ApplyTemplate(Path.Combine(directoryPath, $"GlobalUsings.cs"), "Core", "Repository", "GlobalUsings");
            ApplyTemplate(Path.Combine(directoryPath, $"{ProjectName}CacheRepository.cs"), "Core", "Repository", "CacheRepositoryImpl");
            ApplyTemplate(Path.Combine(directoryPath, $"{ProjectName}Repository.cs"), "Core", "Repository", "RepositoryImpl");
            ApplyTemplate(Path.Combine(directoryPath, $"{ProjectName}RepositoryModule.cs"), "Core", "Repository", "Module");
            ApplyTemplate(Path.Combine(directoryPath, $"{ProjectName}UnitOfWorkImpl.cs"), "Core", "Repository", "UnitOfWorkImpl");
        }
        #endregion
        #region Demo
        /// <summary>
        /// 创建Demo项目
        /// </summary>
        /// <param name="demoDirectoryInfo"></param>
        private void CreateDemoProjects(DirectoryInfo demoDirectoryInfo)
        {
            if (!demoDirectoryInfo.Exists)
            {
                demoDirectoryInfo.Create();
                demoDirectoryInfo.Refresh();
            }
            CreateDemoAbstractionsProject(demoDirectoryInfo);
            CreateDemoApplicationProject(demoDirectoryInfo);
            CreateDemoRepositoryProject(demoDirectoryInfo);
            CreateDemoWebAPIProject(demoDirectoryInfo);
        }
        /// <summary>
        /// 创建Demo抽象项目
        /// </summary>
        /// <param name="demoDirectoryInfo"></param>
        private void CreateDemoAbstractionsProject(DirectoryInfo demoDirectoryInfo)
        {
            string projectName = $"{ProjectName}.Demo.Abstractions";
            CreateNewProject(demoDirectoryInfo, projectName, "Demo", "Abstractions", "Project");
            string directoryPath = Path.Combine(demoDirectoryInfo.FullName, projectName);
            ApplyTemplate(Path.Combine(directoryPath, $"GlobalUsings.cs"), "Demo", "Abstractions", "GlobalUsings");
            ApplyTemplate(Path.Combine(directoryPath, $"IDemoCacheRepository.cs"), "Demo", "Abstractions", "IDemoCacheRepository");
            ApplyTemplate(Path.Combine(directoryPath, $"IDemoRepository.cs"), "Demo", "Abstractions", "IDemoRepository");
            ApplyTemplate(Path.Combine(directoryPath, $"IDemoUnitOfWork.cs"), "Demo", "Abstractions", "IDemoUnitOfWork");
            string controllersDirectoryPath = Path.Combine(directoryPath, "Controllers");
            ApplyTemplate(Path.Combine(controllersDirectoryPath, $"IUserController.cs"), "Demo", "Abstractions", "Controllers", "IUserController");
            string domainDirectoryPath = Path.Combine(directoryPath, "Domain");
            ApplyTemplate(Path.Combine(domainDirectoryPath, $"User.cs"), "Demo", "Abstractions", "Domain", "User");
            string dtoDirectoryPath = Path.Combine(directoryPath, "DTO", "User");
            ApplyTemplate(Path.Combine(dtoDirectoryPath, $"User.cs"), "Demo", "Abstractions", "DTO", "User", "LoginResultDTO");
            string enumDirectoryPath = Path.Combine(directoryPath, "Enums");
            ApplyTemplate(Path.Combine(enumDirectoryPath, $"SexEnum.cs"), "Demo", "Abstractions", "Enums", "SexEnum");
            string requestModelDirectoryPath = Path.Combine(directoryPath, "RequestModel", "User");
            ApplyTemplate(Path.Combine(requestModelDirectoryPath, $"ChangePasswordRequestModel.cs"), "Demo", "Abstractions", "RequestModel", "User", "ChangePasswordRequestModel");
            ApplyTemplate(Path.Combine(requestModelDirectoryPath, $"LoginRequestModel.cs"), "Demo", "Abstractions", "RequestModel", "User", "LoginRequestModel");
            string servicesDirectoryPath = Path.Combine(directoryPath, "Services");
            ApplyTemplate(Path.Combine(servicesDirectoryPath, $"IUserService.cs"), "Demo", "Abstractions", "Services", "IUserService");
            string servicesModelsDirectoryPath = Path.Combine(directoryPath, "Services", "Models", "User");
            ApplyTemplate(Path.Combine(servicesModelsDirectoryPath, $"ChangePasswordModel.cs"), "Demo", "Abstractions", "Services", "Models", "User", "ChangePasswordModel");
            ApplyTemplate(Path.Combine(servicesModelsDirectoryPath, $"LoginModel.cs"), "Demo", "Abstractions", "Services", "Models", "User", "LoginModel");
        }
        /// <summary>
        /// 创建Demo应用项目
        /// </summary>
        /// <param name="demoDirectoryInfo"></param>
        private void CreateDemoApplicationProject(DirectoryInfo demoDirectoryInfo)
        {
            string projectName = $"{ProjectName}.Demo.Application";
            CreateNewProject(demoDirectoryInfo, projectName, "Demo", "Application", "Project");
            string directoryPath = Path.Combine(demoDirectoryInfo.FullName, projectName);
            ApplyTemplate(Path.Combine(directoryPath, $"GlobalUsings.cs"), "Demo", "Application", "GlobalUsings");
            ApplyTemplate(Path.Combine(directoryPath, $"ApplicationConfig.cs"), "Demo", "Application", "ApplicationConfig");
            ApplyTemplate(Path.Combine(directoryPath, $"DemoModule.cs"), "Demo", "Application", "Module");
            ApplyTemplate(Path.Combine(directoryPath, $"{ProjectName}.Demo.Application.json"), "Demo", "Application", "Config");
            ApplyTemplate(Path.Combine(directoryPath, $"DemoController.cs"), "Demo", "Application", "Controller");
            string autoMapperProfileDirectoryPath = Path.Combine(directoryPath, "AutoMapperProfile");
            ApplyTemplate(Path.Combine(autoMapperProfileDirectoryPath, $"UserProfile.cs"), "Demo", "Application", "AutoMapperProfile", "UserProfile");
            string controllersDirectoryPath = Path.Combine(directoryPath, "Controllers");
            ApplyTemplate(Path.Combine(controllersDirectoryPath, $"UserController.cs"), "Demo", "Application", "Controllers", "UserController");
            string ServicesDirectoryPath = Path.Combine(directoryPath, "Services");
            ApplyTemplate(Path.Combine(ServicesDirectoryPath, $"PasswordManager.cs"), "Demo", "Application", "Services", "PasswordManager");
            ApplyTemplate(Path.Combine(ServicesDirectoryPath, $"UserServiceImpl.cs"), "Demo", "Application", "Services", "UserServiceImpl");
        }
        /// <summary>
        /// 创建Demo仓储项目
        /// </summary>
        /// <param name="demoDirectoryInfo"></param>
        private void CreateDemoRepositoryProject(DirectoryInfo demoDirectoryInfo)
        {
            string projectName = $"{ProjectName}.Demo.Repository";
            CreateNewProject(demoDirectoryInfo, projectName, "Demo", "Repository", "Project");
            string directoryPath = Path.Combine(demoDirectoryInfo.FullName, projectName);
            ApplyTemplate(Path.Combine(directoryPath, $"{ProjectName}.Demo.Repository.json"), "Demo", "Repository", "Config");
            ApplyTemplate(Path.Combine(directoryPath, $"DemoCacheRepositoryImpl.cs"), "Demo", "Repository", "DemoCacheRepositoryImpl");
            ApplyTemplate(Path.Combine(directoryPath, $"DemoRepositoryImpl.cs"), "Demo", "Repository", "DemoRepositoryImpl");
            ApplyTemplate(Path.Combine(directoryPath, $"DemoRepositoryModule.cs"), "Demo", "Repository", "DemoRepositoryModule");
            ApplyTemplate(Path.Combine(directoryPath, $"DemoUnitOfWorkImpl.cs"), "Demo", "Repository", "DemoUnitOfWorkImpl");
            ApplyTemplate(Path.Combine(directoryPath, $"GlobalUsings.cs"), "Demo", "Repository", "GlobalUsings");
        }
        /// <summary>
        /// 创建DemoWebAPI项目
        /// </summary>
        /// <param name="demoDirectoryInfo"></param>
        private void CreateDemoWebAPIProject(DirectoryInfo demoDirectoryInfo)
        {
            string projectName = $"{ProjectName}.Demo.WebAPI";
            CreateNewProject(demoDirectoryInfo, projectName, "Demo", "WebAPI", "Project");
            string directoryPath = Path.Combine(demoDirectoryInfo.FullName, projectName);
            ApplyTemplate(Path.Combine(directoryPath, $"appsettings.json"), "Demo", "WebAPI", "Appsettings");
            ApplyTemplate(Path.Combine(directoryPath, $"Program.cs"), "Demo", "WebAPI", "Program");
            string propertiesDirectoryPath = Path.Combine(directoryPath, "Properties");
            ApplyTemplate(Path.Combine(propertiesDirectoryPath, $"launchSettings.json"), "Demo", "WebAPI", "Properties", "LaunchSettings");
        }
        #endregion
        #endregion
        #region 模块方法
        /// <summary>
        /// 创建模块解决方案
        /// </summary>
        private string CreateModuleSolution()
        {
            string moduleDirectoryPath = Path.Combine(ProjectPath, $"{ProjectName}.{ModuleName}");
            string moduleSolutionPath = Path.Combine(moduleDirectoryPath, $"{ProjectName}.{ModuleName}.sln");
            DirectoryInfo moduleDirectoryInfo = new(moduleDirectoryPath);
            if (moduleDirectoryInfo.Exists) return moduleSolutionPath;
            moduleDirectoryInfo.Create();
            moduleDirectoryInfo.Refresh();
            FileInfo moduleSolutionFileInfo = new(moduleSolutionPath);
            CreateModuleSolutionFile(moduleSolutionFileInfo);
            CreateModuleProjects(moduleDirectoryInfo);
            return moduleSolutionPath;
        }
        /// <summary>
        /// 创建模块解决方案文件
        /// </summary>
        /// <param name="moduleSolutionFileInfo"></param>
        private void CreateModuleSolutionFile(FileInfo moduleSolutionFileInfo)
        {
            if (moduleSolutionFileInfo.Exists) return;
            ApplyTemplate(moduleSolutionFileInfo.FullName, "Module", "Solution");
        }
        /// <summary>
        /// 创建模块项目
        /// </summary>
        /// <param name="moduleDirectoryInfo"></param>
        private void CreateModuleProjects(DirectoryInfo moduleDirectoryInfo)
        {
            if (!moduleDirectoryInfo.Exists)
            {
                moduleDirectoryInfo.Create();
                moduleDirectoryInfo.Refresh();
            }
            CreateModuleAbstractionsProject(moduleDirectoryInfo);
            CreateModuleApplicationProject(moduleDirectoryInfo);
            CreateModuleRepositoryProject(moduleDirectoryInfo);
            CreateModuleWebAPIProject(moduleDirectoryInfo);
        }
        /// <summary>
        /// 创建模块抽象项目
        /// </summary>
        /// <param name="moduleDirectoryInfo"></param>
        private void CreateModuleAbstractionsProject(DirectoryInfo moduleDirectoryInfo)
        {
            string projectName = $"{ProjectName}.{ModuleName}.Abstractions";
            CreateNewProject(moduleDirectoryInfo, projectName, "Module", "Abstractions", "Project");
            string directoryPath = Path.Combine(moduleDirectoryInfo.FullName, projectName);
            ApplyTemplate(Path.Combine(directoryPath, $"GlobalUsings.cs"), "Module", "Abstractions", "GlobalUsings");
            ApplyTemplate(Path.Combine(directoryPath, $"I{ModuleName}Repository.cs"), "Module", "Abstractions", "IRepository");
            ApplyTemplate(Path.Combine(directoryPath, $"I{ModuleName}CacheRepository.cs"), "Module", "Abstractions", "ICacheRepository");
            ApplyTemplate(Path.Combine(directoryPath, $"I{ModuleName}UnitOfWork.cs"), "Module", "Abstractions", "IUnitOfWork");
        }
        /// <summary>
        /// 创建模块应用项目
        /// </summary>
        /// <param name="moduleDirectoryInfo"></param>
        private void CreateModuleApplicationProject(DirectoryInfo moduleDirectoryInfo)
        {
            string projectName = $"{ProjectName}.{ModuleName}.Application";
            CreateNewProject(moduleDirectoryInfo, projectName, "Module", "Application", "Project");
            string directoryPath = Path.Combine(moduleDirectoryInfo.FullName, projectName);
            ApplyTemplate(Path.Combine(directoryPath, $"ApplicationConfig.cs"), "Module", "Application", "ApplicationConfig");
            ApplyTemplate(Path.Combine(directoryPath, $"{ProjectName}.{ModuleName}.Application.json"), "Module", "Application", "Config");
            ApplyTemplate(Path.Combine(directoryPath, $"GlobalUsings.cs"), "Module", "Application", "GlobalUsings");
            ApplyTemplate(Path.Combine(directoryPath, $"{ModuleName}Module.cs"), "Module", "Application", "Module");
            ApplyTemplate(Path.Combine(directoryPath, $"{ModuleName}Controller.cs"), "Module", "Application", "Controller");
        }
        /// <summary>
        /// 创建模块仓储项目
        /// </summary>
        /// <param name="moduleDirectoryInfo"></param>
        private void CreateModuleRepositoryProject(DirectoryInfo moduleDirectoryInfo)
        {
            string projectName = $"{ProjectName}.{ModuleName}.Repository";
            CreateNewProject(moduleDirectoryInfo, projectName, "Module", "Repository", "Project");
            string directoryPath = Path.Combine(moduleDirectoryInfo.FullName, projectName);
            ApplyTemplate(Path.Combine(directoryPath, $"{ProjectName}.{ModuleName}.Repository.json"), "Module", "Repository", "Config");
            ApplyTemplate(Path.Combine(directoryPath, $"GlobalUsings.cs"), "Module", "Repository", "GlobalUsings");
            ApplyTemplate(Path.Combine(directoryPath, $"{ModuleName}RepositoryImpl.cs"), "Module", "Repository", "RepositoryImpl");
            ApplyTemplate(Path.Combine(directoryPath, $"{ModuleName}CacheRepositoryImpl.cs"), "Module", "Repository", "CacheRepositoryImpl");
            ApplyTemplate(Path.Combine(directoryPath, $"{ModuleName}RepositoryModule.cs"), "Module", "Repository", "RepositoryModule");
            ApplyTemplate(Path.Combine(directoryPath, $"{ModuleName}UnitOfWorkImpl.cs"), "Module", "Repository", "UnitOfWorkImpl");
        }
        /// <summary>
        /// 创建模块WebAPI项目
        /// </summary>
        /// <param name="moduleDirectoryInfo"></param>
        private void CreateModuleWebAPIProject(DirectoryInfo moduleDirectoryInfo)
        {
            string projectName = $"{ProjectName}.{ModuleName}.WebAPI";
            CreateNewProject(moduleDirectoryInfo, projectName, "Module", "WebAPI", "Project");
            string directoryPath = Path.Combine(moduleDirectoryInfo.FullName, projectName);
            ApplyTemplate(Path.Combine(directoryPath, $"appsettings.json"), "Module", "WebAPI", "Appsettings");
            ApplyTemplate(Path.Combine(directoryPath, $"Program.cs"), "Module", "WebAPI", "Program");
            string propertiesDirectoryPath = Path.Combine(directoryPath, "Properties");
            ApplyTemplate(Path.Combine(propertiesDirectoryPath, $"launchSettings.json"), "Module", "WebAPI", "Properties", "LaunchSettings");
        }
        #endregion
        #region 工具方法
        /// <summary>
        /// 创建新项目
        /// </summary>
        /// <param name="coreDirectoryInfo"></param>
        private void CreateNewProject(DirectoryInfo coreDirectoryInfo, string directoryName, params string[] templateNames)
        {
            string templateName = string.Join(".", templateNames);
            CreateNewProject(coreDirectoryInfo, directoryName, templateName);
        }
        /// <summary>
        /// 创建新项目
        /// </summary>
        /// <param name="coreDirectoryInfo"></param>
        private void CreateNewProject(DirectoryInfo coreDirectoryInfo, string directoryName, string templateName)
        {
            string coreAbstractionsDirectoryPath = Path.Combine(coreDirectoryInfo.FullName, directoryName);
            DirectoryInfo coreAbstractionsDirectoryInfo = new(coreAbstractionsDirectoryPath);
            if (!coreAbstractionsDirectoryInfo.Exists)
            {
                coreAbstractionsDirectoryInfo.Create();
                coreAbstractionsDirectoryInfo.Refresh();
            }
            string coreAbstractionsProjectPath = Path.Combine(coreAbstractionsDirectoryInfo.FullName, $"{directoryName}.csproj");
            FileInfo coreAbstractionsProjectFileInfo = new(coreAbstractionsProjectPath);
            if (coreAbstractionsProjectFileInfo.Exists) return;
            ApplyTemplate(coreAbstractionsProjectFileInfo.FullName, templateName);
        }
        /// <summary>
        /// 应用模版
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="templateNames"></param>
        private void ApplyTemplate(string filePath, params string[] templateNames)
        {
            string templateName = string.Join(".", templateNames);
            ApplyTemplate(filePath, templateName);
        }
        /// <summary>
        /// 应用模版
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="templateNames"></param>
        private void ApplyTemplate(string filePath, string templateName)
        {
            FileInfo fileInfo = new(filePath);
            DirectoryInfo directoryInfo = fileInfo.Directory;
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
                directoryInfo.Refresh();
            }
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
                fileInfo.Refresh();
            }
            string resourceName = $"MateralMergeBlockVSIX.Templates.{templateName}.template";
            using Stream? templateFileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            if (templateFileStream is null || templateFileStream.Length <= 0) throw new Exception($"未找到模版{resourceName}");
            string template = new StreamReader(templateFileStream, Encoding.UTF8).ReadToEnd();
            template = template.Replace("${ProjectName}", ProjectName);
            string projectPath = ProjectPath.Replace(@"\", @"\\");
            template = template.Replace("${ProjectPath}", projectPath);
            template = template.Replace("${ModuleName}", ModuleName);
            File.WriteAllText(filePath, template, Encoding.UTF8);
        }
        /// <summary>
        /// 打开Sln文件
        /// </summary>
        /// <param name="slnPath"></param>
        private void OpenSln(string slnPath)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (!File.Exists(slnPath)) return;
            if (Package.GetGlobalService(typeof(SVsSolution)) is not IVsSolution solution) return;
            solution.OpenSolutionFile((uint)__VSSLNOPENOPTIONS.SLNOPENOPT_Silent, slnPath);
        }
        #endregion
    }
}
