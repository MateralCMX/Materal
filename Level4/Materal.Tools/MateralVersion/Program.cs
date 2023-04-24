using MateralPublish;
using MateralVersion.Helper;
using System.CommandLine;
using System.Text;

namespace MateralVersion
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            RootCommand rootCommand = new("更改Materal包版本");
            Option<string?> verionOption = new("--Version", "指定版本号");
            verionOption.AddAlias("-v");
            verionOption.IsRequired = false;
            verionOption.SetDefaultValue(null);
            rootCommand.AddOption(verionOption);
            rootCommand.SetHandler(ChangeVersionAsync, verionOption);
            int result = await rootCommand.InvokeAsync(args);
            ConsoleHelper.Wait();
            return result;
        }
        private static async Task ChangeVersionAsync(string? version)
        {
            version ??= await NugetServerHelper.GetLastMateralVersionAsync();
            DirectoryInfo directoryInfo = new(Environment.CurrentDirectory);
            await ChangeVersionAsync(version, directoryInfo);
        }
        private static async Task ChangeVersionAsync(string version, DirectoryInfo rootDirectoryInfo)
        {
            FileInfo? csprojFileInfo = rootDirectoryInfo.GetFiles().FirstOrDefault(m => m.Extension == ".csproj");
            if (csprojFileInfo != null)
            {
                await ChangeVersionAsync(version, csprojFileInfo);
            }
            else
            {
                IEnumerable<DirectoryInfo> subDirectoryInfos = rootDirectoryInfo.GetDirectories();
                foreach (DirectoryInfo directoryInfo in subDirectoryInfos)
                {
                    await ChangeVersionAsync(version, directoryInfo);
                }
            }
        }
        /// <summary>
        /// 更新版本号
        /// </summary>
        /// <param name="version"></param>
        /// <param name="csprojFileInfo"></param>
        /// <returns></returns>
        private static async Task ChangeVersionAsync(string version, FileInfo csprojFileInfo)
        {
            const string materalPackageStartCode = "<PackageReference Include=\"Materal.";
            const string materalPackageVersionStartCode = "\" Version=\"";
            string projectName = Path.GetFileNameWithoutExtension(csprojFileInfo.Name);
            ConsoleHelper.WriteLine($"正在更新{projectName}版本号...");
            string[] csprojFileContents = await File.ReadAllLinesAsync(csprojFileInfo.FullName);
            for (int i = 0; i < csprojFileContents.Length; i++)
            {
                string tempCode = csprojFileContents[i].Trim();
                if (tempCode.StartsWith(materalPackageStartCode))
                {
                    int versionLength = tempCode.IndexOf(materalPackageVersionStartCode);
                    string packageName = tempCode[materalPackageStartCode.Length..versionLength];
                    csprojFileContents[i] = $"\t\t<PackageReference Include=\"Materal.{packageName}\" Version=\"{version}\" />";
                }
            }
            await File.WriteAllLinesAsync(csprojFileInfo.FullName, csprojFileContents, Encoding.UTF8);
            ConsoleHelper.WriteLine($"{projectName}版本号已更新到{version}");
        }
    }
}