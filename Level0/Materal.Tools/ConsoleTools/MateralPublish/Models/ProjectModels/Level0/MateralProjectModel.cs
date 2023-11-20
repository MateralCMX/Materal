using Materal.Tools.Helper;

namespace MateralPublish.Models.ProjectModels.Level0
{
    public class MateralProjectModel : BaseProjectModel
    {
        private readonly string[] _publishWhiteList = new[]
        {
            "Materal.Test",
            "Materal.Utils.Windows"
        };
        public MateralProjectModel(string solutionPath) : base(solutionPath, 0, "Materal")
        {
        }
        protected override bool IsPublishProject(string name) => _publishWhiteList.Contains(name);
        public async Task<string> GetNowVersionAsync()
        {
            string rootPath = Path.Combine(ProjectDirectoryInfo.FullName, "Src", "Materal.Abstractions", "Materal.Abstractions.csproj");
            FileInfo csprojFileInfo = new(rootPath);
            if (!csprojFileInfo.Exists) throw new Exception("主项目丢失");
            string[] coedeContent = await File.ReadAllLinesAsync(csprojFileInfo.FullName);
            const string versionStartCode = "<Version>";
            const string versionEndCode = "</Version>";
            foreach (string code in coedeContent)
            {
                string tempCode = code.Trim();
                if (tempCode.StartsWith(versionStartCode))
                {
                    int endStartIndex = tempCode.IndexOf(versionEndCode);
                    string result = tempCode[versionStartCode.Length..endStartIndex];
                    return result;
                }
            }
            throw new Exception("未找到版本号");
        }
        protected override string[] GetPublishCommand(DirectoryInfo publishDirectoryInfo, FileInfo csprojFileInfo)
        {
            if (csprojFileInfo.Name == "Materal.Utils.Windows.csproj")
            {
                string cmd1 = $"dotnet publish {csprojFileInfo.FullName} -c Release -f net6.0-windows";
                string cmd2 = $"dotnet publish {csprojFileInfo.FullName} -c Release -f net8.0-windows";
                string cmd3 = $"dotnet publish {csprojFileInfo.FullName} -c Release -f net472";
                if (NugetServerHelper.NugetDirectoryInfo == null) return new[] { cmd1, cmd2, cmd3 };
                string cmdPack = $"dotnet pack {csprojFileInfo.FullName} -o {NugetServerHelper.NugetDirectoryInfo.FullName} -c Release";
                return new[] { cmd1, cmd2, cmd3, cmdPack };
            }
            else
            {
                return base.GetPublishCommand(publishDirectoryInfo, csprojFileInfo);
            }
        }
    }
}
