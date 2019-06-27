using Materal.WPFCommon;
using System.Deployment.Application;
using System.Reflection;

namespace NCWM.UI
{
    public class MainWindowViewModel : NotifyPropertyChanged
    {
        private string _title = string.Empty;
        /// <summary>
        /// 运行状态
        /// </summary>
        public bool IsRun { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get => ApplicationConfig.TitleConfig.DisplayVersion ? $"{_title} {Version}" : _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version
        {
            get
            {
                string result = "版本号：";
                try
                {
                    result += ApplicationDeployment.CurrentDeployment.CurrentVersion;
                }
                catch
                {
                    result += Assembly.GetExecutingAssembly().GetName().Version.ToString();
                }
                return result;
            }
        }

        public MainWindowViewModel()
        {
            Title = ApplicationConfig.TitleConfig.Text;
        }
    }
}
