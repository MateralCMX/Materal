using Materal.TTA.Common;
using Materal.TTA.Common.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Demo.EFRepository
{
    /// <summary>
    /// Demo仓储实现
    /// </summary>
    public class DemoEFRepositoryImpl<T, TKey> : EFSubordinateRepositoryImpl<T, TKey, DemoDbContext> where T : class, IEntity<TKey>, new()
    {
        public DemoEFRepositoryImpl(DemoDbContext dbContext, IEnumerable<SqlServerSubordinateConfigModel> subordinateConfigs, Action<DbContextOptionsBuilder, string> optionAction) : base(dbContext, subordinateConfigs, optionAction)
    {
    }
}
}
