using DemoConsoleApp.Data;
using Newtonsoft.Json;

namespace DemoConsoleApp.Utilities
{
    public class AKVConfiguration
    {
        public static AKVConfig GetConfig()
        {
            var config = new AKVConfig();
            try
            {
                string jsonFilePath = @"C:\temp\akv\AKVConfig.json";
                string jsonContent = File.ReadAllText(jsonFilePath);
                config = JsonConvert.DeserializeObject<AKVConfig>(jsonContent);

                // Access the values
                Console.WriteLine($"TenantId: {config.TenantId}");
                Console.WriteLine($"ClientId: {config.ClientId}");
                Console.WriteLine($"SubjectName: {config.SubjectName}");
                Console.WriteLine($"KeyVaultName: {config.KeyVaultName}");
                Console.WriteLine($"KeyVaultSecretName: {config.KeyVaultSecretName}");
                return config;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return config;
        }
    }
}
