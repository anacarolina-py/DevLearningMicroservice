using Microsoft.Extensions.Options;
using Models.Models;
using MongoDB.Driver;

namespace DevLearningAuthorAPI.Data;

public class DbConnectionFactory
{
    public readonly IMongoCollection<Author> mongoCollection;

    public DbConnectionFactory(IOptions<MongoDbSettings> mongoDbSettings)
    {
        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        mongoCollection = database.GetCollection<Author>(mongoDbSettings.Value.CollectionName);
    }

    public IMongoCollection<Author> GetMongoCollection()
    {
        return mongoCollection;
    }
}
