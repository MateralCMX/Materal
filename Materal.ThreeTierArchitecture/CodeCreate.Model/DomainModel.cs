using CodeCreate.Common;
using Materal.FileHelper;

namespace CodeCreate.Model
{
    public class DomainModel
    {
        private string _name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get => _name; set => _name = value.Trim(); }
        /// <summary>
        /// 实体名称
        /// </summary>
        public string DomainName { get; set; }
        /// <summary>
        /// 唯一标识类型
        /// </summary>
        public string IDType { get; set; } = "Guid";
        /// <summary>
        /// 有服务
        /// </summary>
        public bool HasService { get; set; } = true;

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="subSystemName"></param>
        public void CreateFile(string targetPath, string subSystemName)
        {
            CreateDomainFile(targetPath, subSystemName);
            CreateRepositoryInterface(targetPath, subSystemName);
        }
        /// <summary>
        /// 创建Domain文件
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="subSystemName">子系统名称</param>
        private void CreateDomainFile(string targetPath, string subSystemName)
        {
            TextFileManager.WriteText($"{targetPath}/{Name}.cs", GetDomainFileContent(subSystemName), ApplicationConfig.ApplicationEncoding);
        }
        /// <summary>
        /// 获得Domain文件内容
        /// </summary>
        /// <param name="subSystemName">子系统名称</param>
        /// <returns></returns>
        private string GetDomainFileContent(string subSystemName)
        {
            string result = "using Domain;\r\n";
            result += "using System;\r\n";
            result += "using System.Collections.Generic;\r\n";
            result += $"namespace {subSystemName}.Domain\r\n";
            result += "{\r\n";
            result += "    /// <summary>\r\n";
            result += $"    /// {DomainName}\r\n";
            result += "    /// </summary>\r\n";
            result += $"    public sealed class {Name} : BaseEntity<{IDType}>\r\n";
            result += "    {\r\n";
            result += "    }\r\n";
            result += "}";
            return result;
        }

        /// <summary>
        /// 创建仓储接口文件
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="subSystemName"></param>
        private void CreateRepositoryInterface(string targetPath, string subSystemName)
        {
            TextFileManager.WriteText($"{targetPath}/Repositories/I{Name}Repository.cs", GetRepositoryInterfaceFileContent(subSystemName), ApplicationConfig.ApplicationEncoding);
        }
        /// <summary>
        /// 获得Domain文件内容
        /// </summary>
        /// <param name="subSystemName">子系统名称</param>
        /// <returns></returns>
        private string GetRepositoryInterfaceFileContent(string subSystemName)
        {
            string result = "using Materal.TTA.Common;\r\n";
            result += "using System;\r\n";
            result += $"namespace {subSystemName}.Domain.Repositories\r\n";
            result += "{\r\n";
            result += "    /// <summary>\r\n";
            result += $"    /// {DomainName}仓储\r\n";
            result += "    /// </summary>\r\n";
            result += $"    public interface I{Name}Repository : IRepository<{Name}, {IDType}>\r\n";
            result += "    {\r\n";
            result += "    }\r\n";
            result += "}";
            return result;
        }

