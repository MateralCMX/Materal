namespace MateralPublish.Models
{
    public class TTAProjectModel : BaseProjectModel
    {
        public TTAProjectModel(string directoryPath) : base(directoryPath)
        {
        }
        protected override bool IsPublishProject(string name)
        {
            string[] whiteList = new[]
            {
                "Materal.TTA.Common",
                "Materal.TTA.DependencyInjection",
                "Materal.TTA.EFRepository",
                "Materal.TTA.MongoDBRepository",
                "Materal.TTA.MySqlRepository",
                "Materal.TTA.RedisRepository",
                "Materal.TTA.SqliteRepository",
                "Materal.TTA.SqlServerRepository"
            };
            return whiteList.Contains(name);
        }
    }
}
