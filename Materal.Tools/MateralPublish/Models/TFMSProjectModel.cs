namespace MateralPublish.Models
{
    public class TFMSProjectModel : BaseProjectModel
    {
        public TFMSProjectModel(string directoryPath) : base(directoryPath)
        {
        }
        protected override bool IsPublishProject(string name)
        {
            string[] whiteList = new[]
            {
                "Materal.TFMS.EventBus",
                "Materal.TFMS.EventBus.RabbitMQ"
            };
            return whiteList.Contains(name);
        }
    }
}
