using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using minimalAPIMongo.Domains;

namespace minimalAPIMongo.ViewModels
{
    public class ProductViewModel
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonIgnore]
        [JsonIgnore]
        public Dictionary<string, string> AdditionalAttributes { get; set; } = new Dictionary<string, string>();

        public ProductViewModel()
        {
            AdditionalAttributes = new Dictionary<string, string>();
        }
    }
}
