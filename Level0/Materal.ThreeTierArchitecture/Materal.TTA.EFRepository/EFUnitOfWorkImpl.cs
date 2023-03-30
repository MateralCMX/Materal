using Materal.Abstractions;
using Materal.TTA.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.TTA.EFRepository
{
    public abstract class EFUnitOfWorkImpl<T> : IEFUnitOfWork<T>
        where T : DbContext
    {
        private readonly object entitiesLockObj = new();
        protected readonly Queue<EntityEntry> changeEntities = new();
        public readonly T _dbContext;
        private readonly IServiceProvider _serviceProvider;
        private readonly List<IRepository> _repositories = new();

        protected EFUnitOfWorkImpl(T context, IServiceProvider serviceProvider)
        {
            _dbContext = context;
            _serviceProvider = serviceProvider;
        }
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            foreach (IRepository repository in _repositories)
            {
                repository.Dispose();
            }
            _dbContext.Dispose();
        }
        public virtual async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }
        private async ValueTask DisposeAsync(bool disposing)
        {
            if (!disposing) return;
            foreach (IRepository repository in _repositories)
            {
                repository.Dispose();
            }
            await _dbContext.DisposeAsync();
        }

        public virtual void RegisterAdd<TEntity, TPrimaryKeyType>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct
        {
            lock (entitiesLockObj)
            {
                EntityEntry<TEntity> entity = _dbContext.Set<TEntity>().Add(obj);
                changeEntities.Enqueue(entity);
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
                changeEntities.Enqueue(entity);
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
                changeEntities.Enqueue(entity);
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
                while (changeEntities.Count > 0)
                {
                    if (!changeEntities.TryDequeue(out EntityEntry? entity)) continue;
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
            where TRepository : IRepository
        {
            Type repositoryType = typeof(TRepository);
            IRepository? repository = _repositories.FirstOrDefault(m => m.GetType() == typeof(TRepository));
            if (repository != null && repository is TRepository result) return result;
            result = _serviceProvider.GetService<TRepository>() ?? throw new MateralException("获取仓储失败");
            if (result is IEFRepository efRepository)
            {
                efRepository.SetDBContext(_dbContext);
            }
            _repositories.Add(result);
            return result;
        }
    }
}
