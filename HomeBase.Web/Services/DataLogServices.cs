using HomeBase.Core;
using HomeBase.Core.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using static HomeBase.Web.Enums;

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

        public async Task<string> GetDataLogs(string sensorId, PeriodInterval periodInterval)
        {
            Periodicity periodicity;
            DateTimeOffset? sinceDateTime;
            switch (periodInterval)
            {
                case PeriodInterval.Day:
                    periodicity = Periodicity.Minute;
                    sinceDateTime = DateTimeOffset.Now.AddDays(-1);
                    break;
                case PeriodInterval.Week:
                    periodicity = Periodicity.Hour;
                    sinceDateTime = DateTimeOffset.Now.AddDays(-7);
                    break;
                case PeriodInterval.Month:
                    periodicity = Periodicity.Day;
                    sinceDateTime = DateTimeOffset.Now.AddMonths(-1);
                    break;
                case PeriodInterval.Year:
                    periodicity = Periodicity.Month;
                    sinceDateTime = DateTimeOffset.Now.AddYears(-1);
                    break;
                default:
                    periodicity = Periodicity.Unknown;
                    sinceDateTime = null; ;
                    break;
            }

            var builder = new UriBuilder(_apiConfiguration.BaseUri + "/datalog");
            if(sinceDateTime == null)
                builder.Query = $"sensorId={sensorId}&periodicity={periodicity}";
            else
                builder.Query = $"sensorId={sensorId}&periodicity={periodicity}&sinceDateTime={sinceDateTime.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")}";

            var result = await _httpClient.GetAsync(builder.Uri);

            if (!result.IsSuccessStatusCode)
                throw new Exception($"Received unsuccessful HTTP response from API. HTTP Response Code: {result.StatusCode}");

            return await result.Content.ReadAsStringAsync();
        }
    }
}
