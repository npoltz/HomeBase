using HomeBase.Web.Services;
using System;

namespace HomeBase.Web.Models
{
    public class SensorDataViewModel
    {
        private readonly IDataLogServices _services;

        public SensorDataViewModel(IDataLogServices services)
        {
            _services = services;
        }

        public string GetSensorData(string sensorId)
        {
            try
            {
                return _services.GetDataLogs(sensorId).Result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
