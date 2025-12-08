using MongoDB.Bson.Serialization.Attributes;

namespace Models.Models.Dtos.Author;

public class UpdateAuthorDto
{
    [BsonElement("name")]
    public string? Name { get; init; } 

    [BsonElement("title")]
    public string? Title { get; init; } 

    [BsonElement("image")]
    public string? Image { get; init; } 

    [BsonElement("bio")]
    public string? Bio { get; init; } 

    [BsonElement("url")]
    public string? Url { get; init; } 
    [BsonElement("email")]
    public string? Email { get; init; } 

    [BsonElement("type")]
    public byte? Type { get; init; } = 1;
}
