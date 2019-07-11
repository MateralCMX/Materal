using Materal.Model;
using System.Diagnostics.CodeAnalysis;

namespace Materal.WeChatHelper.Model.Material.Request
{
    /// <summary>
    /// 获取素材列表请求模型
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class GetMaterialListRequestModel : PageRequestModel
    {
        /// <summary>
        /// 素材类型
        /// </summary>
        public MaterialTypeEnum MaterialType { get; set; }
        /// <summary>
        /// 素材类型文本
        /// </summary>
        public string type => MaterialType.ToString().ToLower();
        /// <summary>
        /// 开始位置
        /// </summary>
        public int offset => Skip;
        /// <summary>
        /// 数量
        /// </summary>
        public int count => PageSize;
    }
}
