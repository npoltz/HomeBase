using System.Threading.Tasks;

namespace HomeBase.Web.Services
{
    public interface IDataLogServices
    {
        Task<string> GetDataLogs(string sensorId);
    }
}