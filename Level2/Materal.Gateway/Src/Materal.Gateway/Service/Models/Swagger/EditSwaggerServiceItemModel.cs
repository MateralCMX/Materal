namespace Materal.Gateway.WebAPI.PresentationModel.Swagger
{
    /// <summary>
    /// 修改Swagger项配置模型
    /// </summary>
    public class EditSwaggerServiceItemModel : AddSwaggerServiceItemModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
    }
}
