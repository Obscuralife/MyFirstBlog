using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyBlog.DataAccessLayer.Models.Identity;
using MyBlog.WebApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[action]")]
    [ApiController]
    [CustomExceptionFilter]
    public class UserController : ControllerBase
    {
    }
}
