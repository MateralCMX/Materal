using MateralPublish.Extensions;
using MateralPublish.Helper;
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
            NugetServerHelper.NugetDirectoryInfo = Path.Combine(ProjectDirectoryInfo.FullName, "Nupkgs").GetNewDirectoryInfo();
            PublishHelper.PublishDirectoryInfo = Path.Combine(ProjectDirectoryInfo.FullName, "Publish").GetNewDirectoryInfo();
            Type baseProjectType = typeof(BaseProjectModel);
            List<Type> allProjectTypes = baseProjectType.Assembly.GetTypes().Where(m => !m.IsAbstract && m.IsAssignableTo(baseProjectType)).ToList();
            _projects = [];
            foreach (Type projectType in allProjectTypes)
            {
                ConstructorInfo? constructorInfo = projectType.GetConstructor([typeof(string)]) ?? throw new MateralPublishException("未找到构造函数");
                object projectObj = constructorInfo.Invoke(new[] { ProjectDirectoryInfo.FullName });
                if (projectObj is not BaseProjectModel projectModel) throw new MateralPublishException("类型不是BaseProjectModel");
                _projects.Add(projectModel);
            }
            _projects = [.. _projects.OrderBy(m => m.Level).ThenBy(m => m.Index).ThenBy(m => m.Name)];
        }
        /// <summary>
        /// 获得下一个版本号
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetNextVersionAsync()
        {
            string nowVersion = await GetVersionAsync();
            string[] versions = nowVersion.Split('.');
            int lastVersionNumber = Convert.ToInt32(versions.Last());
            versions[^1] = (lastVersionNumber + 1).ToString();
            string nextVersion = string.Join('.', versions);
            return nextVersion;
        }
        /// <summary>
        /// 获得版本号
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetVersionAsync()
        {
            ProjectModels.Level0.MateralProjectModel materalProjectModel = _projects.OfType<ProjectModels.Level0.MateralProjectModel>().FirstOrDefault() ?? throw new MateralPublishException("未找到版本号");
            string nowVersion = await materalProjectModel.GetNowVersionAsync();
            return nowVersion;
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
                try
                {
#if DEBUG
                    //if (project is not ProjectModels.Level0.MateralProjectModel) continue;
                    //if (project is not ProjectModels.Level0.ToolsProjectModel) continue;
                    //if (project is not ProjectModels.Level1.LoggerProjectModel) continue;
                    //if (project is not ProjectModels.Level2.TFMSProjectModel) continue;
                    //if (project is not ProjectModels.Level2.TTAProjectModel) continue;
                    //if (project is not ProjectModels.Level3.OscillatorProjectModel) continue;
                    //if (project is not ProjectModels.Level4.BaseCoreProjectModel) continue;
                    //if (project is not ProjectModels.Level4.MergeBlockProjectModel) continue;
                    //if (project is not ProjectModels.Level5.GatewayProjectModel) continue;
                    if (project is not ProjectModels.Level5.RCProjectModel) continue;
#endif
                    await project.PublishAsync(version);
                }
                catch (Exception ex)
                {
                    ConsoleHelper.WriteLine($"项目{project.Name}发布失败:{ex.Message}", ConsoleColor.Red);
                }
            }
            if (uploadNuget)
            {
                await NugetServerHelper.UploadNugetPackagesAsync();
            }
            ConsoleHelper.WriteLine("正在清理临时文件...");
        }
        /// <summary>
        /// 获得项目文件夹信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static DirectoryInfo GetProjectDirectoryInfo(string path)
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
