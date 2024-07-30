using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace minimalAPIMongo.ViewModels
{
    public class ClientViewModel
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("phone_number")]
        public string PhoneNumber { get; set; }

        // Supondo que um cliente possa ter vários endereços associados
        [BsonElement("addresses")]
        public List<AddressViewModel> Addresses { get; set; }

        [BsonIgnore]
        [JsonIgnore]
        public string SecretInfo { get; set; }
    }

    public class AddressViewModel
    {
        [BsonElement("street")]
        public string Street { get; set; }

        [BsonElement("city")]
        public string City { get; set; }

        [BsonElement("state")]
        public string State { get; set; }

    }
}
