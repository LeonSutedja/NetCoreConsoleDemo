using System.IO;
using Microsoft.Extensions.Configuration;

namespace NetCoreConsoleDemo.Infrastructure.Configuration
{
    public class AppSettingsConfiguration : IConfiguration
    {
        public IConfigurationRoot AppSettings { get; }

        public AppSettingsConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            AppSettings = builder.Build();
        }
    }
}