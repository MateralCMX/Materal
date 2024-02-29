using EnvDTE;
using Materal.BaseCore.CodeGenerator.Extensions;
using Materal.BaseCore.CodeGenerator.Models;
using Materal.Utils.Windows;
using Microsoft.VisualStudio.Shell;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MateralBaseCoreVSIX.Models
{
    public class VSIXDomainSolutionModel : DomainSolutionModel
    {
        protected List<ProjectModel> OtherProjects = new List<ProjectModel>();
        public VSIXDomainSolutionModel(Solution solution, Project domainProject)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            DomainProject = new VSIXProjectModel(domainProject);
            FillDoamins(domainProject);
            FillProjects(solution.Projects);
            if (CommonProject == null)
            {
                CommonProject = CreateCommonProjectFile(solution);
            }
            if (EFRepositoryProject == null)
            {
                EFRepositoryProject = CreateEFRepositoryProjectFile(solution);
            }
            if (DataTransmitModelProject == null)
            {
                DataTransmitModelProject = CreateDataTransmitModelProjectFile(solution);
            }
            if (ServicesProject == null)
            {
                ServicesProject = CreateServicesProjectFile(solution);
            }
            if (ServiceImplProject == null)
            {
                ServiceImplProject = CreateServiceImplProjectFile(solution);
            }
            if (PresentationModelProject == null)
            {
                PresentationModelProject = CreatePresentationModelProjectFile(solution);
            }
            if (WebAPIProject == null)
            {
                WebAPIProject = CreateWebAPIProjectFile(solution);
            }
        }
        #region 插件相关
        private DirectoryInfo _plugTempDirectoryInfo;
        private StringBuilder _plugErrorMessages;
        private PlugProjectModelCollection _plugProjectModels;
        private const string _resourceStart = "MateralBaseCoreVSIX.Tools";
        protected override void AllPlugExecuteBefore()
        {
            if (DomainProject == null) return;
            string plugTempPath = Path.Combine(DomainProject.RootPath, "PlugTemp");
            _plugTempDirectoryInfo = new DirectoryInfo(plugTempPath);
            _plugProjectModels = null;
            _plugErrorMessages = new StringBuilder();
        }
        protected override void PlugExecuteBefore(DomainPlugModel domainPlugModel, AttributeModel attributeModel)
        {
        }
        protected override void PlugExecute(DomainPlugModel domainPlugModel, AttributeModel attributeModel)
        {
            if (_plugTempDirectoryInfo == null) return;
            #region 获取插件项目路径
            string projectPath = attributeModel.AttributeArguments[0].Value.RemovePackag();
            ProjectModel codeGeneratorProjectModel = OtherProjects.FirstOrDefault(m => m.Namespace == projectPath) ?? throw new VSIXException("插件项目未引用");
            projectPath = codeGeneratorProjectModel.RootPath;
            #endregion
            if (_plugProjectModels == null)
            {
                _plugProjectModels = new PlugProjectModelCollection
                {
                    WebAPIProject = domainPlugModel.WebAPIProject,
                    CommonProject = domainPlugModel.CommonProject,
                    DataTransmitModelProject = domainPlugModel.DataTransmitModelProject,
                    DomainProject = domainPlugModel.DomainProject,
                    Domains = domainPlugModel.Domains,
                    EFRepositoryProject = domainPlugModel.EFRepositoryProject,
                    Enums = domainPlugModel.Enums,
                    EnumsProject = domainPlugModel.EnumsProject,
                    ServiceImplProject = domainPlugModel.ServiceImplProject,
                    ServicesProject = domainPlugModel.ServicesProject,
                    PresentationModelProject = domainPlugModel.PresentationModelProject
                };
            }
            PlugProjectModel plugProjectModel = _plugProjectModels.Projects.FirstOrDefault(m => m.Name == projectPath);
            if (plugProjectModel == null)
            {
                plugProjectModel = new PlugProjectModel
                {
                    Name = projectPath,
                };
                _plugProjectModels.Projects.Add(plugProjectModel);
            }
            #region 获取插件名称
            string plugName = attributeModel.AttributeArguments[1].Value.RemovePackag();
            if (plugName.EndsWith(".cs"))
            {
                plugName = plugName.Substring(0, plugName.Length - 3);
            }
            PlugModel plugModel = plugProjectModel.Plugs.FirstOrDefault(m => m.Name == plugName);
            if (plugModel == null)
            {
                plugModel = new PlugModel
                {
                    Name = plugName
                };
                plugProjectModel.Plugs.Add(plugModel);
            }
            #endregion
            plugModel.ExcuteDomainNames.Add(domainPlugModel.Domain.Name);
        }
        protected override void PlugExcuteAfter(DomainPlugModel domainPlugModel, AttributeModel attributeModel)
        {
        }
        protected override void AllPlugExcuteAfter()
        {
            if (_plugProjectModels == null || _plugProjectModels.Projects.Count <= 0) return;
            SaveToolsRessources(_plugTempDirectoryInfo);
            string modelJson = JsonConvert.SerializeObject(_plugProjectModels);
            SaveModelJson(_plugTempDirectoryInfo, "ModelData.json", modelJson);
            ProcessHelper processHelper = new ProcessHelper();
            processHelper.ErrorDataReceived += ProcessManager_ErrorDataReceived;
            string plugExe = Path.Combine(_plugTempDirectoryInfo.FullName, "MateralBasePlugBuild.dll");
            processHelper.ProcessStart("dotnet", $"{plugExe}");
            _plugTempDirectoryInfo?.Delete(true);
            if (_plugErrorMessages == null || _plugErrorMessages.Length == 0) return;
            throw new VSIXException("插件生成失败\r\n" + _plugErrorMessages.ToString());
        }
        private void ProcessManager_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Data)) return;
            if (_plugErrorMessages == null) return;
            _plugErrorMessages.AppendLine(e.Data);
        }
        /// <summary>
        /// 保存模型Json
        /// </summary>
        /// <param name="rootDirectoryInfo"></param>
        /// <param name="fileName"></param>
        /// <param name="dataJson"></param>
        private static void SaveModelJson(DirectoryInfo rootDirectoryInfo, string fileName, string dataJson)
        {
            if (!rootDirectoryInfo.Exists) rootDirectoryInfo.Create();
            string filePath = Path.Combine(rootDirectoryInfo.FullName, fileName);
            if (File.Exists(filePath)) File.Delete(filePath);
            File.WriteAllText(filePath, dataJson);
        }
        /// <summary>
        /// 保存资源
        /// </summary>
        /// <param name="rootDirectoryInfo"></param>
        /// <param name="dllName"></param>
        private static void SaveToolsRessources(DirectoryInfo rootDirectoryInfo)
        {
            string[] resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            resourceNames = resourceNames.Where(m => m.StartsWith(_resourceStart)).ToArray();
            foreach (string ressourceName in resourceNames)
            {
                SaveRessource(rootDirectoryInfo, ressourceName);
            }
        }
        /// <summary>
        /// 文件夹名称组
        /// </summary>
        private readonly static string[] resourceDirectoryNames = new[]
        {
            "cs","de","es","fr","it","ja","ko","pl","pt-BR","ru","zh-Hans","zh-Hant",
        };
        /// <summary>
        /// 保存资源
        /// </summary>
        /// <param name="rootDirectoryInfo"></param>
        /// <param name="dllName"></param>
        private static void SaveRessource(DirectoryInfo rootDirectoryInfo, string resourceName)
        {
            if (!resourceName.StartsWith(_resourceStart)) return;
            string temp = resourceName.Substring(_resourceStart.Length + 1);
            string[] temps = temp.Split('.');
            string directoryPath;
            string fileName;
            if (resourceDirectoryNames.Contains(temps[0]))
            {
                directoryPath = temps[0];
                fileName = temp.Substring(temps[0].Length + 1);
            }
            else
            {
                directoryPath = string.Empty;
                fileName = temp;
            }
            directoryPath = Path.Combine(rootDirectoryInfo.FullName, directoryPath);
            Stream dllStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            if (dllStream == null) return;
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            string savePath = Path.Combine(directoryInfo.FullName, fileName);
            if (File.Exists(savePath)) File.Delete(savePath);
            using (FileStream fileStream = new FileStream(savePath, FileMode.OpenOrCreate))
            {
                dllStream.CopyTo(fileStream);
                fileStream.Flush();
            }
            dllStream.Close();
            dllStream.Dispose();
        }
        #endregion
        #region 构建相关
        /// <summary>
        /// 填充Domain
        /// </summary>
        /// <param name="domainProject"></param>
        private void FillDoamins(Project domainProject)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            FillDomains(domainProject.ProjectItems, DomainProject.RootPath);
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
                    Domains.Add(domainModel);
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
            if (project == null || project.GetType().Name != "OAProject") return;
            ThreadHelper.ThrowIfNotOnUIThread();
            if (project.Name == $"{DomainProject.PrefixName}.{DomainProject.ProjectName}.Common")
            {
                CommonProject = new VSIXProjectModel(project);
            }
            else if (project.Name == $"{DomainProject.PrefixName}.{DomainProject.ProjectName}.EFRepository")
            {
                EFRepositoryProject = new VSIXProjectModel(project);
            }
            else if (project.Name == $"{DomainProject.PrefixName}.{DomainProject.ProjectName}.DataTransmitModel")
            {
                DataTransmitModelProject = new VSIXProjectModel(project);
            }
            else if (project.Name == $"{DomainProject.PrefixName}.{DomainProject.ProjectName}.Services")
            {
                ServicesProject = new VSIXProjectModel(project);
            }
            else if (project.Name == $"{DomainProject.PrefixName}.{DomainProject.ProjectName}.ServiceImpl")
            {
                ServiceImplProject = new VSIXProjectModel(project);
            }
            else if (project.Name == $"{DomainProject.PrefixName}.{DomainProject.ProjectName}.PresentationModel")
            {
                PresentationModelProject = new VSIXProjectModel(project);
            }
            else if (project.Name == $"{DomainProject.PrefixName}.{DomainProject.ProjectName}.WebAPI")
            {
                WebAPIProject = new VSIXProjectModel(project);
            }
            else if (project.Name == $"{DomainProject.PrefixName}.{DomainProject.ProjectName}.Enums")
            {
                EnumsProject = new VSIXProjectModel(project);
                FillEnums(project);
            }
            else
            {
                OtherProjects.Add(new VSIXProjectModel(project));
            }
        }
        /// <summary>
        /// 填充枚举
        /// </summary>
        /// <param name="enumProject"></param>
        private void FillEnums(Project enumProject)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            FillEnums(enumProject.ProjectItems, EnumsProject.RootPath);
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
                    Enums.Add(enumModel);
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
        /// 创建项目文件
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="codeContent"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private VSIXProjectModel CreateProjectFile(Solution solution, StringBuilder codeContent, string name)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            string directoryPath = Path.Combine(DomainProject.DiskDirectoryPath, name);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string filePath = codeContent.SaveFile(directoryPath, $"{name}.csproj");
            Project project = solution.AddFromFile(filePath, false);
            return new VSIXProjectModel(project);
        }
        /// <summary>
        /// 创建公共项目文件
        /// </summary>
        /// <param name="solution">解决方案对象</param>
        private VSIXProjectModel CreateCommonProjectFile(Solution solution)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"<Project Sdk=\"Microsoft.NET.Sdk\">");
            codeContent.AppendLine($"\t<PropertyGroup>");
            codeContent.AppendLine($"\t\t<TargetFrameworks>net6.0;net8.0</TargetFrameworks>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.Common\" Version=\"*\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"</Project>");
            return CreateProjectFile(solution, codeContent, $"{DomainProject.PrefixName}.{DomainProject.ProjectName}.Common");
        }
        /// <summary>
        /// 创建数据传输模型项目文件
        /// </summary>
        /// <param name="solution">解决方案对象</param>
        private VSIXProjectModel CreateEFRepositoryProjectFile(Solution solution)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"<Project Sdk=\"Microsoft.NET.Sdk\">");
            codeContent.AppendLine($"\t<PropertyGroup>");
            codeContent.AppendLine($"\t\t<TargetFrameworks>net6.0;net8.0</TargetFrameworks>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.EFRepository\" Version=\"*\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{DomainProject.Namespace}\\{DomainProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"</Project>");
            return CreateProjectFile(solution, codeContent, $"{DomainProject.PrefixName}.{DomainProject.ProjectName}.EFRepository");
        }
        /// <summary>
        /// 创建数据传输模型项目文件
        /// </summary>
        /// <param name="solution">解决方案对象</param>
        private VSIXProjectModel CreateDataTransmitModelProjectFile(Solution solution)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"<Project Sdk=\"Microsoft.NET.Sdk\">");
            codeContent.AppendLine($"\t<PropertyGroup>");
            codeContent.AppendLine($"\t\t<TargetFrameworks>net6.0;net8.0</TargetFrameworks>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t\t<GenerateDocumentationFile>True</GenerateDocumentationFile>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.DataTransmitModel\" Version=\"*\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            if (EnumsProject != null)
            {
                codeContent.AppendLine($"\t<ItemGroup>");
                codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{EnumsProject.Namespace}\\{EnumsProject.Namespace}.csproj\" />");
                codeContent.AppendLine($"\t</ItemGroup>");
            }
            codeContent.AppendLine($"</Project>");
            return CreateProjectFile(solution, codeContent, $"{DomainProject.PrefixName}.{DomainProject.ProjectName}.DataTransmitModel");
        }
        /// <summary>
        /// 创建服务项目文件
        /// </summary>
        /// <param name="solution">解决方案对象</param>
        private VSIXProjectModel CreateServicesProjectFile(Solution solution)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"<Project Sdk=\"Microsoft.NET.Sdk\">");
            codeContent.AppendLine($"\t<PropertyGroup>");
            codeContent.AppendLine($"\t\t<TargetFrameworks>net6.0;net8.0</TargetFrameworks>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.Services\" Version=\"*\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{CommonProject.Namespace}\\{CommonProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{DataTransmitModelProject.Namespace}\\{DataTransmitModelProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"</Project>");
            return CreateProjectFile(solution, codeContent, $"{DomainProject.PrefixName}.{DomainProject.ProjectName}.Services");
        }
        /// <summary>
        /// 创建服务实现项目文件
        /// </summary>
        /// <param name="solution">解决方案对象</param>
        private VSIXProjectModel CreateServiceImplProjectFile(Solution solution)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"<Project Sdk=\"Microsoft.NET.Sdk\">");
            codeContent.AppendLine($"\t<PropertyGroup>");
            codeContent.AppendLine($"\t\t<TargetFrameworks>net6.0;net8.0</TargetFrameworks>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.ServiceImpl\" Version=\"*\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{DomainProject.Namespace}\\{DomainProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{ServicesProject.Namespace}\\{ServicesProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"</Project>");
            return CreateProjectFile(solution, codeContent, $"{DomainProject.PrefixName}.{DomainProject.ProjectName}.ServiceImpl");
        }
        /// <summary>
        /// 创建表现模型项目文件
        /// </summary>
        /// <param name="solution">解决方案对象</param>
        private VSIXProjectModel CreatePresentationModelProjectFile(Solution solution)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"<Project Sdk=\"Microsoft.NET.Sdk\">");
            codeContent.AppendLine($"\t<PropertyGroup>");
            codeContent.AppendLine($"\t\t<TargetFrameworks>net6.0;net8.0</TargetFrameworks>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t\t<GenerateDocumentationFile>True</GenerateDocumentationFile>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.PresentationModel\" Version=\"*\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            if (EnumsProject != null)
            {
                codeContent.AppendLine($"\t<ItemGroup>");
                codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{EnumsProject.Namespace}\\{EnumsProject.Namespace}.csproj\" />");
                codeContent.AppendLine($"\t</ItemGroup>");
            }
            codeContent.AppendLine($"</Project>");
            return CreateProjectFile(solution, codeContent, $"{DomainProject.PrefixName}.{DomainProject.ProjectName}.PresentationModel");
        }
        /// <summary>
        /// 创建WebAPI项目文件
        /// </summary>
        /// <param name="solution">解决方案对象</param>
        private VSIXProjectModel CreateWebAPIProjectFile(Solution solution)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"<Project Sdk=\"Microsoft.NET.Sdk.Web\">");
            codeContent.AppendLine($"\t<PropertyGroup>");
            codeContent.AppendLine($"\t\t<TargetFrameworks>net6.0;net8.0</TargetFrameworks>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t\t<GenerateDocumentationFile>True</GenerateDocumentationFile>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Microsoft.EntityFrameworkCore.Tools\" Version=\"7.0.2\">");
            codeContent.AppendLine($"\t\t\t<PrivateAssets>all</PrivateAssets>");
            codeContent.AppendLine($"\t\t\t<IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>");
            codeContent.AppendLine($"\t\t</PackageReference>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.WebAPI\" Version=\"*\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{PresentationModelProject.Namespace}\\{PresentationModelProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{ServiceImplProject.Namespace}\\{ServiceImplProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{EFRepositoryProject.Namespace}\\{EFRepositoryProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"</Project>");
            return CreateProjectFile(solution, codeContent, $"{DomainProject.PrefixName}.{DomainProject.ProjectName}.WebAPI");
        }
        #endregion
    }
}
