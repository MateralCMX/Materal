using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.JSInterop;

namespace BlazorWebAPP.Common
{
    public class AuthorityManage : IAuthorityManage
    {
        private const string storageName = "localStorage";
        //private const string storageName = "sessionStorage";
        public Metadata GrpcHeaders { get; private set; }
        private readonly IJSRuntime _jsRuntime;
        private string _token;
        public AuthorityManage(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }
        /// <summary>
        /// 是否登录
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsLoginAsync()
        {
            return !string.IsNullOrWhiteSpace(await GetTokenAsync());
        }

        public async Task SetTokenAsync(string token)
        {
            _token = token;
            GrpcHeaders = new Metadata
            {
                {"Authorization", $"Bearer {token}"}
            };
            await _jsRuntime.InvokeVoidAsync($"{storageName}.setItem", BlazorWebAPPConfig.TokenKey, _token);
        }

        public async Task RemoveTokenAsync()
        {
            _token = string.Empty;
            GrpcHeaders = null;
            await _jsRuntime.InvokeVoidAsync($"{storageName}.removeItem", BlazorWebAPPConfig.TokenKey);
        }

        public async Task<string> GetTokenAsync()
        {
            if (string.IsNullOrWhiteSpace(_token))
            {
                _token = await _jsRuntime.InvokeAsync<string>($"{storageName}.getItem", BlazorWebAPPConfig.TokenKey);
            }
            return _token;
        }
    }
}
