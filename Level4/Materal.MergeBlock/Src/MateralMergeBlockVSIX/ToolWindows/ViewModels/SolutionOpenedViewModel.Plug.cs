#nullable enable
using Materal.Abstractions;
using Materal.MergeBlock.GeneratorCode;
using MateralMergeBlockVSIX.Extensions;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.VisualStudio.Shell.Interop;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MateralMergeBlockVSIX.ToolWindows.ViewModels
{
    public partial class SolutionOpenedViewModel
    {
        /// <summary>
        /// 获得所有生成代码插件路径
        /// </summary>
        /// <returns></returns>
        private List<string> GetAllGeneratorCodePlugPaths()
        {
            List<string> generatorCodePlugPaths = [];
            if (_coreAbstractions is not null)
            {
                generatorCodePlugPaths.AddRange(GetAllGeneratorCodePlugPaths(_coreAbstractions));
            }
            if (_coreRepository is not null)
            {
                generatorCodePlugPaths.AddRange(GetAllGeneratorCodePlugPaths(_coreRepository));
            }
            if (_coreApplication is not null)
            {
                generatorCodePlugPaths.AddRange(GetAllGeneratorCodePlugPaths(_coreApplication));
            }
            if (_moduleAbstractions is not null)
            {
                generatorCodePlugPaths.AddRange(GetAllGeneratorCodePlugPaths(_moduleAbstractions));
            }
            if (_moduleRepository is not null)
            {
                generatorCodePlugPaths.AddRange(GetAllGeneratorCodePlugPaths(_moduleRepository));
            }
            if (_moduleApplication is not null)
            {
                generatorCodePlugPaths.AddRange(GetAllGeneratorCodePlugPaths(_moduleApplication));
            }
            if (_moduleWebAPI is not null)
            {
                generatorCodePlugPaths.AddRange(GetAllGeneratorCodePlugPaths(_moduleWebAPI));
            }
            return generatorCodePlugPaths;
        }
        /// <summary>
        /// 获得所有生成代码插件路径
        /// </summary>
        /// <returns></returns>
        private List<string> GetAllEditGeneratorCodePlugPaths()
        {
            List<string> generatorCodePlugPaths = [];
            if (_coreAbstractions is not null)
            {
                generatorCodePlugPaths.AddRange(GetAllEditGeneratorCodePlugPaths(_coreAbstractions));
            }
            if (_coreRepository is not null)
            {
                generatorCodePlugPaths.AddRange(GetAllEditGeneratorCodePlugPaths(_coreRepository));
            }
            if (_coreApplication is not null)
            {
                generatorCodePlugPaths.AddRange(GetAllEditGeneratorCodePlugPaths(_coreApplication));
            }
            if (_moduleAbstractions is not null)
            {
                generatorCodePlugPaths.AddRange(GetAllEditGeneratorCodePlugPaths(_moduleAbstractions));
            }
            if (_moduleRepository is not null)
            {
                generatorCodePlugPaths.AddRange(GetAllEditGeneratorCodePlugPaths(_moduleRepository));
            }
            if (_moduleApplication is not null)
            {
                generatorCodePlugPaths.AddRange(GetAllEditGeneratorCodePlugPaths(_moduleApplication));
            }
            if (_moduleWebAPI is not null)
            {
                generatorCodePlugPaths.AddRange(GetAllEditGeneratorCodePlugPaths(_moduleWebAPI));
            }
            return generatorCodePlugPaths;
        }
        /// <summary>
        /// 获得所有生成代码插件路径
        /// </summary>
        /// <param name="solutionItem"></param>
        /// <returns></returns>
        private List<string> GetAllGeneratorCodePlugPaths(SolutionItem solutionItem)
        {
            List<string> generatorCodePlugPaths = [];
            if (solutionItem.Type == SolutionItemType.SolutionFolder)
            {
                foreach (SolutionItem? item in solutionItem.Children)
                {
                    if (item is null) continue;
                    generatorCodePlugPaths.AddRange(GetAllGeneratorCodePlugPaths(item));
                }
            }
            else if (solutionItem.Type == SolutionItemType.Project)
            {
                generatorCodePlugPaths.AddRange(solutionItem?.GetGeneratorCodePlugPaths() ?? []);
            }
            return generatorCodePlugPaths;
        }
        /// <summary>
        /// 获得所有生成代码插件路径
        /// </summary>
        /// <param name="solutionItem"></param>
        /// <returns></returns>
        private List<string> GetAllEditGeneratorCodePlugPaths(SolutionItem solutionItem)
        {
            List<string> generatorCodePlugPaths = [];
            if (solutionItem.Type == SolutionItemType.SolutionFolder)
            {
                foreach (SolutionItem? item in solutionItem.Children)
                {
                    if (item is null) continue;
                    generatorCodePlugPaths.AddRange(GetAllEditGeneratorCodePlugPaths(item));
                }
            }
            else if (solutionItem.Type == SolutionItemType.Project)
            {
                generatorCodePlugPaths.AddRange(solutionItem?.GetEditGeneratorCodePlugPaths() ?? []);
            }
            return generatorCodePlugPaths;
        }
        /// <summary>
        /// 执行插件
        /// </summary>
        /// <param name="codePaths"></param>
        private async Task ExcutePlugAsync(IReadOnlyCollection<string> codePaths)
        {
            foreach (string codePath in codePaths)
            {
                try
                {
                    IMergeBlockGeneratorCodePlug plug = await GetMergeBlockGeneratorCodePlugAsync(codePath);
                    await plug.ExcuteAsync(Context);
                }
                catch (Exception ex)
                {
                    await VS.MessageBox.ShowAsync($"插件[{codePath}]错误", ex.GetErrorMessage(), OLEMSGICON.OLEMSGICON_WARNING, OLEMSGBUTTON.OLEMSGBUTTON_OK);
                }
            }
        }
        /// <summary>
        /// 获得MergeBlock生成代码插件
        /// </summary>
        /// <param name="codeFilePath"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<IMergeBlockGeneratorCodePlug> GetMergeBlockGeneratorCodePlugAsync(string codeFilePath)
            => await GetMergeBlockEditGeneratorCodePlugAsync<IMergeBlockGeneratorCodePlug>(codeFilePath);
        /// <summary>
        /// 获得MergeBlock生成代码插件
        /// </summary>
        /// <param name="codeFilePath"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<IMergeBlockEditGeneratorCodePlug> GetMergeBlockEditGeneratorCodePlugAsync(string codeFilePath)
            => await GetMergeBlockEditGeneratorCodePlugAsync<IMergeBlockEditGeneratorCodePlug>(codeFilePath);
        /// <summary>
        /// 获得MergeBlock生成代码插件
        /// </summary>
        /// <param name="codeFilePath"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<T> GetMergeBlockEditGeneratorCodePlugAsync<T>(string codeFilePath)
        {
            if (!codeFilePath.EndsWith(".cs")) throw new Exception("不是C#文件");
            string codeFileName = Path.GetFileNameWithoutExtension(codeFilePath);
            string scriptCode = File.ReadAllText(codeFilePath, Encoding.UTF8);
            StringBuilder codeContent = new();
            codeContent.AppendLine("using System;");
            codeContent.AppendLine("using System.IO;");
            codeContent.AppendLine("using System.Text;");
            codeContent.AppendLine("using System.Threading.Tasks;");
            codeContent.AppendLine("using Materal.Abstractions;");
            codeContent.AppendLine("using Materal.Extensions;");
            codeContent.AppendLine("using Materal.Utils;");
            codeContent.AppendLine("using System.Linq;");
            codeContent.AppendLine("using System.Linq.Expressions;");
            codeContent.AppendLine(scriptCode);
            scriptCode = codeContent.ToString();
            Assembly[] trueAssemblies =
            [
                Assembly.Load("IndexRange"),
                Assembly.Load("Materal.Abstractions"),
                Assembly.Load("Materal.Extensions"),
                Assembly.Load("Materal.Utils"),
                Assembly.Load("Materal.MergeBlock.GeneratorCode"),
            ];
            ScriptOptions scriptOptions = ScriptOptions.Default
                .WithReferences(trueAssemblies);
            Script<T> script = CSharpScript.Create<T>(scriptCode, scriptOptions);
            script.Compile();
            Script<T> scripteState = script.ContinueWith<T>($"new {codeFileName}()");
            ScriptState<T> myInterface = await scripteState.RunAsync();
            T result = myInterface.ReturnValue;
            return result;
        }
    }
}
