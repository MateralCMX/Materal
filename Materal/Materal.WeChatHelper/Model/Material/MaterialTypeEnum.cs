using System.ComponentModel;

namespace Materal.WeChatHelper.Model.Material
{
    /// <summary>
    /// 素材类型枚举
    /// </summary>
    public enum MaterialTypeEnum : byte
    {
        /// <summary>
        /// 图片
        /// </summary
        [Description("图片")]
        Image = 0,
        /// <summary>
        /// 视频
        /// </summary>
        [Description("视频")]
        Video = 1,
        /// <summary>
        /// 语音
        /// </summary>
        [Description("语音")]
        Voice = 2,
        /// <summary>
        /// 图文
        /// </summary>
        [Description("图文")]
        News = 3
    }
}
