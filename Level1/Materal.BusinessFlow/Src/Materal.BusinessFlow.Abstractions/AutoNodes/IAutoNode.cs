namespace Materal.BusinessFlow.Abstractions.AutoNodes
{
    public interface IAutoNode
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowRecordID"></param>
        /// <returns></returns>
        Task ExcuteAsync(Guid flowTemplateID, Guid flowRecordID);
    }
}
