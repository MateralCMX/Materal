using Materal.Gateway.Common;
using Materal.Gateway.Common.ConfigModel;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace Materal.Gateway
{
    public class CustomAuthenticationStateProvider : RevalidatingServerAuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private const string _tokenKey = "Token";
        private bool _autoContractExtension = true;
        public CustomAuthenticationStateProvider(ILoggerFactory loggerFactory, ProtectedSessionStorage sessionStorage) : base(loggerFactory)
        {
            _sessionStorage = sessionStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsPrincipal? claimsPrincipal = null;
            try
            {
                ProtectedBrowserStorageResult<UserInfo> storageValue = await _sessionStorage.GetAsync<UserInfo>(_tokenKey);
                if (storageValue.Success && storageValue.Value != null)
                {
                    Claim[] claims = new[]
                    {
                        new Claim("Account",storageValue.Value.Account)
                    };
                    claimsPrincipal = new(new ClaimsIdentity(claims, "CustomAuthenticationState"));
                }
            }
            catch
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
        public void Login(string account, string password)
        {
            UserConfigModel? user = ApplicationConfig.Users.FirstOrDefault(m => m.Account == account && m.Password == password);
            if (user == null) throw new GatewayException("账号或者密码错误");
            UserInfo userInfo = new(user.Account);
            _sessionStorage.SetAsync(_tokenKey, userInfo);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public void Logout()
        {
            try
            {
                _sessionStorage.DeleteAsync(_tokenKey);
            }
            finally
            {
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }
        }
        public void ContractExtension(bool enable) => _autoContractExtension = enable;
        protected override TimeSpan RevalidationInterval => TimeSpan.FromSeconds(30);
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
    public class UserInfo
    {
        public string Account { get; set; }
        public DateTime ExpirationTime { get; set; }
        public UserInfo(string account)
        {
            Account = account;
            ExpirationTime = DateTime.Now.AddHours(1);
        }
    }
}
