using CodeCreate.Common;
using System.IO;
using Materal.FileHelper;

namespace CodeCreate.Model
{
    public class ViewModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 实体名称
        /// </summary>
        public string ViewName { get; set; }
        /// <summary>
        /// 唯一标识类型
        /// </summary>
        public string IDType { get; set; } = "Guid";

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="directoryName"></param>
        /// <param name="subSystemName"></param>
        public void CreateFile(string targetPath, string directoryName, string subSystemName)
        {
            string viewTargetPath = $"{targetPath}/{directoryName}";
            string viewRepositoryTargetPath = $"{targetPath}/Repositories/{directoryName}";
            CreateViewFile(viewTargetPath, subSystemName);
            CreateRepositoryInterface(viewRepositoryTargetPath, subSystemName);
        }
        /// <summary>
        /// 创建View文件
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="subSystemName">子系统名称</param>
        private void CreateViewFile(string targetPath, string subSystemName)
        {
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
            TextFileManager.WriteText($"{targetPath}/{Name}.cs", GetViewFileContent(subSystemName), ApplicationConfig.ApplicationEncoding);
        }
        /// <summary>
        /// 获得View文件内容
        /// </summary>
        /// <param name="subSystemName">子系统名称</param>
        /// <returns></returns>
        private string GetViewFileContent(string subSystemName)
        {
            string result = "using Domain;\r\n";
            result += "using Materal.TTA.Common;\r\n";
            result += "using System;\r\n";
            result += "using System.Collections.Generic;\r\n";
            result += $"namespace {subSystemName}.Domain.Views\r\n";
            result += "{\r\n";
            result += "    /// <summary>\r\n";
            result += $"    /// {ViewName}\r\n";
            result += "    /// </summary>\r\n";
            result += "    [ViewEntity]\r\n";
            result += $"    public sealed class {Name} : IEntity<{IDType}>\r\n";
            result += "    {\r\n";
            result += "        /// <summary>\r\n";
            result += "        /// 唯一标识\r\n";
            result += "        /// </summary>\r\n";
            result += "        public Guid ID { get; set; }\r\n";
            result += "    }\r\n";
            result += "}";
            return result;
        }

        /// <summary>
        /// 创建仓储接口文件
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="subSystemName"></param>
        private void CreateRepositoryInterface(string targetPath, string subSystemName)
        {
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
            TextFileManager.WriteText($"{targetPath}/I{Name}Repository.cs", GetRepositoryInterfaceFileContent(subSystemName), ApplicationConfig.ApplicationEncoding);
        }
        /// <summary>
        /// 获得Domain文件内容
        /// </summary>
        /// <param name="subSystemName">子系统名称</param>
        /// <returns></returns>
        private string GetRepositoryInterfaceFileContent(string subSystemName)
        {
            string result = "using Materal.TTA.Common;\r\n";
            result += "using System;\r\n";
            result += $"using {subSystemName}.Domain.Views;\r\n";
            result += $"namespace {subSystemName}.Domain.Repositories.Views\r\n";
            result += "{\r\n";
            result += "    /// <summary>\r\n";
            result += $"    /// {ViewName}仓储\r\n";
            result += "    /// </summary>\r\n";
            result += $"    public interface I{Name}Repository : IRepository<{Name}, {IDType}>\r\n";
            result += "    {\r\n";
            result += "    }\r\n";
            result += "}";
            return result;
        }

        /// <summary>
        /// 创建仓储文件
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="directoryName"></param>
        /// <param name="subSystemName"></param>
        public void CreateRepositoryImpl(string targetPath, string directoryName, string subSystemName)
        {
            string viewRepositoryTargetPath = $"{targetPath}/RepositoryImpl/{directoryName}";
            if (!Directory.Exists(viewRepositoryTargetPath))
            {
                Directory.CreateDirectory(viewRepositoryTargetPath);
            }
            TextFileManager.WriteText($"{viewRepositoryTargetPath}/{Name}RepositoryImpl.cs", GetRepositoryImplFileContent(subSystemName), ApplicationConfig.ApplicationEncoding);
        }
        /// <summary>
        /// 获得View文件内容
        /// </summary>
        /// <param name="subSystemName">子系统名称</param>
        /// <returns></returns>
        private string GetRepositoryImplFileContent(string subSystemName)
        {
            string result = "using System;\r\n";
            result += $"using {subSystemName}.Domain.Views;\r\n";
            result += $"using {subSystemName}.Domain.Repositories.Views;\r\n";
            result += $"namespace {subSystemName}.EFRepository.RepositoryImpl.Views\r\n";
            result += "{\r\n";
            result += "    /// <summary>\r\n";
            result += $"    /// {ViewName}仓储\r\n";
            result += "    /// </summary>\r\n";
            result += $"    public class {Name}RepositoryImpl : {subSystemName}EFRepositoryImpl<{Name}, Guid>, I{Name}Repository\r\n";
            result += "    {\r\n";
            result += $"        public {Name}RepositoryImpl({subSystemName}DbContext dbContext) : base(dbContext)\r\n";
            result += "        {\r\n";
            result += "        }\r\n";
            result += "    }\r\n";
            result += "}";
            return result;
        }
    }
}
