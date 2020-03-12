using Materal.CacheHelper;
using Materal.ConDep.Common;
using Materal.ConDep.Services;
using Materal.StringHelper;
using System;

namespace Materal.ConDep.ServiceImpl
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
            _cacheManager.SetByAbsolute(token, true, 1);
            return token;
        }
        public bool IsLogin(string token)
        {
            if (string.IsNullOrEmpty(token)) return false;
            return _cacheManager.Get(token) != null;
        }
        public void Logout(string token)
        {
            if (string.IsNullOrEmpty(token)) return;
            _cacheManager.Remove(token);
        }
    }
}
