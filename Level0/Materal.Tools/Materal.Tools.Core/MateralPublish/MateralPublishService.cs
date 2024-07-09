namespace Materal.Tools.Core.MateralPublish
{
    /// <summary>
    /// Materal发布服务
    /// </summary>
    public class MateralPublishService : IMateralPublishService
    {
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
                ConstructorInfo? constructorInfo = projectType.GetConstructor([]) ?? throw new ToolsException("未找到构造函数");
                object projectObj = constructorInfo.Invoke([]);
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
        public async Task<string> GetNowVersionAsync(string projectPath)
        {
            DirectoryInfo projectDirectoryInfo = new(projectPath);
            if (!projectDirectoryInfo.Exists) throw new ToolsException($"\"{projectDirectoryInfo}\"文件夹不存在");
            string csprojFilePath = Path.Combine(projectDirectoryInfo.FullName, "Level0", "Materal", "Src", "Materal.Abstractions", "Materal.Abstractions.csproj");
            FileInfo csprojFileInfo = new(csprojFilePath);
            if (!csprojFileInfo.Exists) throw new ToolsException($"\"{csprojFileInfo.FullName}\"不存在");
            string[] codeContent = await File.ReadAllLinesAsync(csprojFileInfo.FullName);
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
        /// 发布
        /// </summary>
        /// <param name="projectPath"></param>
        /// <param name="version"></param>
        /// <param name="projects"></param>
        /// <param name="OnMessage"></param>
        /// <returns></returns>
        public async Task PublishAsync(string projectPath, string version, ICollection<IMateralProject> projects, Action<MessageLevel, string?>? OnMessage)
        {
            OnMessage?.Invoke(MessageLevel.Information, "开始发布...");
            DirectoryInfo projectDirectoryInfo = new(projectPath);
            DirectoryInfo nugetDirectoryInfo = Path.Combine(projectDirectoryInfo.FullName, "Nupkgs").GetNewDirectoryInfo();
            DirectoryInfo publishDirectoryInfo = Path.Combine(projectDirectoryInfo.FullName, "Publish").GetNewDirectoryInfo();
            foreach (IMateralProject project in projects)
            {
                OnMessage?.Invoke(MessageLevel.Information, $"开始发布{project.Name}...");
                await project.PublishAsync(projectDirectoryInfo, nugetDirectoryInfo, publishDirectoryInfo, version, OnMessage);
                OnMessage?.Invoke(MessageLevel.Information, $"{project.Name}发布完毕");
            }
            OnMessage?.Invoke(MessageLevel.Information, "发布完毕");
        }
    }
}
