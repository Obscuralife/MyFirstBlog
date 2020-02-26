using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyBlog.DataAccessLayer.Models.Identity;

namespace MyBlog.DataAccessLayer.Models
{
    public class UserContext
    {
        public UserContext(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            mongoDatabase = client?.GetDatabase(options.Value.Database);
            UsersCollection = options.Value.UsersCollection;
            UserRolesCollection = options.Value.UserRolesCollection;
        }

        public IMongoDatabase mongoDatabase { get; }
        public string UsersCollection { get; }
        public string UserRolesCollection { get; }
        public IMongoCollection<DbUser> Users { get => mongoDatabase.GetCollection<DbUser>(UsersCollection); }
        public IMongoCollection<UserRole> UserRoles { get => mongoDatabase.GetCollection<UserRole>(UserRolesCollection); }
    }
}
