using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Services.Models
{
    public class CommentRequest
    {
        public string UserId { get; set; }
        public string Body { get; set; }
    }
}
