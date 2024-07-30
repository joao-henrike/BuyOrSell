using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using minimalAPIMongo.Domains;

namespace minimalAPIMongo.ViewModels
{
    public class UserViewModel
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonIgnore]
        [JsonIgnore]
        public string Password { get; set; }

        [BsonIgnore]
        [JsonIgnore]
        public Dictionary<string, string> AdditionalAttributes { get; set; } = new Dictionary<string, string>();

        public UserViewModel()
        {
            AdditionalAttributes = new Dictionary<string, string>();
        }
    }
}
