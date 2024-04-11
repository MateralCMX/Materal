using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;

namespace Materal.TTA.EFRepository
{
    /// <summary>
    /// EF工作单元
    /// </summary>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class EFUnitOfWorkImpl<TDBContext> : IEFUnitOfWork
        where TDBContext : DbContext
    {
        private readonly SemaphoreSlim _semaphoreSlim = new(0, 1);
        /// <summary>
        /// 数据库上下文
        /// </summary>
        protected readonly TDBContext DBContext;
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
            DBContext = context;
            ServiceProvider = serviceProvider;
            _semaphoreSlim.Release();
        }
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="MateralException"></exception>
        public virtual void RegisterAdd(object obj)
        {
            _semaphoreSlim.Wait();
            try
            {
                EntityEntry entity = DBContext.Entry(obj);
                if (entity.State != EntityState.Detached) throw new MateralException($"实体已被标记为{entity.State},不能添加");
                entity.State = EntityState.Added;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
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
            where TPrimaryKeyType : struct => RegisterAdd(obj);
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool TryRegisterAdd(object obj)
        {
            try
            {
                RegisterAdd(obj);
                return true;
            }
            catch
            {
                return false;
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
        /// <param name="obj"></param>
        /// <exception cref="MateralException"></exception>
        public virtual void RegisterEdit(object obj)
        {
            _semaphoreSlim.Wait();
            try
            {
                EntityEntry entity = DBContext.Entry(obj);
                if (entity.State != EntityState.Detached) throw new MateralException($"实体已被标记为{entity.State},不能修改");
                entity.State = EntityState.Modified;
            }
            finally
            {
                _semaphoreSlim.Release();
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
            where TPrimaryKeyType : struct => RegisterEdit(obj);
        /// <summary>
        /// 注册修改
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool TryRegisterEdit(object obj)
        {
            try
            {
                RegisterEdit(obj);
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
        /// <param name="obj"></param>
        /// <exception cref="MateralException"></exception>
        public virtual void RegisterDelete(object obj)
        {
            _semaphoreSlim.Wait();
            try
            {
                EntityEntry entity = DBContext.Entry(obj);
                if (entity.State != EntityState.Detached) throw new MateralException($"实体已被标记为{entity.State},不能删除");
                entity.State = EntityState.Deleted;
            }
            finally
            {
                _semaphoreSlim.Release();
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
            where TPrimaryKeyType : struct => RegisterDelete(obj);
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool TryRegisterDelete(object obj)
        {
            try
            {
                RegisterDelete(obj);
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
            _semaphoreSlim.Wait();
            try
            {
                DBContext.SaveChanges();
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
            finally
            {
                _semaphoreSlim.Release();
            }
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="setDetached"></param>
        /// <returns></returns>
        public virtual async Task CommitAsync(bool setDetached = true)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                await DBContext.SaveChangesAsync();
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
            finally
            {
                _semaphoreSlim.Release();
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
            IEnumerable<EntityEntry> entityEntries = DBContext.ChangeTracker.Entries();
            entityEntries.AsParallel().ForAll(entity =>
            {
                if (entity == null) return;
                entity.State = EntityState.Detached;
            });
        }
    }
}
