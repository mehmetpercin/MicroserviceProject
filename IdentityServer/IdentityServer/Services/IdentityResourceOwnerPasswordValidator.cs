using IdentityModel;
using IdentityServer.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var existsUser = await _userManager.FindByEmailAsync(context.UserName);
            if (existsUser == null)
            {
                context.Result.CustomResponse.Add("errors", new List<string> { "Email veya şifre yanlış" });
                return;
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(existsUser, context.Password);
            if (!passwordCheck)
            {
                context.Result.CustomResponse.Add("errors", new List<string> { "Email veya şifre yanlış" });
                return;
            }

            context.Result = new GrantValidationResult(existsUser.Id, OidcConstants.AuthenticationMethods.Password);
        }
    }
}
