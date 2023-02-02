namespace Materal.BaseCore.CodeGenerator.Models
{
    public abstract class WebAPISolutionModel
    {
        protected ProjectModel? HttpClientProject;
        protected ProjectModel? WebAPIProject;
        protected readonly List<ControllerModel> _controllers = new();

        public WebAPISolutionModel()
        {
        }

        /// <summary>
        /// 创建代码文件
        /// </summary>
        public void CreateCodeFiles()
        {
            HttpClientProject?.CreateHttpClientFiles(_controllers);
        }
    }
}