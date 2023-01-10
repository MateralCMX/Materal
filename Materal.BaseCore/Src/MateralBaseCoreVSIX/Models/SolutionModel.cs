using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MateralBaseCoreVSIX.Models
{
    public class SolutionModel
    {
        #region 项目
        private readonly ProjectModel _commonProject;
        private readonly ProjectModel _domainProject;
        private readonly ProjectModel _webAPIProject;
        private readonly ProjectModel _servicesProject;
        private readonly ProjectModel _serviceImplProject;
        private readonly ProjectModel _efRepositoryProject;
        private readonly ProjectModel _dataTransmitModelProject;
        private readonly ProjectModel _presentationModelProject;
        #endregion
        private readonly string _rootPath;
        private readonly List<DomainModel> _domains = new List<DomainModel>();
        public SolutionModel(Solution solution, Project domainProject)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            _rootPath = Path.GetDirectoryName(solution.FullName);
            FillDoamins(domainProject);
            _domainProject = new ProjectModel(domainProject, _rootPath);
            foreach (Project project in solution.Projects)
            {
                if (project.Name == $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.Common")
                {
                    _commonProject = new ProjectModel(project, _rootPath);
                }
                else if(project.Name == $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.EFRepository")
                {
                    _efRepositoryProject = new ProjectModel(project, _rootPath);
                }
                else if(project.Name == $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.DataTransmitModel")
                {
                    _dataTransmitModelProject = new ProjectModel(project, _rootPath);
                }
                else if (project.Name == $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.Services")
                {
                    _servicesProject = new ProjectModel(project, _rootPath);
                }
                else if (project.Name == $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.ServiceImpl")
                {
                    _serviceImplProject = new ProjectModel(project, _rootPath);
                }
                else if (project.Name == $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.PresentationModel")
                {
                    _presentationModelProject = new ProjectModel(project, _rootPath);
                }
                else if (project.Name == $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.WebAPI")
                {
                    _webAPIProject = new ProjectModel(project, _rootPath);
                }
            }
            if (_commonProject == null)
            {
                _commonProject = CreateCommonProjectFile(solution);
            }
            if (_efRepositoryProject == null)
            {
                _efRepositoryProject = CreateEFRepositoryProjectFile(solution);
            }
            if (_dataTransmitModelProject == null)
            {
                _dataTransmitModelProject = CreateDataTransmitModelProjectFile(solution);
            }
            if (_servicesProject == null)
            {
                _servicesProject = CreateServicesProjectFile(solution);
            }
            if (_serviceImplProject == null)
            {
                _serviceImplProject = CreateServiceImplProjectFile(solution);
            }
            if (_presentationModelProject == null)
            {
                _presentationModelProject = CreatePresentationModelProjectFile(solution);
            }
            if (_webAPIProject == null)
            {
                _webAPIProject = CreateWebAPIProjectFile(solution);
            }
        }
        public void CreateCodeFiles()
        {
            _domainProject.CreateDomainFiles(_domains);
            _efRepositoryProject.CreateEFRepositoryFiles(_domains);
            _dataTransmitModelProject.CreateDataTransmitModelFiles(_domains);
            _servicesProject.CreateServicesFiles(_domains);
            _serviceImplProject.CreateServiceImplFiles(_domains);
            _presentationModelProject.CreatePresentationModelFiles(_domains);
            _webAPIProject.CreateWebAPIFiles(_domains);
        }
        #region 私有方法
        private void FillDoamins(Project domainProject)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            string itemPath = Path.Combine(_rootPath, domainProject.Name);
            FillDomains(domainProject.ProjectItems, itemPath);
        }
        private void FillDomains(ProjectItems projectItems, string path)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            foreach (ProjectItem item in projectItems)
            {
                var itemPath = Path.Combine(path, item.Name);
                if (item.Name == "MCG")
                {
                    continue;
                }
                else if (item.ProjectItems != null && item.ProjectItems.Count > 0)
                {
                    FillDomains(item.ProjectItems, itemPath);
                }
                else if (Path.GetExtension(item.Name) == ".cs")
                {
                    var domainModel = GetDomainModelOrNull(item, path);
                    if (domainModel == null) continue;
                    _domains.Add(domainModel);
                }
            }
        }
        private DomainModel GetDomainModelOrNull(ProjectItem projectItem, string path)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (Path.GetExtension(projectItem.Name) != ".cs") return null;
            string filePath = Path.Combine(path, projectItem.Name);
            if (!File.Exists(filePath)) return null;
            string[] codes = File.ReadAllLines(filePath);
            for (int i = 0; i < codes.Length; i++)
            {
                string namespaceCode = codes[i];
                if (!namespaceCode.StartsWith("namespace ") || !namespaceCode.EndsWith(".Domain")) continue;
                for (int j = i; j < codes.Length; j++)
                {
                    string classCode = codes[j];
                    int publicIndex = classCode.IndexOf("public ");
                    if (publicIndex <= 0) continue;
                    int classIndex = classCode.IndexOf(" class ");
                    if (classIndex <= 0) continue;
                    int domainIndex = classCode.IndexOf(" : BaseDomain, IDomain");
                    if (domainIndex <= 0) continue;
                    return new DomainModel(codes, j);
                }
            }
            return null;
        }
        /// <summary>
        /// 创建公共项目文件
        /// </summary>
        /// <param name="solution">解决方案对象</param>
        private ProjectModel CreateCommonProjectFile(Solution solution)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            string directoryPath = Path.Combine(_rootPath, $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.Common");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            #region CsProject
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"<Project Sdk=\"Microsoft.NET.Sdk\">");
            codeContent.AppendLine($"\t<PropertyGroup>");
            codeContent.AppendLine($"\t\t<TargetFramework>net6.0</TargetFramework>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.Common\" Version=\"0.0.1\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"</Project>");
            string filePath = codeContent.SaveFile(directoryPath, $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.Common.csproj");
            #endregion
            Project project = solution.AddFromFile(filePath, false);
            return new ProjectModel(project, _rootPath);
        }
        /// <summary>
        /// 创建数据传输模型项目文件
        /// </summary>
        /// <param name="solution">解决方案对象</param>
        private ProjectModel CreateEFRepositoryProjectFile(Solution solution)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            string directoryPath = Path.Combine(_rootPath, $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.EFRepository");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            #region CsProject
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"<Project Sdk=\"Microsoft.NET.Sdk\">");
            codeContent.AppendLine($"\t<PropertyGroup>");
            codeContent.AppendLine($"\t\t<TargetFramework>net6.0</TargetFramework>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.EFRepository\" Version=\"0.0.1\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{_domainProject.Namespace}\\{_domainProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"</Project>");
            string filePath = codeContent.SaveFile(directoryPath, $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.EFRepository.csproj");
            #endregion
            Project project = solution.AddFromFile(filePath, false);
            return new ProjectModel(project, _rootPath);
        }
        /// <summary>
        /// 创建数据传输模型项目文件
        /// </summary>
        /// <param name="solution">解决方案对象</param>
        private ProjectModel CreateDataTransmitModelProjectFile(Solution solution)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            string directoryPath = Path.Combine(_rootPath, $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.DataTransmitModel");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            #region CsProject
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"<Project Sdk=\"Microsoft.NET.Sdk\">");
            codeContent.AppendLine($"\t<PropertyGroup>");
            codeContent.AppendLine($"\t\t<TargetFramework>net6.0</TargetFramework>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t\t<GenerateDocumentationFile>True</GenerateDocumentationFile>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.DataTransmitModel\" Version=\"0.0.1\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"</Project>");
            string filePath = codeContent.SaveFile(directoryPath, $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.DataTransmitModel.csproj");
            #endregion
            Project project = solution.AddFromFile(filePath, false);
            return new ProjectModel(project, _rootPath);
        }
        /// <summary>
        /// 创建服务项目文件
        /// </summary>
        /// <param name="solution">解决方案对象</param>
        private ProjectModel CreateServicesProjectFile(Solution solution)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            string directoryPath = Path.Combine(_rootPath, $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.Services");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            #region CsProject
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"<Project Sdk=\"Microsoft.NET.Sdk\">");
            codeContent.AppendLine($"\t<PropertyGroup>");
            codeContent.AppendLine($"\t\t<TargetFramework>net6.0</TargetFramework>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.Services\" Version=\"0.0.1\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{_commonProject.Namespace}\\{_commonProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{_dataTransmitModelProject.Namespace}\\{_dataTransmitModelProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"</Project>");
            string filePath = codeContent.SaveFile(directoryPath, $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.Services.csproj");
            #endregion
            Project project = solution.AddFromFile(filePath, false);
            return new ProjectModel(project, _rootPath);
        }
        /// <summary>
        /// 创建服务实现项目文件
        /// </summary>
        /// <param name="solution">解决方案对象</param>
        private ProjectModel CreateServiceImplProjectFile(Solution solution)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            string directoryPath = Path.Combine(_rootPath, $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.ServiceImpl");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            #region CsProject
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"<Project Sdk=\"Microsoft.NET.Sdk\">");
            codeContent.AppendLine($"\t<PropertyGroup>");
            codeContent.AppendLine($"\t\t<TargetFramework>net6.0</TargetFramework>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.ServiceImpl\" Version=\"0.0.1\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{_domainProject.Namespace}\\{_domainProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{_servicesProject.Namespace}\\{_servicesProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"</Project>");
            string filePath = codeContent.SaveFile(directoryPath, $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.ServiceImpl.csproj");
            #endregion
            Project project = solution.AddFromFile(filePath, false);
            return new ProjectModel(project, _rootPath);
        }
        /// <summary>
        /// 创建表现模型项目文件
        /// </summary>
        /// <param name="solution">解决方案对象</param>
        private ProjectModel CreatePresentationModelProjectFile(Solution solution)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            string directoryPath = Path.Combine(_rootPath, $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.PresentationModel");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            #region CsProject
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"<Project Sdk=\"Microsoft.NET.Sdk\">");
            codeContent.AppendLine($"\t<PropertyGroup>");
            codeContent.AppendLine($"\t\t<TargetFramework>net6.0</TargetFramework>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t\t<GenerateDocumentationFile>True</GenerateDocumentationFile>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.PresentationModel\" Version=\"0.0.1\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"</Project>");
            string filePath = codeContent.SaveFile(directoryPath, $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.PresentationModel.csproj");
            #endregion
            Project project = solution.AddFromFile(filePath, false);
            return new ProjectModel(project, _rootPath);
        }
        /// <summary>
        /// 创建WebAPI项目文件
        /// </summary>
        /// <param name="solution">解决方案对象</param>
        private ProjectModel CreateWebAPIProjectFile(Solution solution)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            string directoryPath = Path.Combine(_rootPath, $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.WebAPI");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            #region CsProject
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"<Project Sdk=\"Microsoft.NET.Sdk.Web\">");
            codeContent.AppendLine($"\t<PropertyGroup>");
            codeContent.AppendLine($"\t\t<TargetFramework>net6.0</TargetFramework>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t\t<GenerateDocumentationFile>True</GenerateDocumentationFile>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Microsoft.EntityFrameworkCore.Tools\" Version=\"7.0.1\">");
            codeContent.AppendLine($"\t\t\t<PrivateAssets>all</PrivateAssets>");
            codeContent.AppendLine($"\t\t\t<IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>");
            codeContent.AppendLine($"\t\t</PackageReference>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.WebAPI\" Version=\"0.0.1\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{_presentationModelProject.Namespace}\\{_presentationModelProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{_serviceImplProject.Namespace}\\{_serviceImplProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{_efRepositoryProject.Namespace}\\{_efRepositoryProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"</Project>");
            string filePath = codeContent.SaveFile(directoryPath, $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.WebAPI.csproj");
            #endregion
            Project project = solution.AddFromFile(filePath, false);
            return new ProjectModel(project, _rootPath);
        }
        #endregion

    }
}
