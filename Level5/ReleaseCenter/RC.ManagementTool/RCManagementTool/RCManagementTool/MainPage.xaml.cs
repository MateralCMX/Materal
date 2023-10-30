using Materal.Utils.Http;
using RC.Core.Common;
using RCManagementTool.Manager;
using RCManagementTool.Pages.Authority;
using RCManagementTool.Pages.Layer;
using System.ComponentModel;
using System.Net;

namespace RCManagementTool
{
    public sealed partial class MainPage : Page
    {
        private readonly Timer _autoCloseInfoBarTimer;
        public MainPage()
        {
            _autoCloseInfoBarTimer = new(CloseInfoBar);
            RCMessageManager.RegisterExceptionMessage(this, (sender, message) => HandlerException(message.Value));
            RCMessageManager.RegisterRootNavigationMessage(this, (sender, message) => NavigationPage(message.Value));
            RCMessageManager.RegisterOpenLoadingMaskMessage(this, (sender, message) => OpenLoadingMask(message.Value));
            RCMessageManager.RegisterCloseLoadingMaskMessage(this, (sender, message) => CloseLoadingMask());
            InitializeComponent();
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }
        private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(ViewModel.InfoBarIsOpen) || !ViewModel.InfoBarIsOpen) return;
            _autoCloseInfoBarTimer.Change(TimeSpan.FromSeconds(3), Timeout.InfiniteTimeSpan);
        }
        private void Page_Unloaded(object sender, RoutedEventArgs e) => RCMessageManager.UnregisterAll(this);
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (AuthorityManager.Islogin)
            {
                NavigationPage(typeof(NavigationPage));
            }
            else
            {
                NavigationPage(typeof(LoginPage));
            }
        }
        #region 私有方法
        /// <summary>
        /// 打开加载遮罩
        /// </summary>
        /// <param name="message"></param>
        private void OpenLoadingMask(string message)
        {
            ViewModel.LoadingMaskMessage = message;
            ViewModel.LoadingMaskVisibility = Visibility.Visible;
        }
        /// <summary>
        /// 关闭加载遮罩
        /// </summary>
        private void CloseLoadingMask() => ViewModel.LoadingMaskVisibility = Visibility.Collapsed;
        /// <summary>
        /// 页面导航
        /// </summary>
        /// <param name="type"></param>
        private void NavigationPage(Type type)
        {
            if (!type.IsAssignableTo<Page>()) return;
            contentFrame.Navigate(type);
        }
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="ex"></param>
        private void HandlerException(Exception ex)
        {
            if(ex is RCException)
            {
                ViewModel.InfoBarTitle = "提示";
                ViewModel.InfoBarSeverity = InfoBarSeverity.Warning;
                ViewModel.InfoBarMessage = ex.Message;
            }
            else if (ex is MateralHttpException materalHttpException)
            {
                ViewModel.InfoBarTitle = "Http请求错误";
                ViewModel.InfoBarSeverity = InfoBarSeverity.Warning;
                if (materalHttpException.HttpResponseMessage is not null)
                {
                    if(materalHttpException.HttpResponseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        ViewModel.InfoBarMessage = ex.Message;
                    }
                    else if(materalHttpException.HttpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        contentFrame.Navigate(typeof(LoginPage));
                    }
                    else
                    {
                        ViewModel.InfoBarMessage = $"[{materalHttpException.HttpResponseMessage.StatusCode}]{ex.Message}";
                    }
                }
                else
                {
                    ViewModel.InfoBarMessage = ex.Message;
                }
            }
            else
            {
                ViewModel.InfoBarTitle = "应用程序错误";
                ViewModel.InfoBarSeverity = InfoBarSeverity.Error;
                ViewModel.InfoBarMessage = ex.GetErrorMessage();
                Console.WriteLine(ViewModel.InfoBarMessage);
            }
            ViewModel.InfoBarIsOpen = true;
        }
        /// <summary>
        /// 关闭提示框
        /// </summary>
        /// <param name="state"></param>
        private void CloseInfoBar(object? state) => DispatcherQueue.TryEnqueue(() => ViewModel.InfoBarIsOpen = false);
        #endregion
    }
}