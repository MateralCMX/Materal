namespace Materal.BaseCore.WebAPI.Common.Models
{
    public class SwaggerConfigModel
    {
        /// <summary>
        /// 启动
        /// </summary>
        public bool Enable { get; set; } = true;
        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; } = $"{WebAPIConfig.AppName}.WebAPI";
        /// <summary>
        /// 版本
        /// </summary>
        public string? Version { get; set; } = "v1";
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; } = "提供WebAPI接口";
        /// <summary>
        /// 作者
        /// </summary>
        public string? Author { get; set; } = "Materal";
        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; } = "cloomcmx1554@hotmail.com";
    }
}