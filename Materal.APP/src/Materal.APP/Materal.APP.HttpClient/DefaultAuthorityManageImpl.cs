using System.Threading.Tasks;

namespace Materal.APP.HttpClient
{
    public class DefaultAuthorityManageImpl : IAuthorityManage
    {
        private string _token;

        public async Task<bool> IsLoginAsync()
        {
            return !string.IsNullOrWhiteSpace(await GetTokenAsync());
        }

        public Task SetTokenAsync(string token)
        {
            _token = token;
            return Task.CompletedTask;
        }

        public Task RemoveTokenAsync()
        {
            _token = string.Empty;
            return Task.CompletedTask;
        }

        public Task<string> GetTokenAsync()
        {
            return Task.FromResult(_token);
        }
    }
}
