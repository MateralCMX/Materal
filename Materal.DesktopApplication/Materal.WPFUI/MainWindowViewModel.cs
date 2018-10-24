using System.Reflection;

namespace Materal.WPFUI
{
    public class MainWindowViewModel
    {
        private string _title = "Materal";
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get => _title + " 当前版本：" + Version;
            set => _title = value;
        }
        /// <summary>
        /// 版本号
        /// </summary>
        private string Version => Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }
}
