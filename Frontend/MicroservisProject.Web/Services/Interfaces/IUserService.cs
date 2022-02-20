using MicroservisProject.Web.Models;

namespace MicroservisProject.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}
