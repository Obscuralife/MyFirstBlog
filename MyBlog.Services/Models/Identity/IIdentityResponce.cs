using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Services.Models.Identity
{
    public interface IIdentityResponce
    {
        public string Token { get; }

        public string Name { get; }

        public string Email { get; }
    }
}
