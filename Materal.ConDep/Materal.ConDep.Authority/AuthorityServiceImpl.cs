using Materal.CacheHelper;
using Materal.ConDep.Common;
using Materal.StringHelper;
using System;

namespace Materal.ConDep.Authority
{
    public class AuthorityServiceImpl : IAuthorityService
    {
        private readonly ICacheManager _cacheManager;
        public AuthorityServiceImpl(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }
        public string Login(string password)
        {
            if (!password.Equals(ApplicationConfig.OperatingPassword)) throw new InvalidOperationException("密码错误");
            string token = StringManager.GetRandomStrByGuid();
            _cacheManager.SetByAbsolute(token, true, 3);
            return token;
        }
        public bool IsLogin(string token)
        {
            return _cacheManager.Get(token) != null;
        }
        public void Logout(string token)
        {
            _cacheManager.Remove(token);
        }
    }
}
