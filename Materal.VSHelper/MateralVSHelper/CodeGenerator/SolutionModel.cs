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
                else
                {
                    _otherProjects.Add(new ProjectModel(project, _rootPath));
                }
            }
        }
        public void FillDomains(ProjectItems projectItems, string path)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            foreach (ProjectItem item in projectItems)
            {
                if (item.ProjectItems.Count > 0)
                {
                    var itemPath = Path.Combine(path, item.Name);
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
            _domainProject.CreateFiles(_domains);
            //创建DB
            //创建仓储接口
            //创建仓储实现
            //创建Add模型
            //创建Edit模型
            //创建Query模型
            //创建服务接口
            //创建服务实现
        }
    }
}
