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
            if (Domains.Count <= 0) return;
            DomainProject?.CreateDomainFiles(Domains);
            EFRepositoryProject?.CreateEFRepositoryFiles(Domains);
            DataTransmitModelProject?.CreateDataTransmitModelFiles(Domains);
            ServicesProject?.CreateServicesFiles(Domains);
            ServiceImplProject?.CreateServiceImplFiles(Domains);
            PresentationModelProject?.CreatePresentationModelFiles(Domains);
            CommonProject?.ClearMCGFiles();
            WebAPIProject?.CreateWebAPIFiles(Domains);
            if (Enums != null && Enums.Count > 0)
            {
                WebAPIProject?.CreateEnumsControllers(Enums);
            }
            RuningPlug();
        }
        /// <summary>
        /// 运行插件
        /// </summary>
        private void RuningPlug()
        {
            AllPlugExecuteBefore();
            foreach (DomainModel domain in Domains)
            {
                AttributeModel attributeModel = domain.GetAttribute<CodeGeneratorPlugAttribute>();
                if (attributeModel == null) continue;
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
                    PlugExecuteBefore(model, attributeModel);
                    PlugExecute(model, attributeModel);
                    PlugExcuteAfter(model, attributeModel);
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
        /// <param name="domainPlugModel"></param>
        /// <param name="attributeModel"></param>
        protected abstract void PlugExecuteBefore(DomainPlugModel domainPlugModel, AttributeModel attributeModel);
        /// <summary>
        /// 插件执行
        /// </summary>
        /// <param name="domainPlugModel"></param>
        /// <param name="attributeModel"></param>
        protected abstract void PlugExecute(DomainPlugModel domainPlugModel, AttributeModel attributeModel);
        /// <summary>
        /// 插件执行完毕
        /// </summary>
        /// <param name="domainPlugModel"></param>
        /// <param name="attributeModel"></param>
        protected abstract void PlugExcuteAfter(DomainPlugModel domainPlugModel, AttributeModel attributeModel);
        /// <summary>
        /// 插件执行完毕
        /// </summary>
        protected abstract void AllPlugExcuteAfter();
    }
}
