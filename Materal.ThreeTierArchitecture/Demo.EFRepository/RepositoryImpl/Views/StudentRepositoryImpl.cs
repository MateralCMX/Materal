using Demo.Domain.Repositories.Views;
using Demo.Domain.Views;
using Materal.TTA.Common.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Demo.EFRepository.RepositoryImpl.Views
{

    public class StudentInfoViewRepositoryImpl : DemoEFRepositoryImpl<StudentInfoView, Guid>, IStudentInfoViewRepository
    {
        public StudentInfoViewRepositoryImpl(DemoDbContext dbContext, IEnumerable<SqlServerSubordinateConfigModel> subordinateConfigs, Action<DbContextOptionsBuilder, string> optionAction) : base(dbContext, subordinateConfigs, optionAction)
        {
        }
    }
}
