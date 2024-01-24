﻿using Materal.TTA.SqliteEFRepository;

namespace RC.Core.Repository
{
    /// <summary>
    /// RC仓储模块
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RCRepositoryModule<T> : RepositoryModule<T, SqliteConfigModel>, IMergeBlockModule
        where T : DbContext
    {
        /// <summary>
        /// 添加数据库上下文
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dBConfig"></param>
        protected override void AddDBContext(IServiceCollection services, SqliteConfigModel dBConfig) => services.AddTTASqliteEFRepository<T>(dBConfig.ConnectionString, GetType().Assembly);
    }
}