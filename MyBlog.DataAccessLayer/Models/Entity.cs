using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MyBlog.DataAccessLayer.Models
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(Entry), typeof(Comment))]
    [BsonIgnoreExtraElements]
    public abstract class Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        public string UserId { get; set; }
        [BsonRequired]
        [BsonDateTimeOptions]
        public DateTime CreatedOn { get; set; }
        [BsonRequired]
        public string Body { get; set; }
    }
}
