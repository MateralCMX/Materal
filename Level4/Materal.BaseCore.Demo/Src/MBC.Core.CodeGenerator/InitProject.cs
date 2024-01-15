using Materal.BaseCore.CodeGenerator;
using Materal.BaseCore.CodeGenerator.Extensions;
using Materal.BaseCore.CodeGenerator.Models;
using System.IO;
using System.Text;

namespace MBC.Core.CodeGenerator
{
    public class InitProject : IMateralBaseCoreCodeGeneratorPlug
    {
        public void PlugExecute(DomainPlugModel model)
        {
            CreateApplicationConfig(model);
            CreateDBContextFactory(model);
            CreateUnitOfWork(model);
            CreateProgram(model);
            CreateDIManager(model);
        }
        /// <summary>
        /// 创建ApplicationConfig.cs
        /// </summary>
        /// <param name="model"></param>
        private static void CreateApplicationConfig(DomainPlugModel model)
        {
            if (model.CommonProject == null) return;
            string configFilePath = Path.Combine(model.CommonProject.GeneratorRootPath, "ApplicationConfig.g.cs");
            FileInfo configFileInfo = new(configFilePath);
            if (configFileInfo.Exists) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using Materal.BaseCore.Common;");
            codeContent.AppendLine($"using Materal.TTA.SqliteEFRepository;");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {model.CommonProject.Namespace}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// 应用程序配置");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class ApplicationConfig");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 数据库配置");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        public static SqliteConfigModel DBConfig");
            codeContent.AppendLine($"        {{");
            codeContent.AppendLine($"            get");
            codeContent.AppendLine($"            {{");
            codeContent.AppendLine($"                SqliteConfigModel result = MateralCoreConfig.GetValueObject<SqliteConfigModel>(nameof(DBConfig), new SqliteConfigModel {{Source = \"./{model.CommonProject.ProjectName}.db\"}});");
            codeContent.AppendLine($"                if (result.Source.StartsWith(\"./\"))");
            codeContent.AppendLine($"                {{");
            codeContent.AppendLine($"                    result.Source = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, result.Source[2..]);");
            codeContent.AppendLine($"                }}");
            codeContent.AppendLine($"                return result;");
            codeContent.AppendLine($"            }}");
            codeContent.AppendLine($"        }}");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveFile(model.CommonProject.GeneratorRootPath, configFileInfo.Name);
        }
        /// <summary>
        /// 创建DBContextFactory
        /// </summary>
        /// <param name="model"></param>
        private static void CreateDBContextFactory(DomainPlugModel model)
        {
            if (model.EFRepositoryProject == null) return;
            string configFilePath = Path.Combine(model.EFRepositoryProject.GeneratorRootPath, $"{model.EFRepositoryProject.ProjectName}DBContextFactory.g.cs");
            FileInfo configFileInfo = new(configFilePath);
            if (configFileInfo.Exists) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using Microsoft.EntityFrameworkCore;");
            codeContent.AppendLine($"using Microsoft.EntityFrameworkCore.Design;");
            codeContent.AppendLine($"using {model.EFRepositoryProject.PrefixName}.{model.EFRepositoryProject.ProjectName}.Common;");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {model.EFRepositoryProject.Namespace}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// 数据库上下文工厂");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {model.EFRepositoryProject.ProjectName}DBContextFactory : IDesignTimeDbContextFactory<{model.EFRepositoryProject.ProjectName}DBContext>");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 创建数据库连接");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        /// <param name=\"args\"></param>");
            codeContent.AppendLine($"        /// <returns></returns>");
            codeContent.AppendLine($"        public {model.EFRepositoryProject.ProjectName}DBContext CreateDbContext(string[] args)");
            codeContent.AppendLine($"        {{");
            codeContent.AppendLine($"            DbContextOptionsBuilder<{model.EFRepositoryProject.ProjectName}DBContext> optionsBuilder = new();");
            codeContent.AppendLine($"            optionsBuilder.UseSqlite(ApplicationConfig.DBConfig.ConnectionString);");
            codeContent.AppendLine($"            return new {model.EFRepositoryProject.ProjectName}DBContext(optionsBuilder.Options);");
            codeContent.AppendLine($"        }}");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveFile(model.EFRepositoryProject.GeneratorRootPath, configFileInfo.Name);
        }
        /// <summary>
        /// 创建UnitOfWork
        /// </summary>
        /// <param name="model"></param>
        private static void CreateUnitOfWork(DomainPlugModel model)
        {
            if (model.EFRepositoryProject == null) return;
            string configFilePath = Path.Combine(model.EFRepositoryProject.GeneratorRootPath, $"{model.EFRepositoryProject.ProjectName}UnitOfWorkImpl.g.cs");
            FileInfo configFileInfo = new(configFilePath);
            if (configFileInfo.Exists) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using Materal.BaseCore.EFRepository;");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {model.EFRepositoryProject.PrefixName}.{model.EFRepositoryProject.ProjectName}.EFRepository");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    public class {model.EFRepositoryProject.ProjectName}UnitOfWorkImpl : MateralCoreUnitOfWorkImpl<{model.EFRepositoryProject.ProjectName}DBContext>");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        public {model.EFRepositoryProject.ProjectName}UnitOfWorkImpl({model.EFRepositoryProject.ProjectName}DBContext context, IServiceProvider serviceProvider) : base(context, serviceProvider) {{ }}");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveFile(model.EFRepositoryProject.GeneratorRootPath, configFileInfo.Name);
        }
        /// <summary>
        /// 生成DIManager
        /// </summary>
        /// <param name="model"></param>
        private static void CreateDIManager(DomainPlugModel model)
        {
            if (model.WebAPIProject == null) return;
            string configFilePath = Path.Combine(model.WebAPIProject.GeneratorRootPath, $"{model.WebAPIProject.ProjectName}DIManager.g.cs");
            FileInfo configFileInfo = new(configFilePath);
            if (configFileInfo.Exists) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"#nullable enable");
            codeContent.AppendLine($"using Materal.BaseCore.Common;");
            codeContent.AppendLine($"using NetCore.AutoRegisterDi;");
            codeContent.AppendLine($"using {model.WebAPIProject.PrefixName}.Core.WebAPI;");
            codeContent.AppendLine($"using {model.WebAPIProject.PrefixName}.{model.WebAPIProject.ProjectName}.Common;");
            codeContent.AppendLine($"using {model.WebAPIProject.PrefixName}.{model.WebAPIProject.ProjectName}.EFRepository;");
            codeContent.AppendLine($"using System.Reflection;");
            codeContent.AppendLine($"using Swashbuckle.AspNetCore.SwaggerGen;");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {model.WebAPIProject.PrefixName}.{model.WebAPIProject.ProjectName}.WebAPI");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {model.WebAPIProject.ProjectName}依赖注入管理器");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {model.WebAPIProject.ProjectName}DIManager : DIManager");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 添加{model.WebAPIProject.ProjectName}服务");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        /// <param name=\"services\"></param>");
            codeContent.AppendLine($"        /// <returns></returns>");
            codeContent.AppendLine($"        public virtual IServiceCollection Add{model.WebAPIProject.ProjectName}Service(IServiceCollection services) => Add{model.WebAPIProject.PrefixName}{model.WebAPIProject.ProjectName}Service(services);");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// 依赖注入管理器");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public abstract class DIManager");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 添加{model.WebAPIProject.ProjectName}服务");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        /// <param name=\"services\"></param>");
            codeContent.AppendLine($"        /// <param name=\"swaggerGenConfig\"></param>");
            codeContent.AppendLine($"        /// <returns></returns>");
            codeContent.AppendLine($"        public virtual IServiceCollection Add{model.WebAPIProject.PrefixName}{model.WebAPIProject.ProjectName}Service(IServiceCollection services, Action<SwaggerGenOptions>? swaggerGenConfig = null)");
            codeContent.AppendLine($"        {{");
            codeContent.AppendLine($"            string basePath = AppDomain.CurrentDomain.BaseDirectory;");
            codeContent.AppendLine($"            string[] swaggerXmlPaths = new[]");
            codeContent.AppendLine($"            {{");
            codeContent.AppendLine($"                $\"{{basePath}}{model.WebAPIProject.PrefixName}.{model.WebAPIProject.ProjectName}.WebAPI.xml\",");
            codeContent.AppendLine($"                $\"{{basePath}}{model.WebAPIProject.PrefixName}.{model.WebAPIProject.ProjectName}.DataTransmitModel.xml\",");
            codeContent.AppendLine($"                $\"{{basePath}}{model.WebAPIProject.PrefixName}.{model.WebAPIProject.ProjectName}.PresentationModel.xml\"");
            codeContent.AppendLine($"            }};");
            codeContent.AppendLine($"            services.AddMateralCoreServices(Assembly.GetExecutingAssembly());");
            codeContent.AppendLine($"            services.Add{model.WebAPIProject.PrefixName}Service<{model.WebAPIProject.ProjectName}DBContext>(ApplicationConfig.DBConfig, swaggerGenConfig, swaggerXmlPaths);");
            codeContent.AppendLine($"            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load(\"{model.WebAPIProject.PrefixName}.{model.WebAPIProject.ProjectName}.ServiceImpl\"))");
            codeContent.AppendLine($"                .Where(m => !m.IsAbstract && m.Name.EndsWith(\"ServiceImpl\"))");
            codeContent.AppendLine($"                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);");
            codeContent.AppendLine($"            return services;");
            codeContent.AppendLine($"        }}");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveFile(model.WebAPIProject.GeneratorRootPath, configFileInfo.Name);
        }
        /// <summary>
        /// 创建Program
        /// </summary>
        /// <param name="model"></param>
        private static void CreateProgram(DomainPlugModel model)
        {
            if (model.WebAPIProject == null) return;
            string configFilePath = Path.Combine(model.WebAPIProject.GeneratorRootPath, $"Program.g.cs");
            FileInfo configFileInfo = new(configFilePath);
            if (configFileInfo.Exists) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using Materal.TTA.EFRepository;");
            codeContent.AppendLine($"using {model.WebAPIProject.PrefixName}.Core.WebAPI;");
            codeContent.AppendLine($"using {model.WebAPIProject.PrefixName}.{model.WebAPIProject.ProjectName}.EFRepository;");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {model.WebAPIProject.Namespace}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// 主程序");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class Program : {model.WebAPIProject.PrefixName}Program");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 入口函数");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        /// <param name=\"args\"></param>");
            codeContent.AppendLine($"        /// <returns></returns>");
            codeContent.AppendLine($"        public static async Task Main(string[] args)");
            codeContent.AppendLine($"        {{");
            codeContent.AppendLine($"            Program program = new();");
            codeContent.AppendLine($"            WebApplication app = {model.WebAPIProject.PrefixName}Program.{model.WebAPIProject.PrefixName}Start(args, services =>");
            codeContent.AppendLine($"            {{");
            codeContent.AppendLine($"                new {model.WebAPIProject.ProjectName}DIManager().Add{model.WebAPIProject.ProjectName}Service(services);");
            codeContent.AppendLine($"                program.ConfigService(services);");
            codeContent.AppendLine($"            }}, program.ConfigApp, program.ConfigBuilder, \"{model.WebAPIProject.PrefixName}.{model.WebAPIProject.ProjectName}\");");
            codeContent.AppendLine($"            using (IServiceScope scope = app.Services.CreateScope())");
            codeContent.AppendLine($"            {{");
            codeContent.AppendLine($"                IMigrateHelper<{model.WebAPIProject.ProjectName}DBContext> migrateHelper = scope.ServiceProvider.GetRequiredService<IMigrateHelper<{model.WebAPIProject.ProjectName}DBContext>>();");
            codeContent.AppendLine($"                await migrateHelper.MigrateAsync();");
            codeContent.AppendLine($"                await program.InitAsync(args, scope.ServiceProvider, app);");
            codeContent.AppendLine($"            }}");
            codeContent.AppendLine($"            await app.RunAsync();");
            codeContent.AppendLine($"        }}");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveFile(model.WebAPIProject.GeneratorRootPath, configFileInfo.Name);
        }
    }
}