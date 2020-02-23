using MyBlog.DataAccessLayer.Models.Identity;
using MyBlog.Services.Models.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyBlog.Services.Abstract
{
    public interface IUserService
    {
        Task<DbUser> GetUserAsync(ClaimsPrincipal user);
        Task<IIdentityResponce> RegisterUserAsync(RegisterRequest request);
        Task<IIdentityResponce> LoginUserAsync(LogInRequest request);
    }
}
