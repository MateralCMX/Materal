using Materal.Tools.Core.ChangeEncoding;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;
using System.Text;
using System.Text.RegularExpressions;
using SubCommand = System.CommandLine.Command;

namespace Materal.Tools.Command
{
    public partial class Program
    {
        /// <summary>
        /// 添加更改编码命令
        /// </summary>
        /// <param name="rootCommand"></param>
        [AddSubCommand]
        public void AddChangeEncodingCommand(RootCommand rootCommand)
        {
            SubCommand command = new("ChangeEncoding", "更改文件编码");
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

            Option<string?> writeEncodingOption = new("--WriteEncoding", "写入编码");
            writeEncodingOption.AddAlias("-write");
            writeEncodingOption.IsRequired = false;
            writeEncodingOption.SetDefaultValue(null);
            writeEncodingOption.FromAmong("UTF-8", "GBK", "其他编码");
            command.AddOption(writeEncodingOption);

            Option<string?> readEncodingOption = new("--ReadEncoding", "写入编码,不传则自动识别");
            readEncodingOption.AddAlias("-read");
            readEncodingOption.IsRequired = false;
            readEncodingOption.SetDefaultValue(null);
            readEncodingOption.FromAmong("UTF-8", "GBK", "其他编码");
            command.AddOption(readEncodingOption);

            Option<string> filterOption = new("--Filter", "过滤正则表达式");
            filterOption.AddAlias("-f");
            filterOption.IsRequired = false;
            filterOption.SetDefaultValue("^.+$");
            filterOption.FromAmong("^.+\\.cs$", "^.+\\.xml$", "其他正则");
            command.AddOption(filterOption);

            command.SetHandler(ChangeEncodingAsync, pathOption, recursiveOption, writeEncodingOption, readEncodingOption, filterOption);
            rootCommand.AddCommand(command);
        }
        private async void ChangeEncodingAsync(string? path, bool recursive, string? writeEncoding, string? readEncoding, string filter)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                path = Environment.CurrentDirectory;
            }
            IChangeEncodingService service = _serviceProvider.GetRequiredService<IChangeEncodingService>();
            ChangeEncodingOptions options = new()
            {
                Recursive = Convert.ToBoolean(recursive),
                Filter = fileInfo => new Regex(filter).Match(fileInfo.Name).Success,
            };
            if (!string.IsNullOrWhiteSpace(writeEncoding))
            {
                options.WriteEncoding = Encoding.GetEncoding(writeEncoding);
            }
            if (!string.IsNullOrWhiteSpace(readEncoding))
            {
                options.ReadEncoding = Encoding.GetEncoding(readEncoding);
            }
            await service.ChangeEncodingAsync(path, options);
        }
    }
}
