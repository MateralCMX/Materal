using System.Threading.Tasks;
using Materal.APP.HttpClient;
using Microsoft.JSInterop;
using WebAPP.Common;

namespace WebAPP
{
    public class JsAuthorityManageImpl : IAuthorityManage
    {
        private const string storageName = "localStorage";
        //private const string storageName = "sessionStorage";
        private readonly IJSRuntime _jsRuntime;
        private string _token;
        public JsAuthorityManageImpl(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<bool> IsLoginAsync()
        {
            return !string.IsNullOrWhiteSpace(await GetTokenAsync());
        }

        public async Task SetTokenAsync(string token)
        {
            _token = token;
            await _jsRuntime.InvokeVoidAsync($"{storageName}.setItem", WebAPPConfig.TokenKey, _token);
        }

        public async Task RemoveTokenAsync()
        {
            _token = string.Empty;
            await _jsRuntime.InvokeVoidAsync($"{storageName}.removeItem", WebAPPConfig.TokenKey);
        }

        public async Task<string> GetTokenAsync()
        {
            if (string.IsNullOrWhiteSpace(_token))
            {
                _token = await _jsRuntime.InvokeAsync<string>($"{storageName}.getItem", WebAPPConfig.TokenKey);
            }
            return _token;
        }
    }
}
