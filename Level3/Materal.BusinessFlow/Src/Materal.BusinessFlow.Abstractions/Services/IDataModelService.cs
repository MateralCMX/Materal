﻿using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.Abstractions.Services.Models;

namespace Materal.BusinessFlow.Abstractions.Services
{
    public interface IDataModelService : IBaseService<DataModel, DataModel, IDataModelRepository, QueryDataModelModel>
    {

    }
}
