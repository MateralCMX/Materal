using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MateralBaseCoreVSIX.Models
{
    public class SolutionModel
    {
        private ProjectModel _commonProject;
        private ProjectModel _domainProject;
        private ProjectModel _webAPIProject;
        private ProjectModel _servicesProject;
        private ProjectModel _serviceImplProject;
        private ProjectModel _efRepositoryProject;
        private ProjectModel _dataTransmitModelProject;
        private ProjectModel _presentationModelProject;
        private ProjectModel _enumsProject;
        private List<DomainModel> _domains = new List<DomainModel>();
        private List<EnumModel> _enums = new List<EnumModel>();
        public SolutionModel(Solution solution, Project domainProject)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            _domainProject = new ProjectModel(domainProject);
            FillDoamins(domainProject);
            FillProjects(solution.Projects);
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
        /// <summary>
        /// 创建代码文件
        /// </summary>
        public void CreateCodeFiles()
        {
            _domainProject.CreateDomainFiles(_domains);
            _efRepositoryProject.CreateEFRepositoryFiles(_domains);
            _dataTransmitModelProject.CreateDataTransmitModelFiles(_domains);
            _servicesProject.CreateServicesFiles(_domains);
            _serviceImplProject.CreateServiceImplFiles(_domains);
            _presentationModelProject.CreatePresentationModelFiles(_domains);
            _webAPIProject.CreateWebAPIFiles(_domains);
            if(_enums != null && _enums.Count > 0)
            {
                _webAPIProject.CreateEnumsControllers(_enums);
            }
        }
        #region 私有方法
        /// <summary>
        /// 填充项目
        /// </summary>
        /// <param name="projects"></param>
        private void FillProjects(Projects projects)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            foreach (Project project in projects)
            {
                if (project == null) continue;
                FillProject(project);
                if (project.ProjectItems != null)
                {
                    foreach (object objItem in project.ProjectItems)
                    {
                        if (!(objItem is ProjectItem projectItem) || projectItem.SubProject == null) continue;
                        FillProject(projectItem.SubProject);
                    }
                }
                else if (project.Collection != null && project.Collection.Count > 0)
                {
                    FillProjects(project.Collection);
                }
            }
        }
        /// <summary>
        /// 填充项目
        /// </summary>
        /// <param name="project"></param>
        private void FillProject(Project project)
        {
            if (project == null && project.GetType().Name == "OAProject") return;
            ThreadHelper.ThrowIfNotOnUIThread();
            if (project.Name == $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.Common")
            {
                _commonProject = new ProjectModel(project);
            }
            else if (project.Name == $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.EFRepository")
            {
                _efRepositoryProject = new ProjectModel(project);
            }
            else if (project.Name == $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.DataTransmitModel")
            {
                _dataTransmitModelProject = new ProjectModel(project);
            }
            else if (project.Name == $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.Services")
            {
                _servicesProject = new ProjectModel(project);
            }
            else if (project.Name == $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.ServiceImpl")
            {
                _serviceImplProject = new ProjectModel(project);
            }
            else if (project.Name == $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.PresentationModel")
            {
                _presentationModelProject = new ProjectModel(project);
            }
            else if (project.Name == $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.WebAPI")
            {
                _webAPIProject = new ProjectModel(project);
            }
            else if (project.Name == $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.Enums")
            {
                _enumsProject = new ProjectModel(project);
                FillEnums(project);
            }
        }
        /// <summary>
        /// 填充枚举
        /// </summary>
        /// <param name="enumProject"></param>
        private void FillEnums(Project enumProject)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            FillEnums(enumProject.ProjectItems, _enumsProject.RootPath);
        }
        /// <summary>
        /// 填充Domain
        /// </summary>
        /// <param name="projectItems"></param>
        /// <param name="path"></param>
        private void FillEnums(ProjectItems projectItems, string path)
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
                    var enumModel = GetEnumModelOrNull(item, path);
                    if (enumModel == null) continue;
                    _enums.Add(enumModel);
                }
            }
        }
        /// <summary>
        /// 获得Enum模型
        /// </summary>
        /// <param name="projectItem"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private EnumModel GetEnumModelOrNull(ProjectItem projectItem, string path)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (Path.GetExtension(projectItem.Name) != ".cs") return null;
            string filePath = Path.Combine(path, projectItem.Name);
            if (!File.Exists(filePath)) return null;
            string[] codes = File.ReadAllLines(filePath);
            for (int i = 0; i < codes.Length; i++)
            {
                string namespaceCode = codes[i];
                if (!namespaceCode.StartsWith("namespace ") || !namespaceCode.EndsWith(".Enums")) continue;
                for (int j = i; j < codes.Length; j++)
                {
                    string classCode = codes[j];
                    int publicIndex = classCode.IndexOf("public ");
                    if (publicIndex <= 0) continue;
                    int classIndex = classCode.IndexOf(" enum ");
                    if (classIndex <= 0) continue;
                    return new EnumModel(codes, j);
                }
            }
            return null;
        }
        /// <summary>
        /// 填充Domain
        /// </summary>
        /// <param name="domainProject"></param>
        private void FillDoamins(Project domainProject)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            FillDomains(domainProject.ProjectItems, _domainProject.RootPath);
        }
        /// <summary>
        /// 填充Domain
        /// </summary>
        /// <param name="projectItems"></param>
        /// <param name="path"></param>
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
        /// <summary>
        /// 获得Domain模型
        /// </summary>
        /// <param name="projectItem"></param>
        /// <param name="path"></param>
        /// <returns></returns>
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
        /// 创建项目文件
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="codeContent"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private ProjectModel CreateProjectFile(Solution solution, StringBuilder codeContent, string name)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            string directoryPath = Path.Combine(_domainProject.DiskDirectoryPath, name);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string filePath = codeContent.SaveFile(directoryPath, $"{name}.csproj");
            Project project = solution.AddFromFile(filePath, false);
            return new ProjectModel(project);
        }
        /// <summary>
        /// 创建公共项目文件
        /// </summary>
        /// <param name="solution">解决方案对象</param>
        private ProjectModel CreateCommonProjectFile(Solution solution)
        {
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"<Project Sdk=\"Microsoft.NET.Sdk\">");
            codeContent.AppendLine($"\t<PropertyGroup>");
            codeContent.AppendLine($"\t\t<TargetFramework>net6.0</TargetFramework>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.Common\" Version=\"0.2.0\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"</Project>");
            return CreateProjectFile(solution, codeContent, $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.Common");
        }
        /// <summary>
        /// 创建数据传输模型项目文件
        /// </summary>
        /// <param name="solution">解决方案对象</param>
        private ProjectModel CreateEFRepositoryProjectFile(Solution solution)
        {
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"<Project Sdk=\"Microsoft.NET.Sdk\">");
            codeContent.AppendLine($"\t<PropertyGroup>");
            codeContent.AppendLine($"\t\t<TargetFramework>net6.0</TargetFramework>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.EFRepository\" Version=\"0.2.0\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{_domainProject.Namespace}\\{_domainProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"</Project>");
            return CreateProjectFile(solution, codeContent, $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.EFRepository");
        }
        /// <summary>
        /// 创建数据传输模型项目文件
        /// </summary>
        /// <param name="solution">解决方案对象</param>
        private ProjectModel CreateDataTransmitModelProjectFile(Solution solution)
        {
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"<Project Sdk=\"Microsoft.NET.Sdk\">");
            codeContent.AppendLine($"\t<PropertyGroup>");
            codeContent.AppendLine($"\t\t<TargetFramework>net6.0</TargetFramework>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t\t<GenerateDocumentationFile>True</GenerateDocumentationFile>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.DataTransmitModel\" Version=\"0.2.0\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            if(_enumsProject != null)
            {
                codeContent.AppendLine($"\t<ItemGroup>");
                codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{_enumsProject.Namespace}\\{_enumsProject.Namespace}.csproj\" />");
                codeContent.AppendLine($"\t</ItemGroup>");
            }
            codeContent.AppendLine($"</Project>");
            return CreateProjectFile(solution, codeContent, $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.DataTransmitModel");
        }
        /// <summary>
        /// 创建服务项目文件
        /// </summary>
        /// <param name="solution">解决方案对象</param>
        private ProjectModel CreateServicesProjectFile(Solution solution)
        {
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"<Project Sdk=\"Microsoft.NET.Sdk\">");
            codeContent.AppendLine($"\t<PropertyGroup>");
            codeContent.AppendLine($"\t\t<TargetFramework>net6.0</TargetFramework>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.Services\" Version=\"0.2.0\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{_commonProject.Namespace}\\{_commonProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{_dataTransmitModelProject.Namespace}\\{_dataTransmitModelProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"</Project>");
            return CreateProjectFile(solution, codeContent, $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.Services");
        }
        /// <summary>
        /// 创建服务实现项目文件
        /// </summary>
        /// <param name="solution">解决方案对象</param>
        private ProjectModel CreateServiceImplProjectFile(Solution solution)
        {
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"<Project Sdk=\"Microsoft.NET.Sdk\">");
            codeContent.AppendLine($"\t<PropertyGroup>");
            codeContent.AppendLine($"\t\t<TargetFramework>net6.0</TargetFramework>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.ServiceImpl\" Version=\"0.2.0\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{_domainProject.Namespace}\\{_domainProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{_servicesProject.Namespace}\\{_servicesProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"</Project>");
            return CreateProjectFile(solution, codeContent, $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.ServiceImpl");
        }
        /// <summary>
        /// 创建表现模型项目文件
        /// </summary>
        /// <param name="solution">解决方案对象</param>
        private ProjectModel CreatePresentationModelProjectFile(Solution solution)
        {
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"<Project Sdk=\"Microsoft.NET.Sdk\">");
            codeContent.AppendLine($"\t<PropertyGroup>");
            codeContent.AppendLine($"\t\t<TargetFramework>net6.0</TargetFramework>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t\t<GenerateDocumentationFile>True</GenerateDocumentationFile>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.PresentationModel\" Version=\"0.2.0\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            if (_enumsProject != null)
            {
                codeContent.AppendLine($"\t<ItemGroup>");
                codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{_enumsProject.Namespace}\\{_enumsProject.Namespace}.csproj\" />");
                codeContent.AppendLine($"\t</ItemGroup>");
            }
            codeContent.AppendLine($"</Project>");
            return CreateProjectFile(solution, codeContent, $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.PresentationModel");
        }
        /// <summary>
        /// 创建WebAPI项目文件
        /// </summary>
        /// <param name="solution">解决方案对象</param>
        private ProjectModel CreateWebAPIProjectFile(Solution solution)
        {
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
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.WebAPI\" Version=\"0.2.0\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{_presentationModelProject.Namespace}\\{_presentationModelProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{_serviceImplProject.Namespace}\\{_serviceImplProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{_efRepositoryProject.Namespace}\\{_efRepositoryProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"</Project>");
            return CreateProjectFile(solution, codeContent, $"{_domainProject.PrefixName}.{_domainProject.ProjectName}.WebAPI");
        }
        #endregion
    }
}
