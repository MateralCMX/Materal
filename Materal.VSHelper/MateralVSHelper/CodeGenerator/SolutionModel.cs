using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System.Collections.Generic;
using System.IO;

namespace MateralVSHelper.CodeGenerator
{
    public class SolutionModel
    {
        /// <summary>
        /// 根路径
        /// </summary>
        private readonly string _rootPath;
        private readonly List<DomainModel> _domains = new List<DomainModel>();
        private readonly ProjectModel _domainProject;
        private readonly ProjectModel _webAPIProject;
        private readonly ProjectModel _servicesProject;
        private readonly ProjectModel _serviceImplProject;
        private readonly ProjectModel _efRepositoryProject;
        private readonly ProjectModel _dataTransmitModelProject;
        private readonly ProjectModel _presentationModelProject;
        private readonly List<ProjectModel> _otherProjects = new List<ProjectModel>();
        public SolutionModel(Solution solution)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            _rootPath = Path.GetDirectoryName(solution.FullName);
            int oldDomainCount = 0;
            foreach (Project project in solution.Projects)
            {
                string itemPath = Path.Combine(_rootPath, project.Name);
                FillDomains(project.ProjectItems, itemPath);
                if(_domains.Count != oldDomainCount)
                {
                    if (oldDomainCount != 0) throw new VSHelperException("发现多个Domain项目，请合并");
                    oldDomainCount = _domains.Count;
                    _domainProject = new ProjectModel(project, _rootPath);
                }
                if (project.Name.EndsWith(".DataTransmitModel"))
                {
                    _dataTransmitModelProject = new ProjectModel(project, _rootPath);
                }
                else if (project.Name.EndsWith(".PresentationModel"))
                {
                    _presentationModelProject = new ProjectModel(project, _rootPath);
                }
                else if (project.Name.EndsWith(".WebAPI"))
                {
                    _presentationModelProject = new ProjectModel(project, _rootPath);
                }
                else if (project.Name.EndsWith(".Services"))
                {
                    _presentationModelProject = new ProjectModel(project, _rootPath);
                }
                else if (project.Name.EndsWith(".ServiceImpl"))
                {
                    _presentationModelProject = new ProjectModel(project, _rootPath);
                }
                else if (project.Name.EndsWith("EFRepository"))
                {
                    _presentationModelProject = new ProjectModel(project, _rootPath);
                }
            }
            if (_webAPIProject == null) _webAPIProject = _domainProject;
            if (_servicesProject == null) _servicesProject = _domainProject;
            if (_serviceImplProject == null) _serviceImplProject = _domainProject;
            if (_efRepositoryProject == null) _efRepositoryProject = _domainProject;
            if (_dataTransmitModelProject == null) _dataTransmitModelProject = _domainProject;
            if (_presentationModelProject == null) _presentationModelProject = _domainProject;
        }
        public void FillDomains(ProjectItems projectItems, string path)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            foreach (ProjectItem item in projectItems)
            {
                var itemPath = Path.Combine(path, item.Name);
                if (item.Name == "MCG")
                {
                    if (Directory.Exists(itemPath))
                    {
                        Directory.Delete(itemPath, true);
                    }
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
            if(!File.Exists(filePath)) return null;
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
        /// 创建代码文件
        /// </summary>
        public void CreateCodeFiles()
        {
            _domainProject.CreateDomainFiles(_domains);
            _webAPIProject.CreateWebAPIFiles(_domains);
            _servicesProject.CreateServicesFiles(_domains);
            _serviceImplProject.CreateServiceImplFiles(_domains);
            _efRepositoryProject.CreateEFRepositoryFiles(_domains);
            _dataTransmitModelProject.CreateDataTransmitModelFiles(_domains);
            _presentationModelProject.CreatePresentationModelFiles(_domains);
        }
    }
}
