using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.DataAccessLayer.Models
{
    public abstract class Entity
    {
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
