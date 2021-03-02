using System;

namespace HomeBase.Core.Models
{
    public interface IDataLog
    {
        string Id { get; set; }
        DateTimeOffset Timestamp { get; set; }
        decimal Temperature { get; set; }
        decimal RelativeHumidity { get; set; }
    }
}