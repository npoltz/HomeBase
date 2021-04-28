using System.Threading.Tasks;
using static HomeBase.Web.Enums;

namespace HomeBase.Web.Services
{
    public interface IDataLogServices
    {
        Task<string> GetDataLogs(string sensorId, PeriodInterval periodInterval);
    }
}