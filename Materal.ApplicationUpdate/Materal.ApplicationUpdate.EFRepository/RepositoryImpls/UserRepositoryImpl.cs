using Materal.ApplicationUpdate.Domain;
using Materal.TTA.SqliteRepository;
using System;

namespace Materal.ApplicationUpdate.EFRepository.RepositoryImpls
{
    public class UserRepositoryImpl : SqliteEFRepositoryImpl<User,Guid>
    {
        public UserRepositoryImpl(AppUpdateContext dbContext) : base(dbContext)
        {
        }
    }
}
