namespace Materal.BaseCore.CodeGenerator.Models
{
    public abstract class ServicesSolutionModel : ISolutionModel
    {
        protected ProjectModel? ServicesProject;
        protected ProjectModel? WebAPIProject;
        protected readonly List<ServiceModel> Services = new();
        public string CreateCodeFiles()
        {
            if (Services.Count <= 0) throw new CodeGeneratorException("未找到任何Controler");
            WebAPIProject?.CreateWebAPIControllerFileByServices(Services);
            return "已生成代码";
        }
    }
}
