﻿using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;

namespace Materal.Oscillator.SqlServerRepositoryImpl.Repositories
{
    public class ScheduleWorkRepositoryImpl : OscillatorSqlServerEFRepositoryImpl<ScheduleWork>, IScheduleWorkRepository
    {
        public ScheduleWorkRepositoryImpl(OscillatorSqlServerDBContext dbContext) : base(dbContext)
        {
        }
    }
}