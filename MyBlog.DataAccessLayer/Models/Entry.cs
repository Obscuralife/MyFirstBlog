using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.DataAccessLayer.Models
{
    public class Entry : Entity
    {
        [BsonRequired]
        public string Article { get; set; }

        [BsonRequired]
        public string Category { get; set; }

        [BsonIgnoreIfNull]
        public List<Comment> Comments { get; set; }

        [BsonIgnoreIfNull]
        public DateTime? UpdatedOn { get; set; }
    }
}
