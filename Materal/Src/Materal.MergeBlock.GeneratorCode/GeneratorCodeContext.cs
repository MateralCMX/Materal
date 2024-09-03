using Materal.MergeBlock.GeneratorCode.Models;
using System.Text;

namespace Materal.MergeBlock.GeneratorCode
{
    /// <summary>
    /// 生成代码插件上下文
    /// </summary>
    public class GeneratorCodeContext(string coreAbstractionsPath, string coreRepositoryPath, string moduleAbstractionsPath, string moduleApplicationPath, string moduleRepositoryPath, string moduleWebAPIPath, string projectName, string moduleName, List<IMergeBlockEditGeneratorCodePlug> editGeneratorCodePlugs)
    {
        /// <summary>
        /// MGC文件夹名称
        /// </summary>
        public const string MGCDirectoryName = "MGC";
        /// <summary>
        /// 核心抽象层路径
        /// </summary>
        public string CoreAbstractionsPath { get; } = coreAbstractionsPath;
        /// <summary>
        /// 核心抽象层MGC路径
        /// </summary>
        public string CoreAbstractionsMGCPath { get; } = Path.Combine(coreAbstractionsPath, MGCDirectoryName);
        /// <summary>
        /// 核心仓储路径
        /// </summary>
        public string CoreRepositoryPath { get; } = coreRepositoryPath;
        /// <summary>
        /// 核心仓储MGC路径
        /// </summary>
        public string CoreRepositoryMGCPath { get; } = Path.Combine(coreRepositoryPath, MGCDirectoryName);
        /// <summary>
        /// 模块抽象层路径
        /// </summary>
        public string ModuleAbstractionsPath { get; } = moduleAbstractionsPath;
        /// <summary>
        /// 模块抽象层MGC路径
        /// </summary>
        public string ModuleAbstractionsMGCPath { get; } = Path.Combine(moduleAbstractionsPath, MGCDirectoryName);
        /// <summary>
        /// 模块应用层路径
        /// </summary>
        public string ModuleApplicationPath { get; } = moduleApplicationPath;
        /// <summary>
        /// 模块应用层MGC路径
        /// </summary>
        public string ModuleApplicationMGCPath { get; } = Path.Combine(moduleApplicationPath, MGCDirectoryName);
        /// <summary>
        /// 模块仓储路径
        /// </summary>
        public string ModuleRepositoryPath { get; } = moduleRepositoryPath;
        /// <summary>
        /// 模块仓储MGC路径
        /// </summary>
        public string ModuleRepositoryMGCPath { get; } = Path.Combine(moduleRepositoryPath, MGCDirectoryName);
        /// <summary>
        /// 模块WebAPI路径
        /// </summary>
        public string ModuleWebAPIPath { get; } = moduleWebAPIPath;
        /// <summary>
        /// 模块WebAPIMGC路径
        /// </summary>
        public string ModuleWebAPIMGCPath { get; } = Path.Combine(moduleWebAPIPath, MGCDirectoryName);
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; } = projectName;
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; } = moduleName;
        /// <summary>
        /// 修改生成代码插件
        /// </summary>
        public List<IMergeBlockEditGeneratorCodePlug> EditGeneratorCodePlugs { get; } = editGeneratorCodePlugs;
        /// <summary>
        /// 领域
        /// </summary>
        public List<DomainModel> Domains { get; set; } = [];
        /// <summary>
        /// 服务
        /// </summary>
        public List<IServiceModel> Services { get; set; } = [];
        /// <summary>
        /// 控制器
        /// </summary>
        public List<IControllerModel> Controllers { get; set; } = [];
        /// <summary>
        /// 枚举
        /// </summary>
        public List<EnumModel> Enums { get; set; } = [];
        /// <summary>
        /// 保存为文件
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <param name="directoryPath"></param>
        /// <param name="paths"></param>
        public void SaveAs(StringBuilder stringBuilder, string directoryPath, params string[] paths)
        {
            if (paths.Length < 1) return;
            DirectoryInfo directoryInfo = new(directoryPath);
            string filePath = directoryInfo.FullName;
            for (int i = 0; i < paths.Length - 1; i++)
            {
                filePath = Path.Combine(filePath, paths[i]);
            }
            directoryInfo = new(filePath);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
                directoryInfo.Refresh();
            }
            filePath = Path.Combine(filePath, paths[^1]);
            string content = stringBuilder.ToString();
            File.WriteAllText(filePath, content, Encoding.UTF8);
        }
    }
}
