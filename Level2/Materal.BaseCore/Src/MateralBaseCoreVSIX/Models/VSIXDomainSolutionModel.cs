using EnvDTE;
using Materal.BaseCore.CodeGenerator;
using Materal.BaseCore.CodeGenerator.Extensions;
using Materal.BaseCore.CodeGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MateralBaseCoreVSIX.Models
{
    public class VSIXDomainSolutionModel : DomainSolutionModel
    {
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
        private Dictionary<string, Assembly> buildSuccessAssembly = new Dictionary<string, Assembly>();
        private List<string> removeFilePaths = new List<string>();
        public static bool firstBuild = true;
        protected override void GetPlugBefore()
        {
            if (!firstBuild) return;
            Assembly defaultAssembly = typeof(DomainSolutionModel).Assembly;
            string savePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, defaultAssembly.GetName().Name + ".dll");
            File.Copy(defaultAssembly.Location, savePath, true);
            firstBuild = false;
        }
        protected override IMateralBaseCoreCodeGeneratorPlug GetPlug(string projectPath, string className)
        {
            Assembly plugAssembly;
            if (!buildSuccessAssembly.ContainsKey(className))
            {
                DirectoryInfo projectDirectoryInfo = new DirectoryInfo(projectPath);
                if (!projectDirectoryInfo.Exists) throw new CodeGeneratorException("插件项目不存在");
                string classPath = Path.Combine(projectDirectoryInfo.FullName, className);
                FileInfo classFileInfo = new FileInfo(classPath);
                if (!classFileInfo.Exists) throw new CodeGeneratorException("插件类不存在");
                string dllLibPath = Path.Combine(projectDirectoryInfo.FullName, "Libs");
                string dllPath = BuildDLLFile(classFileInfo.FullName, dllLibPath);
                plugAssembly = Assembly.Load(File.ReadAllBytes(dllPath));
                buildSuccessAssembly.Add(className, plugAssembly);
            }
            else
            {
                plugAssembly = buildSuccessAssembly[className];
            }
            Type[] types = plugAssembly.GetTypes();
            if (className.EndsWith(".cs"))
            {
                className = className.Substring(0, className.Length - 3);
            }
            Type type = types.FirstOrDefault(m => m.Name == className);
            if (type == null) throw new Exception("未找到插件类");
            if (!type.IsAssignableTo(typeof(IMateralBaseCoreCodeGeneratorPlug))) throw new Exception("插件类必须实现IMateralBaseCoreCodeGeneratorPlug");
            IMateralBaseCoreCodeGeneratorPlug plug = type.Instantiation() as IMateralBaseCoreCodeGeneratorPlug;
            return plug;
        }
        protected override void PlugExcuted()
        {
            List<string> directoryPaths = new List<string>();
            foreach (string removeFilePath in removeFilePaths)
            {
                if(File.Exists(removeFilePath)) File.Delete(removeFilePath);
                directoryPaths.Add(Path.GetDirectoryName(removeFilePath));
            }
            directoryPaths = directoryPaths.Distinct().OrderBy(m => m.Length).ToList();
            foreach (string directoryPath in directoryPaths)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
                if (!directoryInfo.Exists) continue;
                if (directoryInfo.GetFiles().Length > 0 || directoryInfo.GetDirectories().Length > 0) continue;
                directoryInfo.Delete();
            }
        }
        /// <summary>
        /// 保存DLL
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="dllName"></param>
        private void SaveDLL(DirectoryInfo directoryInfo, string dllName)
        {
            Stream dllStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"MateralBaseCoreVSIX.Libs.{dllName}.dll");
            string savePath = Path.Combine(directoryInfo.FullName, $"{dllName}.dll");
            using (FileStream fileStream = new FileStream(savePath, FileMode.OpenOrCreate))
            {
                dllStream.CopyTo(fileStream);
                fileStream.Flush();
            }
            dllStream.Close();
            dllStream.Dispose();
            removeFilePaths.Add(savePath);
        }
        /// <summary>
        /// 构建DLL文件
        /// </summary>
        /// <returns></returns>
        private string BuildDLLFile(string filePath, string dllLibPath)
        {
            FileInfo csFileInfo = new FileInfo(filePath);
            if (!csFileInfo.Exists) throw new Exception(".cs文件不存在");
            DirectoryInfo dllLibDirectoryInfo = new DirectoryInfo(dllLibPath);
            if (!dllLibDirectoryInfo.Exists)
            {
                dllLibDirectoryInfo.Create();
            }
            string dllFileName = csFileInfo.Name.Substring(0, csFileInfo.Name.LastIndexOf('.')) + ".dll";
            string dllFilePath = Path.Combine(dllLibPath ?? "", dllFileName);
            FileInfo dllFileInfo = new FileInfo(dllFilePath);
            if (dllFileInfo.Exists) dllFileInfo.Delete();
            List<string> usingAssemblies = new List<string>()
            {
                typeof(DomainSolutionModel).Assembly.Location,
                typeof(object).Assembly.Location
            };
            SaveDLL(dllLibDirectoryInfo, "netstandard");
            SaveDLL(dllLibDirectoryInfo, "System.Runtime");
            foreach (FileInfo fileInfo in dllLibDirectoryInfo.GetFiles())
            {
                if (fileInfo.Extension.ToUpper() != ".DLL") continue;
                Assembly assembly = Assembly.Load(File.ReadAllBytes(fileInfo.FullName));
                usingAssemblies.Add(assembly.Location);
            }
            List<MetadataReference> refs = usingAssemblies.Select(m => MetadataReference.CreateFromFile(m)).ToList<MetadataReference>();
            var cSharpCompilation = CSharpCompilation
                            .Create(dllFileName)
                            .WithOptions(new CSharpCompilationOptions(
                                OutputKind.DynamicallyLinkedLibrary,
                                usings: null,
                                optimizationLevel: OptimizationLevel.Release,
                                checkOverflow: false,
                                allowUnsafe: true,
                                platform: Platform.AnyCpu,
                                warningLevel: 4,
                                xmlReferenceResolver: null
                            ))
                            .AddReferences(refs);
            string soureCode = File.ReadAllText(csFileInfo.FullName);
            cSharpCompilation = cSharpCompilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText(soureCode));
            EmitResult emitResult = cSharpCompilation.Emit(dllFilePath);
            if (!emitResult.Success)
            {
                if (File.Exists(dllFilePath)) File.Delete(dllFilePath);
                StringBuilder errorMessage = new StringBuilder();
                foreach (Diagnostic item in emitResult.Diagnostics)
                {
                    errorMessage.AppendLine(item.ToString());
                }
                PlugExcuted();
                throw new Exception(errorMessage.ToString());
            }
            removeFilePaths.Add(dllFilePath);
            return dllFilePath;
        }
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
            if (project == null && project.GetType().Name == "OAProject") return;
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
            codeContent.AppendLine($"\t\t<TargetFramework>net6.0</TargetFramework>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.Common\" Version=\"0.2.4\" />");
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
            codeContent.AppendLine($"\t\t<TargetFramework>net6.0</TargetFramework>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.EFRepository\" Version=\"0.2.4\" />");
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
            codeContent.AppendLine($"\t\t<TargetFramework>net6.0</TargetFramework>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t\t<GenerateDocumentationFile>True</GenerateDocumentationFile>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.DataTransmitModel\" Version=\"0.2.4\" />");
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
            codeContent.AppendLine($"\t\t<TargetFramework>net6.0</TargetFramework>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.Services\" Version=\"0.2.4\" />");
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
            codeContent.AppendLine($"\t\t<TargetFramework>net6.0</TargetFramework>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.ServiceImpl\" Version=\"0.2.4\" />");
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
            codeContent.AppendLine($"\t\t<TargetFramework>net6.0</TargetFramework>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t\t<GenerateDocumentationFile>True</GenerateDocumentationFile>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.PresentationModel\" Version=\"0.2.4\" />");
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
            codeContent.AppendLine($"\t\t<TargetFramework>net6.0</TargetFramework>");
            codeContent.AppendLine($"\t\t<ImplicitUsings>enable</ImplicitUsings>");
            codeContent.AppendLine($"\t\t<Nullable>enable</Nullable>");
            codeContent.AppendLine($"\t\t<GenerateDocumentationFile>True</GenerateDocumentationFile>");
            codeContent.AppendLine($"\t</PropertyGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Microsoft.EntityFrameworkCore.Tools\" Version=\"7.0.2\">");
            codeContent.AppendLine($"\t\t\t<PrivateAssets>all</PrivateAssets>");
            codeContent.AppendLine($"\t\t\t<IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>");
            codeContent.AppendLine($"\t\t</PackageReference>");
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.WebAPI\" Version=\"0.2.4\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"\t<ItemGroup>");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{PresentationModelProject.Namespace}\\{PresentationModelProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{ServiceImplProject.Namespace}\\{ServiceImplProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t\t<ProjectReference Include=\"..\\{EFRepositoryProject.Namespace}\\{EFRepositoryProject.Namespace}.csproj\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"</Project>");
            return CreateProjectFile(solution, codeContent, $"{DomainProject.PrefixName}.{DomainProject.ProjectName}.WebAPI");
        }
    }
}
