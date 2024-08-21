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
        /// 添加CRLF转换LF命令
        /// </summary>
        /// <param name="rootCommand"></param>
        [AddSubCommand]
        public void AddCRLFToLFCommand(RootCommand rootCommand)
        {
            SubCommand command = new("CRLFToLF", "CRLF转换LF");

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

            command.SetHandler(CRLFToLFAsync, pathOption, recursiveOption, filterOption);
            rootCommand.AddCommand(command);
        }
        private async void CRLFToLFAsync(string? path, bool recursive, string filter)
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
            await service.CRLFToLFAsync(path, options);
        }
    }
}
