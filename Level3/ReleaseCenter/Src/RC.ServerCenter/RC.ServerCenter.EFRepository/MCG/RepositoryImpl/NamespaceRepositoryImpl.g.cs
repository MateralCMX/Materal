using Microsoft.EntityFrameworkCore;
using RC.Core.EFRepository;
using RC.ServerCenter.Domain;
using RC.ServerCenter.Domain.Repositories;

namespace RC.ServerCenter.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 命名空间仓储实现
    /// </summary>
    public partial class NamespaceRepositoryImpl: RCEFRepositoryImpl<Namespace, Guid>, INamespaceRepository
    {
    }
}
