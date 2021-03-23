using HomeBase.Core.Services;
using HomeBase.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        /// Gets the most recent data logs for the given sensor ID.
        /// </summary>
        /// <param name="sensorId"></param>
        /// <param name="take"></param>
        /// <returns>An ordered list of data logs.</returns>
        [HttpGet]
        public IEnumerable<DataLog> Get(string sensorId, int take = 500)
        {
            return _repository.GetDataLogsBySensorId(sensorId, take);
        }

        /// <summary>
        /// Creates a datalog.
        /// </summary>
        /// <param name="dataLog"></param>
        [HttpPost]
        public void Post(DataLog dataLog)
        {
            _repository.Create(dataLog);
        }
    }
}
