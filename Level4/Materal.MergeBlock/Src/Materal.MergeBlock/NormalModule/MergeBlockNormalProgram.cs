using Materal.MergeBlock.Abstractions.NormalModule;

namespace Materal.MergeBlock.NormalModule
{
    /// <summary>
    /// MergeBlock程序
    /// </summary>
    public class MergeBlockNormalProgram : MergeBlockProgram<IMergeBlockNormalModule, NormalModuleInfo, INormalConfigServiceContext, INormalApplicationContext>, IMergeBlockProgram
    {
        private IServiceCollection? _services;
        private IServiceProvider? _serviceProvider;
        private ConfigurationManager? _configuration;
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="args"></param>
        /// <param name="autoRemoveAssemblies"></param>
        /// <returns></returns>
        public override async Task RunAsync(string[] args, bool autoRemoveAssemblies = true)
        {
            _configuration = new();
            _configuration.AddCommandLine(args);
            _configuration.AddJsonFile("appsettings.json", true, true);
            _services = new ServiceCollection();
            _services.TryAddSingleton<IConfiguration>(_configuration);
            _services.TryAddSingleton<IConfigurationBuilder>(_configuration);
            _services.TryAddSingleton<IConfigurationManager>(_configuration);
            _services.TryAddSingleton<IConfigurationRoot>(_configuration);
            await ConfigModuleAsync(_services, _configuration, autoRemoveAssemblies);
            _serviceProvider = _services.BuildMateralServiceProvider();
            await InitModuleAsync(_serviceProvider);
        }
        /// <summary>
        /// 获得配置服务上下文
        /// </summary>
        /// <returns></returns>
        protected override INormalConfigServiceContext GetConfigServiceContext()
        {
            if (_services is null) throw new MergeBlockException("未初始化ServiceCollection");
            if (_configuration is null) throw new MergeBlockException("未初始化ConfigurationManager");
            return new NormalConfigServiceContext(_services, _configuration);
        }
        /// <summary>
        /// 获得配置服务上下文
        /// </summary>
        /// <returns></returns>
        protected override INormalApplicationContext GetApplicationContext()
        {
            if (_serviceProvider is null) throw new MergeBlockException("未初始化ServiceProvider");
            return new NormalApplicationContext(_serviceProvider);
        }
    }
}
