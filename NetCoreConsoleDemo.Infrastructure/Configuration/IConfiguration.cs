namespace NetCoreConsoleDemo.Infrastructure.Configuration
{
    public interface IConfiguration
    {
        string GetConfig(string setting);
    }
}