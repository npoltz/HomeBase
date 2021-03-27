using System;

namespace HomeBase.Api.Models
{
    public class DataLogDto
    {
        public DateTimeOffset? Timestamp { get; set; }
        public string SensorId { get; set; }
        public decimal Temperature { get; set; }
        public decimal RelativeHumidity { get; set; }
    }
}
