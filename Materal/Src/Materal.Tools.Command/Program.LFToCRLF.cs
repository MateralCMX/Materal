using Materal.Tools.Core.LFConvert;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;
using System.Text.RegularExpressions;
using SubCommand = System.CommandLine.Command;

namespace Materal.Tools.Command
{
    public partial class Program
    {
        /// <summary>
        /// 添加LF转换CRLF命令
        /// </summary>
        /// <param name="rootCommand"></param>
        [AddSubCommand]
        public void AddLFToCRLFCommand(RootCommand rootCommand)
        {
            SubCommand command = new("LFToCRLF", "LF转换CRLF");

            Option<string?> pathOption = new("--Path", "指定路径");
            pathOption.AddAlias("-p");
            pathOption.IsRequired = false;
            pathOption.SetDefaultValue(null);
            command.AddOption(pathOption);

            Option<bool> recursiveOption = new("--Recursive", "递归");
            recursiveOption.AddAlias("-r");
            recursiveOption.IsRequired = false;
            recursiveOption.SetDefaultValue(true);
            command.AddOption(recursiveOption);

            Option<string> filterOption = new("--Filter", "过滤正则表达式");
            filterOption.AddAlias("-f");
            filterOption.IsRequired = false;
            filterOption.SetDefaultValue("^.+$");
            filterOption.FromAmong("^.+\\.cs$", "^.+\\.xml$", "其他正则");
            command.AddOption(filterOption);

            command.SetHandler(LFToCRLFAsync, pathOption, recursiveOption, filterOption);
            rootCommand.AddCommand(command);
        }
        private async void LFToCRLFAsync(string? path, bool recursive, string filter)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                path = Environment.CurrentDirectory;
            }
            ILFConvertService service = _serviceProvider.GetRequiredService<ILFConvertService>();
            LFConvertOptions options = new()
            {
                Recursive = Convert.ToBoolean(recursive),
                Filter = fileInfo => new Regex(filter).Match(fileInfo.Name).Success,
            };
            await service.LFToCRLFAsync(path, options);
        }
    }
}
