using System.CommandLine;

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
            Console.Write("将清理：");
            List<string> message = [];
            _dictionaryWhiteList.Add(".vs");
            message.Add(".vs文件夹");
            _dictionaryWhiteList.Add("bin");
            message.Add("bin文件夹");
            _dictionaryWhiteList.Add("obj");
            message.Add("obj文件夹");
            _dictionaryWhiteList.Add("node_modules");
            message.Add("node_modules文件夹");
            Console.WriteLine(string.Join("、", message));
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
            if (_dictionaryBlackList.Contains(directoryInfo.Name)) return;
            if (_dictionaryWhiteList.Contains(directoryInfo.Name))
            {
                Console.WriteLine($"移除文件夹:{directoryInfo.FullName}");
                try
                {
                    directoryInfo.Delete(true);
                }
                catch (Exception ex)
                {
                    WriteError($"移除文件夹失败:{directoryInfo.FullName}", ex);
                }
                return;
            }
            foreach (DirectoryInfo item in directoryInfo.GetDirectories())
            {
                ClearByNameList(item);
            }
            foreach (FileInfo item in directoryInfo.GetFiles())
            {
                if (!_filesWhiteList.Contains(item.Extension)) continue;
                Console.WriteLine($"移除文件:{item.FullName}");
                try
                {
                    item.Delete();
                }
                catch (Exception ex)
                {
                    WriteError($"移除文件失败:{directoryInfo.FullName}", ex);
                }
            }
            if (directoryInfo.GetFiles().Length <= 0 && directoryInfo.GetDirectories().Length <= 0)//空文件夹
            {
                Console.WriteLine($"移除文件夹:{directoryInfo.FullName}");
                try
                {
                    directoryInfo.Delete(true);
                }
                catch (Exception ex)
                {
                    WriteError($"移除文件夹失败:{directoryInfo.FullName}", ex);
                }
            }
        }
        private static void WriteError(string message, Exception? exception = null)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            if (exception is not null)
            {
                Console.WriteLine(exception.Message);
            }
            Console.ForegroundColor = originalColor;
        }
    }
}