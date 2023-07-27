using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CustomerApi.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public DateOnly OrderDate { get; set; } = new DateOnly();

        public decimal Total { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerId { get; set; }

        [BsonIgnoreIfNull]
        public string? FirstName { get; set; }

        [BsonIgnoreIfNull]
        public string? LastName { get; set; }

        [BsonIgnoreIfNull]
        public string? Phone { get; set; }

        [BsonIgnoreIfNull]
        public string? Street { get; set; }

        [BsonIgnoreIfNull]
        public string? ZipCode { get; set; }
    }
}

