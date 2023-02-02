using Materal.BaseCore.CodeGenerator.Extensions;

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
        protected DomainSolutionModel()
        {
        }
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
            AllPlugExecuteBefore();
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
                DomainPlugModel model = new()
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
                };
                try
                {
                    PlugExecuteBefore(projectPath, className);
                    PlugExecute(model, projectPath, className);
                    PlugExcuteAfter(projectPath, className);
                }
                catch
                {
                    AllPlugExcuteAfter();
                    throw;
                }
            }
            AllPlugExcuteAfter();
        }
        /// <summary>
        /// 所有插件执行之前
        /// </summary>
        protected abstract void AllPlugExecuteBefore();
        /// <summary>
        /// 插件执行之前
        /// </summary>
        /// <param name="projectPath"></param>
        /// <param name="className"></param>
        protected abstract void PlugExecuteBefore(string projectPath, string className);
        /// <summary>
        /// 插件执行
        /// </summary>
        /// <param name="model"></param>
        /// <param name="projectPath"></param>
        /// <param name="className"></param>
        protected abstract void PlugExecute(DomainPlugModel model, string projectPath, string className);
        /// <summary>
        /// 插件执行完毕
        /// </summary>
        /// <param name="projectPath"></param>
        /// <param name="className"></param>
        protected abstract void PlugExcuteAfter(string projectPath, string className);
        /// <summary>
        /// 插件执行完毕
        /// </summary>
        protected abstract void AllPlugExcuteAfter();
    }
}
