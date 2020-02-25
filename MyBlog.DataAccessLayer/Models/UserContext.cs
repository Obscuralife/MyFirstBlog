using AspNetCore.Identity.Mongo;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyBlog.DataAccessLayer.Models.Identity;

namespace MyBlog.DataAccessLayer.Models
{
    public class UserContext
    {
        private readonly IMongoDatabase mongoDatabase;
        private readonly string usersCollection;
        private readonly string userRolesCollection;
        public UserContext(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            mongoDatabase = client?.GetDatabase(options.Value.Database);
            usersCollection = options.Value.UsersCollection;
            userRolesCollection = options.Value.UserRolesCollection;
        }

        public IMongoCollection<DbUser> Users { get => mongoDatabase.GetCollection<DbUser>(usersCollection); }
        public IMongoCollection<UserRole> UserRoles { get => mongoDatabase.GetCollection<UserRole>(userRolesCollection); }
    }
}
