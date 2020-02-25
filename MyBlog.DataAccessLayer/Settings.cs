﻿namespace MyBlog.DataAccessLayer
{
    public class Settings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string EntriesCollection { get; set; }
        public string CommentsCollection { get; set; }
        public string UsersCollection { get; set; }
        public string UserRolesCollection { get; set; }
    }
}
