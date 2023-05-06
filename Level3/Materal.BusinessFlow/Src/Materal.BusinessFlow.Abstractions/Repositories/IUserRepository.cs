﻿using Materal.BusinessFlow.Abstractions.Domain;
using Materal.TTA.Common;

namespace Materal.BusinessFlow.Abstractions.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
    }
}
