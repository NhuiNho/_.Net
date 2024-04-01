using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API_MongoDB.Models
{
    [BsonIgnoreExtraElements]
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        [BsonElement("name")]
        public string Name { get; set; } = String.Empty;

        [BsonElement("menu_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Menu_Id { get; set; } = String.Empty;

        [BsonElement("isShow")]
        public bool IsShow { get; set; }
    }
}
