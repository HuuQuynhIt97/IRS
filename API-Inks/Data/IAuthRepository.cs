using System.Threading.Tasks;
using INK_API.Models;

namespace INK_API.Data
{
    public interface IAuthRepository
    {
        Task<User> Login(string username, string password);
    }
}