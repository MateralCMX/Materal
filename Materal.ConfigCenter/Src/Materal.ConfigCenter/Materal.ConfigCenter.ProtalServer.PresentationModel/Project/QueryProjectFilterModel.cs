using Materal.Model;

namespace Materal.ConfigCenter.ProtalServer.PresentationModel.Project
{
    public class QueryProjectFilterModel : PageRequestModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Contains]
        public string Name { get; set; }
    }
}
