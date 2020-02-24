using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyBlog.DataAccessLayer.Models.Identity;
using MyBlog.Services.Abstract;
using MyBlog.Services.Models.Identity;
using MyBlog.Services.Models.Identity.Responce;
using MyBlog.WebApi.Filters;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MyBlog.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    [ApiController]
    [CustomExceptionFilter]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;

        public UserController(IUserService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("userData")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Returns a user data.", Type = typeof(DbUser))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<DbUser>> UserData()
        {
            var user = await service.GetUserAsync(User);
            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Returns a registration responce.", Type = typeof(SignUpResponce))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<SignUpResponce>> Register([FromBody] RegisterRequest request)
        {
            var responce = await service.RegisterUserAsync(request);
            return Ok(responce);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Returns a log in responce.", Type = typeof(LogInResponce))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<LogInResponce>> Login([FromBody] LogInRequest request)
        {
            var responce = await service.LoginUserAsync(request);
            return Ok(responce);
        }

    }
}
