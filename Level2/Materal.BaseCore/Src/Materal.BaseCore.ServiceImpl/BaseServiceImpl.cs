﻿using AutoMapper;
using Materal.Abstractions;
using Materal.BaseCore.Common;
using Materal.BaseCore.Common.Utils;
using Materal.BaseCore.DataTransmitModel;
using Materal.BaseCore.Domain;
using Materal.BaseCore.EFRepository;
using Materal.BaseCore.Services;
using Materal.TTA.EFRepository;
using Materal.Utils.Model;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;

namespace Materal.BaseCore.ServiceImpl
{
    /// <summary>
    /// 基础服务实现
    /// </summary>
    /// <typeparam name="TAddModel"></typeparam>
    /// <typeparam name="TEditModel"></typeparam>
    /// <typeparam name="TQueryModel"></typeparam>
    /// <typeparam name="TDTO"></typeparam>
    /// <typeparam name="TListDTO"></typeparam>
    /// <typeparam name="TRepository"></typeparam>
    /// <typeparam name="TDomain"></typeparam>
    public abstract class BaseServiceImpl<TAddModel, TEditModel, TQueryModel, TDTO, TListDTO, TRepository, TDomain> : IBaseService<TAddModel, TEditModel, TQueryModel, TDTO, TListDTO>
        where TAddModel : class, IAddServiceModel, new()
        where TEditModel : class, IEditServiceModel, new()
        where TQueryModel : PageRequestModel, IQueryServiceModel, new()
        where TDTO : class, IDTO
        where TListDTO : class, IListDTO
        where TRepository : IEFRepository<TDomain, Guid>
        where TDomain : class, IDomain, new()
    {
        /// <summary>
        /// 映射器
        /// </summary>
        protected readonly IMapper Mapper;
        /// <summary>
        /// 工作单元
        /// </summary>
        protected readonly IMateralCoreUnitOfWork UnitOfWork;
        /// <summary>
        /// 默认仓储
        /// </summary>
        protected readonly TRepository DefaultRepository;
        /// <summary>
        /// 构造方法
        /// </summary>
        public BaseServiceImpl()
        {
            UnitOfWork = MateralServices.GetService<IMateralCoreUnitOfWork>();
            DefaultRepository = MateralServices.GetService<TRepository>();
            Mapper = MateralServices.GetService<IMapper>();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MateralCoreException"></exception>
        public virtual async Task<Guid> AddAsync(TAddModel model)
        {
            TDomain domain = Mapper.Map<TDomain>(model);
            return await AddAsync(domain, model);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MateralCoreException"></exception>
        protected virtual async Task<Guid> AddAsync(TDomain domain, TAddModel model)
        {
            UnitOfWork.RegisterAdd(domain);
            await UnitOfWork.CommitAsync();
            await ClearCacheAsync();
            return domain.ID;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="MateralCoreException"></exception>
        public virtual async Task DeleteAsync([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            TDomain? domainFromDB = await DefaultRepository.FirstOrDefaultAsync(id);
            if (domainFromDB == null) throw new MateralCoreException("数据不存在");
            await DeleteAsync(domainFromDB);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        /// <exception cref="MateralCoreException"></exception>
        protected virtual async Task DeleteAsync(TDomain domain)
        {
            UnitOfWork.RegisterDelete(domain);
            await UnitOfWork.CommitAsync();
            await ClearCacheAsync();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MateralCoreException"></exception>
        public virtual async Task EditAsync(TEditModel model)
        {
            TDomain? domainFromDB = await DefaultRepository.FirstOrDefaultAsync(model.ID);
            if (domainFromDB == null) throw new MateralCoreException("数据不存在");
            Mapper.Map(model, domainFromDB);
            await EditAsync(domainFromDB, model);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="domainFromDB"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MateralCoreException"></exception>
        protected virtual async Task EditAsync(TDomain domainFromDB, TEditModel model)
        {
            domainFromDB.UpdateTime = DateTime.Now;
            UnitOfWork.RegisterEdit(domainFromDB);
            await UnitOfWork.CommitAsync();
            await ClearCacheAsync();
        }
        /// <summary>
        /// 获得信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="MateralCoreException"></exception>
        public virtual async Task<TDTO> GetInfoAsync([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            TDomain? domainFromDB = await DefaultRepository.FirstOrDefaultAsync(id);
            if (domainFromDB == null) throw new MateralCoreException("数据不存在");
            return await GetInfoAsync(domainFromDB);
        }
        /// <summary>
        /// 获得信息
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        /// <exception cref="MateralCoreException"></exception>
        protected virtual async Task<TDTO> GetInfoAsync(TDomain domain)
        {
            TDTO result = Mapper.Map<TDTO>(domain);
            return await GetInfoAsync(result);
        }
        /// <summary>
        /// 获得信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="MateralCoreException"></exception>
        protected virtual Task<TDTO> GetInfoAsync(TDTO dto)
        {
            return Task.FromResult(dto);
        }
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual async Task<(List<TListDTO> data, PageModel pageInfo)> GetListAsync(TQueryModel model)
        {
            Expression<Func<TDomain, bool>> expression = model.GetSearchExpression<TDomain>();
            return await GetListAsync(expression, model, m => m.CreateTime, SortOrder.Descending);
        }
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="model"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        protected virtual async Task<(List<TListDTO> data, PageModel pageInfo)> GetListAsync(Expression<Func<TDomain, bool>> expression, TQueryModel model, Expression<Func<TDomain, object>>? orderExpression = null, SortOrder sortOrder = SortOrder.Descending)
        {
            orderExpression ??= m => m.CreateTime;
            (List<TDomain> data, PageModel pageModel) = await DefaultRepository.PagingAsync(expression, orderExpression, sortOrder, model);
            List<TListDTO> result = Mapper.Map<List<TListDTO>>(data);
            return await GetListAsync(result, pageModel, model);
        }
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="listDto"></param>
        /// <param name="pageInfo"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected virtual Task<(List<TListDTO> data, PageModel pageInfo)> GetListAsync(List<TListDTO> listDto, PageModel pageInfo, TQueryModel model)
        {
            return Task.FromResult((listDto, pageInfo));
        }
        /// <summary>
        /// 清空缓存
        /// </summary>
        /// <param name="targetRepository"></param>
        /// <returns></returns>
        protected virtual async Task ClearCacheAsync(object targetRepository)
        {
            MethodInfo? methodInfo = targetRepository.GetType().GetMethod(nameof(ICacheEFRepository<TDomain, Guid>.ClearAllCacheAsync));
            if (methodInfo != null)
            {
                object? methodResult = methodInfo.Invoke(targetRepository, null);
                if (methodResult != null && methodResult is Task methodResultTask)
                {
                    await methodResultTask;
                }
            }
        }
        /// <summary>
        /// 清空缓存
        /// </summary>
        /// <returns></returns>
        protected virtual async Task ClearCacheAsync()
        {
            await ClearCacheAsync(DefaultRepository);
        }
        /// <summary>
        /// 更改附件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="fileIDs"></param>
        /// <param name="repository"></param>
        /// <param name="targetName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        protected async Task ChangeAdjunctsAsync<T, TR>(Guid[] fileIDs, TR repository, string targetName, Guid? id = null)
            where T : class, IAdjunctDomain, new()
            where TR : IEFRepository<T, Guid>
        {
            Type tType = typeof(T);
            PropertyInfo? propertyInfo = tType.GetProperty(targetName);
            if (propertyInfo == null) throw new MateralCoreException("操作附件失败");
            ICollection<Guid> addIDs;
            if (id == null)
            {
                addIDs = fileIDs;
            }
            else
            {
                ParameterExpression mParameterExpression = Expression.Parameter(tType, "m");
                MemberExpression leftExpression = Expression.Property(mParameterExpression, targetName);
                ConstantExpression rightExpression = Expression.Constant(id);
                BinaryExpression expression = Expression.Equal(leftExpression, rightExpression);
                Expression<Func<T, bool>> searchExpression = Expression.Lambda<Func<T, bool>>(expression, mParameterExpression);
                List<T> allAdjunctInfos = await repository.FindAsync(searchExpression);
                List<Guid> allAdjunctIDs = allAdjunctInfos.Select(m => m.UploadFileID).ToList();
                (addIDs, ICollection<Guid> removeIDs) = fileIDs.GetAddArrayAndRemoveArray(allAdjunctIDs);
                List<T> removeAdjunctInfos = allAdjunctInfos.Where(m => removeIDs.Contains(m.UploadFileID)).ToList();
                foreach (T adjunct in removeAdjunctInfos)
                {
                    UnitOfWork.RegisterDelete(adjunct);
                }
            }
            foreach (Guid adjunctID in addIDs)
            {
                T t = new()
                {
                    UploadFileID = adjunctID
                };
                propertyInfo.SetValue(t, id);
                UnitOfWork.RegisterAdd(t);
            }
        }
    }
    /// <summary>
    /// 基础服务实现
    /// </summary>
    /// <typeparam name="TAddModel"></typeparam>
    /// <typeparam name="TEditModel"></typeparam>
    /// <typeparam name="TQueryModel"></typeparam>
    /// <typeparam name="TDTO"></typeparam>
    /// <typeparam name="TListDTO"></typeparam>
    /// <typeparam name="TRepository"></typeparam>
    /// <typeparam name="TViewRepository"></typeparam>
    /// <typeparam name="TDomain"></typeparam>
    /// <typeparam name="TViewDomain"></typeparam>
    public abstract class BaseServiceImpl<TAddModel, TEditModel, TQueryModel, TDTO, TListDTO, TRepository, TViewRepository, TDomain, TViewDomain> : BaseServiceImpl<TAddModel, TEditModel, TQueryModel, TDTO, TListDTO, TRepository, TDomain>, IBaseService<TAddModel, TEditModel, TQueryModel, TDTO, TListDTO>
        where TAddModel : class, IAddServiceModel, new()
        where TEditModel : class, IEditServiceModel, new()
        where TQueryModel : PageRequestModel, IQueryServiceModel, new()
        where TDTO : class, IDTO
        where TListDTO : class, IListDTO
        where TRepository : IEFRepository<TDomain, Guid>
        where TViewRepository : IEFRepository<TViewDomain, Guid>
        where TDomain : class, IDomain, new()
        where TViewDomain : class, IDomain, new()
    {
        /// <summary>
        /// 默认视图仓储
        /// </summary>
        protected readonly TViewRepository DefaultViewRepository;
        /// <summary>
        /// 构造方法
        /// </summary>
        public BaseServiceImpl() : base()
        {
            DefaultViewRepository = MateralServices.GetService<TViewRepository>();
        }
        /// <summary>
        /// 获得信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="MateralCoreException"></exception>
        public override async Task<TDTO> GetInfoAsync([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            TViewDomain? domainFromDB = await DefaultViewRepository.FirstOrDefaultAsync(id);
            if (domainFromDB == null) throw new MateralCoreException("数据不存在");
            return await GetInfoAsync(domainFromDB);
        }
        /// <summary>
        /// 获得信息
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        /// <exception cref="MateralCoreException"></exception>
        protected virtual async Task<TDTO> GetInfoAsync(TViewDomain domain)
        {
            TDTO result = Mapper.Map<TDTO>(domain);
            return await GetInfoAsync(result);
        }
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override async Task<(List<TListDTO> data, PageModel pageInfo)> GetListAsync(TQueryModel model)
        {
            Expression<Func<TViewDomain, bool>> expression = model.GetSearchExpression<TViewDomain>();
            return await GetViewListAsync(expression, model, m => m.CreateTime, SortOrder.Descending);
        }
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="model"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        protected virtual async Task<(List<TListDTO> data, PageModel pageInfo)> GetViewListAsync(Expression<Func<TViewDomain, bool>> expression, TQueryModel model, Expression<Func<TViewDomain, object>>? orderExpression = null, SortOrder sortOrder = SortOrder.Descending)
        {
            orderExpression ??= m => m.CreateTime;
            (List<TViewDomain> data, PageModel pageModel) = await DefaultViewRepository.PagingAsync(expression, orderExpression, sortOrder, model);
            List<TListDTO> result = Mapper.Map<List<TListDTO>>(data);
            return await GetListAsync(result, pageModel, model);
        }
        /// <summary>
        /// 清空缓存
        /// </summary>
        /// <returns></returns>
        protected override async Task ClearCacheAsync()
        {
            await ClearCacheAsync(DefaultViewRepository);
            await base.ClearCacheAsync();
        }
    }
}
