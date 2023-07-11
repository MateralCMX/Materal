using Materal.Abstractions;
using Materal.TTA.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.TTA.EFRepository
{
    /// <summary>
    /// EF工作单元
    /// </summary>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class EFUnitOfWorkImpl<TDBContext> : IEFUnitOfWork
        where TDBContext : DbContext
    {
        private readonly object entitiesLockObj = new();
        private readonly TDBContext _dbContext;
        /// <summary>
        /// 服务容器
        /// </summary>
        public IServiceProvider ServiceProvider { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="context"></param>
        /// <param name="serviceProvider"></param>
        protected EFUnitOfWorkImpl(TDBContext context, IServiceProvider serviceProvider)
        {
            _dbContext = context;
            ServiceProvider = serviceProvider;
        }
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="obj"></param>
        /// <exception cref="MateralException"></exception>
        public virtual void RegisterAdd<TEntity, TPrimaryKeyType>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct
        {
            lock (entitiesLockObj)
            {
                EntityEntry<TEntity> entity = _dbContext.Entry(obj);
                if(entity.State != EntityState.Detached) throw new MateralException($"实体已被标记为{entity.State},不能添加");
                entity.State = EntityState.Added;
            }
        }
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool TryRegisterAdd<TEntity, TPrimaryKeyType>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct
        {
            try
            {
                RegisterAdd<TEntity, TPrimaryKeyType>(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 注册修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="obj"></param>
        /// <exception cref="MateralException"></exception>
        public virtual void RegisterEdit<TEntity, TPrimaryKeyType>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct
        {
            lock (entitiesLockObj)
            {
                EntityEntry<TEntity> entity = _dbContext.Entry(obj);
                if (entity.State != EntityState.Detached) throw new MateralException($"实体已被标记为{entity.State},不能修改");
                entity.State = EntityState.Modified;
            }
        }
        /// <summary>
        /// 注册修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool TryRegisterEdit<TEntity, TPrimaryKeyType>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct
        {
            try
            {
                RegisterEdit<TEntity, TPrimaryKeyType>(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="obj"></param>
        /// <exception cref="MateralException"></exception>
        public virtual void RegisterDelete<TEntity, TPrimaryKeyType>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct
        {
            lock (entitiesLockObj)
            {
                EntityEntry<TEntity> entity = _dbContext.Entry(obj);
                if (entity.State != EntityState.Detached) throw new MateralException($"实体已被标记为{entity.State},不能删除");
                entity.State = EntityState.Deleted;
            }
        }
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool TryRegisterDelete<TEntity, TPrimaryKeyType>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct
        {
            try
            {
                RegisterDelete<TEntity, TPrimaryKeyType>(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="setDetached"></param>
        public void Commit(bool setDetached = true)
        {
            try
            {
                _dbContext.SaveChanges();
                if (!setDetached) return;
                DetachedAll();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (EntityEntry entry in ex.Entries)
                {
                    entry.Reload();
                }
            }
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="setDetached"></param>
        /// <returns></returns>
        public virtual async Task CommitAsync(bool setDetached = true)
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                if (!setDetached) return;
                DetachedAll();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (EntityEntry entry in ex.Entries)
                {
                    await entry.ReloadAsync();
                }
            }
        }
        /// <summary>
        /// 获得仓储
        /// </summary>
        /// <typeparam name="TRepository"></typeparam>
        /// <returns></returns>
        /// <exception cref="MateralException"></exception>
        public virtual TRepository GetRepository<TRepository>()
            where TRepository : IRepository => ServiceProvider.GetService<TRepository>() ?? throw new MateralException("获取仓储失败");
        /// <summary>
        /// 取消所有跟踪的实例
        /// </summary>
        private void DetachedAll()
        {
            IEnumerable<EntityEntry> entityEntries = _dbContext.ChangeTracker.Entries();
            entityEntries.AsParallel().ForAll(entity =>
            {
                if (entity == null) return;
                entity.State = EntityState.Detached;
            });
        }
    }
    /// <summary>
    /// EF工作单元
    /// </summary>
    /// <typeparam name="TDBContext"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public abstract class EFUnitOfWorkImpl<TDBContext, TPrimaryKeyType> : EFUnitOfWorkImpl<TDBContext>, IEFUnitOfWork<TPrimaryKeyType>
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="context"></param>
        /// <param name="serviceProvider"></param>
        protected EFUnitOfWorkImpl(TDBContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        public virtual void RegisterAdd<TEntity>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
        {
            RegisterAdd<TEntity, TPrimaryKeyType>(obj);
        }
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        public virtual bool TryRegisterAdd<TEntity>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
        {
            return TryRegisterAdd<TEntity, TPrimaryKeyType>(obj);
        }
        /// <summary>
        /// 注册修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        public virtual void RegisterEdit<TEntity>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
        {
            RegisterEdit<TEntity, TPrimaryKeyType>(obj);
        }
        /// <summary>
        /// 注册修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        public virtual bool TryRegisterEdit<TEntity>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
        {
            return TryRegisterEdit<TEntity, TPrimaryKeyType>(obj);
        }
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        public virtual void RegisterDelete<TEntity>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
        {
            RegisterDelete<TEntity, TPrimaryKeyType>(obj);
        }
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        public virtual bool TryRegisterDelete<TEntity>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
        {
            return TryRegisterDelete<TEntity, TPrimaryKeyType>(obj);
        }
    }
}
