using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Materal.MergeBlock
{
    /// <summary>
    /// MergeBlock程序
    /// </summary>
    public class MergeBlockProgram
    {
        private static ILogger<MergeBlockProgram>? _logger;
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task RunAsync(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            ConfigServiceContext configServiceContext = new(builder.Configuration, builder.Services);
            List<IMergeBlockModuleInfo> moduleInfos = LoadAllMergeBlockModule(builder.Services);
            await RunAllModuleFuncAsync(moduleInfos, async (_, module) => await module.OnConfigServiceBeforeAsync(configServiceContext));
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.SetIsOriginAllowed(_ => true)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    });
            });
            configServiceContext.MvcBuilder = builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = true)
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            await RunAllModuleFuncAsync(moduleInfos, async (_, module) => await module.OnConfigServiceAsync(configServiceContext));
            builder.Services.AddResponseCompression();
            await RunAllModuleFuncAsync(moduleInfos, async (_, module) => await module.OnConfigServiceAfterAsync(configServiceContext));
            builder.Services.AddEndpointsApiExplorer();
            WebApplication app = builder.Build();
            MergeBlockManager.ServiceProvider = app.Services;
            ApplicationContext applicationContext = new(app, app.Services);
            _logger = app.Services.GetService<ILogger<MergeBlockProgram>>();
            _logger?.LogDebug("MergeBlock启动");
            _logger?.LogDebug($"共找到{moduleInfos.Count}个模块");
            await RunAllModuleFuncAsync(moduleInfos, async (_, module) => await module.OnApplicationInitBeforeAsync(applicationContext));
            app.UseCors();
            await RunAllModuleFuncAsync(moduleInfos, async (moduleInfo, module) =>
            {
                _logger?.LogDebug($"正在初始化{moduleInfo.ModuleName}模块[{moduleInfo.ModuleAttribute.Description}]");
                await module.OnApplicationInitAsync(applicationContext);
            });
            app.MapControllers();
            await RunAllModuleFuncAsync(moduleInfos, async (_, module) => await module.OnApplicationInitAfterAsync(applicationContext));
            _logger?.LogDebug("MergeBlock初始化完毕");
            await app.RunAsync();
            _logger?.LogDebug("MergeBlock停止");
        }
        /// <summary>
        /// 运行所有模块
        /// </summary>
        /// <param name="moduleInfos"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private static async Task RunAllModuleFuncAsync(List<IMergeBlockModuleInfo> moduleInfos, Func<IMergeBlockModuleInfo, IMergeBlockModule, Task> func) => await RunAllModuleFuncAsync(moduleInfos, async moduleInfo =>
        {
            if (moduleInfo.Module is null) return;
            await func.Invoke(moduleInfo, moduleInfo.Module);
        });
        /// <summary>
        /// 运行所有模块
        /// </summary>
        /// <param name="moduleInfos"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private static async Task RunAllModuleFuncAsync(List<IMergeBlockModuleInfo> moduleInfos, Func<IMergeBlockModuleInfo, Task> func)
        {
            List<IMergeBlockModuleInfo> completeModuleInfos = [];
            moduleInfos = [.. moduleInfos.OrderBy(m => m.ModuleAttribute.Depends.Length)];
            while (moduleInfos.Count != completeModuleInfos.Count)
            {
                foreach (IMergeBlockModuleInfo moduleInfo in moduleInfos)
                {
                    if (completeModuleInfos.Contains(moduleInfo)) continue;
                    string[] depends = moduleInfo.ModuleAttribute.Depends;
                    if (depends.Length > 0)
                    {
                        bool isOK = true;
                        foreach (string depend in depends)
                        {
                            if (moduleInfos.Any(m => m.ModuleName != depend)) throw new MergeBlockException($"模块{moduleInfo.ModuleName}的依赖{depend}不存在");
                            if (completeModuleInfos.Any(m => m.ModuleName == depend)) continue;
                            isOK = false;
                            break;
                        }
                        if (!isOK) continue;
                    }
                    await func.Invoke(moduleInfo);
                    completeModuleInfos.Add(moduleInfo);
                }
            }
        }
        /// <summary>
        /// 加载所有MergeBlock模块
        /// </summary>
        /// <param name="services"></param>
        /// <exception cref="MergeBlockException"></exception>
        private static List<IMergeBlockModuleInfo> LoadAllMergeBlockModule(IServiceCollection services)
        {
            List<IMergeBlockModuleInfo> mergeBlockModuleInfos = [];
            DirectoryInfo directoryInfo = new(AppDomain.CurrentDomain.BaseDirectory);
            LoadMergeBlockModuleFormDirectoryInfo(mergeBlockModuleInfos, directoryInfo);
            foreach (IMergeBlockModuleInfo moduleInfo in mergeBlockModuleInfos)
            {
                services.AddSingleton(moduleInfo);
            }
            return mergeBlockModuleInfos;
        }
        /// <summary>
        /// 从目录信息中加载MergeBlock模块
        /// </summary>
        /// <param name="mergeBlockModuleInfos"></param>
        /// <param name="directoryInfo"></param>
        /// <exception cref="MergeBlockException"></exception>
        private static void LoadMergeBlockModuleFormDirectoryInfo(List<IMergeBlockModuleInfo> mergeBlockModuleInfos, DirectoryInfo directoryInfo)
        {
            if (!directoryInfo.Exists) return;
            FileInfo[] allDllFile = directoryInfo.GetFiles("*.dll");
            LoadMergeBlockModuleFormFileInfos(mergeBlockModuleInfos, allDllFile);
            DirectoryInfo[] allDirectoryInfo = directoryInfo.GetDirectories();
            foreach (DirectoryInfo subDirectoryInfo in allDirectoryInfo)
            {
                LoadMergeBlockModuleFormDirectoryInfo(mergeBlockModuleInfos, subDirectoryInfo);
            }
        }
        /// <summary>
        /// 从文件信息中加载MergeBlock模块
        /// </summary>
        /// <param name="mergeBlockModuleInfos"></param>
        /// <param name="fileInfos"></param>
        private static void LoadMergeBlockModuleFormFileInfos(List<IMergeBlockModuleInfo> mergeBlockModuleInfos, params FileInfo[] fileInfos)
        {
            foreach (FileInfo fileInfo in fileInfos)
            {
                if (!fileInfo.Exists) continue;
                Assembly assembly = Assembly.LoadFrom(fileInfo.FullName);
                LoadMergeBlockModuleFormAssembly(mergeBlockModuleInfos, assembly);
            }
        }
        /// <summary>
        /// 从程序集中加载MergeBlock模块
        /// </summary>
        /// <param name="mergeBlockModuleInfos"></param>
        /// <param name="assembly"></param>
        private static void LoadMergeBlockModuleFormAssembly(List<IMergeBlockModuleInfo> mergeBlockModuleInfos, Assembly assembly)
        {
            MergeBlockAssemblyAttribute? mergeBlockModuleAttribute = assembly.GetCustomAttribute<MergeBlockAssemblyAttribute>();
            if (mergeBlockModuleAttribute is null) return;
            IMergeBlockModuleInfo mergeBlockModuleInfo = new MergeBlockModuleInfo(assembly);
            mergeBlockModuleInfos.Add(mergeBlockModuleInfo);
        }
    }
}
