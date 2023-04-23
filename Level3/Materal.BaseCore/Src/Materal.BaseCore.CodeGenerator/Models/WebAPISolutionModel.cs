namespace Materal.BaseCore.CodeGenerator.Models
{
    public abstract class WebAPISolutionModel : ISolutionModel
    {
        protected ProjectModel? HttpClientProject;
        protected ProjectModel? WebAPIProject;
        protected readonly List<ControllerModel> Controllers = new();

        public WebAPISolutionModel()
        {
        }

        /// <summary>
        /// 创建代码文件
        /// </summary>
        public string CreateCodeFiles()
        {
            if (Controllers.Count <= 0) throw new CodeGeneratorException("未找到任何Controler");
            HttpClientProject?.CreateHttpClientFiles(Controllers);
            return "已生成HttpClient文件";
        }
    }
}