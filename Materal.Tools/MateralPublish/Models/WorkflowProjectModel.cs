namespace MateralPublish.Models
{
    public class WorkflowProjectModel : BaseProjectModel
    {
        public WorkflowProjectModel(string directoryPath) : base(directoryPath)
        {
        }
        protected override bool IsPublishProject(string name)
        {
            string[] whiteList = new[]
            {
                "Materal.Workflow"
            };
            return whiteList.Contains(name);
        }
    }
}
