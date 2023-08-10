using DemoConsoleApp.Data;
using DemoConsoleApp.Utilities;
using Microsoft.Extensions.Configuration;

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

            // Get Connection String from AKV
            var akvConnString = KeyVaultManager.GetConnStringFromAKV();

            // Init Payload Data Demo
            PaylodData.InsertPayloadIfNotExists(dbPath);
                       
            Console.WriteLine($"ConnectionString: {connectionString}\n");

            SQLiteDataAccess dataAccess = new SQLiteDataAccess(dbPath);
            var payloads = dataAccess.GetAllPayloads();
            Console.WriteLine("Records Count: {0}\n", payloads.Count );
            foreach ( var payload in payloads ) { 
                Console.WriteLine("RequestId: {0}\n",payload.RequestId);
            }
        }
    }
}