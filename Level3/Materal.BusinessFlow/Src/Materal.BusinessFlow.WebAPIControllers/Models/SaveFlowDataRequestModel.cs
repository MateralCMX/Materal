namespace Materal.BusinessFlow.WebAPIControllers.Models
{
    public class SaveFlowDataRequestModel
    {
        /// <summary>
        /// 流程模版唯一标识
        /// </summary>
        public Guid FlowTemplateID { get; set; }
        /// <summary>
        /// 流程记录唯一标识
        /// </summary>
        public Guid FlowRecordID { get; set; }
        /// <summary>
        /// Json数据
        /// </summary>
        public string JsonData { get; set; } = "[]";
    }
}
