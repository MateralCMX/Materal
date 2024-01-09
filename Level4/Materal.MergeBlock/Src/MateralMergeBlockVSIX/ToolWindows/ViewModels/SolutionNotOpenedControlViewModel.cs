#nullable enable
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell.Interop;
using System.IO;
using System.Reflection;
using System.Text;

namespace MateralMergeBlockVSIX.ToolWindows.ViewModels
{
    public partial class SolutionNotOpenedControlViewModel : ObservableObject
    {
        private string? _projectName = "MMB";
        public string? ProjectName { get => _projectName; set { _projectName = value; NotifyPropertyChanged(); } }
        private string _moduleName = "NewModule";
        public string ModuleName { get => _moduleName; set { _moduleName = value; NotifyPropertyChanged(); } }
        private string _projectPath = @"C:\Project";
        public string ProjectPath { get => _projectPath; set { _projectPath = value; NotifyPropertyChanged(); } }
        public SolutionNotOpenedControlViewModel() => PropertyChanged += SolutionNotOpenedControlViewModel_PropertyChanged;
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
            if (!coreDirectoryInfo.Exists)
            {
                coreDirectoryInfo.Create();
                coreDirectoryInfo.Refresh();
            }
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
            ApplyTemplate(coreSolutionFileInfo.FullName, "CoreSolution");
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
            CreateCoreRepositoryProject(coreDirectoryInfo);
        }
        /// <summary>
        /// 创建核心抽象项目
        /// </summary>
        /// <param name="coreDirectoryInfo"></param>
        private void CreateCoreAbstractionsProject(DirectoryInfo coreDirectoryInfo)
        {
            string projectName = $"{ProjectName}.Core.Abstractions";
            CreateNewProject(coreDirectoryInfo, projectName, "CoreAbstractionsProject");
            string directoryPath = Path.Combine(coreDirectoryInfo.FullName, projectName);
            ApplyTemplate(Path.Combine(directoryPath, $"GlobalUsings.cs"), "CoreAbstractionsGlobalUsings");
            ApplyTemplate(Path.Combine(directoryPath, $"{ProjectName}Exception.cs"), "CoreAbstractionsException");
            ApplyTemplate(Path.Combine(directoryPath, $"{ProjectName}Module.cs"), "CoreAbstractionsModule");
        }
        /// <summary>
        /// 创建核心仓储项目
        /// </summary>
        /// <param name="coreDirectoryInfo"></param>
        private void CreateCoreRepositoryProject(DirectoryInfo coreDirectoryInfo)
        {
            string projectName = $"{ProjectName}.Core.Repository";
            CreateNewProject(coreDirectoryInfo, projectName, "CoreRepositoryProject");
            string directoryPath = Path.Combine(coreDirectoryInfo.FullName, projectName);
            ApplyTemplate(Path.Combine(directoryPath, $"GlobalUsings.cs"), "CoreRepositoryGlobalUsings");
            ApplyTemplate(Path.Combine(directoryPath, $"I{ProjectName}CacheRepository.cs"), "CoreRepositoryICacheRepository");
            ApplyTemplate(Path.Combine(directoryPath, $"I{ProjectName}Repository.cs"), "CoreRepositoryIRepository");
            ApplyTemplate(Path.Combine(directoryPath, $"{ProjectName}CacheRepository.cs"), "CoreRepositoryCacheRepositoryImpl");
            ApplyTemplate(Path.Combine(directoryPath, $"{ProjectName}Repository.cs"), "CoreRepositoryRepositoryImpl");
            ApplyTemplate(Path.Combine(directoryPath, $"{ProjectName}RepositoryModule.cs"), "CoreRepositoryModule");
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
            CreateNewProject(demoDirectoryInfo, projectName, "DemoAbstractionsProject");
            string directoryPath = Path.Combine(demoDirectoryInfo.FullName, projectName);
            ApplyTemplate(Path.Combine(directoryPath, $"GlobalUsings.cs"), "DemoAbstractionsGlobalUsings");
            string enumDirectoryPath = Path.Combine(directoryPath, "Enums");
            ApplyTemplate(Path.Combine(enumDirectoryPath, $"SexEnum.cs"), "DemoAbstractionsEnum");
            string domainDirectoryPath = Path.Combine(directoryPath, "Domain");
            ApplyTemplate(Path.Combine(domainDirectoryPath, $"User.cs"), "DemoAbstractionsDomain");
            string servicesDirectoryPath = Path.Combine(directoryPath, "Services");
        }
        /// <summary>
        /// 创建Demo应用项目
        /// </summary>
        /// <param name="demoDirectoryInfo"></param>
        private void CreateDemoApplicationProject(DirectoryInfo demoDirectoryInfo)
        {
            string projectName = $"{ProjectName}.Demo.Application";
            CreateNewProject(demoDirectoryInfo, projectName, "DemoApplicationProject");
            string directoryPath = Path.Combine(demoDirectoryInfo.FullName, projectName);
            ApplyTemplate(Path.Combine(directoryPath, $"GlobalUsings.cs"), "DemoApplicationGlobalUsings");
        }
        /// <summary>
        /// 创建Demo仓储项目
        /// </summary>
        /// <param name="demoDirectoryInfo"></param>
        private void CreateDemoRepositoryProject(DirectoryInfo demoDirectoryInfo)
        {
            string projectName = $"{ProjectName}.Demo.Repository";
            CreateNewProject(demoDirectoryInfo, projectName, "DemoRepositoryProject");
            string directoryPath = Path.Combine(demoDirectoryInfo.FullName, projectName);
            ApplyTemplate(Path.Combine(directoryPath, $"GlobalUsings.cs"), "DemoRepositoryGlobalUsings");
            ApplyTemplate(Path.Combine(directoryPath, $"DemoRepositoryModule.cs"), "DemoRepositoryModule");
        }
        /// <summary>
        /// 创建DemoWebAPI项目
        /// </summary>
        /// <param name="demoDirectoryInfo"></param>
        private void CreateDemoWebAPIProject(DirectoryInfo demoDirectoryInfo)
        {
            string projectName = $"{ProjectName}.Demo.WebAPI";
            CreateNewProject(demoDirectoryInfo, projectName, "DemoWebAPIProject");
            string directoryPath = Path.Combine(demoDirectoryInfo.FullName, projectName);
            ApplyTemplate(Path.Combine(directoryPath, $"Program.cs"), "DemoWebAPIProgram");
            ApplyTemplate(Path.Combine(directoryPath, $"appsettings.json"), "DemoWebAPIAppsettings");
            ApplyTemplate(Path.Combine(directoryPath, "Properties", $"launchSettings.json"), "DemoWebAPILaunchSettings");
        }
        #endregion
        #endregion
        #region 模块方法
        /// <summary>
        /// 创建模块解决方案
        /// </summary>
        private string CreateModuleSolution()
        {
            return @"D:\Project\Materal\Materal\Level4\MMB\MMB.Core\MMB.Core.sln";
        }
        #endregion
        #region 工具方法
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
        /// <param name="templateName"></param>
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
            using Stream templateFileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            string template = new StreamReader(templateFileStream, Encoding.UTF8).ReadToEnd();
            template = template.Replace("${ProjectName}", ProjectName);
            template = template.Replace("${ProjectPath}", ProjectPath);
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
