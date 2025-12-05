using MongoDB.Bson.Serialization.Attributes;

namespace Models.Models.Dtos.Author
{
    public class ContadorAuthorDto
    {
        [BsonElement("quantidade")]
        public int Quantidade { get; init; }
    }
}
