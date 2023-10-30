using CommunityToolkit.Mvvm.Input;
using RC.Authority.DataTransmitModel.User;
using RC.Authority.HttpClient;
using RC.Authority.PresentationModel.User;
using RC.Core.Common;
using RCManagementTool.Controls;
using RCManagementTool.Manager;
using RCManagementTool.Pages.Layer;
using System.ComponentModel.DataAnnotations;

namespace RCManagementTool.Pages.Authority
{
    public partial class LoginPageViewModel : ObservableValidatorModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        [ObservableProperty, Required(ErrorMessage = "账号不能为空")]
        private string _account = string.Empty;
        /// <summary>
        /// 账户错误信息
        /// </summary>
        [ObservableProperty]
        private string _accountErrorMessage = string.Empty;
        /// <summary>
        /// 密码
        /// </summary>
        [ObservableProperty, Required(ErrorMessage = "账号不能为空")]
        private string _password = string.Empty;
        /// <summary>
        /// 密码错误信息
        /// </summary>
        [ObservableProperty]
        private string _passwordErrorMessage = string.Empty;
        /// <summary>
        /// 加载遮罩
        /// </summary>
        public LoadingMaskViewModel LoadingMask { get; } = new();
#if DEBUG
        public LoginPageViewModel()
        {
            Account = "cmx";
            Password = "123456";
        }
#endif
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        /// <exception cref="RCException"></exception>
        [RelayCommand]
        private async Task LoginAsync()
        {
            try
            {
                LoadingMask.IsShow = true;
                LoadingMask.Message = "正在登录...";
                UserHttpClient userHttpClient = App.ServiceProvider.GetRequiredService<UserHttpClient>();
                LoginRequestModel requestModel = new() { Account = Account, Password = Password };
                LoginResultDTO? loginResult = await userHttpClient.LoginAsync(requestModel) ?? throw new RCException("登录失败");
                AuthorityManager.Token = loginResult.Token;
                RCMessageManager.SendRootNavigationMessage<NavigationPage>();
            }
            catch (Exception ex)
            {
                RCMessageManager.SendExceptionMessage(ex);
                LoadingMask.IsShow = false;
            }
        }
    }
}
