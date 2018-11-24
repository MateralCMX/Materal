using Materal.ApplicationUpdate.Domain;
using Materal.ApplicationUpdate.Domain.IRepositorys;
using Materal.TTA.SqliteRepository;
using System;

namespace Materal.ApplicationUpdate.EFRepository.RepositoryImpls
{
    public class UserRepositoryImpl : SqliteEFRepositoryImpl<User, Guid>, IUserRepository
    {
        public UserRepositoryImpl(AppUpdateContext dbContext) : base(dbContext)
        {
        }
    }
}
