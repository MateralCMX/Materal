using Materal.BaseCore.Domain;
using Materal.BaseCore.ServiceImpl;
using Materal.BaseCore.Services.Models;
using Materal.Extensions;
using Materal.TTA.Common;
using Materal.Utils.Enums;
using Materal.TTA.EFRepository;
using System.Linq.Expressions;
using MBC.Demo.DataTransmitModel.MyTree;
using MBC.Demo.Domain;
using MBC.Demo.Domain.Repositories;
using MBC.Demo.Services;
using MBC.Demo.Services.Models.MyTree;

namespace MBC.Demo.ServiceImpl
{
    /// <summary>
    /// 我的树服务实现
    /// </summary>
    public partial class MyTreeServiceImpl : BaseServiceImpl<AddMyTreeModel, EditMyTreeModel, QueryMyTreeModel, MyTreeDTO, MyTreeListDTO, IMyTreeRepository, MyTree>, IMyTreeService
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        public MyTreeServiceImpl(IServiceProvider serviceProvider) : base(serviceProvider) { }
        /// <summary>
        /// 交换位序
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task ExchangeIndexAsync(ExchangeIndexModel model)
        {
            OnExchangeIndexBefore(model);
            await ServiceImplHelper.ExchangeIndexAndExchangeParentByGroupPropertiesAsync<IMyTreeRepository, MyTree>(model, DefaultRepository, UnitOfWork, new string[] {  }, new string[] {  });
            OnExchangeIndexAfter(model);
        }
        /// <summary>
        /// 交换位序之前
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        partial void OnExchangeIndexBefore(ExchangeIndexModel model);
        /// <summary>
        /// 交换位序之后
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        partial void OnExchangeIndexAfter(ExchangeIndexModel model);
        /// <summary>
        /// 更改父级
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task ExchangeParentAsync(ExchangeParentModel model)
        {
            OnExchangeParentBefore(model);
            await ServiceImplHelper.ExchangeParentByGroupPropertiesAsync<IMyTreeRepository, MyTree>(model, DefaultRepository, UnitOfWork);
            OnExchangeParentAfter(model);
        }
        /// <summary>
        /// 更改父级之前
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        partial void OnExchangeParentBefore(ExchangeParentModel model);
        /// <summary>
        /// 更改父级之后
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        partial void OnExchangeParentAfter(ExchangeParentModel model);
        /// <summary>
        /// 查询树列表
        /// </summary>
        /// <param name="queryModel"></param>
        public async Task<List<MyTreeTreeListDTO>> GetTreeListAsync(QueryMyTreeTreeListModel queryModel)
        {
            #region 排序表达式
            Type domainType = typeof(MyTree);
            Expression<Func<MyTree, object>> sortExpression = m => m.CreateTime;
            SortOrderEnum sortOrder = SortOrderEnum.Descending;
            if (queryModel.SortPropertyName is not null && !string.IsNullOrWhiteSpace(queryModel.SortPropertyName) && domainType.GetProperty(queryModel.SortPropertyName) is not null)
            {
                sortExpression = queryModel.GetSortExpression<MyTree>();
                sortOrder = queryModel.IsAsc ? SortOrderEnum.Ascending : SortOrderEnum.Descending;
            }
            else if (domainType.IsAssignableTo<IIndexDomain>())
            {
                ParameterExpression parameterExpression = Expression.Parameter(domainType, "m");
                MemberExpression memberExpression = Expression.Property(parameterExpression, nameof(IIndexDomain.Index));
                UnaryExpression unaryExpression = Expression.Convert(memberExpression, typeof(object));
                sortExpression = Expression.Lambda<Func<MyTree, object>>(unaryExpression, parameterExpression);
                sortOrder = SortOrderEnum.Ascending;
            }
            #endregion
            #region 查询数据源
            List<MyTree> allInfo;
            if (DefaultRepository is ICacheEFRepository<MyTree, Guid> cacheRepository)
            {
                allInfo = await cacheRepository.GetAllInfoFromCacheAsync();
                Func<MyTree, bool> searchDlegate = queryModel.GetSearchDelegate<MyTree>();
                Func<MyTree, object> sortDlegate = sortExpression.Compile();
                if (sortOrder == SortOrderEnum.Ascending)
                {
                    allInfo = allInfo.Where(searchDlegate).OrderBy(sortDlegate).ToList();
                }
                else
                {
                    allInfo = allInfo.Where(searchDlegate).OrderByDescending(sortDlegate).ToList();
                }
            }
            else
            {
                allInfo = await DefaultRepository.FindAsync(queryModel, sortExpression, sortOrder);
            }
            #endregion
            OnToTreeBefore(allInfo, queryModel);
            List<MyTreeTreeListDTO> result = allInfo.ToTree<MyTree, MyTreeTreeListDTO>(queryModel.ParentID, (dto, domain) =>
            {
                Mapper.Map(domain, dto);
                OnConvertToTreeDTO(dto, domain, queryModel);
            });
            OnToTreeAfter(result, queryModel);
            return result;
        }
        /// <summary>
        /// 转换树之前
        /// </summary>
        /// <param name="allInfo"></param>
        /// <param name="queryModel"></param>
        partial void OnToTreeBefore(List<MyTree> allInfo, QueryMyTreeTreeListModel queryModel);
        /// <summary>
        /// 转换树之后
        /// </summary>
        /// <param name="dtos"></param>
        /// <param name="queryModel"></param>
        partial void OnToTreeAfter(List<MyTreeTreeListDTO> dtos, QueryMyTreeTreeListModel queryModel);
        /// <summary>
        /// 转换为树DTO
        /// </summary>
        /// <param name="dtos"></param>
        /// <param name="domain"></param>
        /// <param name="queryModel"></param>
        partial void OnConvertToTreeDTO(MyTreeTreeListDTO dto, MyTree domain, QueryMyTreeTreeListModel queryModel);
    }
}
