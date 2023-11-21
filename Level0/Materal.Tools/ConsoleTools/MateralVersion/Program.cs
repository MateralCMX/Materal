using Materal.Tools.Helper;
using System;
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
#if DEBUG
            DirectoryInfo directoryInfo = new(@"D:\Project\Materal\Materal");
#else
            DirectoryInfo directoryInfo = new(Environment.CurrentDirectory);
#endif
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
        private static async Task ChangeVersionAsync(string version, FileInfo csprojFileInfo) => await ChangeVersionAsync(version, csprojFileInfo, [
            "<PackageReference Include=\"Materal.",
            "<PackageReference Include=\"RC.",
        ]);
        /// <summary>
        /// 更新版本号
        /// </summary>
        /// <param name="version"></param>
        /// <param name="csprojFileInfo"></param>
        /// <param name="startCodes"></param>
        /// <returns></returns>
        private static async Task ChangeVersionAsync(string version, FileInfo csprojFileInfo, string[] startCodes)
        {
            string projectName = Path.GetFileNameWithoutExtension(csprojFileInfo.Name);
            ConsoleHelper.WriteLine($"正在更新{projectName}版本号...");
            string[] csprojFileContents = await File.ReadAllLinesAsync(csprojFileInfo.FullName);
            for (int i = 0; i < csprojFileContents.Length; i++)
            {
                foreach (string startCode in startCodes)
                {
                    ChangeVersion(csprojFileContents, i, version, startCode);
                }
            }
            await File.WriteAllLinesAsync(csprojFileInfo.FullName, csprojFileContents, Encoding.UTF8);
            ConsoleHelper.WriteLine($"{projectName}版本号已更新到{version}");
        }
        /// <summary>
        /// 更新版本号
        /// </summary>
        /// <param name="csprojFileContents"></param>
        /// <param name="i"></param>
        /// <param name="version"></param>
        /// <param name="startCode"></param>
        private static void ChangeVersion(string[] csprojFileContents, int i, string version, string startCode)
        {
            string tempCode = csprojFileContents[i].Trim();
            if (!tempCode.StartsWith(startCode)) return;
            const string versionStartCode = "<Version>";
            const string materalPackageVersionStartCode = "\" Version=\"";
            int versionLength = tempCode.IndexOf(materalPackageVersionStartCode);
            if (versionLength > 0)
            {
                string packageName = tempCode[startCode.Length..versionLength];
                csprojFileContents[i] = $"\t\t{startCode}{packageName}\" Version=\"{version}\"";
                if (tempCode.EndsWith("/>"))
                {
                    csprojFileContents[i] += $" />";
                }
                else
                {
                    csprojFileContents[i] += $">";
                }
            }
            else
            {
                if (i + 1 >= csprojFileContents.Length) return;
                string nextTempCode = csprojFileContents[i + 1].Trim();
                if (!nextTempCode.StartsWith(versionStartCode)) return;
                csprojFileContents[i + 1] = $"\t\t\t<Version>{version}</Version>";
            }
        }
    }
}