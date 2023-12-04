using Materal.Gateway.Common;
using Materal.Gateway.Common.ConfigModel;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace Materal.Gateway
{
    /// <summary>
    /// 自定义认证状态提供者
    /// </summary>
    public class CustomAuthenticationStateProvider : RevalidatingServerAuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private const string _tokenKey = "Token";
        private bool _autoContractExtension = true;
        /// <summary>
        /// 构造方法
        /// </summary>
        public CustomAuthenticationStateProvider(ILoggerFactory loggerFactory, ProtectedSessionStorage sessionStorage) : base(loggerFactory)
        {
            _sessionStorage = sessionStorage;
        }
        /// <summary>
        /// 获取认证状态
        /// </summary>
        /// <returns></returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsPrincipal? claimsPrincipal = null;
            try
            {
                ProtectedBrowserStorageResult<UserInfo> storageValue = await _sessionStorage.GetAsync<UserInfo>(_tokenKey);
                if (storageValue.Success && storageValue.Value != null)
                {
                    Claim[] claims = new[] { new Claim("Account", storageValue.Value.Account) };
                    claimsPrincipal = new(new ClaimsIdentity(claims, "CustomAuthenticationState"));
                }
            }
            catch (InvalidOperationException)
            {
            }
            AuthenticationState result = new(claimsPrincipal ?? new());
            return result;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async void LoginAsyn(string account, string password)
        {
            UserConfigModel? user = ApplicationConfig.Users.FirstOrDefault(m => m.Account == account && m.Password == password);
            if (user is not null)
            {
                UserInfo userInfo = new(user.Account);
                await _sessionStorage.SetAsync(_tokenKey, userInfo);
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public async void LogoutAsync()
        {
            try
            {
                await _sessionStorage.DeleteAsync(_tokenKey);
            }
            finally
            {
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }
        }
        /// <summary>
        /// 设置自动续约
        /// </summary>
        /// <param name="enable"></param>
        public void ContractExtension(bool enable) => _autoContractExtension = enable;
        /// <summary>
        /// 续约间隔
        /// </summary>
        protected override TimeSpan RevalidationInterval => TimeSpan.FromSeconds(30);
        /// <summary>
        /// 验证认证状态
        /// </summary>
        /// <param name="authenticationState"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<bool> ValidateAuthenticationStateAsync(AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            bool result = false;
            try
            {
                ProtectedBrowserStorageResult<UserInfo> storageValue = await _sessionStorage.GetAsync<UserInfo>(_tokenKey);
                if (storageValue.Success && storageValue.Value != null)
                {
                    if (storageValue.Value.ExpirationTime > DateTime.Now)
                    {
                        result = true;
                    }
                    else if (_autoContractExtension)
                    {
                        storageValue.Value.ExpirationTime = DateTime.Now.AddHours(1);
                        await _sessionStorage.SetAsync(_tokenKey, storageValue.Value);
                        result = true;
                    }
                }
            }
            catch
            {
                result = false;
            }
            if (!result)
            {
                await _sessionStorage.DeleteAsync(_tokenKey);
            }
            return result;
        }
    }
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpirationTime { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="account"></param>
        public UserInfo(string account)
        {
            Account = account;
            ExpirationTime = DateTime.Now.AddHours(1);
        }
    }
}
