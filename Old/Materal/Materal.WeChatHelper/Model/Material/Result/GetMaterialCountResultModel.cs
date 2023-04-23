using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Materal.WeChatHelper.Model.Material.Result
{
    /// <summary>
    /// 获取素材数量返回模型
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class GetMaterialCountResultModel
    {
        /// <summary>
        /// 语音总数量
        /// </summary>
        [Description("语音总数量")]
        public int voice_count { get; set; }
        /// <summary>
        /// 视频总数量
        /// </summary>
        [Description("视频总数量")]
        public int video_count { get; set; }
        /// <summary>
        /// 图片总数量
        /// </summary>
        [Description("图片总数量")]
        public int image_count { get; set; }
        /// <summary>
        /// 图文总数量
        /// </summary>
        [Description("图文总数量")]
        public int news_count { get; set; }
    }
}
