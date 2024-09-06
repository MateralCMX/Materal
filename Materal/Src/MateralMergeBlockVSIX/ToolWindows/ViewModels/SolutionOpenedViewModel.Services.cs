#nullable enable
using Materal.MergeBlock.GeneratorCode.Attributers;
using Materal.MergeBlock.GeneratorCode.Extensions;
using Materal.MergeBlock.GeneratorCode.Models;
using MateralMergeBlockVSIX.Extensions;
using MateralMergeBlockVSIX.ToolWindows.Attributes;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.Generic;
using System.Text;

namespace MateralMergeBlockVSIX.ToolWindows.ViewModels
{
    public partial class SolutionOpenedViewModel : ObservableObject
    {
        /// <summary>
        /// 创建服务代码
        /// </summary>
        [GeneratorCodeMethod]
        private async Task GeneratorServicesCodeAsync()
        {
            foreach (DomainModel domain in Context.Domains)
            {
                await GeneratorIServicesCodeAsync(domain);
                await GeneratorServiceImplsCodeAsync(domain, Context.Domains);
            }
        }
        /// <summary>
        /// 创建服务代码接口
        /// </summary>
        /// <param name="domain"></param>
        private async Task GeneratorIServicesCodeAsync(DomainModel domain)
        {
            if (domain.HasAttribute<NotServiceAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"/*");
            codeContent.AppendLine($" * Generator Code From MateralMergeBlock=>{nameof(GeneratorIServicesCodeAsync)}");
            codeContent.AppendLine($" */");
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.DTO.{domain.Name};");
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.Services.Models.{domain.Name};");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.Services");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}服务");
            codeContent.AppendLine($"    /// </summary>");
            if (domain.HasAttribute<EmptyServiceAttribute>())
            {
                codeContent.AppendLine($"    public partial interface I{domain.Name}Service : IBaseService");
            }
            else
            {
                codeContent.AppendLine($"    public partial interface I{domain.Name}Service : IBaseService<Add{domain.Name}Model, Edit{domain.Name}Model, Query{domain.Name}Model, {domain.Name}DTO, {domain.Name}ListDTO>");
            }
            codeContent.AppendLine($"    {{");
            if (domain.IsIndexDomain && !domain.HasAttribute<EmptyIndexAttribute>())
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 交换位序");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"model\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        Task ExchangeIndexAsync(ExchangeIndexModel model);");

            }
            if (domain.IsTreeDomain && !domain.HasAttribute<EmptyTreeAttribute>())
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 更改父级");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"model\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        Task ExchangeParentAsync(ExchangeParentModel model);");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 查询树列表");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"queryModel\"></param>");
                codeContent.AppendLine($"        Task<List<{domain.Name}TreeListDTO>> GetTreeListAsync(Query{domain.Name}TreeListModel queryModel);");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            await codeContent.SaveAsAsync(Context, _moduleAbstractions, "Services", $"I{domain.Name}Service.cs");
        }
        /// <summary>
        /// 创建服务代码实现
        /// </summary>
        /// <param name="domain"></param>
        private async Task GeneratorServiceImplsCodeAsync(DomainModel domain, List<DomainModel> domains)
        {
            if (domain.HasAttribute<NotServiceAttribute>()) return;
            DomainModel targetDomain = domain.GetQueryDomain(domains);
            StringBuilder codeContent = new();
            codeContent.AppendLine($"/*");
            codeContent.AppendLine($" * Generator Code From MateralMergeBlock=>{nameof(GeneratorServiceImplsCodeAsync)}");
            codeContent.AppendLine($" */");
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.DTO.{domain.Name};");
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.Services.Models.{domain.Name};");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Application.Services");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}服务");
            codeContent.AppendLine($"    /// </summary>");
            if (domain.HasAttribute<EmptyServiceAttribute>())
            {
                if (domain.HasAttribute<NotRepositoryAttribute>())
                {
                    codeContent.AppendLine($"    public partial class {domain.Name}ServiceImpl : BaseServiceImpl<I{_moduleName}UnitOfWork>, I{domain.Name}Service, IScopedDependency<I{domain.Name}Service>");
                }
                else
                {
                    if (targetDomain != domain)
                    {
                        codeContent.AppendLine($"    public partial class {domain.Name}ServiceImpl : BaseServiceImpl<I{domain.Name}Repository, I{targetDomain.Name}Repository, {domain.Name}, {targetDomain.Name}, I{_moduleName}UnitOfWork>, I{domain.Name}Service, IScopedDependency<I{domain.Name}Service>");
                    }
                    else
                    {
                        codeContent.AppendLine($"    public partial class {domain.Name}ServiceImpl : BaseServiceImpl<I{domain.Name}Repository, {domain.Name}, I{_moduleName}UnitOfWork>, I{domain.Name}Service, IScopedDependency<I{domain.Name}Service>");
                    }
                }
            }
            else
            {
                if (targetDomain != domain)
                {
                    codeContent.AppendLine($"    public partial class {domain.Name}ServiceImpl : BaseServiceImpl<Add{domain.Name}Model, Edit{domain.Name}Model, Query{domain.Name}Model, {domain.Name}DTO, {domain.Name}ListDTO, I{domain.Name}Repository, I{targetDomain.Name}Repository, {domain.Name}, {targetDomain.Name}, I{_moduleName}UnitOfWork>, I{domain.Name}Service, IScopedDependency<I{domain.Name}Service>");
                }
                else
                {
                    codeContent.AppendLine($"    public partial class {domain.Name}ServiceImpl : BaseServiceImpl<Add{domain.Name}Model, Edit{domain.Name}Model, Query{domain.Name}Model, {domain.Name}DTO, {domain.Name}ListDTO, I{domain.Name}Repository, {domain.Name}, I{_moduleName}UnitOfWork>, I{domain.Name}Service, IScopedDependency<I{domain.Name}Service>");
                }
            }
            codeContent.AppendLine($"    {{");
            PropertyModel? treeGroupProperty = null;
            if (domain.IsIndexDomain && !domain.HasAttribute<EmptyIndexAttribute>())
            {
                PropertyModel? indexGroupProperty = domain.GetIndexGroupProperty();
                treeGroupProperty ??= domain.GetTreeGroupProperty();
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 交换位序");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"model\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        public async Task ExchangeIndexAsync(ExchangeIndexModel model)");
                codeContent.AppendLine($"        {{");
                codeContent.AppendLine($"            OnExchangeIndexBefore(model);");
                if (indexGroupProperty is null)
                {
                    codeContent.AppendLine($"            await ServiceImplHelper.ExchangeIndexByGroupPropertiesAsync<I{domain.Name}Repository, {domain.Name}>(model, DefaultRepository, UnitOfWork);");
                }
                else
                {
                    if (treeGroupProperty is null)
                    {
                        codeContent.AppendLine($"            await ServiceImplHelper.ExchangeIndexByGroupPropertiesAsync<I{domain.Name}Repository, {domain.Name}>(model, DefaultRepository, UnitOfWork, [nameof({domain.Name}.{indexGroupProperty.Name})]);");
                    }
                    else
                    {
                        codeContent.AppendLine($"            await ServiceImplHelper.ExchangeIndexAndExchangeParentByGroupPropertiesAsync<I{domain.Name}Repository, {domain.Name}>(model, DefaultRepository, UnitOfWork, [nameof({domain.Name}.{indexGroupProperty.Name})], [nameof({domain.Name}.{treeGroupProperty.Name})]);");
                    }
                }
                codeContent.AppendLine($"            OnExchangeIndexAfter(model);");
                codeContent.AppendLine($"        }}");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 交换位序之前");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"model\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        partial void OnExchangeIndexBefore(ExchangeIndexModel model);");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 交换位序之后");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"model\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        partial void OnExchangeIndexAfter(ExchangeIndexModel model);");
            }
            if (domain.IsTreeDomain && !domain.HasAttribute<EmptyTreeAttribute>())
            {
                treeGroupProperty ??= domain.GetTreeGroupProperty();
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 更改父级");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"model\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        public async Task ExchangeParentAsync(ExchangeParentModel model)");
                codeContent.AppendLine($"        {{");
                codeContent.AppendLine($"            OnExchangeParentBefore(model);");
                if (treeGroupProperty is null)
                {
                    codeContent.AppendLine($"            await ServiceImplHelper.ExchangeParentByGroupPropertiesAsync<I{domain.Name}Repository, {domain.Name}>(model, DefaultRepository, UnitOfWork);");
                }
                else
                {
                    codeContent.AppendLine($"            await ServiceImplHelper.ExchangeParentByGroupPropertiesAsync<I{domain.Name}Repository, {domain.Name}>(model, DefaultRepository, UnitOfWork, nameof({domain.Name}.{treeGroupProperty.Name}));");
                }
                codeContent.AppendLine($"            OnExchangeParentAfter(model);");
                codeContent.AppendLine($"        }}");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 更改父级之前");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"model\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        partial void OnExchangeParentBefore(ExchangeParentModel model);");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 更改父级之后");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"model\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        partial void OnExchangeParentAfter(ExchangeParentModel model);");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 查询树列表");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"queryModel\"></param>");
                codeContent.AppendLine($"        public async Task<List<{domain.Name}TreeListDTO>> GetTreeListAsync(Query{domain.Name}TreeListModel queryModel)");
                codeContent.AppendLine($"        {{");
                codeContent.AppendLine($"            #region 排序表达式");
                codeContent.AppendLine($"            Type domainType = typeof({domain.Name});");
                codeContent.AppendLine($"            Expression<Func<{domain.Name}, object>> sortExpression = m => m.CreateTime;");
                codeContent.AppendLine($"            SortOrderEnum sortOrder = SortOrderEnum.Descending;");
                codeContent.AppendLine($"            if (queryModel.SortPropertyName is not null && !string.IsNullOrWhiteSpace(queryModel.SortPropertyName) && domainType.GetProperty(queryModel.SortPropertyName) is not null)");
                codeContent.AppendLine($"            {{");
                codeContent.AppendLine($"                sortExpression = queryModel.GetSortExpression<{domain.Name}>() ?? sortExpression;");
                codeContent.AppendLine($"                sortOrder = queryModel.IsAsc ? SortOrderEnum.Ascending : SortOrderEnum.Descending;");
                codeContent.AppendLine($"            }}");
                codeContent.AppendLine($"            else if (domainType.IsAssignableTo<IIndexDomain>())");
                codeContent.AppendLine($"            {{");
                codeContent.AppendLine($"                ParameterExpression parameterExpression = Expression.Parameter(domainType, \"m\");");
                codeContent.AppendLine($"                MemberExpression memberExpression = Expression.Property(parameterExpression, nameof(IIndexDomain.Index));");
                codeContent.AppendLine($"                UnaryExpression unaryExpression = Expression.Convert(memberExpression, typeof(object));");
                codeContent.AppendLine($"                sortExpression = Expression.Lambda<Func<{domain.Name}, object>>(unaryExpression, parameterExpression);");
                codeContent.AppendLine($"                sortOrder = SortOrderEnum.Ascending;");
                codeContent.AppendLine($"            }}");
                codeContent.AppendLine($"            #endregion");
                codeContent.AppendLine($"            #region 查询数据源");
                codeContent.AppendLine($"            List<{domain.Name}> allInfo;");
                codeContent.AppendLine($"            if (DefaultRepository is ICacheEFRepository<{domain.Name}, Guid> cacheRepository)");
                codeContent.AppendLine($"            {{");
                codeContent.AppendLine($"                allInfo = await cacheRepository.GetAllInfoFromCacheAsync();");
                codeContent.AppendLine($"                Func<{domain.Name}, bool> searchDlegate = queryModel.GetSearchDelegate<{domain.Name}>();");
                codeContent.AppendLine($"                Func<{domain.Name}, object> sortDlegate = sortExpression.Compile();");
                codeContent.AppendLine($"                if (sortOrder == SortOrderEnum.Ascending)");
                codeContent.AppendLine($"                {{");
                codeContent.AppendLine($"                    allInfo = [.. allInfo.Where(searchDlegate).OrderBy(sortDlegate)];");
                codeContent.AppendLine($"                }}");
                codeContent.AppendLine($"                else");
                codeContent.AppendLine($"                {{");
                codeContent.AppendLine($"                    allInfo = [.. allInfo.Where(searchDlegate).OrderByDescending(sortDlegate)];");
                codeContent.AppendLine($"                }}");
                codeContent.AppendLine($"            }}");
                codeContent.AppendLine($"            else");
                codeContent.AppendLine($"            {{");
                codeContent.AppendLine($"                allInfo = await DefaultRepository.FindAsync(queryModel, sortExpression, sortOrder);");
                codeContent.AppendLine($"            }}");
                codeContent.AppendLine($"            #endregion");
                codeContent.AppendLine($"            OnToTreeBefore(allInfo, queryModel);");
                codeContent.AppendLine($"            List<{domain.Name}TreeListDTO> result = allInfo.ToTree<{domain.Name}, {domain.Name}TreeListDTO>(queryModel.ParentID, (dto, domain) =>");
                codeContent.AppendLine($"            {{");
                codeContent.AppendLine($"                Mapper.Map(domain, dto);");
                codeContent.AppendLine($"                OnConvertToTreeDTO(dto, domain, queryModel);");
                codeContent.AppendLine($"            }});");
                codeContent.AppendLine($"            OnToTreeAfter(result, queryModel);");
                codeContent.AppendLine($"            return result;");
                codeContent.AppendLine($"        }}");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 转换树之前");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"allInfo\"></param>");
                codeContent.AppendLine($"        /// <param name=\"queryModel\"></param>");
                codeContent.AppendLine($"        partial void OnToTreeBefore(List<{domain.Name}> allInfo, Query{domain.Name}TreeListModel queryModel);");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 转换树之后");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"dtos\"></param>");
                codeContent.AppendLine($"        /// <param name=\"queryModel\"></param>");
                codeContent.AppendLine($"        partial void OnToTreeAfter(List<{domain.Name}TreeListDTO> dtos, Query{domain.Name}TreeListModel queryModel);");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 转换为树DTO");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"dto\"></param>");
                codeContent.AppendLine($"        /// <param name=\"domain\"></param>");
                codeContent.AppendLine($"        /// <param name=\"queryModel\"></param>");
                codeContent.AppendLine($"        partial void OnConvertToTreeDTO({domain.Name}TreeListDTO dto, {domain.Name} domain, Query{domain.Name}TreeListModel queryModel);");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            await codeContent.SaveAsAsync(Context, _moduleApplication, "Services", $"{domain.Name}ServiceImpl.cs");
        }
    }
}
