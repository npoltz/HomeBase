using HomeBase.Core.Configuration;
using HomeBase.Core.Services;
using HomeBase.Data.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace HomeBase.Data.Repositories
{
    public class DataLogRepository : IDataLogRepository<DataLog>
    {
        private readonly IMongoCollection<DataLog> _dataLogs;

        public DataLogRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _dataLogs = database.GetCollection<DataLog>("DataLogs");
        }

        public IList<DataLog> GetDataLogs(string sensorId, int? take) =>
            _dataLogs.Find(d => d.SensorId == sensorId).SortByDescending(d => d.Timestamp).Limit(take).ToList();

        public IList<DataLog> GetDataLogsSince(string sensorId, DateTimeOffset sinceDateTime) =>
            _dataLogs.Find(d => d.SensorId == sensorId && d.Timestamp >= sinceDateTime).ToList();

        public IList<DataLog> Get(int? take) =>
            _dataLogs.Find(d => true).SortByDescending(d => d.Timestamp).Limit(take).ToList();

        public DataLog Get(string id) => _dataLogs.Find(d => d.Id == id).FirstOrDefault();

        public DataLog Create(DataLog dataLog)
        {
            _dataLogs.InsertOne(dataLog);
            return dataLog;
        }

        public void Update(string id, DataLog dataLog) => _dataLogs.ReplaceOne(d => d.Id == id, dataLog);

        public void Remove(DataLog dataLog) => _dataLogs.DeleteOne(d => d.Id == dataLog.Id);

        public void Remove(string id) => _dataLogs.DeleteOne(d => d.Id == id);
    }
}