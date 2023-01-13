using Materal.TTA.EFRepository;

namespace RC.Authority.Domain.Repositories
{
    /// <summary>
    /// 用户仓储接口
    /// </summary>
    public partial interface IUserRepository : IEFRepository<User, Guid> { }
}