using Microsoft.Extensions.Configuration;

namespace Unidas.MS.Telemetria.Application.Services
{
    public static class Config
    {
        private static IConfiguration configuration;

        static Config()
        {
            var builder = new ConfigurationBuilder()
                //.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            configuration = builder.Build();
        }

        public static string Get(string name)
        {
            string appSettings = configuration[name];
            return appSettings;
        }

        public static string GetFromAppSettings(string name)
        {
            return Config.Get($"ApplicationSettings:{name}");
        }

        public static IConfigurationSection GetSection(string name)
        {
            return configuration.GetSection(name);
        }
    }
}
