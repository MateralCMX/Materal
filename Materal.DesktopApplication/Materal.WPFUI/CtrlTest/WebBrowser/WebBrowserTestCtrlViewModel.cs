namespace Materal.WPFUI.CtrlTest.WebBrowser
{
    public class WebBrowserTestCtrlViewModel
    {
        /// <summary>
        /// 当前地址
        /// </summary>
        public string NowAddress { get; set; } = "http://localhost:8965/";
        /// <summary>
        /// 主页地址
        /// </summary>
        public string HomeAddress { get; set; } = "http://localhost:8965/";
        /// <summary>
        /// 加载完毕标识
        /// </summary>
        public bool IsLoad { get; set; }
    }
}
