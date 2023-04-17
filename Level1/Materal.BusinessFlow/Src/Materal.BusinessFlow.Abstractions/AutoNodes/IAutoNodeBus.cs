namespace Materal.BusinessFlow.Abstractions.AutoNodes
{
    /// <summary>
    /// 自动节点总线
    /// </summary>
    public interface IAutoNodeBus
    {
        /// <summary>
        /// 执行自动节点
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowRecordID"></param>
        /// <returns></returns>
        void ExcuteAutoNode(Guid flowTemplateID, Guid flowRecordID);
    }
}
