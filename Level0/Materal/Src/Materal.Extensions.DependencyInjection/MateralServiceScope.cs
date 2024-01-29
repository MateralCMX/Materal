namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// Materal服务作用域
    /// </summary>
    public sealed class MateralServiceScope : IServiceScope
    {
        private readonly IServiceScope _serviceScope;
        private readonly MateralServiceProvider _materalServiceProvider;
        /// <summary>
        /// 构造方法
        /// </summary>
        public MateralServiceScope(IServiceScope serviceScope)
        {
            _serviceScope = serviceScope;
            _materalServiceProvider = new MateralServiceProvider(_serviceScope.ServiceProvider);
        }
        /// <summary>
        /// 服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider => _materalServiceProvider;
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose() => _serviceScope.Dispose();
    }
}
