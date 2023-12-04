namespace Materal.Gateway.WebAPI.PresentationModel.SwaggerConfig
{
    /// <summary>
    /// 添加Swagger配置请求模型
    /// </summary>
    public class EditSwaggerConfigRequestModel : AddSwaggerConfigRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
    }
}
