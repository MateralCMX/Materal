namespace Materal.WPFUI.CtrlTest
{
    public class WebBrowserTestCtrlViewModel
    {
        /// <summary>
        /// 当前地址
        /// </summary>
        public string NowAddress { get; set; } = "http://220.165.9.44:13466/map.html";
        /// <summary>
        /// 主页地址
        /// </summary>
        public string HomeAddress { get; set; } = "http://220.165.9.44:13466/map.html";
        /// <summary>
        /// 加载完毕标识
        /// </summary>
        public bool IsLoad { get; set; }
    }
}
