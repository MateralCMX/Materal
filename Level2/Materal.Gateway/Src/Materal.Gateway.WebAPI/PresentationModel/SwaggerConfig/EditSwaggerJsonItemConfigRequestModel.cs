﻿namespace Materal.Gateway.WebAPI.PresentationModel.SwaggerConfig
{
    /// <summary>
    /// 修改Swagger项配置请求模型
    /// </summary>
    public class EditSwaggerJsonItemConfigRequestModel : AddSwaggerJsonItemConfigRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
    }
}
