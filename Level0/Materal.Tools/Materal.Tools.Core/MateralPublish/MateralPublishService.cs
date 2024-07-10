using Microsoft.Extensions.Logging;

namespace Materal.Tools.Core.MateralPublish
{
    /// <summary>
    /// Materal发布服务
    /// </summary>
    public class MateralPublishService(ILoggerFactory? loggerFactory = null) : IMateralPublishService
    {
        private readonly ILogger<MateralPublishService>? _logger = loggerFactory?.CreateLogger<MateralPublishService>();
        /// <summary>
        /// 获得所有项目
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ToolsException"></exception>
        public ICollection<IMateralProject> GetAllProjects()
        {
            Type baseProjectType = typeof(IMateralProject);
            List<Type> allProjectTypes = baseProjectType.Assembly.GetTypes().Where(m => !m.IsAbstract && m.IsAssignableTo(baseProjectType)).ToList();
            List<IMateralProject> _projects = [];
            foreach (Type projectType in allProjectTypes)
            {
                ConstructorInfo? constructorInfo = projectType.GetConstructor([typeof(ILoggerFactory)]) ?? throw new ToolsException("未找到构造函数");
                object projectObj = constructorInfo.Invoke([loggerFactory]);
                if (projectObj is not IMateralProject projectModel) throw new ToolsException("类型不是IMateralProject");
                _projects.Add(projectModel);
            }
            _projects = [.. _projects.OrderBy(m => m.Level).ThenBy(m => m.Index).ThenBy(m => m.Name)];
            return _projects;
        }
        /// <summary>
        /// 获得当前版本号
        /// </summary>
        /// <param name="projectPath"></param>
        /// <returns></returns>
        /// <exception cref="ToolsException"></exception>
        public string GetNowVersion(string projectPath)
        {
            DirectoryInfo projectDirectoryInfo = GetMateralProjectRootPath(projectPath);
            if (!projectDirectoryInfo.Exists) throw new ToolsException($"\"{projectDirectoryInfo}\"文件夹不存在");
            string csprojFilePath = Path.Combine(projectDirectoryInfo.FullName, "Level0", "Materal", "Src", "Materal.Abstractions", "Materal.Abstractions.csproj");
            FileInfo csprojFileInfo = new(csprojFilePath);
            if (!csprojFileInfo.Exists) throw new ToolsException($"\"{csprojFileInfo.FullName}\"不存在");
            string[] codeContent = File.ReadAllLines(csprojFileInfo.FullName);
            const string versionStartCode = "<Version>";
            const string versionEndCode = "</Version>";
            foreach (string code in codeContent)
            {
                string tempCode = code.Trim();
                if (tempCode.StartsWith(versionStartCode))
                {
                    int endStartIndex = tempCode.IndexOf(versionEndCode);
                    string result = tempCode[versionStartCode.Length..endStartIndex];
                    return result;
                }
            }
            throw new ToolsException("未找到版本号");
        }
        /// <summary>
        /// 获得Materal项目根路径
        /// </summary>
        /// <param name="projectPath"></param>
        /// <returns></returns>
        /// <exception cref="ToolsException"></exception>
        public DirectoryInfo GetMateralProjectRootPath(string projectPath)
        {
            DirectoryInfo? projectDirectoryInfo = new(projectPath);
            if (!projectDirectoryInfo.Exists) throw new ToolsException($"\"{projectDirectoryInfo}\"文件夹不存在");
            string csprojFilePath = Path.Combine(projectDirectoryInfo.FullName, "Level0", "Materal", "Src", "Materal.Abstractions", "Materal.Abstractions.csproj");
            FileInfo csprojFileInfo = new(csprojFilePath);
            if (csprojFileInfo.Exists) return projectDirectoryInfo;
            projectDirectoryInfo = projectDirectoryInfo.Parent;
            if (projectDirectoryInfo is null) throw new ToolsException("未找到Materal项目根路径");
            csprojFilePath = Path.Combine(projectDirectoryInfo.FullName, "Level0", "Materal", "Src", "Materal.Abstractions", "Materal.Abstractions.csproj");
            csprojFileInfo = new(csprojFilePath);
            if (csprojFileInfo.Exists) return projectDirectoryInfo;
            projectDirectoryInfo = projectDirectoryInfo.Parent;
            if (projectDirectoryInfo is null) throw new ToolsException("未找到Materal项目根路径");
            csprojFilePath = Path.Combine(projectDirectoryInfo.FullName, "Level0", "Materal", "Src", "Materal.Abstractions", "Materal.Abstractions.csproj");
            csprojFileInfo = new(csprojFilePath);
            if (csprojFileInfo.Exists) return projectDirectoryInfo;
            throw new ToolsException("未找到Materal项目根路径");
        }
        /// <summary>
        /// 是Materal项目路径
        /// </summary>
        /// <param name="projectPath"></param>
        /// <returns></returns>
        /// <exception cref="ToolsException"></exception>
        public bool IsMateralProjectPath(string projectPath)
        {
            try
            {
                GetMateralProjectRootPath(projectPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="projectPath"></param>
        /// <param name="version"></param>
        /// <param name="projects"></param>
        /// <returns></returns>
        public async Task PublishAsync(string projectPath, string version, ICollection<IMateralProject> projects)
        {
            _logger?.LogInformation("开始发布...");
            ClearNugetPackages();
            DirectoryInfo projectDirectoryInfo = new(projectPath);
            foreach (IMateralProject project in projects)
            {
                _logger?.LogInformation($"开始发布{project.Name}...");
                await project.PublishAsync(projectDirectoryInfo, version);
                _logger?.LogInformation($"{project.Name}发布完毕");
            }
            _logger?.LogInformation("发布完毕");
        }
        private void ClearNugetPackages()
        {
            _logger?.LogInformation($"开始清理包缓存...");
            string nugetPackagesPath = Environment.GetEnvironmentVariable("NUGET_PACKAGES") ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".nuget", "packages");
            DirectoryInfo nugetDirectoryInfo = new(nugetPackagesPath);
            if (!nugetDirectoryInfo.Exists) return;
            DirectoryInfo[] directoryInfos = nugetDirectoryInfo.GetDirectories();
            foreach (DirectoryInfo directoryInfo in directoryInfos)
            {
                if (!directoryInfo.Name.StartsWith("materal.") && !directoryInfo.Name.StartsWith("rc.")) continue;
                _logger?.LogInformation($"删除包缓存:{directoryInfo.FullName}");
                directoryInfo.Delete(true);
            }
            _logger?.LogInformation($"包缓存清理完毕");
        }
    }
}
