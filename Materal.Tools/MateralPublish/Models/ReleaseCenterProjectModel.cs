namespace MateralPublish.Models
{
    public class ReleaseCenterProjectModel : BaseProjectModel
    {
        public ReleaseCenterProjectModel(string directoryPath) : base(directoryPath)
        {
        }
        protected override bool IsPublishProject(string name)
        {
            string[] whiteList = new[]
            {
                "RC.Authority.WebAPI",
                "RC.ConfigClient",
                "RC.Deploy.WebAPI",
                "RC.EnvironmentServer.WebAPI",
                "RC.ServerCenter.WebAPI"
            };
            return whiteList.Contains(name);
        }
    }
}
