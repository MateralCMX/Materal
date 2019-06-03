using CodeCreate.Common;
using Materal.FileHelper;
using System.Collections.Generic;

namespace CodeCreate.Model
{
    /// <summary>
    /// EF仓储模型
    /// </summary>
    public class EFRepositoryModel
    {
        /// <summary>
        /// 领域模型
        /// </summary>
        private readonly List<DomainModel> _domains;

        /// <summary>
        /// 视图模型
        /// </summary>
        private readonly List<ViewModel> _views;

        public EFRepositoryModel(List<DomainModel> domains, List<ViewModel> views)
        {
            _domains = domains;
            _views = views;
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="subSystemName"></param>
        public void CreateFile(string targetPath, string subSystemName)
        {
            CreateDbContextFile(targetPath, subSystemName);
            CreateUnitOfWorkInterfaceFile(targetPath, subSystemName);
            CreateUnitOfWorkFile(targetPath, subSystemName);
            CreateRepositoryImplFile(targetPath, subSystemName);
            CreateModelConfigFile(targetPath, subSystemName);
        }
        /// <summary>
        /// 创建模型配置文件
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="subSystemName"></param>
        private void CreateModelConfigFile(string targetPath, string subSystemName)
        {
            foreach (DomainModel domain in _domains)
            {
                domain.CreateModelConfigFile(targetPath, subSystemName);
            }
        }
        /// <summary>
        /// 创建仓储实现文件
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="subSystemName"></param>
        private void CreateRepositoryImplFile(string targetPath, string subSystemName)
        {
            TextFileManager.WriteText($"{targetPath}/{subSystemName}EFRepositoryImpl.cs", GetRepositoryImplFileContent(subSystemName), ApplicationConfig.ApplicationEncoding);
            foreach (DomainModel domain in _domains)
            {
                domain.CreateRepositoryImpl(targetPath, subSystemName);
            }
            foreach (ViewModel view in _views)
            {
                view.CreateRepositoryImpl(targetPath, "Views", subSystemName);
            }
        }
        /// <summary>
        /// 获取仓储实现内容
        /// </summary>
        /// <param name="subSystemName"></param>
        /// <returns></returns>
        private string GetRepositoryImplFileContent(string subSystemName)
        {
            string result = "using Materal.TTA.Common;\r\n";
            result += $"namespace {subSystemName}.EFRepository\r\n";
            result += "{\r\n";
            result += "    /// <summary>\r\n";
            result += $"    /// {subSystemName}仓储实现\r\n";
            result += "    /// </summary>\r\n";
            result += $"    public class {subSystemName}EFRepositoryImpl<T, TKey> : EFRepositoryImpl<T, TKey> where T : class, IEntity<TKey>, new()\r\n";
            result += "    {\r\n";
            result += $"        public {subSystemName}EFRepositoryImpl({subSystemName}DbContext dbContext) : base(dbContext)\r\n";
            result += "        {\r\n";
            result += "        }\r\n";
            result += "    }\r\n";
            result += "}";
            return result;
        }
        /// <summary>
        /// 创建工作单元文件
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="subSystemName"></param>
        private void CreateUnitOfWorkFile(string targetPath, string subSystemName)
        {
            TextFileManager.WriteText($"{targetPath}/{subSystemName}UnitOfWorkImpl.cs", GetUnitOfWorkFileContent(subSystemName), ApplicationConfig.ApplicationEncoding);
        }
        /// <summary>
        /// 获取工作单元内容
        /// </summary>
        /// <param name="subSystemName"></param>
        /// <returns></returns>
        private string GetUnitOfWorkFileContent(string subSystemName)
        {
            string result = "using Materal.TTA.Common;\r\n";
            result += $"namespace {subSystemName}.EFRepository\r\n";
            result += "{\r\n";
            result += "    /// <summary>\r\n";
            result += $"    /// {subSystemName}工作单元\r\n";
            result += "    /// </summary>\r\n";
            result += $"    public class {subSystemName}UnitOfWorkImpl : EFUnitOfWorkImpl<{subSystemName}DbContext>, I{subSystemName}UnitOfWork\r\n";
            result += "    {\r\n";
            result += $"        public {subSystemName}UnitOfWorkImpl({subSystemName}DbContext context) : base(context)\r\n";
            result += "        {\r\n";
            result += "        }\r\n";
            result += "    }\r\n";
            result += "}";
            return result;
        }
        /// <summary>
        /// 创建工作单元接口文件
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="subSystemName"></param>
        private void CreateUnitOfWorkInterfaceFile(string targetPath, string subSystemName)
        {
            TextFileManager.WriteText($"{targetPath}/I{subSystemName}UnitOfWork.cs", GetUnitOfWorkInterfaceFileContent(subSystemName), ApplicationConfig.ApplicationEncoding);
        }
        /// <summary>
        /// 获取工作单元接口内容
        /// </summary>
        /// <param name="subSystemName"></param>
        /// <returns></returns>
        private string GetUnitOfWorkInterfaceFileContent(string subSystemName)
        {
            string result = "using Materal.TTA.Common;\r\n";
            result += $"namespace {subSystemName}.EFRepository\r\n";
            result += "{\r\n";
            result += "    /// <summary>\r\n";
            result += $"    /// {subSystemName}工作单元接口\r\n";
            result += "    /// </summary>\r\n";
            result += $"    public interface I{subSystemName}UnitOfWork : IUnitOfWork\r\n";
            result += "    {\r\n";
            result += "    }\r\n";
            result += "}";
            return result;
        }
        /// <summary>
        /// 创建数据库上下文文件
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="subSystemName"></param>
        private void CreateDbContextFile(string targetPath, string subSystemName)
        {
            TextFileManager.WriteText($"{targetPath}/{subSystemName}DbContext.cs", GetDbContextFileContent(subSystemName), ApplicationConfig.ApplicationEncoding);
        }
        /// <summary>
        /// 获取数据库上下文内容
        /// </summary>
        /// <param name="subSystemName"></param>
        /// <returns></returns>
        private string GetDbContextFileContent(string subSystemName)
        {
            string result = "using Microsoft.EntityFrameworkCore;\r\n";
            result += "using System.Reflection;\r\n";
            result += $"using {subSystemName}.Domain;\r\n";
            result += $"using {subSystemName}.Domain.Views;\r\n";
            result += $"namespace {subSystemName}.EFRepository\r\n";
            result += "{\r\n";
            result += "    /// <summary>\r\n";
            result += $"    /// {subSystemName}数据库上下文\r\n";
            result += "    /// </summary>\r\n";
            result += $"    public sealed class {subSystemName}DbContext : DbContext\r\n";
            result += "    {\r\n";
            result += $"        public {subSystemName}DbContext(DbContextOptions<{subSystemName}DbContext> options) : base(options)\r\n";
            result += "        {\r\n";
            result += "        }\r\n";
            foreach (DomainModel domain in _domains)
            {
                result += "        /// <summary>\r\n";
                result += $"        /// {domain.DomainName}\r\n";
                result += "        /// </summary>\r\n";
                result += $"        public DbSet<{domain.Name}> {domain.Name} " + "{ get; set; }\r\n";
            }
            foreach (ViewModel view in _views)
            {
                result += "        /// <summary>\r\n";
                result += $"        /// {view.ViewName}\r\n";
                result += "        /// </summary>\r\n";
                result += $"        public DbQuery<{view.Name}> {view.Name} " + "{ get; set; }\r\n";
            }
            result += "        protected override void OnModelCreating(ModelBuilder modelBuilder)\r\n";
            result += "        {\r\n";
            result += "            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());\r\n";
            result += "        }\r\n";
            result += "    }\r\n";
            result += "}";
            return result;
        }
    }
}
