using Microsoft.Extensions.DependencyInjection;

namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// Materal服务作用域工厂
    /// </summary>
    public class MateralServiceScopeFactory : IServiceScopeFactory
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly Func<Type, Type, bool> _filter;
        /// <summary>
        /// 构造方法
        /// </summary>
        public MateralServiceScopeFactory(IServiceScopeFactory scopeFactory, Func<Type, Type, bool> filter)
        {
            _scopeFactory = scopeFactory;
            _filter = filter;
        }
        /// <summary>
        /// 创建作用域
        /// </summary>
        /// <returns></returns>
        public IServiceScope CreateScope()
        {
            IServiceScope serviceScope = _scopeFactory.CreateScope();
            IServiceScope result = new MateralServiceScope(serviceScope, _filter);
            return result;
        }
    }
}
