using HomeBase.Core.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace HomeBase.Data.Models
{
    public class DataLog : IDataLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public decimal Temperature { get; set; }
        public decimal RelativeHumidity { get; set; }
    }
}