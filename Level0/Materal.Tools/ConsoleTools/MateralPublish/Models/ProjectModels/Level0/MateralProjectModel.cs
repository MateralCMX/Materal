namespace MateralPublish.Models.ProjectModels.Level0
{
    public class MateralProjectModel(string solutionPath) : BaseProjectModel(solutionPath, 0, "Materal")
    {
        public async Task<string> GetNowVersionAsync()
        {
            string rootPath = Path.Combine(ProjectDirectoryInfo.FullName, "Src", "Materal.Abstractions", "Materal.Abstractions.csproj");
            FileInfo csprojFileInfo = new(rootPath);
            if (!csprojFileInfo.Exists) throw new Exception("主项目丢失");
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
            throw new Exception("未找到版本号");
        }
    }
}
