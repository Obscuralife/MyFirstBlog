using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MyBlog.Services.Abstract;
using MyBlog.DataAccessLayer.Models.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MyBlog.Services.Models.Identity;
using AutoMapper;
using MyBlog.Services.Models.Identity.Responce;
using MyBlog.Services.Models;

namespace MyBlog.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<DbUser> userManager;
        private readonly SignInManager<DbUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public UserService(UserManager<DbUser> userManager, SignInManager<DbUser> signInManager, IConfiguration configuration, IMapper mapper)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<DbUser> GetUserAsync(ClaimsPrincipal dbUser)
        {
            var user = await userManager.GetUserAsync(dbUser);
            return user;
        }

        public async Task<IIdentityResponce> LoginUserAsync(LogInRequest request)
        {
            var loginResult = await signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);
            if (!loginResult.Succeeded)
            {
                //throw new RequestedResourceHasBadRequest(nameof(request));
            }

            var user = userManager.Users.SingleOrDefault(i => string.Equals(i.Email, request.Email, StringComparison.InvariantCultureIgnoreCase));
            var token = AuthenticationHelper.GenerateJwtToken(request.Email, user, configuration);

            return new LogInResponce(token, user.UserName, user.Email);
        }

        public async Task<IIdentityResponce> RegisterUserAsync(RegisterRequest request)
        {
            var dbUser = mapper.Map<RegisterRequest, DbUser>(request);
            var createdResult = await userManager.CreateAsync(dbUser, request.Password);
            if (!createdResult.Succeeded)
            {
                var errors = ": ";
                Array.ForEach(createdResult.Errors.ToArray(), i => errors += $"{i.Code} - {i.Description}");
                throw new RequestedResourceHasBadRequest(nameof(request) + errors);
            }

            await signInManager.SignInAsync(dbUser, false);
            var token = AuthenticationHelper.GenerateJwtToken(request.Email, dbUser, configuration);

            return new SignUpResponce(token, dbUser.UserName, dbUser.Email);
        }
    }
}
