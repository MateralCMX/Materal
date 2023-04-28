﻿using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;

namespace Materal.Oscillator.SqliteRepositoryImpl.Repositories
{
    public class WorkEventViewRepositoryImpl : OscillatorSqliteEFRepositoryImpl<WorkEventView>, IWorkEventViewRepository
    {
        public WorkEventViewRepositoryImpl(OscillatorSqliteDBContext dbContext) : base(dbContext)
        {
        }
    }
}