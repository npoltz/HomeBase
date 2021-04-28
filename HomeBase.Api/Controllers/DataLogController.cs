using HomeBase.Api.Models;
using HomeBase.Core;
using HomeBase.Core.Services;
using HomeBase.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;

namespace HomeBase.Controllers
{
    [ApiController]
    [Route("/v1/datalog")]
    public class DataLogController : ControllerBase
    {
        private readonly IDataLogRepository<DataLog> _repository;
        private readonly ILogger<DataLogController> _logger;

        public DataLogController(
            IDataLogRepository<DataLog> repository,
            ILogger<DataLogController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Gets data logs for the given sensor ID since the given timestamp in the given periodicity.
        /// </summary>
        /// <param name="sensorId"></param>
        /// <param name="take"></param>
        /// <param name="sinceDateTime"></param>
        /// <param name="periodicity"></param>
        /// <returns>A list of data logs.</returns>
        [HttpGet]
        public IEnumerable<DataLog> GetDataLogsSince(string sensorId, Periodicity periodicity = Periodicity.Unknown, DateTimeOffset? sinceDateTime = null, int take = 500)
        {
            IList<DataLog> dataLogs;

            if (sinceDateTime == null)
            {
                dataLogs = _repository.GetDataLogs(sensorId, take);
            }
            else
            {
                dataLogs = _repository.GetDataLogsSince(sensorId, sinceDateTime.Value);
            }

            if (!dataLogs.Any())
                return null;

            switch (periodicity)
            {
                case Periodicity.Hour:
                case Periodicity.Day:
                case Periodicity.Month:
                    return GetDataLogsByPeriodicity(sensorId, periodicity, dataLogs);

                default:
                    return dataLogs;
            }
        }

        /// <summary>
        /// Creates a datalog.
        /// </summary>
        /// <param name="dataLog"></param>
        [HttpPost]
        public void Post(DataLogDto dto)
        {
            var dataLog = new DataLog
            {
                SensorId = dto.SensorId,
                Timestamp = dto.Timestamp ?? DateTimeOffset.Now,
                RelativeHumidity = dto.RelativeHumidity,
                Temperature = dto.Temperature
            };

            _repository.Create(dataLog);
        }

        private IEnumerable<DataLog> GetDataLogsByPeriodicity(string sensorId, Periodicity periodicity, IEnumerable<DataLog> dataLogs)
        {
            switch (periodicity)
            {
                case Periodicity.Hour:
                    return dataLogs.GroupBy(d => new { d.Timestamp.Year, d.Timestamp.Month, d.Timestamp.Day, d.Timestamp.Hour })
                        .Select(g => new
                        DataLog
                        {
                            Timestamp = new DateTimeOffset(g.Key.Year, g.Key.Month, g.Key.Day, g.Key.Hour, 0, 0, TimeSpan.Zero),
                            RelativeHumidity = g.Average(y => y.RelativeHumidity),
                            Temperature = g.Average(y => y.Temperature),
                            SensorId = sensorId
                        });
                case Periodicity.Day:
                    return dataLogs.GroupBy(d => new { d.Timestamp.Year, d.Timestamp.Month, d.Timestamp.Day })
                        .Select(g => new
                        DataLog
                        {
                            Timestamp = new DateTimeOffset(g.Key.Year, g.Key.Month, g.Key.Day, 0, 0, 0, TimeSpan.Zero),
                            RelativeHumidity = g.Average(y => y.RelativeHumidity),
                            Temperature = g.Average(y => y.Temperature),
                            SensorId = sensorId
                        });
                case Periodicity.Month:
                    return dataLogs.GroupBy(d => new { d.Timestamp.Year, d.Timestamp.Month })
                        .Select(g => new
                        DataLog
                        {
                            Timestamp = new DateTimeOffset(g.Key.Year, g.Key.Month, 1, 0, 0, 0, TimeSpan.Zero),
                            RelativeHumidity = g.Average(y => y.RelativeHumidity),
                            Temperature = g.Average(y => y.Temperature),
                            SensorId = sensorId
                        });
                default:
                    return dataLogs;
            }
        }
    }
}
