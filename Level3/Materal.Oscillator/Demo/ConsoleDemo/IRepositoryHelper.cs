using Microsoft.Extensions.DependencyInjection;

namespace ConsoleDemo
{
    public interface IRepositoryHelper
    {
        void AddRepository(IServiceCollection services);
        void AddDRRepository(IServiceCollection services);
        void Init(IServiceProvider service);
    }
}
