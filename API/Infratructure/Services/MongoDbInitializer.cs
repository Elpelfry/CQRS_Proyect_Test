using API.Application.Dtos;
using API.Domain.Entities;
using MongoDB.Driver;

namespace API.Infratructure.Services;

public class MongoDbInitializer
{
    private readonly IMongoDatabase _database;

    public MongoDbInitializer(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("MongoConnection"));
        _database = client.GetDatabase(configuration["ConnectionStrings:MongoDatabaseName"]);
    }

    public async Task InitializeAsync()
    {
        // Crear colecciones y definir índices si es necesario
        var collectionNames = await _database.ListCollectionNames().ToListAsync();
        if (!collectionNames.Contains("Prioridades"))
        {
            await _database.CreateCollectionAsync("Prioridades");
            var collection = _database.GetCollection<Prioridades>("Prioridades");

            // Crear índices u otras configuraciones necesarias
            var indexKeysDefinition = Builders<Prioridades>.IndexKeys.Ascending(p => p.Id);
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexModel = new CreateIndexModel<Prioridades>(indexKeysDefinition, indexOptions);
            await collection.Indexes.CreateOneAsync(indexModel);
        }
    }
}
