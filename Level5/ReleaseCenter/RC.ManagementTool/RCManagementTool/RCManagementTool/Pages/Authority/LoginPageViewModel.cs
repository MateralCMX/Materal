using CommunityToolkit.Mvvm.Input;
using Materal.Utils.Http;
using RC.Authority.DataTransmitModel.User;
using RC.Authority.HttpClient;
using RC.Authority.PresentationModel.User;
using RC.Core.Common;
using RCManagementTool.Manager;
using RCManagementTool.Pages.Layer;
using System.ComponentModel.DataAnnotations;
using System.Net;

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
        /// 登录
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task LoginAsync()
        {
            try
            {
                if (!ValidateErrors()) return;
                UserHttpClient userHttpClient = App.ServiceProvider.GetRequiredService<UserHttpClient>();
                LoginResultDTO? loginResult = await userHttpClient.LoginAsync(new LoginRequestModel { Account = Account, Password = Password }) ?? throw new RCException("登录失败");
                AuthorityManager.Token = loginResult.Token;
                RCMessageManager.SendRootNavigationMessage<NavigationPage>();
            }
            catch (Exception ex)
            {
                RCMessageManager.SendExceptionMessage(ex);
            }
        }
    }
}
