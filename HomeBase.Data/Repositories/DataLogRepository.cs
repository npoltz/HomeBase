using HomeBase.Core.Configuration;
using HomeBase.Core.Services;
using HomeBase.Data.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace HomeBase.Data.Repositories
{
    public class DataLogRepository : IRepository<DataLog>
    {
        private readonly IMongoCollection<DataLog> _dataLogs;

        public DataLogRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _dataLogs = database.GetCollection<DataLog>(settings.CollectionName);
        }

        public IList<DataLog> Get() => _dataLogs.Find(dataLog => true).ToList();

        public DataLog Get(string id) => _dataLogs.Find(book => book.Id == id).FirstOrDefault();

        public DataLog Create(DataLog dataLog)
        {
            _dataLogs.InsertOne(dataLog);
            return dataLog;
        }

        public void Update(string id, DataLog bookIn) => _dataLogs.ReplaceOne(dataLog => dataLog.Id == id, bookIn);

        public void Remove(DataLog bookIn) => _dataLogs.DeleteOne(dataLog => dataLog.Id == bookIn.Id);

        public void Remove(string id) => _dataLogs.DeleteOne(dataLog => dataLog.Id == id);
    }
}