namespace BaseWebAPI.Model
{
    public class BaseConfigureServiceModel
    {
        /// <summary>
        /// App名称
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// Swagger帮助Xml路径组
        /// </summary>
        public string[] SwaggerHelperXmlPathArray { get; set; }
    }
}
