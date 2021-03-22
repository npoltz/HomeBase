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

        public string GetSensorData()
        {
            try
            {
                var result = _services.GetDataLogs().Result;
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
