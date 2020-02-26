using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyBlog.DataAccessLayer.Models;

namespace MyBlog.DataAccessLayer
{
    public class EntryContext
    {
        public EntryContext(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            mongoDatabase = client?.GetDatabase(options.Value.Database);
            Collection = options.Value.EntriesCollection;
        }

        public IMongoDatabase mongoDatabase { get; }
        public string Collection { get; }
        public IMongoCollection<Entry> Entries { get => mongoDatabase.GetCollection<Entry>(Collection); }
    }
}
