using Microsoft.Extensions.Configuration;

namespace NetCoreConsoleDemo.Infrastructure.Configuration
{
    public interface IConfiguration
    {
        IConfigurationRoot AppSettings { get; }
    }
}