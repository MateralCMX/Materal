using Materal.Utils.Model;

namespace Materal.Oscillator.Abstractions.Models.Work
{
    public class QueryWorkModel : PageRequestModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Contains]
        public string? Name { get; set; }
    }
}
