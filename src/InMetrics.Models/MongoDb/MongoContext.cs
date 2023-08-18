using MongoDB.Driver;

namespace InMetrics.Models.MongoDb
{
    public class MongoContext<T>
    {
        private readonly IMongoDatabase _mongoDatabase;
        public MongoContext(ConfigMongoDb configMongoDb)
        {
            MongoClient mongoClient = new MongoClient(configMongoDb.MongoConnection);
            if (mongoClient is not null)
            {
                _mongoDatabase = mongoClient.GetDatabase(configMongoDb.MongoDatabase);
            }
        }
        public IMongoCollection<T> Do
        {
            get
            {
                return _mongoDatabase.GetCollection<T>(typeof(T).Name);
            }
        }
    }
}
