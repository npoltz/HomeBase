using System;
using System.Collections.Generic;

namespace HomeBase.Core.Services
{
    public interface IDataLogRepository<IDataLog> : IRepository<IDataLog>
    {
        IList<IDataLog> GetDataLogs(string sensorId, int? take);
        IList<IDataLog> GetDataLogsSince(string sensorId, DateTimeOffset sinceDateTime);
    }
}
