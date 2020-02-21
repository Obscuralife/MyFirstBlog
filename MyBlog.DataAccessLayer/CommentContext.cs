using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyBlog.DataAccessLayer.Models;

namespace MyBlog.DataAccessLayer
{
    public class CommentContext
    {
        private readonly IMongoDatabase mongoDatabase;
        private readonly string Collection;

        public CommentContext(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            mongoDatabase = client?.GetDatabase(options.Value.Database);
            Collection = options.Value.CommentsCollection;
        }

        public IMongoCollection<Comment> Comments { get => mongoDatabase.GetCollection<Comment>(Collection); }
    }
}
