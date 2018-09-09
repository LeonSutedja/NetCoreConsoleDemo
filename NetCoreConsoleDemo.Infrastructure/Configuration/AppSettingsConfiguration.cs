using System.IO;
using Microsoft.Extensions.Configuration;

namespace NetCoreConsoleDemo.Infrastructure.Configuration
{
    public class AppSettingsConfiguration : IConfiguration
    {
        private IConfigurationRoot AppSettings { get; }

        public AppSettingsConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            AppSettings = builder.Build();
        }

        public string GetConfig(string setting) => AppSettings[setting];
    }
}