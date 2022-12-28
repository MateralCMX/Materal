using System.CommandLine;
using System.Reflection;

namespace ProjectClear
{
    public class Program
    {
        private static List<string> _dictionaryWhiteList = new();
        private static List<string> _filesWhiteList = new();
        private static List<string> _dictionaryBlackList = new()
        {
            ".git"
        };
        public static async Task<int> Main(string[] args)
        {
            Option<bool> canAllOption = new("--All", "清理所有临时文件、文件夹[.vs、bin、obj、*.nupkg]");
            canAllOption.AddAlias("-a");
            canAllOption.IsRequired = true;
            canAllOption.SetDefaultValue(false);
            RootCommand rootCommand = new("清理项目文件夹[.vs、*.nupk]");
            rootCommand.AddOption(canAllOption);
            rootCommand.SetHandler(Clear, canAllOption);
            return await rootCommand.InvokeAsync(args);
        }
        /// <summary>
        /// 清理
        /// </summary>
        /// <param name="canAll"></param>
        private static void Clear(bool canAll)
        {
            Console.Write("将清理：.vs文件夹");
            _dictionaryWhiteList.Add(".vs");
            if (canAll)
            {
                Console.Write("、obj文件夹、bin文件夹");
                _dictionaryWhiteList.Add("obj");
                _dictionaryWhiteList.Add("bin");
            }
            Console.WriteLine("、*.nupkg文件");
            _filesWhiteList.Add(".nupkg");
            Console.WriteLine("开始清理....");
            ClearByNameList();
            Console.WriteLine("清理完毕");
        }
        /// <summary>
        /// 根据白名单清理
        /// </summary>
        private static void ClearByNameList()
        {
            string basePath = Environment.CurrentDirectory;
            DirectoryInfo rootDirectoryInfo = new(basePath);
            ClearByNameList(rootDirectoryInfo);
        }
        private static void ClearByNameList(DirectoryInfo directoryInfo)
        {
            if (!directoryInfo.Exists) return;
            if (_dictionaryWhiteList.Contains(directoryInfo.Name) && !_dictionaryBlackList.Contains(directoryInfo.Name))
            {
                Console.WriteLine($"移除文件夹:{directoryInfo.FullName}");
                directoryInfo.Delete(true);
                return;
            }
            foreach (DirectoryInfo item in directoryInfo.GetDirectories())
            {
                ClearByNameList(item);
            }
            foreach (FileInfo item in directoryInfo.GetFiles())
            {
                if (_filesWhiteList.Contains(item.Extension))
                {
                    Console.WriteLine($"移除文件:{item.FullName}");
                    item.Delete();
                }
            }
        }
    }
}