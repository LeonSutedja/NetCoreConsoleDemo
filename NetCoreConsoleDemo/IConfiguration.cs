using Microsoft.Extensions.Configuration;
using System.IO;

namespace NetCoreConsoleDemo
{
    public interface IConfiguration
    {
        IConfigurationRoot AppSettings { get; }
    }

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