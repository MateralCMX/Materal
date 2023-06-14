namespace Materal.Utils.Wechat.Model.OfficialAccount.Request
{
    /// <summary>
    /// 跳转小程序模型
    /// </summary>
    public class GoToMiniprogramModel
    {
        /// <summary>
        /// AppID
        /// </summary>
        public string AppID { get; set; } = string.Empty;
        /// <summary>
        /// 页面路径
        /// </summary>
        public string PagePath { get; set; } = string.Empty;
    }
}
