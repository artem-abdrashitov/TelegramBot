using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using TelegramBot.Models.DataBase;

namespace TelegramBot.Data
{
    public class DataContext
    {
        private readonly IMongoDatabase _database;

        public DataContext(AppSettings settings)
        {
            
            var client = new MongoClient(settings.Db.ConnectionString);

            BsonClassMap.RegisterClassMap<User>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });

            _database = client.GetDatabase(settings.Db.DatabaseName);
         
        }

        public IMongoCollection<T> GetItems<T>() => _database.GetCollection<T>(typeof(T).Name);
    }
}