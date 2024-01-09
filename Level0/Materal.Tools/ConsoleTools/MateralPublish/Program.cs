using Materal.Tools.Helper;
using MateralPublish.Models;
using System.CommandLine;

namespace MateralPublish
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            RootCommand rootCommand = new("发布Materal项目");

            Option<string?> verionOption = new("--Version", "指定版本号");
            verionOption.AddAlias("-v");
            verionOption.IsRequired = false;
            verionOption.SetDefaultValue(null);
            rootCommand.AddOption(verionOption);

            Option<bool> uploadNugetOption = new("--UploadNuget", "上传Nuget包");
            uploadNugetOption.AddAlias("-u");
            uploadNugetOption.IsRequired = false;
            uploadNugetOption.SetDefaultValue(false);
            rootCommand.AddOption(uploadNugetOption);

            Option<bool> nextVerionOption = new("--NextVersion", "版本号+1");
            nextVerionOption.AddAlias("-n");
            nextVerionOption.IsRequired = false;
            nextVerionOption.SetDefaultValue(false);
            rootCommand.AddOption(nextVerionOption);

            rootCommand.SetHandler(PublishAsync, verionOption, nextVerionOption, uploadNugetOption);
            int result = await rootCommand.InvokeAsync(args);
            ConsoleHelper.Wait();
            return result;
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="version"></param>
        /// <param name="uploadNuget"></param>
        private static async Task PublishAsync(string? version, bool nextVersion, bool uploadNuget)
        {
            ClearNugetPackages();
            MateralSolutionModel materalProject = new(Environment.CurrentDirectory);
            version ??= nextVersion ? await materalProject.GetNextVersionAsync() : await materalProject.GetVersionAsync();
            await materalProject.PublishAsync(version, uploadNuget);
        }
        private static void ClearNugetPackages()
        {
            string nugetPackagesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".nuget", "packages");
            DirectoryInfo nugetDirectoryInfo = new(nugetPackagesPath);
            if (!nugetDirectoryInfo.Exists) return;
            DirectoryInfo[] directoryInfos = nugetDirectoryInfo.GetDirectories();
            foreach (DirectoryInfo directoryInfo in directoryInfos)
            {
                if (!directoryInfo.Name.StartsWith("materal.")) continue;
                directoryInfo.Delete(true);
            }
        }
    }
}