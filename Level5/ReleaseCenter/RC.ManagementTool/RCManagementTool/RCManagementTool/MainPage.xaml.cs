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
        #region ˽�з���
        /// <summary>
        /// �򿪼�������
        /// </summary>
        /// <param name="message"></param>
        private void OpenLoadingMask(string message)
        {
            ViewModel.LoadingMaskMessage = message;
            ViewModel.LoadingMaskVisibility = Visibility.Visible;
        }
        /// <summary>
        /// �رռ�������
        /// </summary>
        private void CloseLoadingMask() => ViewModel.LoadingMaskVisibility = Visibility.Collapsed;
        /// <summary>
        /// ҳ�浼��
        /// </summary>
        /// <param name="type"></param>
        private void NavigationPage(Type type)
        {
            if (!type.IsAssignableTo<Page>()) return;
            contentFrame.Navigate(type);
        }
        /// <summary>
        /// �����쳣
        /// </summary>
        /// <param name="ex"></param>
        private void HandlerException(Exception ex)
        {
            if(ex is RCException)
            {
                ViewModel.InfoBarTitle = "��ʾ";
                ViewModel.InfoBarSeverity = InfoBarSeverity.Warning;
                ViewModel.InfoBarMessage = ex.Message;
            }
            else if (ex is MateralHttpException materalHttpException)
            {
                ViewModel.InfoBarTitle = "Http�������";
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
                ViewModel.InfoBarTitle = "Ӧ�ó������";
                ViewModel.InfoBarSeverity = InfoBarSeverity.Error;
                ViewModel.InfoBarMessage = ex.GetErrorMessage();
                Console.WriteLine(ViewModel.InfoBarMessage);
            }
            ViewModel.InfoBarIsOpen = true;
        }
        /// <summary>
        /// �ر���ʾ��
        /// </summary>
        /// <param name="state"></param>
        private void CloseInfoBar(object? state) => DispatcherQueue.TryEnqueue(() => ViewModel.InfoBarIsOpen = false);
        #endregion
    }
}