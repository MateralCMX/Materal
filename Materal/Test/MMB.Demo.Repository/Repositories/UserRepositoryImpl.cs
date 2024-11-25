using Materal.Extensions.DependencyInjection;
using MMB.Demo.Abstractions.Domain;
using MMB.Demo.Abstractions.Repositories;

namespace MMB.Demo.Repository.Repositories
{
    /// <summary>
    /// 用户仓储
    /// </summary>
    public partial class UserRepositoryImpl(DemoDBContext dbContext) : DemoRepositoryImpl<User>(dbContext), IUserRepository, IScopedDependency<IUserRepository>
    {
    }
}
