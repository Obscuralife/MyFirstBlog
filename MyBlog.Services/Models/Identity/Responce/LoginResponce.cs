using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Services.Models.Identity
{
    public class LoginResponce : IIdentityResponce
    {
        public LoginResponce(string token, string name, string email)
        {
            this.Token = token;
            this.Name = name;
            this.Email = email;
        }

        public string Token { get; }

        public string Name { get; }

        public string Email { get; }
    }
}
