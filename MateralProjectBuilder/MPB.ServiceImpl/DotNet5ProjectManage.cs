using MPB.Common;
using MPB.Domain;
using MPB.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPB.ServiceImpl
{
    public class DotNet5ProjectManage : IProjectManage
    {
        private ProjectConfigModel _projectConfig;
        public async Task<bool> CreateProjectAsync(string modelPath, ProjectConfigModel projectConfig, string outputPath)
        {
            _projectConfig = projectConfig;
            DirectoryInfo modelDirectoryInfo = InitModelDirectory(modelPath);
            List<DomainModel> domainModels = await GetDomainModelsAsync(modelDirectoryInfo);
            domainModels = domainModels.OrderBy(m => m.Namespace).ToList();
            await HandlerOutPutAsync(domainModels, outputPath);
            return true;
        }
        #region 私有方法
        /// <summary>
        /// 处理输出
        /// </summary>
        /// <param name="domainModels"></param>
        /// <param name="outputPath"></param>
        /// <returns></returns>
        private async Task HandlerOutPutAsync(List<DomainModel> domainModels, string outputPath)
        {
            if (domainModels.Count <= 0) return;
            DomainModel domainModel = domainModels[0];
            DirectoryInfo outputDirectoryInfo = InitOutputDirectory(outputPath, domainModel);
            DirectoryInfo commonDirectoryInfo = GetDirectoryInfo(outputDirectoryInfo, $"{domainModel.ProjectName}.Common");
            DirectoryInfo dataTransmitModelDirectoryInfo = GetDirectoryInfo(outputDirectoryInfo, $"{domainModel.ProjectName}.DataTransmitModel");
            DirectoryInfo dependencyInjectionDirectoryInfo = GetDirectoryInfo(outputDirectoryInfo, $"{domainModel.ProjectName}.DependencyInjection");
            DirectoryInfo domainDirectoryInfo = GetDirectoryInfo(outputDirectoryInfo, $"{domainModel.ProjectName}.Domain");
            DirectoryInfo enumsDirectoryInfo = GetDirectoryInfo(outputDirectoryInfo, $"{domainModel.ProjectName}.Enums");
            DirectoryInfo presentationModelDirectoryInfo = GetDirectoryInfo(outputDirectoryInfo, $"{domainModel.ProjectName}.PresentationModel");
            DirectoryInfo serviceDirectoryInfo = GetDirectoryInfo(outputDirectoryInfo, $"{domainModel.ProjectName}.Service");
            DirectoryInfo serviceImplDirectoryInfo = GetDirectoryInfo(outputDirectoryInfo, $"{domainModel.ProjectName}.ServiceImpl");
            DirectoryInfo sqlServerEFDirectoryInfo = GetDirectoryInfo(outputDirectoryInfo, $"{domainModel.ProjectName}.SqlServerEFRepository");
            DirectoryInfo webAPIDirectoryInfo = GetDirectoryInfo(outputDirectoryInfo, $"{domainModel.ProjectName}.WebAPI");
            #region Sln
            await CreateTemplateFileAsync(domainModel, "Sln.txt", outputDirectoryInfo, $"{domainModel.ProjectName}.sln");
            #endregion
            #region Common
            {
                await CreateTemplateFileAsync(domainModel, "NLog.txt", commonDirectoryInfo, $"NLog.config");
                await CreateTemplateFileAsync(domainModel, "CommonCsproj.txt", commonDirectoryInfo, $"{commonDirectoryInfo.Name}.csproj");
                await CreateTemplateFileAsync(domainModel, "Exception.txt", commonDirectoryInfo, $"{domainModel.ProjectName}Exception.cs");
                await CreateTemplateFileAsync(domainModel, "BaseDomain.txt", commonDirectoryInfo, $"BaseDomain.cs");
                await CreateTemplateFileAsync(domainModel, "ApplicationConfig.txt", commonDirectoryInfo, $"ApplicationConfig.cs");
                await CreateTemplateFileAsync(domainModel, "BaseModelConfig.txt", commonDirectoryInfo, $"BaseModelConfig.cs");
                DirectoryInfo configModelsDirectoryInfo = GetDirectoryInfo(commonDirectoryInfo, "ConfigModels");
                await CreateTemplateFileAsync(domainModel, "BaseConfigModel.txt", configModelsDirectoryInfo, $"BaseConfigModel.cs");
                await CreateTemplateFileAsync(domainModel, "NLogConfigModel.txt", configModelsDirectoryInfo, $"NLogConfigModel.cs");
                await CreateTemplateFileAsync(domainModel, "SQLServerConfigModel.txt", configModelsDirectoryInfo, $"SQLServerConfigModel.cs");
            }
            #endregion
            #region DataTransmitModel
            {
                await CreateTemplateFileAsync(domainModel, "DataTransmitModelCsproj.txt", dataTransmitModelDirectoryInfo, $"{dataTransmitModelDirectoryInfo.Name}.csproj");
            }
            #endregion
            #region DependencyInjection
            {
                await CreateTemplateFileAsync(domainModel, "DependencyInjectionCsproj.txt", dependencyInjectionDirectoryInfo, $"{dependencyInjectionDirectoryInfo.Name}.csproj");
                await CreateTemplateFileAsync(domainModel, "AutoMapperDIExtension.txt", dependencyInjectionDirectoryInfo, $"AutoMapperDIExtension.cs");
                await CreateTemplateFileAsync(domainModel, "DIExtension.txt", dependencyInjectionDirectoryInfo, $"{domainModel.ProjectName}DIExtension.cs");
            }
            #endregion
            #region Domain
            {
                await CreateTemplateFileAsync(domainModel, "DomainCsproj.txt", domainDirectoryInfo, $"{domainDirectoryInfo.Name}.csproj");
            }
            #endregion
            #region Enums
            {
                await CreateTemplateFileAsync(domainModel, "EnumsCsproj.txt", enumsDirectoryInfo, $"{enumsDirectoryInfo.Name}.csproj");
            }
            #endregion
            #region PresentationModel
            {
                await CreateTemplateFileAsync(domainModel, "PresentationModelCsproj.txt", presentationModelDirectoryInfo, $"{presentationModelDirectoryInfo.Name}.csproj");
            }
            #endregion
            #region Service
            {
                await CreateTemplateFileAsync(domainModel, "ServiceCsproj.txt", serviceDirectoryInfo, $"{serviceDirectoryInfo.Name}.csproj");
            }
            #endregion
            #region ServiceImpl
            {
                await CreateTemplateFileAsync(domainModel, "ServiceServiceImplCsproj.txt", serviceImplDirectoryInfo, $"{serviceImplDirectoryInfo.Name}.csproj");
            }
            #endregion
            #region SqlServerEFRepository
            {
                await CreateTemplateFileAsync(domainModel, "SqlServerEFRepositoryCsproj.txt", sqlServerEFDirectoryInfo, $"{sqlServerEFDirectoryInfo.Name}.csproj");
                GetDirectoryInfo(sqlServerEFDirectoryInfo, "Migrations");
                #region 创建DbContext
                List<string> codes = new();
                foreach (string item in domainModels.Select(m => m.Namespace).Distinct())
                {
                    codes.Add($"using {item};");
                }
                codes.Add("using Microsoft.EntityFrameworkCore;");
                codes.Add("using System.Reflection;");
                codes.Add("");
                codes.Add($"namespace {domainModel.ProjectName}.SqlServerEFRepository");
                codes.Add("{");
                codes.Add("    /// <summary>");
                codes.Add($"    /// {domainModel.ProjectName}数据库上下文");
                codes.Add("    /// </summary>");
                codes.Add($"    public sealed class {domainModel.ProjectName}DbContext : DbContext");
                codes.Add("    {");
                codes.Add($"        public {domainModel.ProjectName}DbContext(DbContextOptions<{domainModel.ProjectName}DbContext> options) : base(options)");
                codes.Add("        {");
                codes.Add("        }");
                string nowNamespace = string.Empty;
                foreach (var item in domainModels)
                {
                    if (nowNamespace != item.Namespace)
                    {
                        if (!string.IsNullOrWhiteSpace(nowNamespace))
                        {
                            codes.Add("        #endregion");
                        }
                        nowNamespace = item.Namespace;
                        codes.Add($"        #region {item.Namespaces.Last()}");
                    }
                    codes.Add("        /// <summary>");
                    codes.Add($"        /// {item.Annotation}");
                    codes.Add("        /// </summary>");
                    codes.Add($"        public DbSet<{item.Name}> {item.Name}" + "{ get; set; }");
                }
                codes.Add("        #endregion");
                codes.Add("        protected override void OnModelCreating(ModelBuilder modelBuilder)");
                codes.Add("        {");
                codes.Add("            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());");
                codes.Add("        }");
                codes.Add("    }");
                codes.Add("}");
                string templateContent = string.Join("\r\n", codes);
                await CreateContentFileAsync(templateContent, sqlServerEFDirectoryInfo, $"{domainModel.ProjectName}DbContext.cs");
                await CreateTemplateFileAsync(domainModel, "DbContextFactory.txt", sqlServerEFDirectoryInfo, $"{domainModel.ProjectName}DbContextFactory.cs");
                #endregion
                #region 创建工作单元
                await CreateTemplateFileAsync(domainModel, "IUnitOfWork.txt", sqlServerEFDirectoryInfo, $"I{domainModel.ProjectName}UnitOfWork.cs");
                await CreateTemplateFileAsync(domainModel, "UnitOfWorkImpl.txt", sqlServerEFDirectoryInfo, $"{domainModel.ProjectName}UnitOfWorkImpl.cs");
                #endregion
                #region 创建BaseRepositoryImpl
                await CreateTemplateFileAsync(domainModel, "BaseRepositoryImpl.txt", sqlServerEFDirectoryInfo, $"{domainModel.ProjectName}EFRepositoryImpl.cs");
                #endregion
            }
            #endregion
            #region WebAPI
            {
                if (_projectConfig.EnableWebAPI)
                {
                    await CreateTemplateFileAsync(domainModel, "WebAPIDIExtension.txt", webAPIDirectoryInfo, $"{domainModel.ProjectName}WebAPIDIExtension.cs");
                    DirectoryInfo controllersDirectoryInfo = GetDirectoryInfo(webAPIDirectoryInfo, "Controllers");
                    await CreateTemplateFileAsync(domainModel, "HealthController.txt", controllersDirectoryInfo, "HealthController.cs");
                    await CreateTemplateFileAsync(domainModel, "WebAPIControllerBase.txt", controllersDirectoryInfo, "WebAPIControllerBase.cs");
                }
            }
            #endregion
            foreach (DomainModel item in domainModels)
            {
                await CreateDataTransmitModelFile(item, dataTransmitModelDirectoryInfo);
                await CreateDomainFile(item, domainDirectoryInfo);
                await CreatePresentationModelFile(item, presentationModelDirectoryInfo);
                await CreateServiceFile(item, serviceDirectoryInfo);
                await CreateServiceImplFile(item, serviceImplDirectoryInfo);
                await CreateSqlServerEFRepositoryFile(item, sqlServerEFDirectoryInfo);
                if (_projectConfig.EnableWebAPI)
                {
                    await CreateWebAPIFile(item, webAPIDirectoryInfo);
                }
            }
        }
        /// <summary>
        /// 创建DataTransmitModel文件
        /// </summary>
        /// <param name="domainModel"></param>
        /// <param name="directoryInfo"></param>
        private async Task CreateDataTransmitModelFile(DomainModel domainModel, DirectoryInfo directoryInfo)
        {
            List<string> paths = domainModel.RelativeNamespace.Split(".").ToList();
            paths.Add(domainModel.Name);
            DirectoryInfo dtoDirectoryInfo = GetDirectoryInfo(directoryInfo, paths.ToArray());
            #region ListDTO
            List<string> codes = new();
            codes.Add("using System;");
            codes.Add($"using {domainModel.ProjectName}.Enums;");
            codes.Add("");
            codes.Add($"namespace {domainModel.ProjectName}.DataTransmitModel.{domainModel.RelativeNamespace}.{domainModel.Name}");
            codes.Add("{");
            codes.Add("    /// <summary>");
            codes.Add($"    /// {domainModel.Annotation}列表数据传输模型");
            codes.Add("    /// </summary>");
            codes.Add($"    public class {domainModel.Name}ListDTO");
            codes.Add("    {");
            foreach (PropertyModel property in domainModel.Properties)
            {
                codes.Add("        /// <summary>");
                codes.Add($"        /// {property.Annotation}");
                codes.Add("        /// </summary>");
                codes.Add($"        public {property.Type} {property.Name} "+ "{ get; set; }");
            }
            codes.Add("    }");
            codes.Add("}");
            string templateContent = string.Join("\r\n", codes);
            await CreateContentFileAsync(templateContent, dtoDirectoryInfo, $"{domainModel.Name}ListDTO.cs");
            #endregion
            #region DTO
            await CreateTemplateFileAsync(domainModel, "DTO.txt", dtoDirectoryInfo, $"{domainModel.Name}DTO.cs");
            #endregion
        }
        /// <summary>
        /// 创建Domain文件
        /// </summary>
        /// <param name="domainModel"></param>
        /// <param name="directoryInfo"></param>
        /// <returns></returns>
        private async Task CreateDomainFile(DomainModel domainModel, DirectoryInfo directoryInfo)
        {
            #region 移动Domain文件
            DirectoryInfo domainDirectoryInfo = GetDirectoryInfo(directoryInfo, domainModel.RelativeNamespace);
            var filePath = Path.Combine(domainDirectoryInfo.FullName, $"{domainModel.Name}.cs");
            domainModel.FileInfo.CopyTo(filePath);
            #endregion
            #region 创建IRepository
            DirectoryInfo repositoriesDirectoryInfo = GetDirectoryInfo(domainDirectoryInfo, "Repositories");
            await CreateTemplateFileAsync(domainModel, "IRepository.txt", repositoriesDirectoryInfo, $"I{domainModel.Name}Repository.cs");
            #endregion
        }
        /// <summary>
        /// 创建PresentationModel文件
        /// </summary>
        /// <param name="domainModel"></param>
        /// <param name="directoryInfo"></param>
        private async Task CreatePresentationModelFile(DomainModel domainModel, DirectoryInfo directoryInfo)
        {
            List<string> paths = domainModel.RelativeNamespace.Split(".").ToList();
            paths.Add(domainModel.Name);
            DirectoryInfo serviceModelsDirectoryInfo = GetDirectoryInfo(directoryInfo, paths.ToArray());
            #region Add
            {
                List<string> codes = new();
                codes.Add("using System;");
                codes.Add($"using {domainModel.ProjectName}.Enums;");
                codes.Add("");
                codes.Add($"namespace {domainModel.ProjectName}.PresentationModel.{domainModel.RelativeNamespace}.{domainModel.Name}");
                codes.Add("{");
                codes.Add("    /// <summary>");
                codes.Add($"    /// 添加{domainModel.Annotation}请求模型");
                codes.Add("    /// </summary>");
                codes.Add($"    public class Add{domainModel.Name}RequestModel");
                codes.Add("    {");
                foreach (PropertyModel property in domainModel.Properties)
                {
                    codes.Add("        /// <summary>");
                    codes.Add($"        /// {property.Annotation}");
                    codes.Add("        /// </summary>");
                    codes.Add($"        public {property.Type} {property.Name} " + "{ get; set; }");
                }
                codes.Add("    }");
                codes.Add("}");
                string templateContent = string.Join("\r\n", codes);
                await CreateContentFileAsync(templateContent, serviceModelsDirectoryInfo, $"Add{domainModel.Name}RequestModel.cs");
            }
            #endregion
            #region Edit
            await CreateTemplateFileAsync(domainModel, "EditRequestModel.txt", serviceModelsDirectoryInfo, $"Edit{domainModel.Name}RequestModel.cs");
            #endregion
            #region Query
            {
                List<string> codes = new();
                codes.Add("using Materal.Model;");
                codes.Add("using System;");
                codes.Add($"using {domainModel.ProjectName}.Enums;");
                codes.Add("");
                codes.Add($"namespace {domainModel.ProjectName}.PresentationModel.{domainModel.RelativeNamespace}.{domainModel.Name}");
                codes.Add("{");
                codes.Add("    /// <summary>");
                codes.Add($"    /// 查询{domainModel.Annotation}请求模型");
                codes.Add("    /// </summary>");
                codes.Add($"    public class Query{domainModel.Name}FilterRequestModel : PageRequestModel");
                codes.Add("    {");
                foreach (PropertyModel property in domainModel.Properties)
                {
                    codes.Add("        /// <summary>");
                    codes.Add($"        /// {property.Annotation}");
                    codes.Add("        /// </summary>");
                    codes.Add($"        public {property.Type} {property.Name} " + "{ get; set; }");
                }
                codes.Add("    }");
                codes.Add("}");
                string templateContent = string.Join("\r\n", codes);
                await CreateContentFileAsync(templateContent, serviceModelsDirectoryInfo, $"Query{domainModel.Name}FilterRequestModel.cs");
            }
            #endregion
        }
        /// <summary>
        /// 创建Service文件
        /// </summary>
        /// <param name="domainModel"></param>
        /// <param name="directoryInfo"></param>
        private async Task CreateServiceFile(DomainModel domainModel, DirectoryInfo directoryInfo)
        {
            string[] paths = domainModel.RelativeNamespace.Split(".");
            #region 创建IService
            DirectoryInfo serviceDirectoryInfo = GetDirectoryInfo(directoryInfo, paths);
            await CreateTemplateFileAsync(domainModel, "IService.txt", serviceDirectoryInfo, $"I{domainModel.Name}Service.cs");
            #endregion
            #region 创建Models
            DirectoryInfo serviceModelsDirectoryInfo = GetDirectoryInfo(serviceDirectoryInfo, "Models", domainModel.Name);
            #region Add
            {
                List<string> codes = new();
                codes.Add("using System;");
                codes.Add($"using {domainModel.ProjectName}.Enums;");
                codes.Add("");
                codes.Add($"namespace {domainModel.ProjectName}.Service.{domainModel.RelativeNamespace}.Models.{domainModel.Name}");
                codes.Add("{");
                codes.Add("    /// <summary>");
                codes.Add($"    /// 添加{domainModel.Annotation}模型");
                codes.Add("    /// </summary>");
                codes.Add($"    public class Add{domainModel.Name}Model");
                codes.Add("    {");
                foreach (PropertyModel property in domainModel.Properties)
                {
                    codes.Add("        /// <summary>");
                    codes.Add($"        /// {property.Annotation}");
                    codes.Add("        /// </summary>");
                    codes.Add($"        public {property.Type} {property.Name} " + "{ get; set; }");
                }
                codes.Add("    }");
                codes.Add("}");
                string templateContent = string.Join("\r\n", codes);
                await CreateContentFileAsync(templateContent, serviceModelsDirectoryInfo, $"Add{domainModel.Name}Model.cs");
            }
            #endregion
            #region Edit
            await CreateTemplateFileAsync(domainModel, "EditModel.txt", serviceModelsDirectoryInfo, $"Edit{domainModel.Name}Model.cs");
            #endregion
            #region Query
            {
                List<string> codes = new();
                codes.Add("using Materal.Model;");
                codes.Add("using System;");
                codes.Add($"using {domainModel.ProjectName}.Enums;");
                codes.Add("");
                codes.Add($"namespace {domainModel.ProjectName}.Service.{domainModel.RelativeNamespace}.Models.{domainModel.Name}");
                codes.Add("{");
                codes.Add("    /// <summary>");
                codes.Add($"    /// 查询{domainModel.Annotation}模型");
                codes.Add("    /// </summary>");
                codes.Add($"    public class Query{domainModel.Name}FilterModel : PageRequestModel");
                codes.Add("    {");
                foreach (PropertyModel property in domainModel.Properties)
                {
                    codes.Add("        /// <summary>");
                    codes.Add($"        /// {property.Annotation}");
                    codes.Add("        /// </summary>");
                    codes.Add("        [Equal]");
                    codes.Add($"        public {property.Type} {property.Name} " + "{ get; set; }");
                }
                codes.Add("    }");
                codes.Add("}");
                string templateContent = string.Join("\r\n", codes);
                await CreateContentFileAsync(templateContent, serviceModelsDirectoryInfo, $"Query{domainModel.Name}FilterModel.cs");
            }
            #endregion
            #endregion
        }
        /// <summary>
        /// 创建ServiceImpl文件
        /// </summary>
        /// <param name="domainModel"></param>
        /// <param name="directoryInfo"></param>
        private async Task CreateServiceImplFile(DomainModel domainModel, DirectoryInfo directoryInfo)
        {
            string[] paths = domainModel.RelativeNamespace.Split(".");
            DirectoryInfo serviceImplDirectoryInfo = GetDirectoryInfo(directoryInfo, paths);
            await CreateTemplateFileAsync(domainModel, "ServiceImpl.txt", serviceImplDirectoryInfo, $"{domainModel.Name}ServiceImpl.cs");
            DirectoryInfo autoMapperProfileDirectoryInfo = GetDirectoryInfo(serviceImplDirectoryInfo, "AutoMapperProfile");
            await CreateTemplateFileAsync(domainModel, "ServiceImplAutoMapperProfile.txt", autoMapperProfileDirectoryInfo, $"{domainModel.Name}Profile.cs");
        }
        /// <summary>
        /// 创建SqlServerEFRepository文件
        /// </summary>
        /// <param name="domainModel"></param>
        /// <param name="directoryInfo"></param>
        private async Task CreateSqlServerEFRepositoryFile(DomainModel domainModel, DirectoryInfo directoryInfo)
        {
            #region ModelConfig
            DirectoryInfo modelConfigDirectoryInfo = GetDirectoryInfo(directoryInfo, "ModelConfig");
            modelConfigDirectoryInfo = GetDirectoryInfo(modelConfigDirectoryInfo, domainModel.RelativeNamespace);
            await CreateTemplateFileAsync(domainModel, "DomainConfig.txt", modelConfigDirectoryInfo, $"{domainModel.Name}Config.cs");
            #endregion
            #region RepositoryImpl
            DirectoryInfo repositoryImplDirectoryInfo = GetDirectoryInfo(directoryInfo, "RepositoryImpl");
            repositoryImplDirectoryInfo = GetDirectoryInfo(repositoryImplDirectoryInfo, domainModel.RelativeNamespace);
            await CreateTemplateFileAsync(domainModel, "RepositoryImpl.txt", repositoryImplDirectoryInfo, $"{domainModel.Name}RepositoryImpl.cs");
            #endregion
        }
        /// <summary>
        /// 创建WebAPI文件
        /// </summary>
        /// <param name="domainModel"></param>
        /// <param name="directoryInfo"></param>
        private async Task CreateWebAPIFile(DomainModel domainModel, DirectoryInfo directoryInfo)
        {
            #region Controllers
            DirectoryInfo controllersDirectoryInfo = GetDirectoryInfo(directoryInfo, "Controllers", domainModel.RelativeNamespace);
            await CreateTemplateFileAsync(domainModel, "Controller.txt", controllersDirectoryInfo, $"{domainModel.Name}Controller.cs");
            #endregion
            DirectoryInfo autoMapperProfileDirectoryInfo = GetDirectoryInfo(directoryInfo, "AutoMapperProfile", domainModel.RelativeNamespace);
            await CreateTemplateFileAsync(domainModel, "WebAPIAutoMapperProfile.txt", autoMapperProfileDirectoryInfo, $"{domainModel.Name}Profile.cs");
        }
        /// <summary>
        /// 获得领域模型
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <returns></returns>
        private async Task<List<DomainModel>> GetDomainModelsAsync(DirectoryInfo directoryInfo)
        {
            List<DomainModel> domainModels = new();
            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                if (!fileInfo.Extension.Equals(".cs", StringComparison.OrdinalIgnoreCase)) continue;
                var domainModel = new DomainModel(fileInfo);
                if (!await domainModel.InitAsync()) continue;
                domainModels.Add(domainModel);
            }
            foreach (DirectoryInfo subDirectoryInfo in directoryInfo.GetDirectories())
            {
                domainModels.AddRange(await GetDomainModelsAsync(subDirectoryInfo));
            }
            return domainModels;
        }
        /// <summary>
        /// 初始化模型文件夹
        /// </summary>
        /// <param name="modelPath"></param>
        /// <returns></returns>
        /// <exception cref="MPBException"></exception>
        private DirectoryInfo InitModelDirectory(string modelPath)
        {
            DirectoryInfo modelDirectoryInfo = new(modelPath);
            if (!modelDirectoryInfo.Exists) throw new MPBException("模型文件夹不存在");
            return modelDirectoryInfo;
        }
        /// <summary>
        /// 初始化输出文件夹
        /// </summary>
        /// <param name="modelPath"></param>
        /// <returns></returns>
        /// <exception cref="MPBException"></exception>
        private DirectoryInfo InitOutputDirectory(string outputPath, DomainModel domainModel)
        {
            outputPath = Path.Combine(outputPath, domainModel.ProjectName);
            DirectoryInfo outputDirectoryInfo = new(outputPath);
            if (!outputDirectoryInfo.Exists)
            {
                outputDirectoryInfo.Create();
            }
            else
            {
                outputDirectoryInfo.Delete(true);
                outputDirectoryInfo.Create();
            }
            return outputDirectoryInfo;
        }
        /// <summary>
        /// 写内容
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="conent"></param>
        /// <returns></returns>
        private async Task WriteContentAsync(FileStream fileStream, string conent)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(conent);
            await fileStream.WriteAsync(buffer);
            await fileStream.FlushAsync();
            fileStream.Close();
            await fileStream.DisposeAsync();
        }
        /// <summary>
        /// 获得模版内容
        /// </summary>
        /// <param name="domainModel"></param>
        /// <param name="templatePath"></param>
        /// <returns></returns>
        private async Task<string> GetTemplateContentAsync(DomainModel domainModel, string templatePath)
        {
            templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template", "DotNet5", templatePath);
            if (!File.Exists(templatePath)) throw new MPBException($"\"{templatePath}\"模版文件丢失");
            string templateContent = await File.ReadAllTextAsync(templatePath);
            templateContent = ReplaceTemplateText(templateContent, domainModel);
            return templateContent;
        }
        /// <summary>
        /// 获得文件夹信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private DirectoryInfo GetDirectoryInfo(DirectoryInfo directoryInfo, params string[] paths)
        {
            var path = directoryInfo.FullName;
            foreach (string item in paths)
            {
                path = Path.Combine(path, item);
            }
            return GetDirectoryInfo(path);
        }
        /// <summary>
        /// 获得文件夹信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private DirectoryInfo GetDirectoryInfo(string path)
        {
            DirectoryInfo result = new(path);
            if (!result.Exists)
            {
                result.Create();
            }
            return result;
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="domainModel"></param>
        /// <param name="templateName"></param>
        /// <param name="directoryInfo"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private async Task CreateTemplateFileAsync(DomainModel domainModel, string templateName, DirectoryInfo directoryInfo, string fileName)
        {
            string templateContent = await GetTemplateContentAsync(domainModel, templateName);
            await CreateContentFileAsync(templateContent, directoryInfo, fileName);
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="domainModel"></param>
        /// <param name="templateName"></param>
        /// <param name="directoryInfo"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private async Task CreateContentFileAsync(string content, DirectoryInfo directoryInfo, string fileName)
        {
            var filePath = Path.Combine(directoryInfo.FullName, fileName);
            FileInfo fileInfo = new(filePath);
            await CreateFileAsync(content, fileInfo);
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="content"></param>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        private async Task CreateFileAsync(string content, FileInfo fileInfo)
        {
            if (!fileInfo.Exists)
            {
                await using FileStream fileStream = fileInfo.Create();
                await WriteContentAsync(fileStream, content);
            }
        }
        /// <summary>
        /// 替换模版
        /// </summary>
        /// <param name="templateText"></param>
        /// <param name="domainModel"></param>
        /// <returns></returns>
        private string ReplaceTemplateText(string templateText, DomainModel domainModel)
        {
            templateText = templateText.Replace("${ProjectName}", domainModel.ProjectName, StringComparison.Ordinal);
            templateText = templateText.Replace("${projectName}", ToLowercaseLetter(domainModel.ProjectName), StringComparison.Ordinal);
            templateText = templateText.Replace("${Name}", domainModel.Name, StringComparison.Ordinal);
            templateText = templateText.Replace("${name}", ToLowercaseLetter(domainModel.Name), StringComparison.Ordinal);
            templateText = templateText.Replace("${Annotation}", domainModel.Annotation, StringComparison.Ordinal);
            templateText = templateText.Replace("${annotation}", ToLowercaseLetter(domainModel.Annotation), StringComparison.Ordinal);
            templateText = templateText.Replace("${Namespace}", domainModel.Namespace, StringComparison.Ordinal);
            templateText = templateText.Replace("${namespace}", ToLowercaseLetter(domainModel.Namespace), StringComparison.Ordinal);
            templateText = templateText.Replace("${RelativeNamespace}", domainModel.RelativeNamespace, StringComparison.Ordinal);
            templateText = templateText.Replace("${relativeNamespace}", ToLowercaseLetter(domainModel.RelativeNamespace), StringComparison.Ordinal);
            return templateText;
        }
        /// <summary>
        /// 转换为首字母小写
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        private string ToLowercaseLetter(string inputString)
        {
            string result = inputString.ToLower().Substring(0, 1);
            result += inputString.Substring(1, inputString.Length - 1);
            return result;
        }
        #endregion
    }
}
