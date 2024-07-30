using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace minimalAPIMongo.Domains
{
    public class Order
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        [BsonElement("products")]
        public List<string> ProductIds { get; set; } 

        [BsonElement("client_id"), BsonRepresentation(BsonType.ObjectId)]
        public string ClientId { get; set; }
        public object ProductId { get; internal set; }
        public object Client { get; internal set; }

        public Order()
        {
            ProductIds = new List<string>();
        }
    }
}
