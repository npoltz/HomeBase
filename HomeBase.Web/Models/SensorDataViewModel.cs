using HomeBase.Core.Models;
using HomeBase.Data.Models;
using HomeBase.Web.Services;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;

namespace HomeBase.Web.Models
{
    public class SensorDataViewModel
    {
        private readonly IDataLog _latestDataLog;

        public SensorDataViewModel(IDataLogServices services, string sensorId)
        {
            JsonDataLogs = GetSensorData(services, sensorId);

            if (JsonDataLogs != null)
            {
                DataLogs = JsonConvert.DeserializeObject<IEnumerable<DataLog>>(JsonDataLogs);
                _latestDataLog = DataLogs.FirstOrDefault(); // The datalogs are returned from the API in descending order.
            }
        }

        public string JsonDataLogs { get; }

        public IEnumerable<IDataLog> DataLogs { get; }

        public string LastUpdated
        {
            get
            {
                if (_latestDataLog == null)
                    return "Never";

                var timeDiff = DateTimeOffset.Now - _latestDataLog.Timestamp;

                var roundedSeconds = Math.Round(timeDiff.TotalSeconds);
                if (roundedSeconds < 60)
                    return $"{roundedSeconds} second{(roundedSeconds != 1 ? "s" : "")} ago";

                var roundedMinutes = Math.Round(timeDiff.TotalMinutes);
                if (roundedMinutes < 60)
                    return $"{roundedMinutes} minute{(roundedMinutes != 1 ? "s" : "")} ago";

                var roundedHours = Math.Round(timeDiff.TotalHours);
                if (timeDiff.TotalHours < 24)
                    return $"{roundedHours} hour{(roundedHours != 1 ? "s" : "")} ago";

                var roundedDays = Math.Round(timeDiff.TotalDays);
                if(roundedDays == 1)
                    return $"yesterday";
                
                return $"{roundedDays} days ago";
            }
        }

        public string LatestTemperature
        {
            get
            {
                if (_latestDataLog == null)
                    return "-";

                return $"{Math.Round(_latestDataLog.Temperature, 1)}°C";
            }
        }

        public string LatestRelativeHumidity
        {
            get
            {
                if (_latestDataLog == null)
                    return "-";

                return $"{Math.Round(_latestDataLog.RelativeHumidity, 1)}%";
            }
        }

        private string GetSensorData(IDataLogServices services, string sensorId)
        {
            try
            {
                return services.GetDataLogs(sensorId).Result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}