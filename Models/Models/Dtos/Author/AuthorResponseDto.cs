using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace Models.Models.Dtos.Author;

public class AuthorResponseDto
{
    
    public string Id { get; init; } 

    [BsonElement("name")]
    public string Name { get; init; } = string.Empty;

    [BsonElement("title")]
    public string Title { get; init; } = string.Empty;

    [BsonElement("image")]
    public string Image { get; init; } = string.Empty;

    [BsonElement("bio")]
    public string Bio { get; init; } = string.Empty;

    [BsonElement("url")]
    public string Url { get; init; } = string.Empty;

    [BsonElement("email")]
    public string Email { get; init; } = string.Empty;

    [BsonElement("type")]
    public byte Type { get; init; } = 1;
}
