using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace MyBlog.Services.Models.Identity
{
    public class LogInRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
