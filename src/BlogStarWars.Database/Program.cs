using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using FluentMigrator;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgroLowCost.Database
{
    public class Program
    {
        static void Main()
        {
            var serviceProvider = CreateServices();

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        private static IServiceProvider CreateServices()
        {
            /*var builder = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath("../BlogStarWars.Api"))
                .AddJsonFile("appsettings.Development.json");*/

            //var configuration = builder.Build();

            var migrationAssemblies = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(e => e.IsClass && e.Namespace == "BlogStarWars.Database.Migrations" &&
                            e.BaseType == typeof(Migration))
                .Select(e => e.Assembly).ToArray();

            /*var connectionString = configuration
                .GetConnectionString(
                    RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                        ? "AgroLowCostMac"
                        : "AgroLowCostLinux");*/

            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    .AddSQLite()
                    // Set the connection string
                    .WithGlobalConnectionString("Data Source=BlogStarWars.db;")
                    // Define the assembly containing the migrations
                    .ScanIn(migrationAssemblies).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }

        /// <summary/>
        /// Update the database
        /// 
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }
    }
}