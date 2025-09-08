using RoleBasedLoginApp.Models;
using System.Threading.Tasks;

namespace RoleBasedLoginApp.Services
{
    public interface IUserService
    {
        Task<UserModel?> ValidateUserAsync(string username, string password);
    }
}