        /// <summary>
        /// 创建仓储文件
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="subSystemName"></param>
        public void CreateRepositoryImpl(string targetPath, string subSystemName)
        {
            TextFileManager.WriteText($"{targetPath}/RepositoryImpl/{Name}RepositoryImpl.cs", GetRepositoryImplFileContent(subSystemName), ApplicationConfig.ApplicationEncoding);
        }
        /// <summary>
        /// 获得Domain文件内容
        /// </summary>
        /// <param name="subSystemName">子系统名称</param>
        /// <returns></returns>
        private string GetRepositoryImplFileContent(string subSystemName)
        {
            string result = "using System;\r\n";
            result += $"using {subSystemName}.Domain;\r\n";
            result += $"using {subSystemName}.Domain.Repositories;\r\n";
            result += $"namespace {subSystemName}.EFRepository.RepositoryImpl\r\n";
            result += "{\r\n";
            result += "    /// <summary>\r\n";
            result += $"    /// {DomainName}仓储\r\n";
            result += "    /// </summary>\r\n";
            result += $"    public class {Name}RepositoryImpl : {subSystemName}EFRepositoryImpl<{Name}, Guid>, I{Name}Repository\r\n";
            result += "    {\r\n";
            result += $"        public {Name}RepositoryImpl({subSystemName}DbContext dbContext) : base(dbContext)\r\n";
            result += "        {\r\n";
            result += "        }\r\n";
            result += "    }\r\n";
            result += "}";
            return result;
        }
        /// <summary>
        /// 创建模型配置文件
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="subSystemName"></param>
        public void CreateModelConfigFile(string targetPath, string subSystemName)
        {
            TextFileManager.WriteText($"{targetPath}/ModelConfig/{Name}Config.cs", GetModelConfigFileContent(subSystemName), ApplicationConfig.ApplicationEncoding);
        }
        /// <summary>
        /// 获取模型配置文件内容
        /// </summary>
        /// <param name="subSystemName"></param>
        /// <returns></returns>
        private string GetModelConfigFileContent(string subSystemName)
        {
            string result = "using Microsoft.EntityFrameworkCore;\r\n";
            result += "using Microsoft.EntityFrameworkCore.Metadata.Builders;\r\n";
            result += $"using {subSystemName}.Domain;\r\n";
            result += $"namespace {subSystemName}.EFRepository.ModelConfig\r\n";
            result += "{\r\n";
            result += "    /// <summary>\r\n";
            result += $"    /// {DomainName}模型配置\r\n";
            result += "    /// </summary>\r\n";
            result += $"    internal sealed class {Name}Config : IEntityTypeConfiguration<{Name}>\r\n";
            result += "    {\r\n";
            result += $"        public void Configure(EntityTypeBuilder<{Name}> builder)\r\n";
            result += "        {\r\n";
            result += "            builder.HasKey(e => e.ID);\r\n";
            result += "            builder.Property(e => e.CreateTime)\r\n";
            result += "                .IsRequired();\r\n";
            result += "            builder.Property(e => e.UpdateTime)\r\n";
            result += "                .IsRequired();\r\n";
            result += "        }\r\n";
            result += "    }\r\n";
            result += "}";
            return result;
        }
        /// <summary>
        /// 创建数据传输模型文件
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="subSystemName"></param>
        public void CreateDataTransmitModelFile(string targetPath, string subSystemName)
        {
            if (!HasService) return;
            TextFileManager.WriteText($"{targetPath}/{Name}/{Name}ListDTO.cs", GetListDTOFileContent(subSystemName), ApplicationConfig.ApplicationEncoding);
            TextFileManager.WriteText($"{targetPath}/{Name}/{Name}DTO.cs", GetDTOFileContent(subSystemName), ApplicationConfig.ApplicationEncoding);
        }
        /// <summary>
        /// 获取数据传输模型文件内容
        /// </summary>
        /// <param name="subSystemName"></param>
        /// <returns></returns>
        private string GetListDTOFileContent(string subSystemName)
        {
            string result = "using System;\r\n";
            result += $"namespace {subSystemName}.DataTransmitModel.{Name}\r\n";
            result += "{\r\n";
            result += "    /// <summary>\r\n";
            result += $"    /// {DomainName}列表数据传输模型\r\n";
            result += "    /// </summary>\r\n";
            result += $"    public class {Name}ListDTO\r\n";
            result += "    {\r\n";
            result += "        /// <summary>\r\n";
            result += "        /// 唯一标识\r\n";
            result += "        /// </summary>\r\n";
            result += "        public Guid ID { get; set; }\r\n";
            result += "    }\r\n";
            result += "}";
            return result;
        }
        /// <summary>
        /// 获取数据传输模型文件内容
        /// </summary>
        /// <param name="subSystemName"></param>
        /// <returns></returns>
        private string GetDTOFileContent(string subSystemName)
        {
            string result = "using System;\r\n";
            result += $"namespace {subSystemName}.DataTransmitModel.{Name}\r\n";
            result += "{\r\n";
            result += "    /// <summary>\r\n";
            result += $"    /// {DomainName}数据传输模型\r\n";
            result += "    /// </summary>\r\n";
            result += $"    public class {Name}DTO : {Name}ListDTO\r\n";
            result += "    {\r\n";
            result += "    }\r\n";
            result += "}";
            return result;
        }
        /// <summary>
        /// 创服务文件
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="subSystemName"></param>
        public void CreateServiceFile(string targetPath, string subSystemName)
        {
            if (!HasService) return;
            TextFileManager.WriteText($"{targetPath}/Model/{Name}/Add{Name}Model.cs", GetAddModelFileContent(subSystemName), ApplicationConfig.ApplicationEncoding);
            TextFileManager.WriteText($"{targetPath}/Model/{Name}/Edit{Name}Model.cs", GetEditModelFileContent(subSystemName), ApplicationConfig.ApplicationEncoding);
            TextFileManager.WriteText($"{targetPath}/Model/{Name}/Query{Name}FilterModel.cs", GetQueryModelFileContent(subSystemName), ApplicationConfig.ApplicationEncoding);
            TextFileManager.WriteText($"{targetPath}/I{Name}Service.cs", GetServiceFileContent(subSystemName), ApplicationConfig.ApplicationEncoding);
        }
        /// <summary>
        /// 获取添加模型文件内容
        /// </summary>
        /// <param name="subSystemName"></param>
        /// <returns></returns>
        private string GetAddModelFileContent(string subSystemName)
        {
            string result = "using System;\r\n";
            result += $"namespace {subSystemName}.Service.Model.{Name}\r\n";
            result += "{\r\n";
            result += "    /// <summary>\r\n";
            result += $"    /// {DomainName}添加模型\r\n";
            result += "    /// </summary>\r\n";
            result += $"    public class Add{Name}Model\r\n";
            result += "    {\r\n";
            result += "    }\r\n";
            result += "}";
            return result;
        }
        /// <summary>
        /// 获取修改模型文件内容
        /// </summary>
        /// <param name="subSystemName"></param>
        /// <returns></returns>
        private string GetEditModelFileContent(string subSystemName)
        {
            string result = "using System;\r\n";
            result += $"namespace {subSystemName}.Service.Model.{Name}\r\n";
            result += "{\r\n";
            result += "    /// <summary>\r\n";
            result += $"    /// {DomainName}修改模型\r\n";
            result += "    /// </summary>\r\n";
            result += $"    public class Edit{Name}Model : Add{Name}Model\r\n";
            result += "    {\r\n";
            result += "        /// <summary>\r\n";
            result += "        /// 唯一标识\r\n";
            result += "        /// </summary>\r\n";
            result += "        public Guid ID { get; set; }\r\n";
            result += "    }\r\n";
            result += "}";
            return result;
        }
        /// <summary>
        /// 获取查询模型文件内容
        /// </summary>
        /// <param name="subSystemName"></param>
        /// <returns></returns>
        private string GetQueryModelFileContent(string subSystemName)
        {
            string result = "using System;\r\n";
            result += "using Materal.Common;\r\n";
            result += $"namespace {subSystemName}.Service.Model.{Name}\r\n";
            result += "{\r\n";
            result += "    /// <summary>\r\n";
            result += $"    /// {DomainName}查询模型\r\n";
            result += "    /// </summary>\r\n";
            result += $"    public class Query{Name}FilterModel : PageRequestModel\r\n";
            result += "    {\r\n";
            result += "    }\r\n";
            result += "}";
            return result;
        }
        /// <summary>
        /// 获取服务文件内容
        /// </summary>
        /// <param name="subSystemName"></param>
        /// <returns></returns>
        private string GetServiceFileContent(string subSystemName)
        {
            string result = "using System;\r\n";
            result += "using System.Collections.Generic;\r\n";
            result += "using System.Threading.Tasks;\r\n";
            result += "using Materal.Common;\r\n";
            result += $"using {subSystemName}.DataTransmitModel.{Name};\r\n";
            result += $"using {subSystemName}.Service.Model.{Name};\r\n";
            result += $"namespace {subSystemName}.Service\r\n";
            result += "{\r\n";
            result += "    /// <summary>\r\n";
            result += $"    /// {DomainName}服务\r\n";
            result += "    /// </summary>\r\n";
            result += $"    public interface I{Name}Service\r\n";
            result += "    {\r\n";
            result += "        /// <summary>\r\n";
            result += $"        /// 添加{DomainName}\r\n";
            result += "        /// </summary>\r\n";
            result += "        /// <param name=\"model\">添加模型</param>\r\n";
            result += "        /// <returns></returns>\r\n";
            result += "        /// <exception cref=\"InvalidOperationException\"></exception>\r\n";
            result += $"        Task Add{Name}Async(Add{Name}Model model);\r\n";
            result += "        /// <summary>\r\n";
            result += $"        /// 修改{DomainName}\r\n";
            result += "        /// </summary>\r\n";
            result += "        /// <param name=\"model\">修改模型</param>\r\n";
            result += "        /// <returns></returns>\r\n";
            result += "        /// <exception cref=\"InvalidOperationException\"></exception>\r\n";
            result += $"        Task Edit{Name}Async(Edit{Name}Model model);\r\n";
            result += "        /// <summary>\r\n";
            result += $"        /// 删除{DomainName}\r\n";
            result += "        /// </summary>\r\n";
            result += "        /// <param name=\"id\">唯一标识</param>\r\n";
            result += "        /// <returns></returns>\r\n";
            result += "        /// <exception cref=\"InvalidOperationException\"></exception>\r\n";
            result += $"        Task Delete{Name}Async(Guid id);\r\n";
            result += "        /// <summary>\r\n";
            result += $"        /// 获得{DomainName}信息\r\n";
            result += "        /// </summary>\r\n";
            result += "        /// <param name=\"id\">唯一标识</param>\r\n";
            result += "        /// <returns></returns>\r\n";
            result += "        /// <exception cref=\"InvalidOperationException\"></exception>\r\n";
            result += $"        Task<{Name}DTO> Get{Name}InfoAsync(Guid id);\r\n";
            result += "        /// <summary>\r\n";
            result += $"        /// 获得{DomainName}列表\r\n";
            result += "        /// </summary>\r\n";
            result += "        /// <param name=\"filterModel\">查询模型</param>\r\n";
            result += "        /// <returns></returns>\r\n";
            result += "        /// <exception cref=\"InvalidOperationException\"></exception>\r\n";
            result += $"        Task<(List<{Name}ListDTO> result, PageModel pageModel)> Get{Name}ListAsync(Query{Name}FilterModel filterModel);\r\n";
            result += "    }\r\n";
            result += "}";
            return result;
        }
        /// <summary>
        /// 创建服务实现文件
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="subSystemName"></param>
        public void CreateServiceImplFile(string targetPath, string subSystemName)
        {
            if (!HasService) return;
            TextFileManager.WriteText($"{targetPath}/AutoMapperProfile/{Name}Profile.cs", GetDomainAutoMapperFileContent(subSystemName), ApplicationConfig.ApplicationEncoding);
            TextFileManager.WriteText($"{targetPath}/{Name}ServiceImpl.cs", GetServiceImplFileContent(subSystemName), ApplicationConfig.ApplicationEncoding);
        }
        /// <summary>
        /// 获取服务实现文件内容
        /// </summary>
        /// <param name="subSystemName"></param>
        /// <returns></returns>
        private string GetDomainAutoMapperFileContent(string subSystemName)
        {
            string result = "using AutoMapper;\r\n";
            result += $"using {subSystemName}.DataTransmitModel.{Name};\r\n";
            result += $"using {subSystemName}.Domain;\r\n";
            result += $"namespace {subSystemName}.ServiceImpl.AutoMapperProfile\r\n";
            result += "{\r\n";
            result += "    /// <summary>\r\n";
            result += $"    /// {DomainName}AutoMapper配置\r\n";
            result += "    /// </summary>\r\n";
            result += $"    public sealed class {Name}Profile : Profile\r\n";
            result += "    {\r\n";
            result += $"        public {Name}Profile()\r\n";
            result += "        {\r\n";
            result += $"            CreateMap<{Name}, {Name}ListDTO>();\r\n";
            result += $"            CreateMap<{Name}, {Name}DTO>();\r\n";
            result += "        }\r\n";
            result += "    }\r\n";
            result += "}";
            return result;
        }
        /// <summary>
        /// 获取服务实现文件内容
        /// </summary>
        /// <param name="subSystemName"></param>
        /// <returns></returns>
        private string GetServiceImplFileContent(string subSystemName)
        {
            string result = "using AutoMapper;\r\n";
            result += "using Materal.Common;\r\n";
            result += "using Materal.ConvertHelper;\r\n";
            result += "using Materal.LinqHelper;\r\n";
            result += "using Microsoft.EntityFrameworkCore;\r\n";
            result += "using System;\r\n";
            result += "using System.Collections.Generic;\r\n";
            result += "using System.Linq;\r\n";
            result += "using System.Linq.Expressions;\r\n";
            result += "using System.Threading.Tasks;\r\n";
            result += $"using {subSystemName}.DataTransmitModel.{Name};\r\n";
            result += $"using {subSystemName}.Domain;\r\n";
            result += $"using {subSystemName}.Domain.Repositories;\r\n";
            result += $"using {subSystemName}.EFRepository;\r\n";
            result += $"using {subSystemName}.Service;\r\n";
            result += $"using {subSystemName}.Service.Model.{Name};\r\n";
            result += $"namespace {subSystemName}.ServiceImpl\r\n";
            result += "{\r\n";
            result += "    /// <summary>\r\n";
            result += $"    /// {DomainName}服务\r\n";
            result += "    /// </summary>\r\n";
            result += $"    public sealed class {Name}ServiceImpl : I{Name}Service\r\n";
            result += "    {\r\n";
            result += $"        private readonly I{Name}Repository _{Name.FirstLower()}Repository;\r\n";
            result += "        private readonly IMapper _mapper;\r\n";
            result += $"        private readonly I{subSystemName}UnitOfWork _{subSystemName.FirstLower()}UnitOfWork;\r\n";
            result += $"        public {Name}ServiceImpl(I{Name}Repository {Name.FirstLower()}Repository, IMapper mapper, I{subSystemName}UnitOfWork {subSystemName.FirstLower()}UnitOfWork)\r\n";
            result += "        {\r\n";
            result += $"            _{Name.FirstLower()}Repository = {Name.FirstLower()}Repository;\r\n";
            result += "            _mapper = mapper;\r\n";
            result += $"            _{subSystemName.FirstLower()}UnitOfWork = {subSystemName.FirstLower()}UnitOfWork;\r\n";
            result += "        }\r\n";
            result += $"        public async Task Add{Name}Async(Add{Name}Model model)\r\n";
            result += "        {\r\n";
            result += "            throw new NotImplementedException();\r\n";
            result += "        }\r\n";
            result += $"        public async Task Edit{Name}Async(Edit{Name}Model model)\r\n";
            result += "        {\r\n";
            result += "            throw new NotImplementedException();\r\n";
            result += "        }\r\n";
            result += $"        public async Task Delete{Name}Async(Guid id)\r\n";
            result += "        {\r\n";
            result += "            throw new NotImplementedException();\r\n";
            result += "        }\r\n";
            result += $"        public async Task<{Name}DTO> Get{Name}InfoAsync(Guid id)\r\n";
            result += "        {\r\n";
            result += "            throw new NotImplementedException();\r\n";
            result += "        }\r\n";
            result += $"        public async Task<(List<{Name}ListDTO> result, PageModel pageModel)> Get{Name}ListAsync(Query{Name}FilterModel filterModel)\r\n";
            result += "        {\r\n";
            result += "            throw new NotImplementedException();\r\n";
            result += "        }\r\n";
            result += "    }\r\n";
            result += "}";
            return result;
        }

