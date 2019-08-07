using Demo.Domain;
using Demo.Domain.Repositories;
using Materal.TTA.Common.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Demo.EFRepository.RepositoryImpl
{
    public class ClassRepositoryImpl : DemoEFRepositoryImpl<Class, Guid>, IClassRepository
    {
        public ClassRepositoryImpl(DemoDbContext dbContext, IEnumerable<SqlServerSubordinateConfigModel> subordinateConfigs, Action<DbContextOptionsBuilder, string> optionAction) : base(dbContext, subordinateConfigs, optionAction)
        {
        }
    }
}
