﻿using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.TTA.ADONETRepository;

namespace Materal.BusinessFlow.SqliteRepository.Repositories
{
    public class UserRepositoryImpl : BusinessFlowRepositoryImpl<User>, IUserRepository
    {
        public UserRepositoryImpl(IADONETUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
