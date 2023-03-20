using Materal.BaseCore.Common;
using Materal.BaseCore.Domain;
using Materal.BaseCore.EFRepository;
using Materal.BaseCore.Services.Models;
using Materal.TTA.EFRepository;
using System.Linq.Expressions;
using System.Reflection;

namespace Materal.BaseCore.ServiceImpl
{
    /// <summary>
    /// 服务实现帮助
    /// </summary>
    public static class ServiceImplHelper
    {
        /// <summary>
        ///根据分组交换位序
        /// </summary>
        /// <typeparam name="TRepository"></typeparam>
        /// <typeparam name="TDomain"></typeparam>
        /// <param name="model"></param>
        /// <param name="repository"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="groupProperties"></param>
        /// <returns></returns>
        /// <exception cref="MateralCoreException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static async Task ExchangeIndexByGroupPropertiesAsync<TRepository, TDomain>(ExchangeIndexModel model, TRepository repository, IMateralCoreUnitOfWork unitOfWork, params string[] groupProperties)
            where TRepository : IEFRepository<TDomain, Guid>
            where TDomain : class, IIndexDomain, new()
        {
            if (model.SourceID == model.TargetID) throw new MateralCoreException("不能以自己为排序对象");
            var domains = await repository.FindAsync(m => m.ID == model.SourceID || m.ID == model.TargetID);
            if (domains.Count != 2) throw new MateralCoreException("数据不存在");
            int minIndex = domains.Min(m => m.Index);
            int maxIndex = domains.Max(m => m.Index);
            Type domainType = typeof(TDomain);
            ParameterExpression mValue = Expression.Parameter(domainType, "m");
            Expression leftExpression = Expression.PropertyOrField(mValue, nameof(IIndexDomain.Index));
            Expression rightExpression = Expression.Constant(minIndex);
            Expression expression = Expression.GreaterThanOrEqual(leftExpression, rightExpression);
            rightExpression = Expression.Constant(maxIndex);
            rightExpression = Expression.LessThanOrEqual(leftExpression, rightExpression);
            expression = Expression.And(expression, rightExpression);
            foreach (string groupProperty in groupProperties)
            {
                PropertyInfo propertyInfo = domainType.GetProperty(groupProperty) ?? throw new ArgumentException($"属性名称{groupProperty}不存在");
                object? value1 = propertyInfo.GetValue(domains[0]);
                object? value2 = propertyInfo.GetValue(domains[1]);
                if (value1 == null || value2 == null)
                {
                    if (value1 != null || value2 != null) throw new MateralCoreException("不是同一组数据不能更改位序");
                }
                else
                {
                    if (!value1.Equals(value2)) throw new MateralCoreException("不是同一组数据不能更改位序");
                }
                leftExpression = Expression.PropertyOrField(mValue, propertyInfo.Name);
                rightExpression = Expression.Constant(value1);
                rightExpression = Expression.Equal(leftExpression, rightExpression);
                expression = Expression.And(expression, rightExpression);
            }
            Expression<Func<TDomain, bool>> queryExpression = Expression.Lambda<Func<TDomain, bool>>(expression, new[] { mValue });
            domains = await repository.FindAsync(queryExpression);
            domains.ExchangeIndex(model.SourceID, model.Before);
            foreach (var item in domains)
            {
                unitOfWork.RegisterEdit(item);
            }
            await unitOfWork.CommitAsync();
        }
    }
}
