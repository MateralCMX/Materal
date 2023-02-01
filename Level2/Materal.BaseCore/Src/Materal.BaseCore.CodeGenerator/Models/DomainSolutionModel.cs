using Materal.BaseCore.CodeGenerator.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.Reflection;
using System.Text;

namespace Materal.BaseCore.CodeGenerator.Models
{
    public abstract class DomainSolutionModel
    {
        protected ProjectModel? CommonProject;
        protected ProjectModel? DomainProject;
        protected ProjectModel? WebAPIProject;
        protected ProjectModel? ServicesProject;
        protected ProjectModel? ServiceImplProject;
        protected ProjectModel? EFRepositoryProject;
        protected ProjectModel? DataTransmitModelProject;
        protected ProjectModel? PresentationModelProject;
        protected ProjectModel? EnumsProject;
        protected List<DomainModel> Domains = new();
        protected List<EnumModel> Enums = new();
        /// <summary>
        /// 创建代码文件
        /// </summary>
        public void CreateCodeFiles()
        {
            DomainProject?.CreateDomainFiles(Domains);
            EFRepositoryProject?.CreateEFRepositoryFiles(Domains);
            DataTransmitModelProject?.CreateDataTransmitModelFiles(Domains);
            ServicesProject?.CreateServicesFiles(Domains);
            ServiceImplProject?.CreateServiceImplFiles(Domains);
            PresentationModelProject?.CreatePresentationModelFiles(Domains);
            WebAPIProject?.CreateWebAPIFiles(Domains);
            if (Enums != null && Enums.Count > 0)
            {
                WebAPIProject?.CreateEnumsControllers(Enums);
            }
            GetPlugBefore();
            foreach (DomainModel domain in Domains)
            {
                AttributeModel attributeModel = domain.GetAttribute<CodeGeneratorPlugAttribute>();
                if (attributeModel == null) return;
                string className = attributeModel.AttributeArguments[1].Value.RemovePackag();
                if (!className.EndsWith(".cs"))
                {
                    className += ".cs";
                }
                string projectPath = attributeModel.AttributeArguments[0].Value.RemovePackag();
                projectPath = Path.Combine(DomainProject?.DiskDirectoryPath, projectPath);
                try
                {
                    IMateralBaseCoreCodeGeneratorPlug plug = GetPlug(projectPath, className);
                    plug?.CreateFileByDomain(new PlugCreateFileModel
                    {
                        Domain = domain,
                        WebAPIProject = WebAPIProject,
                        CommonProject = CommonProject,
                        DataTransmitModelProject = DataTransmitModelProject,
                        DomainProject = DomainProject,
                        Domains = Domains,
                        EFRepositoryProject = EFRepositoryProject,
                        Enums = Enums,
                        EnumsProject = EnumsProject,
                        PresentationModelProject = PresentationModelProject,
                        ServiceImplProject = ServiceImplProject,
                        ServicesProject = ServicesProject
                    });
                }
                catch
                {
                    PlugExcuted();
                    throw;
                }
            }
            PlugExcuted();
        }
        /// <summary>
        /// 获取插件之前
        /// </summary>
        protected abstract void GetPlugBefore();
        /// <summary>
        /// 获取插件
        /// </summary>
        /// <param name="projectPath"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        protected abstract IMateralBaseCoreCodeGeneratorPlug GetPlug(string projectPath, string className);
        /// <summary>
        /// 插件执行完毕
        /// </summary>
        protected abstract void PlugExcuted();
    }
}
