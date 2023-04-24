namespace MateralPublish.Models
{
    public class BusinessFlowProjectModel : BaseProjectModel
    {
        public BusinessFlowProjectModel(string directoryPath) : base(directoryPath)
        {
        }
        protected override bool IsPublishProject(string name)
        {
            string[] whiteList = new[]
            {
                "Materal.BusinessFlow",
                "Materal.BusinessFlow.Abstractions",
                "Materal.BusinessFlow.ADONETRepository",
                "Materal.BusinessFlow.SqliteRepository",
                "Materal.BusinessFlow.SqlServerRepository"
            };
            return whiteList.Contains(name);
        }
    }
}
