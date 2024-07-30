using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using minimalAPIMongo.Domains;

namespace minimalAPIMongo.ViewModels
{
    public class OrderViewModel
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

        [BsonIgnore]
        [JsonIgnore]


        [BsonElement("client_id"), BsonRepresentation(BsonType.ObjectId)]
        public string ClientId { get; set; }
        public Client? Client { get; set; }
        public object ProductId { get; internal set; }
    }
}

