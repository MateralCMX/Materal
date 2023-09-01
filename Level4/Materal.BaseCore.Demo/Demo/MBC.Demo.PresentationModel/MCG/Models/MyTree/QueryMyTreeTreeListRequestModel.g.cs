#nullable enable

namespace MBC.Demo.PresentationModel.MyTree
{
    /// <summary>
    /// 我的树查询模型
    /// </summary>
    public partial class QueryMyTreeTreeListRequestModel
    {
        /// <summary>
        /// 父级唯一标识
        /// </summary>
        public Guid? ParentID { get; set; }
    }
}
