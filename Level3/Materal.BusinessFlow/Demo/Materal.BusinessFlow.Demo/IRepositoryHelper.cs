using Microsoft.Extensions.DependencyInjection;

namespace Materal.BusinessFlow.Demo
{
    public interface IRepositoryHelper
    {
        void AddRepository(IServiceCollection services);
        void Init(IServiceProvider service);
    }
}
