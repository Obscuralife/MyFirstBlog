using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.WebApi
{
    public class Settings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string EntriesCollection { get; set; }
        public string CommentsCollection { get; set; }
    }
}
