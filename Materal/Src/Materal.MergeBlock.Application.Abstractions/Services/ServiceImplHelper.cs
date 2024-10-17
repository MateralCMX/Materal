namespace Materal.MergeBlock.Application.Abstractions.Services
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
        /// <exception cref="MergeBlockModuleException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static async Task ExchangeIndexByGroupPropertiesAsync<TRepository, TDomain>(ExchangeIndexModel model, TRepository repository, IMergeBlockUnitOfWork unitOfWork, params string[] groupProperties)
            where TRepository : IRepository<TDomain, Guid>
            where TDomain : class, IIndexDomain, new() => await ExchangeIndexByGroupPropertiesAsync<TRepository, TDomain>(model, repository, unitOfWork, null, groupProperties);
        /// <summary>
        ///根据分组交换位序
        /// </summary>
        /// <typeparam name="TRepository"></typeparam>
        /// <typeparam name="TDomain"></typeparam>
        /// <param name="model"></param>
        /// <param name="repository"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="action"></param>
        /// <param name="groupProperties"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockModuleException"></exception>
        /// <exception cref="ArgumentException"></exception>
        private static async Task ExchangeIndexByGroupPropertiesAsync<TRepository, TDomain>(ExchangeIndexModel model, TRepository repository, IMergeBlockUnitOfWork unitOfWork, Action<TDomain, TDomain>? action, params string[] groupProperties)
            where TRepository : IRepository<TDomain, Guid>
            where TDomain : class, IIndexDomain, new()
        {
            if (model.SourceID == model.TargetID) throw new MergeBlockModuleException("不能以自己为排序对象");
            var domains = await repository.FindAsync(m => m.ID == model.SourceID || m.ID == model.TargetID);
            if (domains.Count != 2) throw new MergeBlockModuleException("数据不存在");
            if (action != null)
            {
                action.Invoke(domains[0], domains[1]);
                domains = await repository.FindAsync(m => m.ID == model.SourceID || m.ID == model.TargetID);
            }
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
                    if (value1 != null || value2 != null) throw new MergeBlockModuleException("不是同一组数据不能更改位序");
                }
                else
                {
                    if (!value1.Equals(value2)) throw new MergeBlockModuleException("不是同一组数据不能更改位序");
                }
                leftExpression = Expression.PropertyOrField(mValue, propertyInfo.Name);
                rightExpression = Expression.Constant(value1);
                rightExpression = Expression.Equal(leftExpression, rightExpression);
                expression = Expression.And(expression, rightExpression);
            }
            Expression<Func<TDomain, bool>> queryExpression = Expression.Lambda<Func<TDomain, bool>>(expression, [mValue]);
            domains = await repository.FindAsync(queryExpression);
            domains.ExchangeIndex(model.SourceID, model.Before);
            foreach (var item in domains)
            {
                unitOfWork.RegisterEdit(item);
            }
            await unitOfWork.CommitAsync();
        }
        /// <summary>
        ///根据分组交换位序与父级
        /// </summary>
        /// <typeparam name="TRepository"></typeparam>
        /// <typeparam name="TDomain"></typeparam>
        /// <param name="model"></param>
        /// <param name="repository"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="indexGroupProperties"></param>
        /// <param name="treeGroupProperties"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockModuleException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static async Task ExchangeIndexAndExchangeParentByGroupPropertiesAsync<TRepository, TDomain>(ExchangeIndexModel model, TRepository repository, IMergeBlockUnitOfWork unitOfWork, string[] indexGroupProperties, string[] treeGroupProperties)
            where TRepository : IEFRepository<TDomain, Guid>
            where TDomain : class, IIndexDomain, ITreeDomain, new()
        {
            await ExchangeIndexByGroupPropertiesAsync<TRepository, TDomain>(model, repository, unitOfWork, async (domain1, domain2) =>
            {
                if (domain1.ParentID == domain2.ParentID) return;
                await ExchangeParentByGroupPropertiesAsync<TRepository, TDomain>(new ExchangeParentModel
                {
                    SourceID = model.SourceID,
                    TargetID = model.SourceID == domain1.ID ? domain2.ParentID : domain1.ParentID,
                }, repository, unitOfWork, treeGroupProperties);
            }, indexGroupProperties);
        }
        /// <summary>
        /// 更改父级
        /// </summary>
        /// <param name="model"></param>
        /// <param name="repository"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="groupProperties"></param>
        /// <returns></returns>
        public static async Task ExchangeParentByGroupPropertiesAsync<TRepository, TDomain>(ExchangeParentModel model, TRepository repository, IMergeBlockUnitOfWork unitOfWork, params string[] groupProperties)
            where TRepository : IEFRepository<TDomain, Guid>
            where TDomain : class, ITreeDomain, new()
        {
            if (model.SourceID == model.TargetID) throw new MergeBlockModuleException("不能以自己为父级");
            if (!model.TargetID.HasValue || model.TargetID.Value == Guid.Empty)
            {
                TDomain domain = await repository.FirstOrDefaultAsync(m => m.ID == model.SourceID) ?? throw new MergeBlockModuleException("对象不存在");
                domain.ParentID = null;
                unitOfWork.RegisterEdit(domain);
            }
            else
            {
                List<TDomain> domains = await repository.FindAsync(m => m.ID == model.SourceID || m.ID == model.TargetID);
                if (domains.Count != 2) throw new MergeBlockModuleException("对象不存在");
                Type domainType = typeof(TDomain);
                foreach (string groupProperty in groupProperties)
                {
                    PropertyInfo propertyInfo = domainType.GetProperty(groupProperty) ?? throw new ArgumentException($"属性名称{groupProperty}不存在");
                    object? value1 = propertyInfo.GetValue(domains[0]);
                    object? value2 = propertyInfo.GetValue(domains[1]);
                    if (value1 == null || value2 == null)
                    {
                        if (value1 != null || value2 != null) throw new MergeBlockModuleException("不是同一组数据不能更改父级");
                    }
                    else
                    {
                        if (!value1.Equals(value2)) throw new MergeBlockModuleException("不是同一组数据不能更改父级");
                    }
                }
                TDomain domain = domains.First(m => m.ID == model.SourceID);
                TDomain targetDomain = domains.First(m => m.ID == model.TargetID);
                domain.ParentID = targetDomain.ID;
                if (targetDomain.ParentID == domain.ID) throw new MergeBlockModuleException("父级循环引用");
                unitOfWork.RegisterEdit(domain);
            }
            await unitOfWork.CommitAsync();
        }
        /// <summary>
        /// 更改附件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TRepository"></typeparam>
        /// <param name="fileIDs"></param>
        /// <param name="repository"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="targetName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task ChangeAdjunctsAsync<T, TRepository>(Guid[] fileIDs, TRepository repository, IMergeBlockUnitOfWork unitOfWork, string targetName, Guid? id = null)
            where T : class, IAdjunctDomain, new()
            where TRepository : IEFRepository<T, Guid>
        {
            Type tType = typeof(T);
            PropertyInfo propertyInfo = tType.GetProperty(targetName) ?? throw new MergeBlockModuleException("操作附件失败");
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
                    unitOfWork.RegisterDelete(adjunct);
                }
            }
            foreach (Guid adjunctID in addIDs)
            {
                T t = new()
                {
                    UploadFileID = adjunctID
                };
                propertyInfo.SetValue(t, id);
                unitOfWork.RegisterAdd(t);
            }
        }
        /// <summary>
        /// 获得查询树结构领域表达式
        /// </summary>
        /// <typeparam name="TDomain"></typeparam>
        /// <typeparam name="TQueryModel"></typeparam>
        /// <param name="expression"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Expression<Func<TDomain, bool>> GetSearchTreeDomainExpression<TDomain, TQueryModel>(Expression<Func<TDomain, bool>> expression, TQueryModel model)
            where TQueryModel : notnull
        {
            if (!typeof(TDomain).IsAssignableTo<ITreeDomain>()) return expression;
            PropertyInfo? modelParentIDPropertyInfo = model.GetType().GetProperty(nameof(ITreeDomain.ParentID));
            if (modelParentIDPropertyInfo == null) return expression;
            object? modelParentID = modelParentIDPropertyInfo.GetValue(model);
            if (modelParentID != null) return expression;
            ParameterExpression parameterExpression = expression.Parameters[0];
            MemberExpression memberExpression = Expression.Property(parameterExpression, nameof(ITreeDomain.ParentID));
            BinaryExpression binaryExpression = Expression.Equal(memberExpression, Expression.Constant(null));
            Expression<Func<TDomain, bool>> newExpression = Expression.Lambda<Func<TDomain, bool>>(binaryExpression, parameterExpression);
            expression = expression.Compose(newExpression, new Func<Expression, Expression, Expression>(Expression.AndAlso));
            return expression;
        }
    }
}
