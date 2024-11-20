using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ConsoleRpgEntities.Helpers
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot GetConfiguration(string basePath = null, string environmentName = null)
        {
            // Use the directory of the main project (ConsoleRpg) as the base path
            basePath ??= AppContext.BaseDirectory;

            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath) // Look for appsettings.json in ConsoleRpg/bin/Debug/net6.0
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // Optionally add environment-specific configuration
            if (!string.IsNullOrEmpty(environmentName))
            {
                builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
            }

            builder.AddEnvironmentVariables();

            return builder.Build();
        }

        public static void ConfigureDbContextOptions(DbContextOptionsBuilder optionsBuilder, string connectionString)
        {
            optionsBuilder.UseSqlServer(connectionString)
                .UseLazyLoadingProxies();
        }
    }

}
