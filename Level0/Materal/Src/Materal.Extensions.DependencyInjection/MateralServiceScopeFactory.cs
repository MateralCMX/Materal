namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// Materal服务作用域工厂
    /// </summary>
    public class MateralServiceScopeFactory(IServiceScopeFactory scopeFactory) : IServiceScopeFactory
    {
        /// <summary>
        /// 创建作用域
        /// </summary>
        /// <returns></returns>
        public IServiceScope CreateScope()
        {
            IServiceScope serviceScope = scopeFactory.CreateScope();
            IServiceScope result = new MateralServiceScope(serviceScope);
            return result;
        }
    }
}
