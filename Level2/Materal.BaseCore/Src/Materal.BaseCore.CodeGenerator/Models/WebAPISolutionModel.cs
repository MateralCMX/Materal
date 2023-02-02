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
            if (_controllers.Count <= 0) return;
            HttpClientProject?.CreateHttpClientFiles(_controllers);
        }
    }
}