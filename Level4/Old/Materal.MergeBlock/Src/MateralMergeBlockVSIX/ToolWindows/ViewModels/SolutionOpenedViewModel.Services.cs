#nullable enable
using Materal.MergeBlock.GeneratorCode;
using Materal.MergeBlock.GeneratorCode.Attributers;
using Materal.MergeBlock.GeneratorCode.Models;
using MateralMergeBlockVSIX.Extensions;
using MateralMergeBlockVSIX.ToolWindows.Attributes;
using Microsoft.VisualStudio.PlatformUI;
using System.CodeDom;
using System.Collections.Generic;
using System.Text;

namespace MateralMergeBlockVSIX.ToolWindows.ViewModels
{
    public partial class SolutionOpenedViewModel : ObservableObject
    {
        /// <summary>
        /// 创建操作模型
        /// </summary>
        /// <param name="domains"></param>
        [GeneratorCodeMethod]
        private void GeneratorOperationalModel(List<DomainModel> domains)
        {
            foreach (DomainModel domain in domains)
            {
                GeneratorAddModel(domain);
                GeneratorEditModel(domain);
                GeneratorQueryModel(domain, domains);
                GeneratorTreeQueryModel(domain);
            }
        }
        /// <summary>
        /// 创建添加模型
        /// </summary>
        /// <param name="domain"></param>
        private void GeneratorAddModel(DomainModel domain)
        {
            if (domain.HasAttribute<NotServiceAttribute, ViewAttribute, NotAddAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.Services.Models.{domain.Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}添加模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class Add{domain.Name}Model : IAddServiceModel");
            codeContent.AppendLine($"    {{");
            foreach (PropertyModel property in domain.Properties)
            {
                if (property.HasAttribute<NotAddAttribute>()) continue;
                GeneratorOperationalModelProperty(codeContent, property);
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleAbstractions, "Services", "Models", domain.Name, $"Add{domain.Name}Model.cs");
        }
        /// <summary>
        /// 创建修改模型
        /// </summary>
        /// <param name="domain"></param>
        private void GeneratorEditModel(DomainModel domain)
        {
            if (domain.HasAttribute<NotServiceAttribute, ViewAttribute, NotEditAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.Services.Models.{domain.Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}修改模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class Edit{domain.Name}Model : IEditServiceModel");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 唯一标识");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        [Required(ErrorMessage = \"唯一标识为空\")]");
            codeContent.AppendLine($"        public Guid ID {{ get; set; }}");
            foreach (PropertyModel property in domain.Properties)
            {
                if (property.HasAttribute<NotEditAttribute>()) continue;
                GeneratorOperationalModelProperty(codeContent, property);
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleAbstractions, "Services", "Models", domain.Name, $"Edit{domain.Name}Model.cs");
        }
        /// <summary>
        /// 创建查询模型
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="domains"></param>
        private void GeneratorQueryModel(DomainModel domain, List<DomainModel> domains)
        {
            if (domain.HasAttribute<NotServiceAttribute, ViewAttribute, NotQueryAttribute>()) return;
            DomainModel targetDomain = domain.GetQueryDomain(domains);
            StringBuilder codeContent = new();
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.Services.Models.{domain.Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}查询模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class Query{domain.Name}Model : PageRequestModel, IQueryServiceModel");
            codeContent.AppendLine($"    {{");
            foreach (PropertyModel property in targetDomain.Properties)
            {
                if (property.HasAttribute<NotQueryAttribute>()) continue;
                if (!property.HasAttribute<BetweenAttribute>())
                {
                    if (property.Annotation is not null && !string.IsNullOrWhiteSpace(property.Annotation))
                    {
                        codeContent.AppendLine($"        /// <summary>");
                        codeContent.AppendLine($"        /// {property.Annotation}");
                        codeContent.AppendLine($"        /// </summary>");
                    }
                    string? queryAttributesCode = property.GetQueryAttributesCode();
                    if (queryAttributesCode is not null && !string.IsNullOrWhiteSpace(queryAttributesCode))
                    {
                        codeContent.AppendLine($"        {queryAttributesCode}");
                    }
                    codeContent.AppendLine($"        public {property.NullPredefinedType} {property.Name} {{ get; set; }}");
                }
                else
                {
                    if (property.Annotation is not null && !string.IsNullOrWhiteSpace(property.Annotation))
                    {
                        codeContent.AppendLine($"        /// <summary>");
                        codeContent.AppendLine($"        /// 最小{property.Annotation}");
                        codeContent.AppendLine($"        /// </summary>");
                    }
                    codeContent.AppendLine($"        [GreaterThanOrEqual(\"{property.Name}\")]");
                    codeContent.AppendLine($"        public {property.NullPredefinedType} Min{property.Name} {{ get; set; }}");
                    if (property.Annotation is not null && !string.IsNullOrWhiteSpace(property.Annotation))
                    {
                        codeContent.AppendLine($"        /// <summary>");
                        codeContent.AppendLine($"        /// 最大{property.Annotation}");
                        codeContent.AppendLine($"        /// </summary>");
                    }
                    codeContent.AppendLine($"        [LessThanOrEqual(\"{property.Name}\")]");
                    codeContent.AppendLine($"        public {property.NullPredefinedType} Max{property.Name} {{ get; set; }}");
                }
            }
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 唯一标识组");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        [Contains(\"ID\")]");
            codeContent.AppendLine($"        public List<Guid>? IDs {{ get; set; }}");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 最小创建时间");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        [GreaterThanOrEqual(\"CreateTime\")]");
            codeContent.AppendLine($"        public DateTime? MinCreateTime {{ get; set; }}");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 最大创建时间");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        [LessThanOrEqual(\"CreateTime\")]");
            codeContent.AppendLine($"        public DateTime? MaxCreateTime {{ get; set; }}");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleAbstractions, "Services", "Models", domain.Name, $"Query{domain.Name}Model.cs");
        }
        /// <summary>
        /// 创建树查询模型
        /// </summary>
        /// <param name="domain"></param>
        private void GeneratorTreeQueryModel(DomainModel domain)
        {
            if (!domain.IsTreeDomain) return;
            if (domain.HasAttribute<NotServiceAttribute, ViewAttribute, NotQueryAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.Services.Models.{domain.Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}树查询模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class Query{domain.Name}TreeListModel : FilterModel");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 父级唯一标识");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        public Guid? ParentID {{ get; set; }}");
            PropertyModel? treePropertyModel = domain.GetTreeGroupProperty();
            if(treePropertyModel is not null)
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {treePropertyModel.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        [Equal]");
                codeContent.AppendLine($"        public {treePropertyModel.NullPredefinedType} {treePropertyModel.Name} {{ get; set; }}");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleAbstractions, "Services", "Models", domain.Name, $"Query{domain.Name}TreeListModel.cs");
        }
        /// <summary>
        /// 创建列表数据传输模型
        /// </summary>
        /// <param name="domains"></param>
        [GeneratorCodeMethod]
        private void GeneratorDTOModel(List<DomainModel> domains)
        {
            foreach (DomainModel domain in domains)
            {
                GeneratorListDTOModel(domain, domains);
                GeneratorDTOModel(domain, domains);
                GeneratorTreeListDTOModel(domain);
            }
        }
        /// <summary>
        /// 创建列表数据传输模型
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="domains"></param>
        private void GeneratorListDTOModel(DomainModel domain, List<DomainModel> domains)
        {
            if (domain.HasAttribute<NotServiceAttribute, NotListDTOAttribute>()) return;
            DomainModel targetDomain = domain.GetQueryDomain(domains);
            StringBuilder codeContent = new();
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.DTO.{domain.Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}列表数据传输模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {domain.Name}ListDTO : IListDTO");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 唯一标识");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        [Required(ErrorMessage = \"唯一标识为空\")]");
            codeContent.AppendLine($"        public Guid ID {{ get; set; }}");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 创建时间");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        [Required(ErrorMessage = \"创建时间为空\")]");
            codeContent.AppendLine($"        public DateTime CreateTime {{ get; set; }}");
            foreach (PropertyModel property in targetDomain.Properties)
            {
                if (property.HasAttribute<NotListDTOAttribute>()) continue;
                GeneratorDTOModelProperty(codeContent, property);
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleAbstractions, "DTO", domain.Name, $"{domain.Name}ListDTO.cs");
        }
        /// <summary>
        /// 创建列表数据传输模型
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="domains"></param>
        private void GeneratorDTOModel(DomainModel domain, List<DomainModel> domains)
        {
            if (domain.HasAttribute<NotServiceAttribute, NotDTOAttribute>()) return;
            DomainModel targetDomain = domain.GetQueryDomain(domains);
            StringBuilder codeContent = new();
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.DTO.{domain.Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}数据传输模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {domain.Name}DTO : {domain.Name}ListDTO, IDTO");
            codeContent.AppendLine($"    {{");
            foreach (PropertyModel property in targetDomain.Properties)
            {
                if (property.HasAttribute<NotDTOAttribute>() || !property.HasAttribute<NotListDTOAttribute>()) continue;
                GeneratorDTOModelProperty(codeContent, property);
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleAbstractions, "DTO", domain.Name, $"{domain.Name}DTO.cs");
        }
        /// <summary>
        /// 创建树列表数据传输模型
        /// </summary>
        /// <param name="domain"></param>
        private void GeneratorTreeListDTOModel(DomainModel domain)
        {
            if (domain.HasAttribute<NotServiceAttribute>()) return;
            if (!domain.IsTreeDomain) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.DTO.{domain.Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}树列表数据传输模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {domain.Name}TreeListDTO: {domain.Name}ListDTO, ITreeDTO<{domain.Name}TreeListDTO>");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 子级");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        public List<{domain.Name}TreeListDTO> Children {{ get; set; }} = [];");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleAbstractions, "DTO", domain.Name, $"{domain.Name}TreeListDTO.cs");
        }
        /// <summary>
        /// 创建DTO模型属性
        /// </summary>
        /// <param name="codeContent"></param>
        /// <param name="property"></param>
        private void GeneratorDTOModelProperty(StringBuilder codeContent, PropertyModel property)
        {
            if (property.Annotation is not null && !string.IsNullOrWhiteSpace(property.Annotation))
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {property.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
            }
            string? queryAttributesCode = property.GetVerificationAttributesCode();
            if (queryAttributesCode is not null && !string.IsNullOrWhiteSpace(queryAttributesCode))
            {
                codeContent.AppendLine($"        {queryAttributesCode}");
            }
            codeContent.AppendLine($"        public {property.PredefinedType} {property.Name} {{ get; set; }}");
            if (property.Initializer is not null && !string.IsNullOrWhiteSpace(property.Initializer))
            {
                codeContent.Insert(codeContent.Length - 2, $"  = {property.Initializer};");
            }
        }
        /// <summary>
        /// 创建服务代码
        /// </summary>
        /// <param name="domains"></param>
        [GeneratorCodeMethod]
        private void GeneratorServicesCode(List<DomainModel> domains)
        {
            foreach (DomainModel domain in domains)
            {
                GeneratorIServicesCode(domain);
                GeneratorServiceImplsCode(domain, domains);
            }
        }
        /// <summary>
        /// 创建服务代码接口
        /// </summary>
        /// <param name="domain"></param>
        private void GeneratorIServicesCode(DomainModel domain)
        {
            if (domain.HasAttribute<NotServiceAttribute, ViewAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using Materal.MergeBlock.Abstractions.Services;");
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
            if (domain.IsIndexDomain)
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 交换位序");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"model\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        Task ExchangeIndexAsync(ExchangeIndexModel model);");

            }
            if (domain.IsTreeDomain)
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
            codeContent.SaveAs(_moduleAbstractions, "Services", $"I{domain.Name}Service.cs");
        }
        /// <summary>
        /// 创建服务代码实现
        /// </summary>
        /// <param name="domain"></param>
        private void GeneratorServiceImplsCode(DomainModel domain, List<DomainModel> domains)
        {
            if (domain.HasAttribute<NotServiceAttribute, ViewAttribute>()) return;
            DomainModel targetDomain = domain.GetQueryDomain(domains);
            StringBuilder codeContent = new();
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
                codeContent.AppendLine($"    public partial class {domain.Name}ServiceImpl : BaseServiceImpl, I{domain.Name}Service");
            }
            else if (targetDomain != domain)
            {
                codeContent.AppendLine($"    public partial class {domain.Name}ServiceImpl : BaseServiceImpl<Add{domain.Name}Model, Edit{domain.Name}Model, Query{domain.Name}Model, {domain.Name}DTO, {domain.Name}ListDTO, I{domain.Name}Repository, I{targetDomain.Name}Repository, {domain.Name}, {targetDomain.Name}>, I{domain.Name}Service");
            }
            else
            {
                codeContent.AppendLine($"    public partial class {domain.Name}ServiceImpl : BaseServiceImpl<Add{domain.Name}Model, Edit{domain.Name}Model, Query{domain.Name}Model, {domain.Name}DTO, {domain.Name}ListDTO, I{domain.Name}Repository, {domain.Name}>, I{domain.Name}Service");
            }
            codeContent.AppendLine($"    {{");
            PropertyModel? treeGroupProperty = null;
            if (domain.IsIndexDomain)
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
                    codeContent.AppendLine($"            await ServiceImplHelper.ExchangeIndexAndExchangeParentByGroupPropertiesAsync<I{domain.Name}Repository, {domain.Name}>(model, DefaultRepository, UnitOfWork);");
                }
                else
                {
                    if(treeGroupProperty is null)
                    {
                        codeContent.AppendLine($"            await ServiceImplHelper.ExchangeIndexAndExchangeParentByGroupPropertiesAsync<I{domain.Name}Repository, {domain.Name}>(model, DefaultRepository, UnitOfWork, new string[] {{ nameof({domain.Name}.{indexGroupProperty.Name}) }});");
                    }
                    else
                    {
                        codeContent.AppendLine($"            await ServiceImplHelper.ExchangeIndexAndExchangeParentByGroupPropertiesAsync<I{domain.Name}Repository, {domain.Name}>(model, DefaultRepository, UnitOfWork, new string[] {{ nameof({domain.Name}.{indexGroupProperty.Name}) }}, new string[] {{ nameof({domain.Name}.{treeGroupProperty.Name}) }});");
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
            if (domain.IsTreeDomain)
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
                codeContent.AppendLine($"                    allInfo = allInfo.Where(searchDlegate).OrderBy(sortDlegate).ToList();");
                codeContent.AppendLine($"                }}");
                codeContent.AppendLine($"                else");
                codeContent.AppendLine($"                {{");
                codeContent.AppendLine($"                    allInfo = allInfo.Where(searchDlegate).OrderByDescending(sortDlegate).ToList();");
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
            codeContent.SaveAs(_moduleApplication, "Services", $"{domain.Name}ServiceImpl.cs");
        }
    }
}
