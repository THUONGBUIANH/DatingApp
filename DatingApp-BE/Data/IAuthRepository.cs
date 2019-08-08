using System.Threading.Tasks;
using PetAppAPIAngular.API.Models;

namespace DatingApp_BE.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}