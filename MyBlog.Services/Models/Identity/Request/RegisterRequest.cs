using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace MyBlog.Services.Models.Identity
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
