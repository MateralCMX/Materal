using Materal.BaseCore.CodeGenerator;
using Materal.BaseCore.CodeGenerator.Extensions;
using Materal.BaseCore.CodeGenerator.Models;
using System.IO;
using System.Text;

namespace RC.Core.CodeGenerator
{
    public class InitProject : IMateralBaseCoreCodeGeneratorPlug
    {
        public void PlugExecute(DomainPlugModel model)
        {
            CreateApplicationConfig(model);
            CreateDBContextFactory(model);
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
            codeContent.AppendLine($"using Materal.TTA.SqliteRepository.Model;");
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
            codeContent.AppendLine($"using RC.{model.EFRepositoryProject.ProjectName}.Common;");
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
            codeContent.AppendLine($"using RC.Core.WebAPI;");
            codeContent.AppendLine($"using RC.{model.WebAPIProject.ProjectName}.Common;");
            codeContent.AppendLine($"using RC.{model.WebAPIProject.ProjectName}.EFRepository;");
            codeContent.AppendLine($"using System.Reflection;");
            codeContent.AppendLine($"using Swashbuckle.AspNetCore.SwaggerGen;");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace RC.{model.WebAPIProject.ProjectName}.WebAPI");
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
            codeContent.AppendLine($"        public virtual IServiceCollection Add{model.WebAPIProject.ProjectName}Service(IServiceCollection services) => AddRC{model.WebAPIProject.ProjectName}Service(services);");
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
            codeContent.AppendLine($"        public virtual IServiceCollection AddRC{model.WebAPIProject.ProjectName}Service(IServiceCollection services, Action<SwaggerGenOptions>? swaggerGenConfig = null)");
            codeContent.AppendLine($"        {{");
            codeContent.AppendLine($"            string basePath = AppDomain.CurrentDomain.BaseDirectory;");
            codeContent.AppendLine($"            string[] swaggerXmlPaths = new[]");
            codeContent.AppendLine($"            {{");
            codeContent.AppendLine($"                $\"{{basePath}}RC.{model.WebAPIProject.ProjectName}.WebAPI.xml\",");
            codeContent.AppendLine($"                $\"{{basePath}}RC.{model.WebAPIProject.ProjectName}.DataTransmitModel.xml\",");
            codeContent.AppendLine($"                $\"{{basePath}}RC.{model.WebAPIProject.ProjectName}.PresentationModel.xml\"");
            codeContent.AppendLine($"            }};");
            codeContent.AppendLine($"            services.AddMateralCoreServices(Assembly.GetExecutingAssembly());");
            codeContent.AppendLine($"            services.AddRCService<{model.WebAPIProject.ProjectName}DBContext>(ApplicationConfig.DBConfig, swaggerGenConfig, swaggerXmlPaths);");
            codeContent.AppendLine($"            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load(\"RC.{model.WebAPIProject.ProjectName}.EFRepository\"))");
            codeContent.AppendLine($"                .Where(m => !m.IsAbstract && m.Name.EndsWith(\"RepositoryImpl\"))");
            codeContent.AppendLine($"                .AsPublicImplementedInterfaces();");
            codeContent.AppendLine($"            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load(\"RC.{model.WebAPIProject.ProjectName}.ServiceImpl\"))");
            codeContent.AppendLine($"                .Where(m => !m.IsAbstract && m.Name.EndsWith(\"ServiceImpl\"))");
            codeContent.AppendLine($"                .AsPublicImplementedInterfaces();");
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
            codeContent.AppendLine($"using Materal.Abstractions;");
            codeContent.AppendLine($"using Materal.TTA.EFRepository;");
            codeContent.AppendLine($"using RC.Core.WebAPI;");
            codeContent.AppendLine($"using RC.{model.WebAPIProject.ProjectName}.EFRepository;");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {model.WebAPIProject.Namespace}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// 主程序");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class Program : RCProgram");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 入口函数");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        /// <param name=\"args\"></param>");
            codeContent.AppendLine($"        /// <returns></returns>");
            codeContent.AppendLine($"        public static async Task Main(string[] args)");
            codeContent.AppendLine($"        {{");
            codeContent.AppendLine($"            Program program = new();");
            codeContent.AppendLine($"            WebApplication app = RCProgram.RCStart(args, services =>");
            codeContent.AppendLine($"            {{");
            codeContent.AppendLine($"                new {model.WebAPIProject.ProjectName}DIManager().Add{model.WebAPIProject.ProjectName}Service(services);");
            codeContent.AppendLine($"                program.ConfigService(services);");
            codeContent.AppendLine($"            }}, program.ConfigApp, \"RC.{model.WebAPIProject.ProjectName}\");");
            codeContent.AppendLine($"            MateralServices.Services ??= app.Services;");
            codeContent.AppendLine($"            MigrateHelper<{model.WebAPIProject.ProjectName}DBContext> migrateHelper = MateralServices.GetService<MigrateHelper<{model.WebAPIProject.ProjectName}DBContext>>();");
            codeContent.AppendLine($"            await migrateHelper.MigrateAsync();");
            codeContent.AppendLine($"            await program.InitAsync(args, app.Services, app);");
            codeContent.AppendLine($"            await app.RunAsync();");
            codeContent.AppendLine($"        }}");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveFile(model.WebAPIProject.GeneratorRootPath, configFileInfo.Name);
        }
    }
}