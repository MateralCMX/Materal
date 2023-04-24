namespace MateralPublish.Models
{
    public class MainProjectModel : BaseProjectModel
    {
        public MainProjectModel(string directoryPath) : base(directoryPath)
        {
        }
        protected override bool IsPublishProject(string name)
        {
            string[] whiteList = new[]
            {
                "Materal.Abstractions",
                "Materal.Extensions",
                "Materal.Utils",
                "Materal.Utils.Redis",
                "Materal.Utils.Wechat",
                "Materal.Utils.Windows"
            };
            return whiteList.Contains(name);
        }
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
    }
}
