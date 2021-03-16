using HomeBase.Core.Configuration;

namespace HomeBase.Data.Configuration
{
    public class ApiConfiguration : IApiConfiguration
    {
        public string BaseUri { get; set; }
    }
}
