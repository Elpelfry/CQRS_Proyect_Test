using API.Domain.Entities;
using MongoDB.Driver;

namespace API.Infratructure.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    public MongoDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<Prioridades> Priorities => _database.GetCollection<Prioridades>("Prioridades");
}
