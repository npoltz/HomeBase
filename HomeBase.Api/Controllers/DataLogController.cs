using HomeBase.Core.Models;
using HomeBase.Core.Services;
using HomeBase.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace HomeBase.Controllers
{
    [ApiController]
    [Route("/v1/datalog")]
    public class DataLogController : ControllerBase
    {
        private readonly IRepository<DataLog> _repository;
        private readonly ILogger<DataLogController> _logger;

        public DataLogController(
            IRepository<DataLog> repository,
            ILogger<DataLogController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<IDataLog> Get(int take = 500)
        {
            return _repository.Get().OrderBy(x => x.Timestamp).TakeLast(take);
        }

        [HttpPost]
        public void Post(DataLog dataLog)
        {
            _repository.Create(dataLog);
        }
    }
}
