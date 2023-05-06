﻿using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;

namespace Materal.Oscillator.SqlServerRepository.Repositories
{
    /// <summary>
    /// 任务仓储
    /// </summary>
    public class WorkRepositoryImpl : OscillatorRepositoryImpl<Work>, IWorkRepository
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        public WorkRepositoryImpl(OscillatorSqlServerDBContext dbContext) : base(dbContext)
        {
        }
    }
}