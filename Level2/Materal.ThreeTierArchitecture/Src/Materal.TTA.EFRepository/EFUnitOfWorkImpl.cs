using Materal.Abstractions;
using Materal.TTA.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.TTA.EFRepository
{
    public abstract class EFUnitOfWorkImpl<TDBContext> : IEFUnitOfWork
        where TDBContext : DbContext
    {
        private readonly object entitiesLockObj = new();
        protected readonly Queue<EntityEntry> ChangeEntities = new();
        private readonly TDBContext _dbContext;
        public IServiceProvider ServiceProvider { get; }

        protected EFUnitOfWorkImpl(TDBContext context, IServiceProvider serviceProvider)
        {
            _dbContext = context;
            ServiceProvider = serviceProvider;
        }

        public virtual void RegisterAdd<TEntity, TPrimaryKeyType>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct
        {
            lock (entitiesLockObj)
            {
                EntityEntry<TEntity> entity = _dbContext.Set<TEntity>().Add(obj);
                ChangeEntities.Enqueue(entity);
            }
        }

        public virtual void RegisterEdit<TEntity, TPrimaryKeyType>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct
        {
            lock (entitiesLockObj)
            {
                EntityEntry<TEntity> entity = _dbContext.Entry(obj);
                entity.State = EntityState.Modified;
                ChangeEntities.Enqueue(entity);
            }
        }

        public virtual void RegisterDelete<TEntity, TPrimaryKeyType>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct
        {
            lock (entitiesLockObj)
            {
                EntityEntry<TEntity> entity = _dbContext.Entry(obj);
                entity.State = EntityState.Deleted;
                ChangeEntities.Enqueue(entity);
            }
        }
        public void Commit()
        {
            try
            {
                _dbContext.SaveChanges();
                ClearChangeEntities();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
            }
        }
        public virtual async Task CommitAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                ClearChangeEntities();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
            }
        }
        /// <summary>
        /// 清空更改的实体
        /// </summary>
        private void ClearChangeEntities()
        {
            lock (entitiesLockObj)
            {
                while (ChangeEntities.Count > 0)
                {
                    if (!ChangeEntities.TryDequeue(out EntityEntry? entity)) continue;
                    if (entity == null) continue;
                    entity.State = EntityState.Detached;
                }
            }
        }
        /// <summary>
        /// 获得仓储
        /// </summary>
        /// <typeparam name="TRepository"></typeparam>
        /// <returns></returns>
        public virtual TRepository GetRepository<TRepository>()
            where TRepository : IRepository => ServiceProvider.GetService<TRepository>() ?? throw new MateralException("获取仓储失败");
    }
}
