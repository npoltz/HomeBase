using HomeBase.Core.Configuration;
using HomeBase.Core.Models;
using HomeBase.Data.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HomeBase.Web.Services
{
    public class DataLogServices
    {
        private readonly IApiConfiguration _apiConfiguration;
        private readonly HttpClient _httpClient;

        public DataLogServices(IApiConfiguration apiConfiguration)
        {
            _apiConfiguration = apiConfiguration;
            _httpClient = new HttpClient();
        }

        public Task<IList<IDataLog>> GetDataLogs()
        {
            return Task.FromResult<IList<IDataLog>>(new List<IDataLog>
            {
                new DataLog{SensorId = "sensor01", Timestamp = DateTimeOffset.Now.AddMinutes(-1), RelativeHumidity = 50, Temperature = 21},
                new DataLog{SensorId = "sensor01", Timestamp = DateTimeOffset.Now.AddMinutes(-2), RelativeHumidity = 50, Temperature = 21},
                new DataLog{SensorId = "sensor01", Timestamp = DateTimeOffset.Now.AddMinutes(-3), RelativeHumidity = 50, Temperature = 21},
                new DataLog{SensorId = "sensor01", Timestamp = DateTimeOffset.Now.AddMinutes(-4), RelativeHumidity = 50, Temperature = 23},
                new DataLog{SensorId = "sensor01", Timestamp = DateTimeOffset.Now.AddMinutes(-5), RelativeHumidity = 50, Temperature = 23}
            });

            /*var result = await _httpClient.GetAsync(_apiConfiguration.BaseUri);

            if (!result.IsSuccessStatusCode)
                throw new Exception($"Received unsuccessful HTTP response from API. HTTP Response Code: {result.StatusCode}");

            return await result.Content.ReadAsStream();*/
        }
    }
}
