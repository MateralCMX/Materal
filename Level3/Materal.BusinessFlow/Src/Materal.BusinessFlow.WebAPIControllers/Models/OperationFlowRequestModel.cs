namespace Materal.BusinessFlow.WebAPIControllers.Models
{
    public class OperationFlowRequestModel : SaveFlowDataRequestModel
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public Guid UserID { get; set; }
    }
}
