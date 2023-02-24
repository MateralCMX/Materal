using System.Diagnostics.CodeAnalysis;

namespace Materal.WeChatHelper.Model.Material.Result
{
    /// <summary>
    /// 获取素材列表返回模型
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class GetMaterialListResultModel
    {
        public int media_id { get; set; }
        public string name { get; set; }
        public string update_time { get; set; }
        public string url { get; set; }
    }
}
