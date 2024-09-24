using Materal.MergeBlock.Authorization.Abstractions;
using Microsoft.Extensions.Options;

namespace Materal.MergeBlock.Authorization
{
    /// <summary>
    /// Token服务
    /// </summary>
    /// <param name="authorizationConfig"></param>
    public class TokenServiceImpl(IOptionsMonitor<AuthorizationOptions> authorizationConfig) : TokenServiceBase(authorizationConfig)
    {
    }
}
