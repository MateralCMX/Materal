using Authority.DataTransmitModel.User;
using Authority.Service;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Threading.Tasks;

namespace Authority.IdentityServer
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserService _userService;
        private readonly IAPIAuthorityService _apiAuthorityService;

        public ResourceOwnerPasswordValidator(IUserService userService, IAPIAuthorityService apiAuthorityService)
        {
            _userService = userService;
            _apiAuthorityService = apiAuthorityService;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                UserListDTO userFromDb = await _userService.LoginAsync(context.UserName, context.Password);
                if (await _apiAuthorityService.HasLoginAuthorityAsync(userFromDb.ID))
                {
                    context.Result = new GrantValidationResult(userFromDb.ID.ToString(), "custom");
                }
                else
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.UnauthorizedClient, "权限不足");
                }
            }
            catch (InvalidOperationException ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, ex.Message);
            }
        }
    }
}
