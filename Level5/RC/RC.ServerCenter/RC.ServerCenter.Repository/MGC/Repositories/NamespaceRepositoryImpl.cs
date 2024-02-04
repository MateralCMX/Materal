﻿namespace RC.ServerCenter.Repository.Repositories
{
    /// <summary>
    /// 命名空间仓储
    /// </summary>
    public partial class NamespaceRepositoryImpl(ServerCenterDBContext dbContext) : ServerCenterRepositoryImpl<Namespace>(dbContext), INamespaceRepository, IScopedDependencyInjectionService<INamespaceRepository>
    {
    }
}
