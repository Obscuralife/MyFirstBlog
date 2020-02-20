using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyBlog.WebApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.DataAccessLayer
{
    public class EntryContext
    {
        private readonly IMongoDatabase mongoDatabase;
        private readonly string Collection;
        public EntryContext(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            mongoDatabase = client?.GetDatabase(options.Value.Database);
            Collection = options.Value.EntriesCollection;
        }

        public IMongoCollection<Entry> Entries { get => mongoDatabase.GetCollection<Entry>(Collection); }
    }
}
