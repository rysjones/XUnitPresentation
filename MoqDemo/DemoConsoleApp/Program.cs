using DemoConsoleApp.Helpers;
using DemoConsoleApp.Utilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SQLite;

namespace DemoConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables()
              .AddCommandLine(args)
              .Build();
            string connectionString = configuration["AppSettings:ConnectionString"];
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, connectionString);
#if DEBUG
            if (args.Length > 0)
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory.Replace(".UnitTests", string.Empty);
                dbPath = Path.GetFullPath(basePath + args[0]);
            }
#endif
            var sqlConn = new SQLiteConnection($"Data Source={dbPath};Version=3;");

            // Get Connection String from AKV
            IAKVConfiguration akvConfig = new AKVConfiguration();
            var akvManager = new KeyVaultManager(akvConfig);
            var akvConnString = akvManager.GetConnStringFromAKV();

            // Init Payload Data Demo
            PayloadDataHelper.InsertPayloadIfNotExists(sqlConn);
                       
            Console.WriteLine($"ConnectionString: {connectionString}\n");
            SQLiteDataAccess dataAccess = new SQLiteDataAccess(sqlConn);
            var payloads = dataAccess.GetAllPayloads();
            Console.WriteLine("Records Count: {0}\n", payloads.Count );
            foreach ( var payload in payloads ) { 
                Console.WriteLine("RequestId: {0}\n",payload.RequestId);
            }
        }
    }
}