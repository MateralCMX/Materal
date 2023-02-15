﻿using Materal.TTA.Common;
using Materal.TTA.SqliteRepository;
using Microsoft.EntityFrameworkCore;

namespace MBC.Core.EFRepository
{
    /// <summary>
    /// MBC仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public abstract class MBCEFRepositoryImpl<T, TPrimaryKeyType> : SqliteEFRepositoryImpl<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        protected MBCEFRepositoryImpl(DbContext dbContext) : base(dbContext)
        {
        }
    }
}