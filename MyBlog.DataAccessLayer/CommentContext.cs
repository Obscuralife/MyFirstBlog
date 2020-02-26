using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyBlog.DataAccessLayer.Models;

namespace MyBlog.DataAccessLayer
{
    public class CommentContext
    {
        public CommentContext(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            mongoDatabase = client?.GetDatabase(options.Value.Database);
            Collection = options.Value.CommentsCollection;
        }
        public string Collection { get; }
        public IMongoDatabase mongoDatabase { get; }
        public IMongoCollection<Comment> Comments { get => mongoDatabase.GetCollection<Comment>(Collection); }
    }
}
