using System.Collections.Generic;

namespace HomeBase.Core.Services
{
    public interface IDataLogRepository<IDataLog> : IRepository<IDataLog>
    {
        IList<IDataLog> GetDataLogsBySensorId(string sensorId, int? take);
    }
}
