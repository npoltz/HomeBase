using HomeBase.Core;
using HomeBase.Core.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HomeBase.Web.Services
{
    public class DataLogServices : IDataLogServices
    {
        private readonly IApiConfiguration _apiConfiguration;
        private readonly HttpClient _httpClient;

        public DataLogServices(IApiConfiguration apiConfiguration)
        {
            _apiConfiguration = apiConfiguration;
            _httpClient = new HttpClient();
        }

        public async Task<string> GetDataLogs(string sensorId, Periodicity periodicity)
        {
            UriBuilder builder = new UriBuilder(_apiConfiguration.BaseUri + "/datalog");
            builder.Query = $"sensorId={sensorId}&periodicity={periodicity}";

            var result = await _httpClient.GetAsync(builder.Uri);

            if (!result.IsSuccessStatusCode)
                throw new Exception($"Received unsuccessful HTTP response from API. HTTP Response Code: {result.StatusCode}");

            return await result.Content.ReadAsStringAsync();
        }
    }
}
