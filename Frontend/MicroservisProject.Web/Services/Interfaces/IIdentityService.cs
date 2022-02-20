using IdentityModel.Client;
using MicroservisProject.Web.Models;
using SharedLibrary.Dtos;

namespace MicroservisProject.Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<object>> SignIn(SigninInput signinInput);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeRefrehToken();
    }
}
