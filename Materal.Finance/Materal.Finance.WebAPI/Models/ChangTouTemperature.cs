namespace Materal.Finance.WebAPI.Models
{
    /// <summary>
    /// 长投温度
    /// </summary>
    public class ChangTouTemperature
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 场外基金
        /// </summary>
        public string OtcFund { get; set; }
        /// <summary>
        /// 场内基金
        /// </summary>
        public string BasedFund { get; set; }
        /// <summary>
        /// 温度
        /// </summary>
        public int Temperature { get; set; }
    }
}
