using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API_MongoDB.Models
{
    [BsonIgnoreExtraElements]
    public class Menu
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        [BsonElement("name")]
        public string Name { get; set; } = String.Empty;

        [BsonElement("isShow")]
        public bool IsShow { get; set; }
    }
}
