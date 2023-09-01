using Materal.BaseCore.DataTransmitModel;

namespace MBC.Demo.DataTransmitModel.MyTree
{
    /// <summary>
    /// 我的树树列表数据传输模型
    /// </summary>
    public partial class MyTreeTreeListDTO: MyTreeListDTO, ITreeDTO<MyTreeTreeListDTO>
    {
        /// <summary>
        /// 子级
        /// </summary>
        public List<MyTreeTreeListDTO> Children { get; set; } = new();
    }
}
