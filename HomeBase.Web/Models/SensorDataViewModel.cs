using HomeBase.Core.Models;
using HomeBase.Data.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using HomeBase.Core;

namespace HomeBase.Web.Models
{
    public class SensorDataViewModel
    {
        private string _jsonDataLogs;

        public string JsonDataLogs
        {
            get
            {
                return _jsonDataLogs;
            }
            set
            {
                _jsonDataLogs = value;

                if (value == null)
                {
                    DataLogs = null;
                    LatestDataLog = null;
                }
                else
                {
                    DataLogs = JsonConvert.DeserializeObject<IEnumerable<DataLog>>(JsonDataLogs);
                    LatestDataLog = DataLogs.Aggregate((i, j) => i.Timestamp > j.Timestamp ? i : j);
                }
            }
        }

        public IEnumerable<IDataLog> DataLogs { get; set; }

        public IDataLog LatestDataLog { get; set; }

        public Periodicity Periodicity { get; set; }

        public string LastUpdated
        {
            get
            {
                if (LatestDataLog == null)
                    return "Never";

                var timeDiff = DateTimeOffset.Now - LatestDataLog.Timestamp;

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
                if (roundedDays == 1)
                    return $"yesterday";

                return $"{roundedDays} days ago";
            }
        }

        public string LatestTemperature
        {
            get
            {
                if (LatestDataLog == null)
                    return "-";

                return $"{Math.Round(LatestDataLog.Temperature, 1)}°C";
            }
        }

        public string LatestRelativeHumidity
        {
            get
            {
                if (LatestDataLog == null)
                    return "-";

                return $"{Math.Round(LatestDataLog.RelativeHumidity, 1)}%";
            }
        }
    }
}