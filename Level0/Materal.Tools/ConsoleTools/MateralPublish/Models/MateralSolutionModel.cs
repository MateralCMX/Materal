using Materal.Tools.Helper;
using MateralPublish.Extensions;
using MateralPublish.Models.ProjectModels.Level0;
using System.Reflection;

namespace MateralPublish.Models
{
    public class MateralSolutionModel
    {
        /// <summary>
        /// 项目文件夹信息
        /// </summary>
        public DirectoryInfo ProjectDirectoryInfo { get; }
        /// <summary>
        /// 发布文件夹信息
        /// </summary>
        public DirectoryInfo PublishDirectoryInfo { get; }
        /// <summary>
        /// 项目模型列表
        /// </summary>
        private readonly List<BaseProjectModel> _projects;
        /// <summary>
        /// Materal解决方案模型
        /// </summary>
        /// <param name="path"></param>
        public MateralSolutionModel(string path)
        {
            ProjectDirectoryInfo = GetProjectDirectoryInfo(path);
            NugetServerHelper.NugetDirectoryInfo = Path.Combine(ProjectDirectoryInfo.FullName, "nupkgs").GetNewDirectoryInfo();
            PublishDirectoryInfo = Path.Combine(ProjectDirectoryInfo.FullName, "publish").GetNewDirectoryInfo();
            Type baseProjectType = typeof(BaseProjectModel);
            List<Type> allProjectTypes = baseProjectType.Assembly.GetTypes().Where(m => !m.IsAbstract && m.IsAssignableTo(baseProjectType)).ToList();
            _projects = new();
            foreach (Type projectType in allProjectTypes)
            {
                ConstructorInfo? constructorInfo = projectType.GetConstructor(new[] { typeof(string) }) ?? throw new MateralPublishException("未找到构造函数");
                object projectObj = constructorInfo.Invoke(new[] { ProjectDirectoryInfo.FullName });
                if (projectObj is not BaseProjectModel projectModel) throw new MateralPublishException("类型不是BaseProjectModel");
                _projects.Add(projectModel);
            }
            _projects = _projects.OrderBy(m => m.Level).ThenBy(m => m.Index).ThenBy(m=>m.Name).ToList();
        }
        /// <summary>
        /// 获得下一个版本号
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetNextVersionAsync()
        {
            foreach (BaseProjectModel project in _projects)
            {
                if (project is not MateralProjectModel materalProject) continue;
                string nowVersion = await materalProject.GetNowVersionAsync();
                string[] versions = nowVersion.Split('.');
                int lastVersionNumber = Convert.ToInt32(versions.Last());
                versions[^1] = (lastVersionNumber + 1).ToString();
                string nextVersion = string.Join('.', versions);
                return nextVersion;
            }
            throw new MateralPublishException("未找到版本号");
        }
        /// <summary>
        /// 获得版本号
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetVersionAsync()
        {
            foreach (BaseProjectModel project in _projects)
            {
                if (project is not MateralProjectModel materalProject) continue;
                string nowVersion = await materalProject.GetNowVersionAsync();
                return nowVersion;
            }
            throw new MateralPublishException("未找到版本号");
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="version"></param>
        /// <param name="uploadNuget"></param>
        public async Task PublishAsync(string version, bool uploadNuget)
        {
            foreach (BaseProjectModel project in _projects)
            {
                await project.PublishAsync(PublishDirectoryInfo, version);
            }
            if (uploadNuget)
            {
                await NugetServerHelper.UploadNugetPackagesAsync();
            }
            ConsoleHelper.WriteLine("正在清理临时文件...");
            ConsoleHelper.WriteLine($"删除文件夹{PublishDirectoryInfo.FullName}");
            PublishDirectoryInfo.Delete(true);
        }
        /// <summary>
        /// 获得项目文件夹信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private DirectoryInfo GetProjectDirectoryInfo(string path)
        {
            path ??= Environment.CurrentDirectory;
            DirectoryInfo result = new(path);
            if (result.Name != "Materal" || !result.GetFiles().Any(m => m.Name == "LICENSE"))
            {
                if (result.Parent == null) throw new Exception("未找到项目根路径");
                result = GetProjectDirectoryInfo(result.Parent.FullName);
            }
            return result;
        }
    }
}
