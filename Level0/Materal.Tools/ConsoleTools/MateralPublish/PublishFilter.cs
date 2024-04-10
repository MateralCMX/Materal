#if DEBUG
using MateralPublish.Models;

namespace MateralPublish
{
    public static class PublishFilter
    {
        /// <summary>
        /// 是否可以发布
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public static bool CanPublish(BaseProjectModel project)
        {
            if (false
                //|| project is Models.ProjectModels.Level0.MateralProjectModel
                //|| project is Models.ProjectModels.Level0.ToolsProjectModel
                //|| project is Models.ProjectModels.Level1.LoggerProjectModel
                //|| project is Models.ProjectModels.Level2.TFMSProjectModel
                //|| project is Models.ProjectModels.Level2.EventBusProjectModel
                //|| project is Models.ProjectModels.Level2.TTAProjectModel
                //|| project is Models.ProjectModels.Level3.OscillatorProjectModel
                //|| project is Models.ProjectModels.Level4.BaseCoreProjectModel
                || project is Models.ProjectModels.Level4.MergeBlockProjectModel
                //|| project is Models.ProjectModels.Level5.GatewayProjectModel
                //|| project is Models.ProjectModels.Level5.RCProjectModel
                )
            {
                return true;
            }
            return false;
        }
    }
}
#endif
