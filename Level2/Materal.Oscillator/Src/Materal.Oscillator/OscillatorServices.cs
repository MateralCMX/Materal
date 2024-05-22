namespace Materal.Oscillator
{
    /// <summary>
    /// Oscillator服务
    /// </summary>
    public static class OscillatorServices
    {
        private static IServiceProvider? _serviceProvider;
        /// <summary>
        /// 服务容器
        /// </summary>
        public static IServiceProvider ServiceProvider { get => _serviceProvider ?? throw new OscillatorException("获取服务容器失败"); set => _serviceProvider = value; }
    }
}
