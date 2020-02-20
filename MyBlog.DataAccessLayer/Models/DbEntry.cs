using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.DataAccessLayer.Models
{
    public abstract class DbEntry
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        [BsonDateTimeOptions]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        [BsonRequired]
        public string Body { get; set; }
    }
}
