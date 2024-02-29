using EnvDTE;
using Materal.BaseCore.CodeGenerator.Models;
using Microsoft.VisualStudio.Shell;
using System.IO;
using System.Linq;

namespace MateralBaseCoreVSIX.Models
{
    public class VSIXServicesSolutionModel : ServicesSolutionModel
    {
        public VSIXServicesSolutionModel(Solution solution, Project servicesProject)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            ServicesProject = new VSIXProjectModel(servicesProject);
            FillServices(servicesProject);
            FillProjects(solution.Projects);
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
            if (project.Name == $"{ServicesProject.PrefixName}.{ServicesProject.ProjectName}.WebAPI")
            {
                WebAPIProject = new VSIXProjectModel(project);
            }
        }
        /// <summary>
        /// 填充Service
        /// </summary>
        /// <param name="servicesProject"></param>
        private void FillServices(Project servicesProject)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            FillServices(servicesProject.ProjectItems, ServicesProject.RootPath);
        }
        /// <summary>
        /// 填充Service
        /// </summary>
        /// <param name="projectItems"></param>
        /// <param name="path"></param>
        private void FillServices(ProjectItems projectItems, string path)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            foreach (ProjectItem item in projectItems)
            {
                var itemPath = Path.Combine(path, item.Name);
                if (item.ProjectItems != null && item.ProjectItems.Count > 0)
                {
                    FillServices(item.ProjectItems, itemPath);
                }
                else if (item.Name.StartsWith("I") && item.Name.EndsWith("Service.cs"))
                {
                    var serviceModel = GetServiceModelOrNull(item, path);
                    if (serviceModel == null) continue;
                    Services.Add(serviceModel);
                }
            }
        }
        /// <summary>
        /// 获得Service模型
        /// </summary>
        /// <param name="projectItem"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private ServiceModel GetServiceModelOrNull(ProjectItem projectItem, string path)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (!projectItem.Name.StartsWith("I") || !projectItem.Name.EndsWith("Service.cs")) return null;
            string filePath = Path.Combine(path, projectItem.Name);
            if (!File.Exists(filePath)) return null;
            string[] codes = File.ReadAllLines(filePath);
            for (int i = 0; i < codes.Length; i++)
            {
                string classCode = codes[i];
                int publicIndex = classCode.IndexOf("public ");
                if (publicIndex <= 0) continue;
                int interfaceIndex = classCode.IndexOf(" interface ");
                if (interfaceIndex <= 0) continue;
                ServiceModel result = null;
                string serviceName = ServiceModel.GetServiceName(classCode);
                if (!string.IsNullOrWhiteSpace(serviceName))
                {
                    result = Services.FirstOrDefault(m => m.Name == serviceName);
                }
                if (result == null)
                {
                    return new ServiceModel(codes, i);
                }
                else
                {
                    result.Append(codes);
                    return null;
                }
            }
            return null;
        }
        #endregion
    }
}
