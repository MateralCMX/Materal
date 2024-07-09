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
        /// <param name="projectPath"></param>
        /// <returns></returns>
        /// <exception cref="ToolsException"></exception>
        public ICollection<IMateralProject> GetAllProjects(string projectPath)
        {
            DirectoryInfo projectDirectoryInfo = new(projectPath);
            if (!projectDirectoryInfo.Exists) throw new ToolsException($"\"{projectDirectoryInfo}\"文件夹不存在");
            Type baseProjectType = typeof(IMateralProject);
            List<Type> allProjectTypes = baseProjectType.Assembly.GetTypes().Where(m => !m.IsAbstract && m.IsAssignableTo(baseProjectType)).ToList();
            List<IMateralProject> _projects = [];
            foreach (Type projectType in allProjectTypes)
            {
                ConstructorInfo? constructorInfo = projectType.GetConstructor([typeof(DirectoryInfo)]) ?? throw new ToolsException("未找到构造函数");
                object projectObj = constructorInfo.Invoke([projectDirectoryInfo]);
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
    }
}
