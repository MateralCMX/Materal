﻿using EnvDTE;
using Materal.BaseCore.CodeGenerator.Extensions;
using Materal.BaseCore.CodeGenerator.Models;
using Microsoft.VisualStudio.Shell;
using System.IO;
using System.Linq;
using System.Text;

namespace MateralBaseCoreVSIX.Models
{
    public class VSIXWebAPISolutionModel : WebAPISolutionModel
    {
        public VSIXWebAPISolutionModel(Solution solution, Project webAPIProject)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            WebAPIProject = new VSIXProjectModel(webAPIProject);
            FillControllers(webAPIProject);
            FillProjects(solution.Projects);
            if (HttpClientProject == null)
            {
                HttpClientProject = CreateHttpClientProjectFile(solution);
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
            if (project.Name == $"{WebAPIProject.PrefixName}.{WebAPIProject.ProjectName}.HttpClient")
            {
                HttpClientProject = new VSIXProjectModel(project);
            }
        }
        /// <summary>
        /// 填充Controller
        /// </summary>
        /// <param name="webAPIProject"></param>
        private void FillControllers(Project webAPIProject)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            FillControllers(webAPIProject.ProjectItems, WebAPIProject.RootPath);
        }
        /// <summary>
        /// 填充Controller
        /// </summary>
        /// <param name="projectItems"></param>
        /// <param name="path"></param>
        private void FillControllers(ProjectItems projectItems, string path)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            foreach (ProjectItem item in projectItems)
            {
                var itemPath = Path.Combine(path, item.Name);
                if (item.ProjectItems != null && item.ProjectItems.Count > 0)
                {
                    FillControllers(item.ProjectItems, itemPath);
                }
                else if (item.Name.Contains("Controller") && item.Name != "Controllers")
                {
                    var controllerModel = GetControllerModelOrNull(item, path);
                    if (controllerModel == null) continue;
                    Controllers.Add(controllerModel);
                }
            }
        }        
        /// <summary>
        /// 获得Controller模型
        /// </summary>
        /// <param name="projectItem"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private ControllerModel GetControllerModelOrNull(ProjectItem projectItem, string path)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (!projectItem.Name.Contains("Controller") || projectItem.Name == "Controllers") return null;
            string filePath = Path.Combine(path, projectItem.Name);
            if (!File.Exists(filePath)) return null;
            string[] codes = File.ReadAllLines(filePath);
            for (int i = 0; i < codes.Length; i++)
            {
                string classCode = codes[i];
                int publicIndex = classCode.IndexOf("public ");
                if (publicIndex <= 0) continue;
                int classIndex = classCode.IndexOf(" class ");
                if (classIndex <= 0) continue;
                int controllerIndex = classCode.IndexOf("Controller");
                if (controllerIndex <= 0) continue;
                int enumsControllerIndex = classCode.IndexOf("EnumsController");
                if (enumsControllerIndex >= 0) continue;
                ControllerModel result = null;
                string controllerName = ControllerModel.GetControllerName(classCode);
                if (!string.IsNullOrWhiteSpace(controllerName))
                {
                    result = Controllers.FirstOrDefault(m => m.Name == controllerName);
                }
                if(result == null)
                {
                    return new ControllerModel(codes, i);
                }
                else
                {
                    result.Append(codes, i);
                    return null;
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
            string directoryPath = Path.Combine(WebAPIProject.DiskDirectoryPath, name);
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
        private VSIXProjectModel CreateHttpClientProjectFile(Solution solution)
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
            codeContent.AppendLine($"\t\t<PackageReference Include=\"Materal.BaseCore.HttpClient\" Version=\"*\" />");
            codeContent.AppendLine($"\t</ItemGroup>");
            codeContent.AppendLine($"</Project>");
            return CreateProjectFile(solution, codeContent, $"{WebAPIProject.PrefixName}.{WebAPIProject.ProjectName}.HttpClient");
        }
        #endregion
    }
}
