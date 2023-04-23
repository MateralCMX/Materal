﻿using Materal.Oscillator.Abstractions;
using Materal.TTA.Common;
using Materal.TTA.SqliteRepository;

namespace Materal.Oscillator.SqliteRepositoryImpl.Repositories
{
    public abstract class OscillatorSqliteEFRepositoryImpl<T> : SqliteEFRepositoryImpl<T, Guid, OscillatorSqliteDBContext>
        where T : class, IEntity<Guid>, new()
    {
        protected OscillatorSqliteEFRepositoryImpl(OscillatorSqliteDBContext dbContext) : base(dbContext)
        {
        }
    }
}