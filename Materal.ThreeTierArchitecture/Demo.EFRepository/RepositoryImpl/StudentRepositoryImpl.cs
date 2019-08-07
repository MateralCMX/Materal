using Demo.Domain;
using Demo.Domain.Repositories;
using Materal.TTA.Common.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Demo.EFRepository.RepositoryImpl
{

    public class StudentRepositoryImpl : DemoEFRepositoryImpl<Student, Guid>, IStudentRepository
    {
        public StudentRepositoryImpl(DemoDbContext dbContext, IEnumerable<SqlServerSubordinateConfigModel> subordinateConfigs, Action<DbContextOptionsBuilder, string> optionAction) : base(dbContext, subordinateConfigs, optionAction)
        {
        }
    }
}
