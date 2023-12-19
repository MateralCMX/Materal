using AutoMapper;
using Materal.Abstractions;
using Materal.MergeBlock.Abstractions.Config;
using Materal.MergeBlock.Abstractions.Services;
using Materal.MergeBlock.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json.Serialization;

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
            List<IMergeBlockModuleInfo> moduleInfos = LoadAllMergeBlockModule(builder.Services);
            ConfigServiceContext configServiceContext = new(builder.Host, builder.Configuration, builder.Services, moduleInfos);
            await RunAllModuleFuncAsync(moduleInfos, async (_, module) => await module.OnConfigServiceBeforeAsync(configServiceContext));
            #region 添加高优先级服务
            builder.Services.AddSingleton<IMergeBlockService, MergeBlockService>();
            builder.Services.AddMateralUtils();
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
            configServiceContext.MvcBuilder = builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ActionPageQueryFilterAttribute>();
                options.Filters.Add<BindBaseInfoToServiceFilterAttribute>();
                options.Filters.Add<GlobalExceptionFilter>();
                options.SuppressAsyncSuffixInActionNames = true;
            }).AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null)
            .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            await RunAllModuleFuncAsync(moduleInfos, async (moduleInfo, _) =>
            {
                configServiceContext.MvcBuilder.AddApplicationPart(moduleInfo.ModuleAssembly);
                await Task.CompletedTask;
            });
            #endregion
            await RunAllModuleFuncAsync(moduleInfos, async (_, module) => await module.OnConfigServiceAsync(configServiceContext));
            #region 添加低优先级服务
            builder.Services.AddResponseCompression();
            #endregion
            List<Assembly> autoMapperAssemblyList = [];
            await RunAllModuleFuncAsync(moduleInfos, async (moduleInfo, module) =>
            {
                await module.OnConfigServiceAfterAsync(configServiceContext);
                Type[] types = moduleInfo.ModuleAssembly.GetTypes();
                bool isAutoMapperProfileAssembly = false;
                foreach (Type type in types)
                {
                    if (!isAutoMapperProfileAssembly && type.IsSubclassOf(typeof(Profile)))
                    {
                        autoMapperAssemblyList.Add(moduleInfo.ModuleAssembly);
                        isAutoMapperProfileAssembly = true;
                        continue;
                    }
                    if(type.IsAssignableTo<IBaseService>())
                    {
                        List<Type> allInterfaces = type.GetAllInterfaces();
                        foreach (Type item in allInterfaces)
                        {
                            if (item.Name.StartsWith(nameof(IBaseService))) continue;
                            builder.Services.TryAddScoped(item, type);
                        }
                    }
                }
            });
            builder.Services.AddAutoMapper(config => config.AllowNullCollections = true, autoMapperAssemblyList);
            builder.Services.Configure<MergeBlockConfig>(builder.Configuration);
            builder.Services.AddEndpointsApiExplorer();
            WebApplication app = builder.Build();
            MateralServices.Services = app.Services;
            ApplicationContext applicationContext = new(app, app.Services);
            _logger = app.Services.GetService<ILogger<MergeBlockProgram>>();
            _logger?.LogDebug("MergeBlock启动");
            _logger?.LogDebug($"共找到{moduleInfos.Count}个模块");
            app.Services.GetRequiredService<IMergeBlockService>().InitMergeBlockManage();
            await RunAllModuleFuncAsync(moduleInfos, async (_, module) => await module.OnApplicationInitBeforeAsync(applicationContext));
            #region 初始化高优先级服务
            app.Use(async (context, next) =>
            {
                context.Request.EnableBuffering();
                await next.Invoke();
            });
            app.UseCors();
            #endregion
            await RunAllModuleFuncAsync(moduleInfos, async (moduleInfo, module) =>
            {
                _logger?.LogDebug($"正在初始化{moduleInfo.ModuleName}模块[{moduleInfo.ModuleAttribute.Description}]");
                await module.OnApplicationInitAsync(applicationContext);
            });
            #region 初始化低优先级服务
            app.MapControllers();
            if (MergeBlockManager.BaseUris.Any(m => m.Scheme == "https"))
            {
                app.UseHttpsRedirection();
            }
            #endregion
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
                            if (!moduleInfos.Any(m => m.ModuleName == depend)) throw new MergeBlockException($"模块{moduleInfo.ModuleName}的依赖{depend}不存在");
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
                try
                {
                    Assembly assembly = Assembly.LoadFrom(fileInfo.FullName);
                    LoadMergeBlockModuleFormAssembly(mergeBlockModuleInfos, assembly);
                }
                catch
                {
                }
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
