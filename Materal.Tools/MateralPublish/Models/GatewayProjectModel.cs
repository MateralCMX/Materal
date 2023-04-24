namespace MateralPublish.Models
{
    public class GatewayProjectModel : BaseProjectModel
    {
        public GatewayProjectModel(string directoryPath) : base(directoryPath)
        {
        }
        protected override bool IsPublishProject(string name)
        {
            string[] whiteList = new[]
            {
                "Materal.Gateway",
                "Materal.Gateway.Common",
                "Materal.Gateway.OcelotExtension"
            };
            return whiteList.Contains(name);
        }
    }
}
