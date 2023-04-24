namespace MateralPublish.Models
{
    public class LoggerProjectModel : BaseProjectModel
    {
        public LoggerProjectModel(string directoryPath) : base(directoryPath)
        {
        }
        protected override bool IsPublishProject(string name)
        {
            string[] whiteList = new[]
            {
                "Materal.Logger",
                "Materal.Logger.Message",
                "Materal.LoggerClient"
            };
            return whiteList.Contains(name);
        }
    }
}