        /// <summary>
        /// 创建数据传输模型文件
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="subSystemName"></param>
        public void CreatePresentationModelFile(string targetPath, string subSystemName)
        {
            if (!HasService) return;
            TextFileManager.WriteText($"{targetPath}/{Name}/Request/Add{Name}RequestModel.cs", GetAddRequestModelFileContent(subSystemName), ApplicationConfig.ApplicationEncoding);
            TextFileManager.WriteText($"{targetPath}/{Name}/Request/Edit{Name}RequestModel.cs", GetEditRequestModelFileContent(subSystemName), ApplicationConfig.ApplicationEncoding);
            TextFileManager.WriteText($"{targetPath}/{Name}/Request/Query{Name}FilterRequestModel.cs", GetQueryRequestModelFileContent(subSystemName), ApplicationConfig.ApplicationEncoding);
            TextFileManager.WriteText($"{targetPath}/AutoMapperProfile/{Name}Profile.cs", GetPresentationAutoMapperFileContent(subSystemName), ApplicationConfig.ApplicationEncoding);
        }
        /// <summary>
        /// 获取服务实现文件内容
        /// </summary>
        /// <param name="subSystemName"></param>
        /// <returns></returns>
        private string GetPresentationAutoMapperFileContent(string subSystemName)
        {
            string result = "using AutoMapper;\r\n";
            result += $"using {subSystemName}.PresentationModel.{Name}.Request;\r\n";
            result += $"using {subSystemName}.Service.Model.{Name};\r\n";
            result += $"namespace {subSystemName}.PresentationModel.AutoMapperProfile\r\n";
            result += "{\r\n";
            result += "    /// <summary>\r\n";
            result += $"    /// {DomainName}AutoMapper配置\r\n";
            result += "    /// </summary>\r\n";
            result += $"    public sealed class {Name}Profile : Profile\r\n";
            result += "    {\r\n";
            result += "        /// <summary>\r\n";
            result += $"        /// {DomainName}AutoMapper配置\r\n";
            result += "        /// </summary>\r\n";
            result += $"        public {Name}Profile()\r\n";
            result += "        {\r\n";
            result += $"            CreateMap<Add{Name}RequestModel, Add{Name}Model>();\r\n";
            result += $"            CreateMap<Edit{Name}RequestModel, Edit{Name}Model>();\r\n";
            result += $"            CreateMap<Query{Name}FilterRequestModel, Query{Name}FilterModel>();\r\n";
            result += "        }\r\n";
            result += "    }\r\n";
            result += "}";
            return result;
        }
        /// <summary>
        /// 获取添加请求模型文件内容
        /// </summary>
        /// <param name="subSystemName"></param>
        /// <returns></returns>
        private string GetAddRequestModelFileContent(string subSystemName)
        {
            string result = "using System;\r\n";
            result += "using System.ComponentModel.DataAnnotations;\r\n";
            result += $"namespace {subSystemName}.PresentationModel.{Name}.Request\r\n";
            result += "{\r\n";
            result += "    /// <summary>\r\n";
            result += $"    /// {DomainName}添加请求模型\r\n";
            result += "    /// </summary>\r\n";
            result += $"    public class Add{Name}RequestModel\r\n";
            result += "    {\r\n";
            result += "    }\r\n";
            result += "}";
            return result;
        }
        /// <summary>
        /// 获取修改请求模型文件内容
        /// </summary>
        /// <param name="subSystemName"></param>
        /// <returns></returns>
        private string GetEditRequestModelFileContent(string subSystemName)
        {
            string result = "using System;\r\n";
            result += "using System.ComponentModel.DataAnnotations;\r\n";
            result += $"namespace {subSystemName}.PresentationModel.{Name}.Request\r\n";
            result += "{\r\n";
            result += "    /// <summary>\r\n";
            result += $"    /// {DomainName}修改请求模型\r\n";
            result += "    /// </summary>\r\n";
            result += $"    public class Edit{Name}RequestModel : Add{Name}RequestModel\r\n";
            result += "    {\r\n";
            result += "        /// <summary>\r\n";
            result += "        /// 唯一标识\r\n";
            result += "        /// </summary>\r\n";
            result += "        [Required(ErrorMessage = \"唯一标识不可以为空\")]\r\n";
            result += "        public Guid ID { get; set; }\r\n";
            result += "    }\r\n";
            result += "}";
            return result;
        }
        /// <summary>
        /// 获取查询请求模型文件内容
        /// </summary>
        /// <param name="subSystemName"></param>
        /// <returns></returns>
        private string GetQueryRequestModelFileContent(string subSystemName)
        {
            string result = "using System;\r\n";
            result += "using System.ComponentModel.DataAnnotations;\r\n";
            result += "using Materal.Common;\r\n";
            result += $"namespace {subSystemName}.PresentationModel.{Name}.Request\r\n";
            result += "{\r\n";
            result += "    /// <summary>\r\n";
            result += $"    /// {DomainName}查询请求模型\r\n";
            result += "    /// </summary>\r\n";
            result += $"    public class Query{Name}FilterRequestModel : PageRequestModel\r\n";
            result += "    {\r\n";
            result += "    }\r\n";
            result += "}";
            return result;
        }
    }
}
